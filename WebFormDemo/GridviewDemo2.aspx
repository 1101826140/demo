<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridviewDemo2.aspx.cs" Inherits="WebFormDemo.GridviewDemo2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>使用aspx页面+ashx接口+easyui  实现数据绑定功能</h2>

            <div id="dvList"></div>
        </div>
    </form>

    <link rel="stylesheet" type="text/css" href="Scripts/jquery-easyui-1.7.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="Scripts/jquery-easyui-1.7.0/themes/icon.css" />
    <script type="text/javascript" src="Scripts/jquery-easyui-1.7.0/jquery.min.js"></script>

    <script type="text/javascript" src="Scripts/jquery-easyui-1.7.0/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(function () {

            //加载数据
            <%-- $.ajax({
                type: 'post',
                url: "<%= Page.ResolveUrl("~/services/BookApi.ashx")%>?method=GetList",
            data: [],
            async: false,
            success: function (data) {
                console.log(JSON.stringify(data));
            },
            error: function () {

            }
            });--%>


            var datagrid = {
                bind: function (filter) {
                    $('#dvList').datagrid({
                        title: '测试datagrid数据绑定',
                        url: '../services/BookApi.ashx?method=GetList',
                        //method: "POST",
                        height: 500,
                        nowrap: false,
                        autoRowHeight: true,
                        fitColumns: true,
                        striped: true,  //隔行变色
                        border: false,
                        singleSelect: true,
                        pagination: true,
                        columns: [[
                            {
                                field: 'book_id', title: '编号',sortable:true,
                                formatter: function (value, row, index) {
                                    return '<span style="padding-left:3px">' + value + '</span>';
                                }
                            },
                            { field: 'book_name', title: '书名', width: 50 },
                            { field: 'book_price', title: '价格',  },
                            {
                                field: 'book_auth', title: '作者', formatter: function (value, row, index) {
                                    return value;
                                }
                            }, {
                                field: "book_id", title: "操作", formatter: function (value, row, index) {

                                    return '';
                                }
                            }

                        ]],
                        onLoadSuccess: function (data) {//数据加载成功绑定

                        },


                    });
                }
            }
            datagrid.bind("");
        })

    </script>
</body>
</html>
