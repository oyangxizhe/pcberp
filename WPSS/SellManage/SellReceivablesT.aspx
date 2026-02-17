<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SellReceivablesT.aspx.cs" Inherits="WPSS.SellManage.SellReceivablesT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑销货收款单</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
<meta name ="Description" content ="进销存管理系统" />
<meta name ="keywords" content ="进销存管理系统,进销存管理软件,ERP,小微企业管理系统,希哲软件" />
       <link href ="../Css/SSBase.css"  type ="text/css" rel ="Stylesheet" />
       <link href ="../Css/S131017.css"  type ="text/css" rel ="Stylesheet" />
    </head>
<body >
   <form id="form1" runat="server">
    <input id="cuid" type="hidden"  runat="server" />
   <input id="hint" type="hidden"  runat="server" />
    <input id="emid" type="hidden"  runat="server" />
      <input id="x" type="hidden"  runat="server" />
       <input id="ControlFileDisplay" type="hidden"  runat="server" />
        <input id="x2" type="hidden"  runat="server" />
         <input id="CUKEY" type="hidden"  runat="server" />
                <div class ="c13101905">
      <div class="c13101906" id ="Div9">
          &gt;编辑销货收款单 </div>
     <div class="c13101907" id ="Div10">
 </div>
    </div>
<div class ="c13110501">
      <div class="c13110502" id ="Div17">
   <span class="c13110508" id ="Span1">
       <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Image/btnAdd.png"    onclick="btnAdd_Click"  />
          </span>
       </div>
              <div class="c13110510" id ="Div18">
   <span class="c13110511" id ="Span4">
                    <asp:Label ID="Label2" runat="server" Text="Label">(新增)</asp:Label>
          </span>
       </div>
             <div class="c13110502" id ="Div19">
   <span class="c13110508" id ="Span3">
       <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/btnSave.png" 
                     onclick="btnSave_Click"  />
          </span>
       </div>
              <div class="c13110510" id ="Div20">
   <span class="c13110511" id ="Span5">
                        <asp:Label ID="Label3" runat="server" Text="Label">(保存)</asp:Label>
          </span>
       </div>
          
         <div class="c13110507" id ="Div22">
                  <span class="c13110503" id ="Span2">
     <asp:ImageButton ID="btnExit" 
                 runat="server" ImageUrl="~/Image/btnExit.png" Width="60px" 
                      onclick="btnExit_Click" />
          </span>
   </div>
                 <div class="c13110510" id ="Div23">
   <span class="c13110511" id ="Span6">
                     (退出)
          </span>
       </div>
            <div class="c13110507" id ="Div1">
                  <span class="c13110503" id ="Span7">
     <asp:ImageButton ID="btnPrint" 
                 runat="server" ImageUrl="~/Image/btnPrint.png" Width="60px" 
                      onclick="btnPrint_Click" />
          </span>
   </div>
                 <div class="c13110510" id ="Div3">
   <span class="c13110511" id ="Span8">
                     (打印)
          </span>
       </div>
                 <div class="c13110507" id ="Div30" style="display :none;">
                  <span class="c13110503" id ="Span9">
     <asp:ImageButton ID="btnEXCEL_PRINT" 
                 runat="server" ImageUrl="~/Image/btnPrint.png" Width="60px" 
                      onclick="btnEXCEL_PRINT_Click" />
          </span>
   </div>
                       <div class="c13110507" id ="Div32" style ="display :block  ">
                <span class="c13110503" id ="Span10">
    <asp:LinkButton ID="btnReconcile" runat="server" onclick="btnReconcile_Click" CssClass ="" Visible ="false" >确认对帐</asp:LinkButton>
    </span> 
   </div>
          
                 <div class="c13110510" id ="Div33">
   <span class="c13110511" id ="Span11">
                 
          </span>
       </div>
                       <div class="c13110507" id ="Div34">
                <span class="c13110503" id ="Span12" style ="display :block  ">
    <asp:LinkButton ID="btnReductionReconcil" runat="server" onclick="btnReductionReconcile_Click" CssClass ="" Visible ="false" >对帐还原</asp:LinkButton>
    </span> 
   </div>
    </div>
