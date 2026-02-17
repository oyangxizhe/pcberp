<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseT.aspx.cs" Inherits="WPSS.PurchaseManage.PurchaseT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑采购单</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
<meta name ="Description" content ="进销存管理系统" />
<meta name ="keywords" content ="进销存管理系统,进销存管理软件,ERP,小微企业管理系统,希哲软件" />
       <link href ="../Css/SSBase.css"  type ="text/css" rel ="Stylesheet" />
       <link href ="../Css/S131017.css"  type ="text/css" rel ="Stylesheet" />

    </head>
<body >
   <form id="form1" runat="server">
   <input id="hint" type="hidden"  runat="server" />
      <input id="x" type="hidden"  runat="server" />
       <input id="x1" type="hidden"  runat="server" />
       <input id="pur" type="hidden"  runat="server" />
         <input id="RDID" type="hidden"  runat="server" />
            <input id="COKEY" type="hidden"  runat="server" />
                <div class ="c13101905">
      <div class="c13101906" id ="Div9">
          编辑采购单 </div>
     <div class="c13101907" id ="Div10">
 </div>
    </div>
<div class ="c13110501">
      <div class="c13110502" id ="Div17">
<input id="Submit1" type="submit" value="添加采购单"  runat ="server"  onmouseover="over(this,'Submit2s')"
               onmouseout="out(this,'Submit2s')" class ="c19012703"  
              onserverclick="submit1_Click" />
       </div>
              <div class="c13110510" id ="Div18">
       </div>
             <div class="c13110502" id ="Div19">

      <input id="Submit2" type="submit" value="保存"  runat ="server"  onmouseover="over(this,'Submit2s')"
               onmouseout="out(this,'Submit2s')" class ="c19012703"  
              onserverclick="submit1_Click" />
      
       </div>
              <div class="c13110510" id ="Div20">
       </div>
          
         <div class="c13110507" id ="Div22">
    <input id="Submit3" type="submit" value="退出"  runat ="server"  onmouseover="over(this,'Submit2s')"
               onmouseout="out(this,'Submit2s')" class ="c19012703"  
              onserverclick="submit1_Click" />
   </div>
                 <div class="c13110510" id ="Div23">

       </div>
                <div class="c13110507" id ="Div30" style ="display :none ">
                <span class="c13110503" id ="Span7">
    <asp:LinkButton ID="btnReconcile" runat="server" onclick="btnReconcile_Click" CssClass ="">确认对帐</asp:LinkButton>
    </span> 
   </div>
          
                       <div class="c13110507" id ="Div32" style ="display :none ">
                <span class="c13110503" id ="Span9">
    <asp:LinkButton ID="btnReductionReconcil" runat="server" onclick="btnReductionReconcile_Click" CssClass ="">对帐还原</asp:LinkButton>
    </span> 
   </div>
          
                      <div class="c13110507" id ="Div438">
  <input id="Submit4" type="submit" value="打印"  runat ="server"  onmouseover="over(this,'Submit2s')"
               onmouseout="out(this,'Submit2s')" class ="c19012703"  
              onserverclick="submit1_Click" />
   </div>
                 <div class="c13110510" id ="Div4338">
       </div>


    </div>
<div  id="i13102301" class ="c13102101">
<span  class ="c13102102"><asp:Label ID="prompt" runat="server"  ForeColor="#f80707"></asp:Label></span>
</div> 
  <div class ="c13101902">
      <div class="c13101903" id ="Div24">
          采购单号</div>
     <div class="c13111503" id ="Div25">
