<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GissaDetHemligaTalet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/app.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <h1>Gissa det hemliga talet</h1>
            <p>
                <asp:Label ID="LabelNumber" runat="server" Text="Ange ett tal mellan 1 och 100: "></asp:Label>
                <asp:TextBox ID="TextBoxNumber" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RequiredNumberValidator"
                    ControlToValidate="TextBoxNumber"
                    runat="server"
                    ErrorMessage="Fyll i ett tal."
                    Text="*"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RangeValidator 
                    ID="RangeNumberValidator"
                    runat="server"
                    ControlToValidate="TextBoxNumber"
                    Type="Integer"
                    MinimumValue="1"
                    MaximumValue="100"
                    ErrorMessage="Ange ett tal mellan 1 och 100."
                    Text="*"
                    Display="Dynamic">
                </asp:RangeValidator>
            </p>

            <p>
                <asp:Button ID="ButtonCheckNumber" runat="server" Text="Skicka gissning" OnClick="ButtonCheckNumber_Click" />
            </p>

            <asp:ValidationSummary
                ID="errorList"
                DisplayMode="BulletList"
                EnableClientScript="true"
                HeaderText="Fel inträffade. Åtgärda felen och försök igen."
                runat="server" />

            <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false">
                <p>
                    <asp:Label ID="LabelPreviousNumbers" runat="server" Text=""></asp:Label>

                    <asp:Label ID="LabelResult" runat="server" Text=""></asp:Label>
                </p>
                
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible="false">
                <p>
                    <asp:Button ID="ButtonGenerateNewNumber" runat="server" Text="Slumpa nytt hemligt tal" OnClick="ButtonGenerateNewNumber_Click" />
                </p>
            </asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
