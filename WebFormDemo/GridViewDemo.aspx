<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewDemo.aspx.cs" Inherits="WebFormDemo.GridViewDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <h2>gridview界面手动拖拽控件，完成编辑和删除操作</h2>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="book_id" DataSourceID="gvDataSource">
                <Columns>
                    <asp:BoundField DataField="book_id" HeaderText="编号" InsertVisible="False" ReadOnly="True" SortExpression="book_id" />
                    <asp:BoundField DataField="book_name" HeaderText="书名" SortExpression="book_name" />
                    <asp:BoundField DataField="book_price" HeaderText="价格" SortExpression="book_price" />
                    <asp:BoundField DataField="book_auth" HeaderText="作者" SortExpression="book_auth" />
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:CommandField />
                    <asp:TemplateField HeaderText="操作列">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete2" runat="server" Text="删除" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="gvDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DemoTestConnectionString %>" SelectCommand="SELECT * FROM [books]" DeleteCommand="DELETE FROM [books] WHERE [book_id] = @book_id" InsertCommand="INSERT INTO [books] ([book_name], [book_price], [book_auth]) VALUES (@book_name, @book_price, @book_auth)" UpdateCommand="UPDATE [books] SET [book_name] = @book_name, [book_price] = @book_price, [book_auth] = @book_auth WHERE [book_id] = @book_id">
                <DeleteParameters>
                    <asp:Parameter Name="book_id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="book_name" Type="String" />
                    <asp:Parameter Name="book_price" Type="Double" />
                    <asp:Parameter Name="book_auth" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="book_name" Type="String" />
                    <asp:Parameter Name="book_price" Type="Double" />
                    <asp:Parameter Name="book_auth" Type="String" />
                    <asp:Parameter Name="book_id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