<input id="Text1" type="text"  runat="server"   readonly ="readonly" class="c14112011"/> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Text1" Text="必填" runat="server" /></div>
         <div class="c13101903" id ="Div26">
             供应商代码</div>
     <div class="c13102401" id ="Div27">
   <input id="Text2" type="text"  runat ="server"  class ="c14112009"  />
    <span style =" margin-left :5px; margin-right :2px;"><a  href="javascript:f13100202('Text2','');"> 
         选择</a></span>
       </div>
                    <div class="c13101903" id ="Div28">
                        采购日期</div>
     <div class="c14112005" id ="Div29">
         <input id="Text3" type="text" runat="server" onclick ="f13100202('Text3')" readonly="readonly" class ="c13110901"/>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="Text3" Text="必填" runat="server" />
         </div>
           </div>
           
  <div class ="c13101902">
                      <div class="c13101903" id ="Div2">
                          采购员工号</div>
     <div class="c13111503" id ="Div4">
         <input id="Text4" type="text" runat="server"  class ="c14112009" /> 
         <span style =" margin-left :5px;"><a  href="javascript:f13100202('Text4','');"> 
         选择</a></span>
               <asp:Label ID="Label1" runat="server" Text="" ></asp:Label>
         </div>
         <div class="c13101903" id ="Div5">
             供应商名称</div>
     <div class="c13102401" id ="Div6">
   <input id="Text5" type="text"  runat ="server"  class ="c14111909"  /></div>
                        <div class="c13101903" id ="Div38">
                            联系人</div>
     <div class="c14112005" id ="Div39">
         <input id="Text10" type="text" runat="server"  class ="c14112007" /> 
         </div>
           </div>
       <div class ="c13101902">
      <div class="c13101903" id ="Div1">
                   地址</div>
     <div class="c14032101" id ="Div3">
           <input id="Text6" type="text"  runat="server"  readonly ="readonly" class="c14112004"/></div>
                  <div class="c13101903" id ="Div40">
                      联系电话</div>
     <div class="c14112005" id ="Div41">
   <input id="Text11" type="text"  runat ="server"  class ="c14112007"  /></div>
           </div>
           
           <div class ="c13101902">
                      <div class="c13101903" id ="Div15">
                          收货人</div>
     <div class="c13111503" id ="Div21">
         <input id="Text12" type="text" runat="server"  readonly ="readonly" class ="c14111912"/> 
              <span style =" margin-left :5px;"><a  href="javascript:f13100202('Text12','');"> 
         选择</a></span>
         </div>
         <div class="c13101903" id ="Div34">
             收货地址</div>
     <div class="c13102402" id ="Div37">
   <input id="Text13" type="text"  runat ="server"  class ="c14112008"  /></div>
  
           </div>
           <div class ="c13101902">
                      <div class="c13101903" id ="Div42">
                          公司名称</div>
     <div class="c13111503" id ="Div43">
         <input id="Text14" type="text" runat="server" class ="c14032102" /> 
         </div>
         <div class="c13101903" id ="Div44">
             联系人</div>
     <div class="c14112006" id ="Div45">
   <input id="Text15" type="text"  runat ="server"  readonly ="readonly" class ="c14032103"  />
           <span style =" margin-left :5px;"><a  href="javascript:f13100202('Text15','');"> 
         选择</a></span>
   </div>
                        <div class="c13101903" id ="Div46">
                            电话</div>
     <div class="c14112005" id ="Div47">
         <input id="Text16" type="text" runat="server"  class ="c14112007"/> 
         </div>
           </div>
           <div class ="c13111601">
       <div class="c13101903" id ="Div35">
           添加品号信息</div>
 
         
           </div>
