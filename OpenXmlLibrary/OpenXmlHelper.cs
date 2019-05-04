using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OpenXmlLibrary
{
    public class OpenXmlHelper
    {

        public static void CreateSpreadsheetWorkbook(string filepath)
        {
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "mySheet" };
            sheets.Append(sheet);

            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();
        }


        public static SpreadsheetDocument CreateWorkbook(string fileName)
        {
            SpreadsheetDocument spreadSheet = null;
            SharedStringTablePart sharedStringTablePart;
            WorkbookStylesPart workbookStylesPart;

            try
            {
                // Create the Excel workbook
                spreadSheet = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook, false);

                // Create the parts and the corresponding objects
                // Workbook
                spreadSheet.AddWorkbookPart();
                spreadSheet.WorkbookPart.Workbook = new Workbook();
                spreadSheet.WorkbookPart.Workbook.Save();

                // Shared string table
                sharedStringTablePart = spreadSheet.WorkbookPart.AddNewPart<SharedStringTablePart>();
                sharedStringTablePart.SharedStringTable = new SharedStringTable();
                sharedStringTablePart.SharedStringTable.Save();

                // Sheets collection
                spreadSheet.WorkbookPart.Workbook.Sheets = new Sheets();
                spreadSheet.WorkbookPart.Workbook.Save();

                // Stylesheet
                workbookStylesPart = spreadSheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                workbookStylesPart.Stylesheet = new Stylesheet();
                workbookStylesPart.Stylesheet.Save();
            }
            catch (System.Exception exception)
            {

            }

            return spreadSheet;
        }



        /// <summary>
        /// 读取Excel数据到DataSet中，默认读取所有Sheet中的数据
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetNames">Sheet名称列表，默认为null查询所有Sheet中的数据</param>
        /// <returns></returns>
        public static DataSet ReadExcel(string filePath)//, params string[] sheetNames
        {
            try
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, true))
                {
                    IEnumerable<Sheet> sheets = doc.WorkbookPart.Workbook.Descendants<Sheet>();
                    //if (sheetNames != null && sheetNames.Length > 0)
                    //{
                    //    sheets = sheets.Where(s => sheetNames.ToList().Contains(s.Name));
                    //}
                    DataSet ds = new DataSet();
                    SharedStringTable stringTable = doc.WorkbookPart.SharedStringTablePart.SharedStringTable;
                    foreach (Sheet sheet in sheets)
                    {
                        WorksheetPart sheetPart = (WorksheetPart)doc.WorkbookPart.GetPartById(sheet.Id);
                        IEnumerable<Row> rows = sheetPart.Worksheet.Descendants<Row>();
                        DataTable dt = new DataTable(sheet.Name);
                        foreach (Row row in rows)
                        {
                            if (row.RowIndex == 1)
                            {
                                GetDataColumn(row, stringTable, dt);
                            }
                            GetDataRow(row, stringTable, dt);
                        }
                        ds.Tables.Add(dt);
                    }
                    return ds;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }
        }


        /// <summary>
        /// 把Spreadsheet行中的数据导出到DataTable中。
        /// </summary>
        /// <param name="row">Spreadsheet行</param>
        /// <param name="stringTable">共享字符串表</param>
        /// <param name="dt">DataTable</param>
        private static void GetDataRow(Row row, SharedStringTable stringTable, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            string cellValue = string.Empty;
            int i = 0;
            int nullCellCount = i;
            foreach (Cell cell in row)
            {
                cellValue = GetCellValue(cell, stringTable);
                if (cellValue == string.Empty)
                    nullCellCount++;
                dr[i] = cellValue;
                i++;
            }
            //如果一整行数据都没有数据，则不添加此行到DataTable中
            if (nullCellCount != i)
                dt.Rows.Add(dr);
        }

        /// <summary>
        /// 从Spreadsheet行中读取表头信息
        /// </summary>
        /// <param name="row">Spreadsheet行</param>
        /// <param name="stringTable">共享字符串表</param>
        /// <param name="dt">DataTable</param>
        private static void GetDataColumn(Row row, SharedStringTable stringTable, DataTable dt)
        {
            DataColumn col = new DataColumn();
            Dictionary<string, int> columnCountDict = new Dictionary<string, int>();
            foreach (Cell cell in row)
            {
                string cellValue = GetCellValue(cell, stringTable);
                col = new DataColumn(cellValue);
                //由于Excel中的数据表列标题可以重复，而DataTable中不允许重复，因此在重复的列标题后追加递增数字
                if (dt != null && dt.Columns.Contains(cellValue))
                {
                    if (!columnCountDict.ContainsKey(cellValue))
                    {
                        columnCountDict.Add(cellValue, 0);
                    }
                    col.ColumnName = cellValue + (columnCountDict[cellValue]++);
                }
                dt.Columns.Add(col);
            }
        }

        /// <summary>
        /// 获取Spreadsheet单元格的值
        /// </summary>
        /// <param name="cell">Spreadsheet单元格</param>
        /// <param name="stringTable">共享字符串表</param>
        /// <returns>Spreadsheet单元格的值</returns>
        private static string GetCellValue(Cell cell, SharedStringTable stringTable)
        {
            string value = string.Empty;
            try
            {
                if (cell.ChildElements.Count == 0)
                {
                    return value;
                }
                value = cell.CellValue.InnerText;

                if(cell.DataType == null)
                {
                    return "";
                }
                switch (cell.DataType.Value)
                {
                    case CellValues.SharedString:
                        return stringTable.ChildElements[Int32.Parse(cell.CellValue.Text)].InnerText;
                    case CellValues.Boolean:
                        return cell.CellValue.Text == "1" ? "true" : "false";
                    case CellValues.Date:
                        {
                            DateTime ReleaseDate = new DateTime(1899, 12, 30);
                            DateTime answer = ReleaseDate.AddDays(Convert.ToDouble(cell.CellValue.Text));
                            return answer.ToShortDateString();
                        }
                    case CellValues.Number:
                        return Convert.ToDecimal(cell.CellValue.Text).ToString();
                    default:
                        if (cell.CellValue != null)
                            return cell.CellValue.Text;
                        return string.Empty;
                }
                //if (cell.DataType != null && cell.DataType == CellValues.SharedString)
                //// if (cell.DataType != null && (cell.DataType.Value == CellValues.SharedString || cell.DataType.Value == CellValues.String || cell.DataType.Value == CellValues.Number))

                //{
                //    value = stringTable.ChildElements[int.Parse(value)].InnerText;

                //}
                //else
                //{
                //    //如果当Cell 里面是 日期和浮点型的话，对应的Cell.DataType==Null，对应的时间会转换为一个浮点型，
                //    //对于这块可以通过DateTime.FromOADate(double d)转换为时间。 可是缺点的地方就是，如果Cell.DataType ==NULL,
                //    //根本无法确认这个数据到底是 浮点型还是[被转换为了日期的浮点数]。查阅了很多国外资料，的确国外博客有一部分都反映了
                //    //有关Openxml读取Excel时Cell.DataType==NULL的问题。本例子没考虑那个问题，现在还没解决。等后面查询到更详细的资料再解决。

                //    //  其次解决这个问题的方法只有，在数据处理的时候，数据分析我们是可以知道这一列的数据到底是什么类型，然后根据自己的需求，
                //    //自己对获取的数据做相应转换处理。不过如果使用OleDb的Select语句来读取Excel的时候，就不会出现这个问题，
                //    //读取到Datable时候是日期就不会转换为浮点型数据。而且对象的Datable对于的那个单元格数据还可以直接强制转换为DateTime。
                //    //不过用OleDB读取数据感觉上应该没有Openxml目前还没测试大数据，太晚了。该sleep了。如果有大神了解Openxml读取表格，
                //    // 请指点[需要解决问题是：EXCEL的表格中CELL 的 DateTime类型和浮点类型数据，在获取后如何区分。因为使用Openxml获取后日期会被自动转换为浮点型]
                //    // value = DateTime.FromOADate(double.Parse(value)).ToString("yyyy-MM-dd");
                //}
            }
            catch (Exception)
            {
                value = "N/A";
            }
            return value;
        }

      


        private static string GetCellValue(WorkbookPart wbPart, List<Cell> theCells, string cellColumnReference)
        {
            Cell theCell = null;
            string value = "";
            foreach (Cell cell in theCells)
            {
                if (cell.CellReference.Value.StartsWith(cellColumnReference))
                {
                    theCell = cell;
                    break;
                }
            }
            if (theCell != null)
            {
                value = theCell.InnerText;
                // If the cell represents an integer number, you are done. 
                // For dates, this code returns the serialized value that represents the date. The code handles strings and 
                // Booleans individually. For shared strings, the code looks up the corresponding value in the shared string table. For Booleans, the code converts the value into the words TRUE or FALSE.
                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            // For shared strings, look up the value in the shared strings table.
                            var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                            // If the shared string table is missing, something is wrong. Return the index that is in the cell. Otherwise, look up the correct text in the table.
                            if (stringTable != null)
                            {
                                value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                            }
                            break;
                        case CellValues.Boolean:
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }
                }
            }
            return value;
        }

    } 

}