<div  id="i13102301" class ="c13102101">
<span  class ="c13102102"><asp:Label ID="prompt" runat="server"  ForeColor="#f80707"></asp:Label></span>
</div> 
  <div class ="c13101902">
      <div class="c13101903" id ="Div24">
          收款单号</div>
     <div class="c13101904" id ="Div25">
<input id="Text1" type="text"  runat="server"   readonly ="readonly" class="c13102103"/> </div>
       <div class="c13101903" id ="Div5">
             客户名称</div>
     <div class="c13111503" id ="Div6">
   <input id="Text5" type="text"  runat ="server"  class ="c13102103"  />
     <span style =" margin-left :2px; margin-right :2px;"><a  href="javascript:f13100202('Text5','');">
         选择</a></span></div>
         <div class="c13101903" id ="Div26">
             订单号</div>
     <div class="c13110801"  id ="Div27">
   <input id="Text2" type="text"  runat ="server"  readonly ="readonly" class ="c14112013" />
   <span style =" margin-left :2px; margin-right :2px;"><a  href="javascript:f13100202('Text2','');">
         选择</a></span> 
   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="Text2" Text="必填的！" runat="server" />
       <asp:LinkButton 
             ID="btnSure" runat="server" onclick="btnSure_Click"  
            >确定</asp:LinkButton></div>
   
           </div>
  <div class ="c13101902">
                   <div class="c13101903" id ="Div28">
                        销货日期</div>
     <div class="c13101904" id ="Div29">
         <input id="Text3" type="text" runat="server" onclick ="f13100202('Text3')" readonly="readonly" class ="c13110901"/>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="Text3" Text="必填的！" runat="server" />
          </div>
                      <div class="c13101903" id ="Div2">
                          销货员工号</div>
                          
     <div class="c13111503" id ="Div4">
         <input id="Text4" type="text" runat="server"  class ="c14112009"/>
          <span style =" margin-left :2px; margin-right :2px;"><a  href="javascript:f13100202('Text4','');">
         选择</a></span>
                   <span  style =" margin-left :10px"><asp:Label ID="Label1" runat="server" Text="" ></asp:Label></span>
   </div>

  <div class="c13101903" id ="Div38">
                          联系人</div>
     <div class="c13101904" id ="Div39">
         <input id="Text6" type="text" runat="server"  class ="c13102103"/> 
         </div>
           </div>
              <div class ="c13101902">
    
       <div class="c13101903" id ="Div15">
                   送货地址</div>
     <div class="c13102604" id ="Div21">
           <input id="Text11" type="text"  runat="server"  readonly ="readonly" class="c13102103"/>
             <span style =" margin-left :2px; margin-right :2px;"><a  href="javascript:f13100202('Text11','');">
           选择</a></span>
           </div>
          <div class="c13101903" id ="Div40">
              联系电话</div>
     <div class="c13101904" id ="Div41">
   <input id="Text10" type="text"  runat ="server"  class ="c13102103"  /></div>
           </div>
       <div class ="c13101902">
                   <div class="c13101903" id ="Div31">
                        快递单号</div>
     <div class="c13101904" id ="Div35">
         <input id="Text12" type="text" runat="server"  class ="c13102103"/>
     <asp:LinkButton ID="LinkButton3" runat="server" onclick="btnCourier_Click"   CssClass =""  Visible="false"  >更新</asp:LinkButton>
          </div>
                      <div class="c13101903" id ="Div36">
                          </div>
                          
     <div class="c13111503" id ="Div37">
         &nbsp;<span style =" margin-left :2px; margin-right :2px;"></span>
   </div>

  <div class="c13101903" id ="Div42">
                          </div>
     <div class="c13101904" id ="Div43">
         &nbsp;</div>
           </div>