<div class ="c13111602">
             
          
            <asp:GridView ID="GridView1" runat="server" 
                    AllowSorting="True"   
                    onrowdatabound="GridView1_RowDataBound" 
                        AutoGenerateColumns="False"  PageSize="15" 
                             CssClass ="c13122602"
                   >
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns > 
           <asp:TemplateField HeaderText="ID" >
                <ItemTemplate >
                                <asp:TextBox ID="TextBox1" runat="server"  Text='<%#Eval ("品号") %>' CssClass ="c14071603"     ></asp:TextBox>   
                                 <a  href="javascript:f13100202('TextBox1','<%#Eval ("项次") %>');"> 选择</a> 
                </ItemTemplate>
                 <HeaderStyle Width="11%" />
                 <ItemStyle Width="11%"  ForeColor="#595d5a" />
            </asp:TemplateField> 
           <asp:TemplateField HeaderText="料号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox2" runat="server"  Text='<%#Eval ("料号") %>'  CssClass ="c13120501"  ></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="8%" />
                 <ItemStyle Width="8%"  ForeColor="#595d5a"/>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="品名">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox3" runat="server"  Text='<%#Eval ("品名") %>'  CssClass ="c13120501" ></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="9%" />
                 <ItemStyle Width="9%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
         
                          <asp:TemplateField HeaderText="客户料号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox4" runat="server"  Text='<%#Eval ("客户料号") %>'  CssClass ="c13120501"></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="7%" />
                 <ItemStyle Width="7%"  ForeColor="#595d5a"/>
            </asp:TemplateField>
  
      
                    <asp:TemplateField HeaderText="采购数量">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox5" runat="server" Text='<%#Eval ("采购数量") %>' CssClass ="c13112302"  ></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"  ForeColor="#595d5a"/>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="采购单价">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox6" runat="server" CssClass ="c13112302"  ></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                     <asp:TemplateField HeaderText="税率%">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox7" runat="server"  Text='<%#Eval ("税率") %>' CssClass ="c13112302" ></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                     <asp:TemplateField HeaderText="需求日期">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox8" runat="server"  Text='<%#Eval ("需求日期") %>' CssClass ="c14071603"></asp:TextBox>      
                    <a  href="javascript:f13100202('TextBox8','<%#Eval ("项次") %>');"> 选择</a>                
                </ItemTemplate>
                 <HeaderStyle Width="11%" />
                 <ItemStyle Width="11%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                   <asp:TemplateField HeaderText="工程费">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox9" runat="server"  CssClass ="c13120501" ></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                    <asp:TemplateField HeaderText="备注">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox10" runat="server"  Text='<%#Eval ("备注") %>'   CssClass ="c13120501"></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="15%" />
                 <ItemStyle Width="15%"  ForeColor="#595d5a"/>
            </asp:TemplateField> 
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
                </div>


 <div id="i13103001" class ="c13102201">
        <asp:GridView ID="GridView2" runat="server" 
                    onrowdeleting="GridView2_RowDeleting" 
                    AllowSorting="True"   
                    onrowdatabound="GridView2_RowDataBound" 
                        AutoGenerateColumns="False" style="margin-left: 8px" PageSize="15" 
                        CssClass ="c13112304"
                   >
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns > 
                            <asp:TemplateField HeaderText="删除" >
                <ItemTemplate >
                    <asp:LinkButton ID="LinkButton2" runat="server" 
                        OnClientClick="return confirm('您确认删除该记录吗?');" Text="删除"  CommandName ="delete" ></asp:LinkButton>                     
                </ItemTemplate>
                 <HeaderStyle Width="40px" />
                 <ItemStyle Width="40px"  />
            </asp:TemplateField>
                                     <asp:BoundField DataField="索引" HeaderText="索引"  Visible ="false"  >
                              <ItemStyle Width="40px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="项次" HeaderText="项次" >
                              <ItemStyle Width="40px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                          </asp:BoundField>
          <asp:BoundField DataField="品号" HeaderText="ID" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
              <asp:BoundField DataField="料号" HeaderText="料号" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
            <asp:BoundField DataField="品名" HeaderText="品名" >
                              <ItemStyle Width="200px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                          </asp:BoundField> 
       <asp:BoundField DataField="客户料号" HeaderText="客户料号" >
                              <ItemStyle Width="120px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                          </asp:BoundField>
      
            <asp:BoundField DataField="采购数量" HeaderText="采购数量" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"  HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
                          
            <asp:BoundField DataField="采购单价" HeaderText="采购单价" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a" HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
             <asp:BoundField DataField="税率" HeaderText="税率" >
                              <ItemStyle Width="40px"  ForeColor="#595d5a" HorizontalAlign="Right" />
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                          </asp:BoundField>
           <asp:BoundField DataField="未税金额" HeaderText="未税金额"    DataFormatString="{0:0.00}"  >
                              <ItemStyle Width="80px"  ForeColor="#595d5a" HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
             <asp:BoundField DataField="税额" HeaderText="税额"   DataFormatString="{0:0.00}">
                              <ItemStyle Width="80px"  ForeColor="#595d5a" HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
            <asp:BoundField DataField="含税金额" HeaderText="含税金额"   DataFormatString="{0:0.00}">
                              <ItemStyle Width="80px"  ForeColor="#595d5a" HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
             <asp:BoundField DataField="需求日期" HeaderText="需求日期"   >
                              <ItemStyle Width="80px"  ForeColor="#595d5a" HorizontalAlign="Center" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
                        <asp:BoundField DataField="工程费" HeaderText="工程费"  DataFormatString="{0:0.00}" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"  HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
                              <asp:BoundField DataField="备注" HeaderText="备注"   >
                              <ItemStyle Width="200px"  ForeColor="#595d5a" HorizontalAlign="Center" />
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                          </asp:BoundField>
                      
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
                   
                </div>
                            <div class ="c13101902">
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
                <div id="Div51" class ="c13102201" >
        <asp:GridView ID="GridView4" runat="server" Width="65%" 
                    AllowSorting="True"   
                    onrowdatabound="GridView4_RowDataBound" 
                        onselectedindexchanged="GridView4_SelectedIndexChanged" 
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
                        Text='<%# Bind("PATH") %>'></asp:LinkButton>                     
                </ItemTemplate>
                 <HeaderStyle Width="500px" />
                 <ItemStyle Width="500px"  ForeColor="#595d5a"/>
            </asp:TemplateField>  
                      
         
           
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
                   
                </div>
     <script type ="text/javascript" >
         window.onload = function onload1() {
             var Invocation = document.getElementById("hint").value;
             var Invocation1 = document.getElementById("x").value;
             var Invocation2 = document.getElementById("x1").value;
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



                 var url = "../PurchaseManage/Supplierinfo.aspx?come=pu";
                 //传入参数示例
                 var dlgResult = myShowModalDialog(url, window, 900, 500);
                 if (navigator.userAgent.indexOf("Chrome") > 0) {

                 }
                 else {

                     document.getElementById("Text2").value = dlgResult[0];
                     document.getElementById("Text5").value = dlgResult[1];
                     document.getElementById("Text6").value = dlgResult[2];
                     document.getElementById("Text10").value = dlgResult[3];
                     document.getElementById("Text11").value = dlgResult[4];
                 }

             }
             else if (obj == "TextBox1") {


                 var url = "../BaseInfo/Wareinfo.aspx?obj1=" + obj1 + "&come=purchase";
                 var dlgResult = myShowModalDialog(url, window, 900, 500);
                 if (navigator.userAgent.indexOf("Chrome") > 0) {

                 }
                 else {

                     if (dlgResult != undefined) {

                         var table = document.getElementById('<%=GridView1.ClientID%>');
                          var tr = table.getElementsByTagName("tr");
                          for (i = 1; i < tr.length; i++) {
                              if (obj1 == i) {
                                  var v1 = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]; //获取girdview里第1列TextBox的值
                                  var v2 = tr[i].getElementsByTagName("td")[1].getElementsByTagName("input")[0];
                                  var v3 = tr[i].getElementsByTagName("td")[2].getElementsByTagName("input")[0];
                                  var v4 = tr[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0];
                                  var v5 = tr[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0];
                                  var v8 = tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0];

                                  v1.value = dlgResult[0];
                                  v2.value = dlgResult[1];
                                  v3.value = dlgResult[2];
                                  v4.value = dlgResult[3];
                                  v5.value = dlgResult[14];
                                  v8.value = dlgResult[7];
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
              else if (obj == "TextBox8") {
                  dlgResult = window.showModalDialog("../WDate.aspx", window, "dialogWidth:160px; dialogHeight:240px; status:0;");

                  if (dlgResult != undefined) {

                      table = document.getElementById('<%=GridView1.ClientID%>');
                      tr = table.getElementsByTagName("tr");
                      for (i = 1; i < tr.length; i++) {
                          if (obj1 == i) {
                              v1 = tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0]; //获取girdview里第1列TextBox的值

                              v1.value = dlgResult;


                              break;
                          }
                      }


                  }

              }
              else if (obj == "Text12") {


                  var url = "../BASEINFO/ReceivingAndDelivery.aspx?come=pu";
                  var dlgResult = myShowModalDialog(url, window, 900, 500);
                  if (navigator.userAgent.indexOf("Chrome") > 0) {

                  }
                  else {
                      if (dlgResult != undefined) {

                          document.getElementById("RDID").value = dlgResult[0];
                          document.getElementById("Text12").value = dlgResult[2];
                          document.getElementById("Text13").value = dlgResult[4];
                      }
                  }
              }
              else if (obj == "Text15") {
                  var url = "../BASEINFO/companyinfox.aspx?come=pu";
                  var dlgResult = myShowModalDialog(url, window, 900, 500);
                  if (navigator.userAgent.indexOf("Chrome") > 0) {

                  }
                  else {
                      if (dlgResult != undefined) {

                          document.getElementById("COKEY").value = dlgResult[0];
                          document.getElementById("Text14").value = dlgResult[1];
                          document.getElementById("Text15").value = dlgResult[2];
                          document.getElementById("Text16").value = dlgResult[3];
                      }
                  }
              }
              else {

                  var url = "../BaseInfo/EMPLOYEEINFO.aspx?come=pu";
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
         function over(obj, obj1) {

             if (obj1 == "Submit2s") {

                 obj.className = "c19012704";
             }
             else if (obj1 == "Submit2s_left") {


                 obj.className = "c19021303";

             }
             else if (obj1 == "Submit3s_left") {

                 obj.className = "c19021305";
             }
             else if (obj1 == "2s1") {

                 obj.className = "c19011802";
             }
             else if (obj1 == "xwidth50_height22") {

                 obj.className = "c19030502";
             }
             else if (obj1 == "left_xwidth50_height20") {

                 obj.className = "c19042102";
             }
             else if (obj1 == "xwidth80_height22") {

                 obj.className = "c19022511";
             }
             else if (obj1 == "xwidth90_height22") {

                 obj.className = "c19022802";
             }
             else if (obj1 == "left_xwidth110_height30") {

                 obj.className = "c19031202";
             }
             else if (obj1 == "xwidth100_height30") {

                 obj.className = "c19031204";
             }
             else if (obj1 == "left_xwidth100_height30") {

                 obj.className = "c19032302";
             }
             else if (obj1 == "xwidth110_height30") {

                 obj.className = "c19031502";
             }
             else if (obj1 == "add") {

                 obj.className = "c19011002";
             }
             else if (obj1 == "edit") {

                 obj.className = "c19010902";
             }
             else if (obj1 == "select") {

                 obj.className = "c19020802";

             }
             else if (obj1 == "edit_margin") {

                 obj.className = "c19011002";
             }
             else if (obj1 == "edit_top") {

                 obj.className = "c19021903";
             }
             else if (obj1 == "delete") {

                 obj.className = "c19011002";
             }
             else if (obj.value == "搜索") {

                 obj.className = "c19010505";
             }
             else if (obj.value == "导出") {

                 obj.className = "c19022502";
             }
             else if (obj.value == "提交") {

                 obj.className = "c19010507";
             }
             else if (obj.value == "添加部门" || obj.value == "添加员工" || obj.value == "添加角色"
                 || obj.value == "添加公告" || obj.value == "添加供应商" || obj.value == "添加PI"
                 || obj.value == "添加VPO") {
                 alert("w")
                 obj.className = "c19010503";
             }
             else if (obj.value == "添加采购入库单" || obj.value == "添加供应商对账单" || obj.value == "添加客户对账单") {

                 obj.className = "c19011502";
             }
             else if (obj1 == "last_page") {

                 obj.className = "c19012411";
             }
             else if (obj.value == "提示窗") {
                 obj.className = "c19011411";
             }
             else if (obj.value == "上传") {
                 obj.className = "c19010507";
             }
             else if (obj1 == "edit_div") {

                 obj.className = "c19020804";
             }
             else if (obj1 == "delete_div") {
                 obj.className = "c19020806";
             }
             else if (obj1 == "i1") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_i1").src = "/image/6.png";
             }
             else if (obj1 == "i2") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_i2").src = "/image/8.png";
             }
             else if (obj1 == "i3") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_i3").src = "/image/x3.png";
             }
             else if (obj1 == "width_60") {
                 obj.className = "c19040206";
                 document.getElementById("ctl00_ContentPlaceHolder1_width_60").src = "/image/x3.png";
             }
             else if (obj1 == "width_61") {
                 obj.className = "c19040206";
                 document.getElementById("ctl00_ContentPlaceHolder1_width_61").src = "/image/x3.png";
             }
             else if (obj1 == "i3_g") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_GridView1_ctl02_i3_g").src = "/image/x3.png";
             }
             else if (obj1 == "i4") {
                 obj.className = "c19022006";

                 document.getElementById("ctl00_ContentPlaceHolder1_i4").src = "/image/2.png";
             }
             else if (obj1 == "i5") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_i5").src = "/image/2.png";
             }
             else if (obj1 == "i51") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_i51").src = "/image/2.png";
             }
             else if (obj1 == "i52") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_i52").src = "/image/2.png";
             }
             else if (obj1 == "i6") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_i6").src = "/image/x6.png";
             }
             else if (obj1 == "i7") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_i7").src = "/image/4.png";
             }
             else if (obj1 == "i8") {
                 obj.className = "c19022006";
                 document.getElementById("ctl00_ContentPlaceHolder1_i8").src = "/image/2.png";
             }

             //alert("obj.id="+obj.value+","+obj1 + "," + obj.className+",obj2="+obj2+",obj2 classname="+obj2.className);

         }
         function out(obj, obj1) {

             if (obj1 == "Submit2s") {
                 obj.className = "c19012703";
             }
             else if (obj1 == "Submit2s_left") {


                 obj.className = "c19021302";

             }
             else if (obj1 == "Submit3s_left") {

                 obj.className = "c19021304";
             }
             else if (obj1 == "2s1") {

                 obj.className = "c19011801";
             }
             else if (obj1 == "xwidth50_height22") {

                 obj.className = "c19030501";
             }
             else if (obj1 == "left_xwidth50_height20") {

                 obj.className = "c19042101";
             }
             else if (obj1 == "xwidth80_height22") {

                 obj.className = "c19022510";
             }
             else if (obj1 == "xwidth90_height22") {

                 obj.className = "c19022801";
             }
             else if (obj1 == "left_xwidth110_height30") {

                 obj.className = "c19031201";
             }
             else if (obj1 == "xwidth100_height30") {

                 obj.className = "c19031203";
             }
             else if (obj1 == "xwidth110_height30") {

                 obj.className = "c19031501";
             }
             else if (obj1 == "left_xwidth100_height30") {

                 obj.className = "c19032301";
             }
             else if (obj1 == "add") {

                 obj.className = "c19011001";
             }
             else if (obj1 == "edit") {

                 obj.className = "c19010901";
             }
             else if (obj1 == "select") {

                 obj.className = "c19020801";

             }
             else if (obj1 == "edit_margin") {

                 obj.className = "c19011001";
             }
             else if (obj1 == "edit_top") {

                 obj.className = "c19021902";
             }
             else if (obj1 == "delete") {
                 obj.className = "c19011001";
             }
             else if (obj1 == "last_page") {
                 obj.className = "c19012410";
             }
             else if (obj.value == "搜索") {
                 obj.className = "c19010504";
             }
             else if (obj.value == "导出") {
                 obj.className = "c19022501";
             }
             else if (obj.value == "提交") {
                 obj.className = "c19010506";
             }
             else if (obj.value == "添加部门" || obj.value == "添加角色" || obj.value == "添加公告"
                 || obj.value == "添加供应商" || obj.value == "添加PI"
                 || obj.value == "添加VPO") {
                 obj.className = "c19010502";
             }
             else if (obj.value == "添加采购入库单" || obj.value == "添加供应商对账单" || obj.value == "添加客户对账单") {

                 obj.className = "c19011501";
             }

             else if (obj.value == "提示窗") {
                 obj.className = "c19011410";
             }
             else if (obj.value == "上传") {
                 obj.className = "c19010506";
             }
             else if (obj1 == "edit_div") {
                 obj.className = "c19020803";
             }
             else if (obj1 == "delete_div") {
                 obj.className = "c19020805";
             }
             else if (obj1 == "i1") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i1").src = "/image/5.png";
             }
             else if (obj1 == "i2") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i2").src = "/image/7.png";
             }
             else if (obj1 == "i3") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i3").src = "/image/x2.png";
             }
             else if (obj1 == "width_60") {
                 obj.className = "c19040205";
                 document.getElementById("ctl00_ContentPlaceHolder1_width_60").src = "/image/x2.png";
             }
             else if (obj1 == "width_61") {
                 obj.className = "c19040205";
                 document.getElementById("ctl00_ContentPlaceHolder1_width_61").src = "/image/x2.png";
             }
             else if (obj1 == "i3_g") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_GridView1_ctl02_i3_g").src = "/image/x2.png";
             }
             else if (obj1 == "i4") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i4").src = "/image/1.png";
             }
             else if (obj1 == "i5") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i5").src = "/image/1.png";
             }
             else if (obj1 == "i51") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i51").src = "/image/1.png";
             }
             else if (obj1 == "i52") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i52").src = "/image/1.png";
             }
             else if (obj1 == "i6") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i6").src = "/image/x5.png";
             }
             else if (obj1 == "i7") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i7").src = "/image/3.png";
             }
             else if (obj1 == "i8") {
                 obj.className = "c19022005";
                 document.getElementById("ctl00_ContentPlaceHolder1_i8").src = "/image/1.png";
             }

             //alert("obj.id=" + obj.value + "," + obj1 + "," + obj.className + ",obj2=" + obj2 + ",obj2 classname=" + obj2.className);
         }
     </script>

    </form>
</body>
</html>