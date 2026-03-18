<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gridTeste.aspx.cs" Inherits="gridTeste" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server">
            <Columns>
                <asp:CheckBoxField Text="a" />
                <asp:CheckBoxField Text="b" />
                <asp:CheckBoxField Text="c" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
