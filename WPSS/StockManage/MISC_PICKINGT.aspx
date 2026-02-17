<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MISC_PICKINGT.aspx.cs" Inherits="WPSS.StockManage.MISC_PICKINGT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑开料单信息</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
<meta name ="Description" content ="ERP管理系统" />
<meta name ="keywords" content ="ERP管理系统,ERP管理软件,ERP,小微企业管理系统,希哲软件" />
       <link href ="../Css/SSBase.css"  type ="text/css" rel ="Stylesheet" />
       <link href ="../Css/S131017.css"  type ="text/css" rel ="Stylesheet" />
    </head>
<body >
   <form id="form1" runat="server">
    <input id="cuid" type="hidden"  runat="server" />
   <input id="hint" type="hidden"  runat="server" />
      <input id="x" type="hidden"  runat="server" />
       <input id="ControlFileDisplay" type="hidden"  runat="server" />
        <input id="x2" type="hidden"  runat="server" />
         <input id="CUKEY" type="hidden"  runat="server" />
                <div class ="c13101905">
      <div class="c13101906" id ="Div9">
          &gt;编辑开料单作业 </div>
     <div class="c13101907" id ="Div10">
 </div>
    </div>
       <div class="c13110501">
           <div id="Div923" class="c13110502">
               <input id="Submit1" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="添加开料单" />
           </div>
           <div id="Div130" class="c13110510">
               <span id="Span4" class="c13110511"></span>
           </div>
           <div id="Div924" class="c13110502">
               <input id="Submit2" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="保存" />
           </div>
           <div id="Div925" class="c13110510">
               <span id="Span5" class="c13110511"></span>
           </div>
           <div id="Div18" class="c13110507">
               &nbsp;</div>
           <div id="Div19" class="c13110510">
               <span id="Span6" class="c13110511"></span>
           </div>
           <div id="Div926" class="c13110510">
&nbsp;</div>
       </div>

<div  id="i13102301" class ="c13102101">
<span  class ="c13102102"><asp:Label ID="prompt" runat="server"  ForeColor="#f80707"></asp:Label></span>
</div> 
  <div class ="c13101902">
      <div class="c13101903" id ="Div24">
          开料单号</div>
     <div class="c13102904" id ="Div25">
<input id="Text1" type="text"  runat="server"   readonly ="readonly"   class="c14031401" /> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Text1" Text="必填" runat="server" /></div>
                            <div class="c13101903" id ="Div28">
                                开料日期</div>
       <div class="c13102904" id ="Div11">
         <input id="Text3" type="text" runat="server" onclick ="f13100202('Text3')" class ="c13110901"/> 
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="Text3" Text="必填" runat="server" />
         </div>
                               <div class="c13101903" id ="Div2">
                                   开料员工号</div>                        
<div class="c14120312" id ="Div4">
<input id="Text4" type="text" runat="server" class ="c14120310" />
<span style =" margin-left :0px;"><a  href="javascript:f13100202('Text4','');">选择 </a> </span> 
 <span  style =" margin-left :0px"><asp:Label ID="Label1" runat="server" Text="" ></asp:Label></span>
</div>
           </div>