<div id="i13111001" class ="c13102201">
           <asp:GridView ID="GridView1" runat="server" 
                    AllowSorting="True"   
                    onrowdatabound="GridView1_RowDataBound" 
                        AutoGenerateColumns="False"  PageSize="15" 
                           CssClass ="c13122601" onrowdeleting="GridView1_RowDeleting"
               Width="1600px"
                   >
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns > 
                    <asp:TemplateField HeaderText="删除" >
                <ItemTemplate >
                    <asp:LinkButton ID="LinkButton2" runat="server" 
                        OnClientClick="return confirm('您确认删除该记录吗?');" Text="删除"  CommandName ="delete" ></asp:LinkButton>                     
                </ItemTemplate>
               <HeaderStyle Width="2%" />
                 <ItemStyle Width="2%"  />
            </asp:TemplateField>
                              <asp:TemplateField HeaderText="项次" >
                <ItemTemplate >
                  <asp:TextBox ID="TextBox1" runat="server"  Text='<%#Eval ("项次") %>'  CssClass="c14071612" ></asp:TextBox>   
                               
                </ItemTemplate>
                    <HeaderStyle Width="2%" />
                 <ItemStyle Width="2%"  ForeColor="#595d5a" />
            </asp:TemplateField> 
           <asp:TemplateField HeaderText="ID" >
                <ItemTemplate >
                                <asp:TextBox ID="TextBox2" runat="server"  Text='<%#Eval ("品号") %>' CssClass="c14071609" ></asp:TextBox>   
                               
                </ItemTemplate>
                  <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a" />
            </asp:TemplateField> 
                       <asp:TemplateField HeaderText="料号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox5" runat="server"  Text='<%#Eval ("料号") %>' CssClass="c14071609"></asp:TextBox>                     
                </ItemTemplate>
                <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a" />
            </asp:TemplateField> 
               <asp:TemplateField HeaderText="品名">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox3" runat="server" Text='<%#Eval ("品名") %>'  CssClass="c14071609"></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="5%" />
                 <ItemStyle Width="5%"  ForeColor="#595d5a" />
            </asp:TemplateField> 
                    
               <asp:TemplateField HeaderText="未销货数量">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox9" runat="server" Text='<%#Eval ("未销货数量") %>'   CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                      <asp:TemplateField HeaderText="累计销货数量"   >
                <ItemTemplate >
                 <asp:TextBox ID="TextBox10" runat="server" Text='<%#Eval ("累计销货数量") %>'  CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                    <HeaderStyle Width="5%" />
                 <ItemStyle Width="5%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                     <asp:TemplateField HeaderText="销货数量">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox11" runat="server" Text='<%#Eval ("销货数量") %>'  CssClass ="c13112302"></asp:TextBox>                     
                </ItemTemplate>
              <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                <asp:TemplateField HeaderText="仓库">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox12" runat="server" Text='<%#Eval ("仓库") %>'  CssClass="c14071617"></asp:TextBox>
                 <a  href="javascript:f13100202('TextBox10','<%#Eval ("项次") %>');">选择 </a>                     
                </ItemTemplate>
               <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                <asp:TemplateField HeaderText="批号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox13" runat="server" Text='<%#Eval ("批号") %>' CssClass ="c13112302"></asp:TextBox>                     
                </ItemTemplate>
               <HeaderStyle Width="5%" />
                 <ItemStyle Width="5%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                          <asp:TemplateField HeaderText="库存数量">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox14" runat="server" Text='<%#Eval ("库存数量") %>'     CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                        <asp:TemplateField HeaderText="本销货单累计销货数量">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox15" runat="server" Text='<%#Eval ("本销货单累计销货数量") %>'   CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="8%" />
                 <ItemStyle Width="8%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="备注">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox16" runat="server"     CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a"/>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Free数量">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox17" runat="server"   CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                  <HeaderStyle Width="3%" />
                 <ItemStyle Width="3%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="铜厚">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox4" runat="server" Text='<%#Eval ("规格") %>'  CssClass="c14071609"></asp:TextBox>                     
                </ItemTemplate>
             <HeaderStyle Width="5%" />
                 <ItemStyle Width="5%"  ForeColor="#595d5a" />
            </asp:TemplateField> 
         
        <asp:TemplateField HeaderText="客户料号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox100" runat="server" Text='<%#Eval ("客户料号") %>'   CssClass="c14071609"></asp:TextBox>                     
                </ItemTemplate>
                  <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a" />
            </asp:TemplateField> 
                    <asp:TemplateField HeaderText="订单数量">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox6" runat="server" Text='<%#Eval ("订单数量") %>'  CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                   <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a"/>
            </asp:TemplateField>
                             <asp:TemplateField HeaderText="销售单价">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox7" runat="server" Text='<%#Eval ("销售单价") %>'  CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                    <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a"/>
            </asp:TemplateField>
                             <asp:TemplateField HeaderText="税率">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox8" runat="server" Text='<%#Eval ("税率") %>' CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="3%" />
                 <ItemStyle Width="3%"  ForeColor="#595d5a"/>
            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工程费">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox18" runat="server" Text='<%#Eval ("工程费") %>' CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="3%" />
                 <ItemStyle Width="3%"  ForeColor="#595d5a"/>
            </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
                </div>
    <div id ="i13111501" class ="c13101902">
                                 <div class="c13102907" id ="Div16">
                                 </div>
      <div class="c14112014" id ="Div7">
          合计未税金额</div>
     <div class="c13101904" id ="Div8">
        <input id="Text7" type="text"  runat="server"    class="c13102908"/></div>
          <div class="c14112014" id ="Div12">
              合计税额</div>
     <div class="c13101904" id ="Div13">
   <input id="Text8" type="text"  runat ="server" class="c13102908" /> 
         </div>
                  <div class="c14112014" id ="Div11">
                      合计含税金额</div>
     <div class="c13101904" id ="Div14">
   <input id="Text9" type="text"  runat ="server" class ="c13102908"  /> 
         </div>
           </div>         
            <div id="i13103002" class ="c13102201">
        <asp:GridView ID="GridView3" runat="server" Width="65%" 
                    AllowSorting="True"   
                    onrowdatabound="GridView3_RowDataBound" 
                        onselectedindexchanged="GridView3_SelectedIndexChanged" 
                        AutoGenerateColumns="False" style="margin-left: 8px" PageSize="15" 
                        CssClass ="c13102001"
                   >
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns > 
              
                                     <asp:BoundField DataField="FLKEY" HeaderText="索引"  Visible ="false"  >
                              <ItemStyle Width="40px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                          </asp:BoundField>

          <asp:BoundField DataField="WAREID" HeaderText="ID" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
                <asp:TemplateField HeaderText="附件">
                <ItemTemplate >
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" 
                        Text='<%# Bind("OLDFILENAME") %>'></asp:LinkButton>                     
                </ItemTemplate>
                 <HeaderStyle Width="500px" />
                 <ItemStyle Width="500px"  ForeColor="#595d5a"/>
            </asp:TemplateField>  
                      
         
           
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
                   
                </div>
                <div id="i13103001" class ="c13102201">
          
                    <asp:GridView ID="GridView2" runat="server"  CssClass ="c13112502"   >
                          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    </asp:GridView>
                </div>
       <script type ="text/javascript" >
           window.onload = function onload1() {
               var Invocation = document.getElementById("hint").value;
               var Invocation1 = document.getElementById("x").value;
               var Invocation2 = document.getElementById("ControlFileDisplay").value;
               var Invocation3 = document.getElementById("x2").value;
               if (Invocation != "") {
                   document.getElementById("i13102301").style.display = "block";
                   document.all("prompt").innerText = Invocation;
               }
               else {
                   document.getElementById("i13102301").style.display = "none";
               }
               if (Invocation1 != "") {
                   document.getElementById("i13103001").style.display = "block";

               }
               else {
                   document.getElementById("i13103001").style.display = "none";

               }
               if (Invocation2 != "") {
                   document.getElementById("i13103002").style.display = "block";

               }
               else {
                   document.getElementById("i13103002").style.display = "none";
               }
               if (Invocation3 != "") {

                   document.getElementById("i13111001").style.display = "block";
                   document.getElementById("i13111501").style.display = "block";

               }
               else {

                   document.getElementById("i13111001").style.display = "none";
                   document.getElementById("i13111501").style.display = "none";

               }
           }
           function myShowModalDialog(url, args, width, height) {
               var tempReturnValue;
               if (navigator.userAgent.indexOf("Chrome") > 0) {
                   var paramsChrome = 'height=' + height + ', width=' + width + ', top=' + (((window.screen.height - height) / 2) - 50) +
                   ',left=' + ((window.screen.width - width) / 2) + ',toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no';
                   window.open(url, "newwindow", paramsChrome);
               }
               else {
                   var params = 'dialogWidth:' + width + 'px;dialogHeight:' + height + 'px;status:no;dialogLeft:'
                   + ((window.screen.width - width) / 2) + 'px;dialogTop:' + (((window.screen.height - height) / 2) - 50) + 'px;';
                   tempReturnValue = window.showModalDialog(url, args, params);
               }
               return tempReturnValue;
           }
           function f13100202(obj, obj1) {
               var dlgResult;
               if (obj == "Text2") {

                   var url = "../SellManage/order.aspx?cuid=" + document.getElementById("cuid").value + "&emid=" + document.getElementById("emid").value + "&come=se";
                   var dlgResult = myShowModalDialog(url, window, 900, 500);
                   if (navigator.userAgent.indexOf("Chrome") > 0) {

                   }
                   else {
                       document.getElementById("Text2").value = dlgResult[0];
                       document.getElementById("Text5").value = dlgResult[1];
                       document.getElementById("Text6").value = dlgResult[2];
                       document.getElementById("Text10").value = dlgResult[3];
                       document.getElementById("Text11").value = dlgResult[4];;
                   }
               }
               else if (obj == "TextBox10") {

                   var url = "../StockManage/StorageCase.aspx?obj1=" + obj1 + "&come=se";
                   var dlgResult = myShowModalDialog(url, window, 980,490);
                   if (navigator.userAgent.indexOf("Chrome") > 0) {

                   }
                   else {
                     
                       if (dlgResult != undefined) {
                       var table = document.getElementById('<%=GridView1.ClientID%>');
                       var tr = table.getElementsByTagName("tr");
                       for (i = 1; i < tr.length; i++) {
                           if (obj1 == i) {
                               var v5 = tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0];
                               var v6 = tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0];
                               var v7 = tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0];
                               v5.value = dlgResult[0];
                               v6.value = dlgResult[1];
                               v7.value = dlgResult[2];
                               break;
                           }
                       }
                   }
                      
                  }

                  }
                  else if (obj == "Text3") {
                      dlgResult = window.showModalDialog("../WDate.aspx", window, "dialogWidth:160px; dialogHeight:240px; status:0;");
                      if (dlgResult != undefined) {


                          document.getElementById("Text3").value = dlgResult;
                      }

                  }
                  else if (obj == "Text5") {
                      var url = "../SellManage/Customerinfo.aspx?come=se";
                      var dlgResult = myShowModalDialog(url, window, 900, 500);
                      if (navigator.userAgent.indexOf("Chrome") > 0) {

                      }
                      else {
                          document.getElementById("cuid").value = dlgResult[0];
                          document.getElementById("Text5").value = dlgResult[1];
                      }
                  }
                  else if (obj == "Text11") {
                      var url = "../BASEINFO/CompanyInfoPS.aspx";
                      var dlgResult = myShowModalDialog(url, window, 900, 500);
                      if (navigator.userAgent.indexOf("Chrome") > 0) {

                      }
                      else {
                          document.getElementById("CUKEY").value = dlgResult[0];
                          document.getElementById("Text6").value = dlgResult[1];
                          document.getElementById("Text10").value = dlgResult[2];
                          document.getElementById("Text11").value = dlgResult[3];
                      }
                  }
                  else {


                      var url = "../BaseInfo/EMPLOYEEINFO.aspx?come=se";
                      var dlgResult = myShowModalDialog(url, window, 900, 500);
                      if (navigator.userAgent.indexOf("Chrome") > 0) {

                      }
                      else {
                          if (dlgResult != undefined) {

                              document.getElementById("Text4").value = dlgResult[0];
                              document.all("Label1").innerText = dlgResult[1];
                          }
                      }
                  }
          }
          function enter2tab(e) {
              if (window.event.keyCode == 13) window.event.keyCode = 9
          }
          document.onkeydown = enter2tab;

        </script>
    </form>
</body>
</html>