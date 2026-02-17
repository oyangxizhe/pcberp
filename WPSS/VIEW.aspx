<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VIEW.aspx.cs" Inherits="WPSS.VIEW" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>视图</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta name="Description" content="进销存管理系统" />
    <meta name="keywords" content="进销存管理系统,进销存管理软件,ERP,小微企业管理系统,希哲软件" />
    <link href="Css/SSBase.css" type="text/css" rel="Stylesheet" />

    <link href="Css/view.css" type="text/css" rel="Stylesheet" />
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <input id="usid" type="hidden" runat="server" />
        <div>
            <div class="c15060601">

                <div class="c14103006" id="Div13">
                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="5" Height="100%">
                        <ItemTemplate>
                            <div id="<%#Eval ("NODEID") %>" class="c13050101 " onclick="f15060601('<%#Eval ("nodeid")%>')">
                                <img src="<%#Eval ("IMAGE_URL") %>" alt="">
                            </div>
                            <div class="c15060810"><%#Eval ("NODE_NAME") %></div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>

    </form>
    <script type="text/javascript">
        window.onload = function onload1() {

        }
        function f15060601(obj) {

            if (obj == 2) {
                location.replace("baseinfo/employeeinfo.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 3) {
                location.replace("baseinfo/wareinfo.aspx?usid=" + document.getElementById("usid").value + "");

            }
            else if (obj == 4) {
                location.replace("baseinfo/companyinfo.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 5) {
                location.replace("baseinfo/ReceivingAndDelivery.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 6) {
                location.replace("baseinfo/set_showname.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 8) {
                location.replace("baseinfo/depart.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 10) {
                location.replace("purchasemanage/supplierinfo.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 11) {
                location.replace("purchasemanage/purchaseunitprice.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 12) {
                location.replace("purchasemanage/purchase.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 13) {
                location.replace("purchasemanage/purchasegode.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 14) {
                location.replace("purchasemanage/return.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 15) {
                location.replace("purchasemanage/purchasesearch.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 18) {
                location.replace("purchasemanage/sureconcile.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 17) {
                location.replace("purchasemanage/pisi.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 19) {
                location.replace("sellmanage/customerinfo.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 20) {
                location.replace("sellmanage/sellunitprice.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 21) {
                location.replace("sellmanage/order.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 22) {
                location.replace("sellmanage/selltable.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 23) {
                location.replace("sellmanage/sellreturn.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 24) {
                location.replace("sellmanage/ordersearch.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 25) {
                location.replace("sellmanage/cureconcile.aspx?usid=" + document.getElementById("usid").value + "");



                // top.window.location.replace("sellmanage/cureconcile.aspx?usid=" + document.getElementById("usid").value + "");


            }
            else if (obj == 75) {
                location.replace("sellmanage/OFFERSEARCH.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 27) {
                location.replace("stockmanage/storageinfo.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 28) {
                location.replace("stockManage/transfers.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 29) {
                location.replace("stockmanage/storagecase.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 77) {
                location.replace("financial_manage/purchase_invoicet.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 78) {
                location.replace("financial_manage/advance_payment.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 79) {
                location.replace("financial_manage/request_money.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 80) {
                location.replace("financial_manage/payment_order.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 81) {
                location.replace("financial_manage/sale_invoicet.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 82) {
                location.replace("financial_manage/advance_receivables.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 83) {
                location.replace("financial_manage/receivables.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 84) {
                location.replace("financial_manage/receivables_order.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 42) {
                location.replace("usermanage/userinfo.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 43) {
                location.replace("usermanage/editpwd.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 44) {
                location.replace("usermanage/editright.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 45) {
                location.replace("usermanage/limite_search.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 74) {
                location.replace("quality_manage/quality_info.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 47) {
                location.replace("WareNature/spec.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 48) {
                location.replace("WareNature/PLANK_TYPE.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 49) {
                location.replace("WareNature/PLANK_THICKNESS.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 50) {
                location.replace("WareNature/PLANK_TOLERANCE.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 51) {
                location.replace("WareNature/PANEL.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 52) {
                location.replace("WareNature/Surface_Treatment.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 53) {
                location.replace("WareNature/Surface_Thickness.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 54) {
                location.replace("WareNature/Solder_Mask.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 55) {
                location.replace("WareNature/Character_Color.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 56) {
                location.replace("WareNature/Core_Copper.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 57) {
                location.replace("WareNature/Out_Copper.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 58) {
                location.replace("WareNature/Circuit_Spec.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 59) {
                location.replace("WareNature/COPPER_NEED.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 60) {
                location.replace("WareNature/MINIMUM_HOLE.aspx?usid=" + document.getElementById("usid").value + "");;
            }
            else if (obj == 61) {
                location.replace("WareNature/MOLDING_STYLE.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 62) {
                location.replace("WareNature/MOLDING_TOLERANCE.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 63) {
                location.replace("WareNature/VCUT_ANGLE.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 64) {
                location.replace("WareNature/VCUT_DISABLED.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 65) {
                location.replace("WareNature/HYPOTENUSE_ANGLE.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 66) {
                location.replace("WareNature/IMPEDANCE.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 67) {
                location.replace("WareNature/ASSIGN_STACKUP.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 68) {
                location.replace("WareNature/THICKNESS_COPPER.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 69) {
                location.replace("WareNature/BGA_DESIGN.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 70) {
                location.replace("WareNature/IF_HYPOTENUSE.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 71) {
                location.replace("WareNature/BIGANDSMALL_PANEL.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 72) {
                location.replace("WareNature/DEFECT.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 86) {
                location.replace("producttrace/parameter.aspx");
            }
            else if (obj == 87) {
                location.replace("producttrace/utable.aspx");
            }
            else if (obj == 88) {
                location.replace("producttrace/step.aspx");
            }
            else if (obj == 89) {
                location.replace("producttrace/flow.aspx");
            }
            else if (obj == 90) {
                location.replace("producttrace/trace.aspx");
            }
            else if (obj == 91) {
                location.replace("stockmanage/misc_godet.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 92) {
                location.replace("stockmanage/misc_pickingt.aspx?usid=" + document.getElementById("usid").value + "");
            }
            else if (obj == 93) {
                location.replace("stockmanage/pickingandgode.aspx?usid=" + document.getElementById("usid").value + "");
            }
        }
        function enter2tab(e) {
            if (window.event.keyCode == 13) window.event.keyCode = 9
        }
        document.onkeydown = enter2tab;

    </script>
</body>
</html>