<div  class ="c13111602">
           <asp:GridView ID="GridView1" runat="server" 
                    AllowSorting="True"   
                    onrowdatabound="GridView1_RowDataBound" 
                        AutoGenerateColumns="False" 
              CssClass ="c21050401" PageSize="15" 
                   >
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns > 
   
                       <asp:TemplateField HeaderText="ID" >
                <ItemTemplate >
                                <asp:TextBox ID="TextBox1" runat="server"   CssClass ="c14071603"    ></asp:TextBox>   
                                 <a  href="javascript:f13100202('TextBox1','<%#Eval ("项次") %>');"> 选择</a> 
                </ItemTemplate>
               <HeaderStyle Width="7%" />
                 <ItemStyle Width="7%"/>
            </asp:TemplateField> 
           
                       <asp:TemplateField HeaderText="料号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox2" runat="server"  CssClass="c14071609"></asp:TextBox>                     
                </ItemTemplate>
              <HeaderStyle Width="5%" />
                 <ItemStyle Width="5%"/>
            </asp:TemplateField> 
               <asp:TemplateField HeaderText="品名">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox3" runat="server"  CssClass="c14071610"></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"/>
            </asp:TemplateField> 
       
                     <asp:TemplateField HeaderText="开料数量">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox4" runat="server"   CssClass ="c13112302"></asp:TextBox>                     
                </ItemTemplate>
              <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"/>
            </asp:TemplateField> 
                      <asp:TemplateField HeaderText="领用单位"  Visible="false">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox5" runat="server"  CssClass ="c14071616"></asp:TextBox>                     
                </ItemTemplate>
                <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"/>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="仓库">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox6" runat="server"  CssClass="c14071617"></asp:TextBox>
                 <a  href="javascript:f13100202('TextBox6','<%#Eval ("项次") %>');">选择 </a>                     
                </ItemTemplate>
             <HeaderStyle Width="7%" />
                 <ItemStyle Width="7%"/>
            </asp:TemplateField> 
                 <asp:TemplateField HeaderText="库位" Visible="false">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox7" runat="server" CssClass ="c14120202"></asp:TextBox>                     
                </ItemTemplate>
           <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"/>
            </asp:TemplateField> 
                <asp:TemplateField HeaderText="批号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox8" runat="server" CssClass ="c14120202"></asp:TextBox>                     
                </ItemTemplate>
               <HeaderStyle Width="5%" />
                 <ItemStyle Width="5%"/>
            </asp:TemplateField> 
                          <asp:TemplateField HeaderText="库存数量">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox9" runat="server"   CssClass ="c14071615"></asp:TextBox>                     
                </ItemTemplate>
                 <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"/>
            </asp:TemplateField> 
                       <asp:TemplateField HeaderText="库存单位" Visible="false">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox10" runat="server"  CssClass ="c14071616"></asp:TextBox>                     
                </ItemTemplate>
              <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"/>
            </asp:TemplateField> 
        
              <asp:TemplateField HeaderText="客户料号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox11" runat="server"  CssClass ="c14070203" ></asp:TextBox>                     
                </ItemTemplate>
               <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"/>
            </asp:TemplateField> 
              <asp:TemplateField HeaderText="品牌" Visible="false">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox12" runat="server"  CssClass ="c14070203"></asp:TextBox>                     
                </ItemTemplate>
               <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"/>
            </asp:TemplateField> 
        <asp:TemplateField HeaderText="铜厚">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox13" runat="server"  CssClass ="c13120501"></asp:TextBox>                     
                </ItemTemplate>
                  <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"/>
            </asp:TemplateField> 
                <asp:TemplateField HeaderText="备注">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox14" runat="server"   CssClass ="c13120501"></asp:TextBox>                     
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                 <ItemStyle Width="10%"/>
            </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
                </div>
         <div id="i13111001" class ="c13102201">
        <asp:GridView ID="GridView2" runat="server" 
                    onrowdeleting="GridView2_RowDeleting" 
                    AllowSorting="True"   
                    onrowdatabound="GridView2_RowDataBound" 
                        AutoGenerateColumns="False" style="margin-left: 8px" PageSize="15" 
                          CssClass ="c21050102"
                   >
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns > 
                                        <asp:TemplateField HeaderText="选取">
                   <ItemTemplate>
         <a href ="javascript:f21120801('<%#Eval ("开料单号") %>')">
                       选取</a>
                   </ItemTemplate>
                            <HeaderStyle Width="40px" HorizontalAlign="Center" />
                 <ItemStyle Width="40px"  />
                   </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" >
                <ItemTemplate >
                    <asp:LinkButton ID="LinkButton2" runat="server" 
                        OnClientClick="return confirm('您确认删除该记录吗?');" Text="删除"  CommandName ="delete" ></asp:LinkButton>                     
                </ItemTemplate>
                 <HeaderStyle Width="40px" />
                 <ItemStyle Width="40px"  />
            </asp:TemplateField>
                                     <asp:BoundField DataField="索引" HeaderText="索引"    >
                              <ItemStyle Width="40px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                          </asp:BoundField>
                           <asp:BoundField DataField="开料单号" HeaderText="开料单号" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
                         <asp:BoundField DataField="状态" HeaderText="状态" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="项次" HeaderText="项次" >
                              <ItemStyle Width="40px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                          </asp:BoundField>
          <asp:BoundField DataField="ID" HeaderText="ID" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>

            <asp:BoundField DataField="品名" HeaderText="品名" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"/>
                                    <HeaderStyle HorizontalAlign="Center" />
                          </asp:BoundField> 
                         <asp:BoundField DataField="PLANK_THICKNESS" HeaderText="板厚" >
                              <ItemStyle Width="120px" ForeColor="#595d5a" />
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                          </asp:BoundField>
                           <asp:BoundField DataField="spec" HeaderText="铜厚" >
                              <ItemStyle Width="120px" ForeColor="#595d5a" />
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                          </asp:BoundField>
       <asp:BoundField DataField="客户料号" HeaderText="客户料号" >
                              <ItemStyle Width="120px"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                          </asp:BoundField>
      
            <asp:BoundField DataField="开料数量" HeaderText="开料数量" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a"  HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
                      
            <asp:BoundField DataField="仓库名称" HeaderText="仓库" >
                              <ItemStyle Width="120px"  ForeColor="#595d5a"  />
                                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                          </asp:BoundField>
   
                           <asp:BoundField DataField="批号" HeaderText="批号" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
                        
                           <asp:BoundField DataField="制单人" HeaderText="制单人" >
                              <ItemStyle Width="80px"  ForeColor="#595d5a" />
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                          </asp:BoundField>
         <asp:BoundField DataField="制单日期" HeaderText="制单日期"   >
                              <ItemStyle Width="130px"  ForeColor="#595d5a" HorizontalAlign="Center" />
                                    <HeaderStyle Width="130px" HorizontalAlign="Center" />
                          </asp:BoundField>
                              <asp:BoundField DataField="备注" HeaderText="备注"   >
                              <ItemStyle Width="100px"  ForeColor="#595d5a" HorizontalAlign="Center" />
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                          </asp:BoundField>
                      
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
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
                  document.getElementById("i13111001").style.display = "block";
              }
              else {

                  document.getElementById("i13111001").style.display = "none";
              }
              //document.getElementById("Text10").focus();
          }
          function f21120801(obj) {
              var arr1 = new Array();
              arr1[0] = obj;
              if (navigator.userAgent.indexOf("Chrome") > 0) {
                  window.opener.document.getElementById("Text6").value = obj;
                  window.close();
              }
              else {
                  if (window.opener != undefined) {
                      //for chrome
                      window.opener.returnValue = arr1;
                  }
                  else {
                      window.returnValue = arr1;
                  }
                  window.close();
              }
          }

          function f13100202(obj, obj1) {
              var dlgResult;
              if (obj == "Text5") {
                  dlgResult = window.showModalDialog("../sellmanage/customerinfo.aspx", window, "dialogWidth:970px; dialogHeight:490px; status:0");
                  if (dlgResult != undefined) {

                      document.getElementById("cuid").value = dlgResult[0];
                      document.getElementById("Text5").value = dlgResult[1];

                  }
              }
              else if (obj == "Text2") {
                  dlgResult = window.showModalDialog("../workorder_manage/workorder.aspx?cuid=" + document.getElementById("cuid").value + "", window, "dialogWidth:970px; dialogHeight:490px; status:0");
                  if (dlgResult != undefined) {

                      document.getElementById("Text2").value = dlgResult[0];/*woid*/
                      document.getElementById("Text5").value = dlgResult[1];/*cname*/
                      document.getElementById("Text6").value = dlgResult[2];/*id*/
                      document.getElementById("Text7").value = dlgResult[3];/*co_wareid*/
                      document.getElementById("Text8").value = dlgResult[4];/*wname*/
                      document.getElementById("Text9").value = dlgResult[5];/*cwareid*/
                      document.getElementById("Text10").value = dlgResult[6];/*allow picking count*/
                  }
              }
              else if (obj == "TextBox1") {
                  dlgResult = window.showModalDialog("../BaseInfo/Wareinfo.aspx", window, "dialogWidth:1100px; dialogHeight:490px; status:0");

                  if (dlgResult != undefined) {

                      var table = document.getElementById('<%=GridView1.ClientID%>');
                      var tr = table.getElementsByTagName("tr");
                      for (i = 1; i < tr.length; i++) {
                          if (obj1 == i) {
                              tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].value = dlgResult[0]; //获取girdview里第1列TextBox的值
                              tr[i].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value = dlgResult[1];/*co_wareid*/
                              tr[i].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value = dlgResult[2]; /*wname*/
                              tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value = dlgResult[3];/*cwareid*/
                              tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value = dlgResult[6]; /*spec*/

                              tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value = dlgResult[11]; 
                              tr[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value = dlgResult[12]; 
                              tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value = dlgResult[13];
                      
                             // alert("id=" + dlgResult[0] + ",料号=" + dlgResult[1] + ",品名=" + dlgResult[2] +
                                  //", 客户料号 = " + dlgResult[3] + ", 客户名称 = " + dlgResult[4] + ", 规格 = " + dlgResult[6] + ", 备注 = " + dlgResult[7] + "," + dlgResult[12] + "," + dlgResult[13])
                              //GODE_sku.value = dlgResult[12];
                              break;
                          }
                      }
                  }
              }
              else if (obj == "TextBox6") {
              dlgResult = window.showModalDialog("../StockManage/StorageCase.aspx", window,
                   "dialogWidth:970px; dialogHeight:490px; status:0");
                  if (dlgResult != undefined) {
                      var table1 = document.getElementById('<%=GridView1.ClientID%>');
                      var tr1 = table1.getElementsByTagName("tr");
                      for (i = 1; i < tr1.length; i++) {
                          if (obj1 == i) {
                              var v1 = tr1[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0];
                              var v2 = tr1[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0];
                              var v3 = tr1[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0];
                              v1.value = dlgResult[0];
                              v2.value = dlgResult[1];
                              v3.value = dlgResult[2];
                              break;
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
              else if (obj == "Text11") {
                  dlgResult = window.showModalDialog("../BASEINFO/CompanyInfoPS.aspx", window, "dialogWidth:970px; dialogHeight:490px; status:0");
                  if (dlgResult != undefined) {

                      document.getElementById("CUKEY").value = dlgResult[0];
                      document.getElementById("Text6").value = dlgResult[1];
                      document.getElementById("Text10").value = dlgResult[2];
                      document.getElementById("Text11").value = dlgResult[3];
                  }
              }
              else {
                  dlgResult = window.showModalDialog("../BaseInfo/EMPLOYEEINFO.aspx", window, "dialogWidth:970px; dialogHeight:490px; status:0;");
                  if (dlgResult != undefined) {

                      document.getElementById("Text4").value = dlgResult[0];
                      document.all("Label1").innerText = dlgResult[1];
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