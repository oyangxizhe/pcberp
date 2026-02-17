<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuReconcileT.aspx.cs" Inherits="WPSS.PurchaseManage.SuReconcileT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑供应商对账单</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
<meta name ="Description" content ="进销存管理系统" />
<meta name ="keywords" content ="进销存管理系统,进销存管理软件,ERP,小微企业管理系统,希哲软件" />
       <link href ="../Css/SSBase.css"  type ="text/css" rel ="Stylesheet" />
       <link href ="../Css/S131017.css"  type ="text/css" rel ="Stylesheet" />
    </head>
<body >
   <form id="form1" runat="server">
    <input id="Suid" type="hidden"  runat="server" />
   <input id="hint" type="hidden"  runat="server" />
    <input id="emid" type="hidden"  runat="server" />
      <input id="x" type="hidden"  runat="server" />
       <input id="ControlFileDisplay" type="hidden"  runat="server" />
        <input id="x2" type="hidden"  runat="server" />
         <input id="SuKEY" type="hidden"  runat="server" />
                <div class ="c13101905">
      <div class="c13101906" id ="Div9">
          &gt;编辑供应商对账单 </div>
     <div class="c13101907" id ="Div10">
 </div>
    </div>
<div class ="c13110501">
      <div class="c21070201" id ="Div17">
<input id="Submit1" type="submit" value="添加供应商对账单"  runat ="server"  onmouseover="over(this)"
               onmouseout="out(this)" class ="c19011501"  
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
            <div class="c13110507" id ="Div1">
<input id="Submit4" type="submit" value="打印"  runat ="server"  onmouseover="over(this,'Submit2s')"
               onmouseout="out(this,'Submit2s')" class ="c19012703"  
              onserverclick="submit1_Click" />
   </div>
                 <div class="c13110510" id ="Div3">
       </div>
                 <div class="c13110507" id ="Div30" style="display :none;">
                  <span class="c13110503" id ="Span9">
     <asp:ImageButton ID="btnEXCEL_PRINT" 
                 runat="server" ImageUrl="~/Image/btnPrint.png" Width="60px" 
                      onclick="btnEXCEL_PRINT_Click" />
          </span>
   </div>
                       <div class="c13110507" id ="Div32" style ="display :block  ">
<input id="Submit5" type="submit" value="确认对账"  runat ="server"  onmouseover="over(this,'Submit2s')"
               onmouseout="out(this,'Submit2s')" class ="c19012703"  
              onserverclick="submit1_Click" />
   </div>
          
                 <div class="c13110510" id ="Div33">
   <span class="c13110511" id ="Span11">
                 
          </span>
       </div>
                       <div class="c13110507" id ="Div34">
<input id="Submit6" type="submit" value="对账还原"  runat ="server"  onmouseover="over(this,'Submit2s')"
               onmouseout="out(this,'Submit2s')" class ="c19012703"  
              onserverclick="submit1_Click" />
   </div>
    </div>
<div  id="i13102301" class ="c13102101">
<span  class ="c13102102"><asp:Label ID="prompt" runat="server"  ForeColor="#f80707"></asp:Label></span>
</div> 
  <div class ="c13101902">
      <div class="c13101903" id ="Div24">
          供应商对账单号</div>
     <div class="c13101904" id ="Div25">
<input id="Text1" type="text"  runat="server"   readonly ="readonly" class="c13102103"/> </div>
       <div class="c13101903" id ="Div5">
             供应商名称</div>
     <div class="c13111503" id ="Div6">
   <input id="Text2" type="text"  runat ="server"  class ="c13102103"  />
     <span style =" margin-left :2px; margin-right :2px;"><a  href="javascript:f13100202('Text2','');">
         选择</a></span> <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="Text2" Text="必填的！" runat="server" />
      </div>
         <div class="c18061301" id ="Div26">
              <asp:LinkButton 
             ID="btnSure" runat="server" onclick="btnSure_Click"  
            >确定</asp:LinkButton>
             </div>
     <div class="c13110801"  id ="Div27">
  </div>
   
           </div>
       <div id="i14073102" class ="c13111601">
     
      <div class ="c15013101">    
       <asp:CheckBox ID="CheckBox2" runat="server"  Text ="全选" oncheckedchanged="CheckBox2_CheckedChanged"  /> 
           <asp:CheckBox ID="CheckBox3" runat="server" Text ="反选" oncheckedchanged="CheckBox3_CheckedChanged" />
           </div>
     <div class="c15013001" id ="Div36">

      <span style="color :#990033">
      
        </span>
</div>

          </div>
<div id="i14073101" class ="c15012901">
            <asp:GridView ID="GridView1" runat="server" 
                    AllowSorting="True"   
                    onrowdatabound="GridView1_RowDataBound" 
                        AutoGenerateColumns="False" PageSize="15" 
                             CssClass ="c18052903"
                   >
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns > 
                       <asp:TemplateField HeaderText="选择" >
                <ItemTemplate >
                    <asp:CheckBox ID="CheckBox1" runat="server"  Checked='<%# Bind("选择")%>'   CssClass ="c14080104" />
                </ItemTemplate>
                 <HeaderStyle Width="2%" />
                 <ItemStyle Width="2%"  />
            </asp:TemplateField>

                     <asp:TemplateField HeaderText="序号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox20" runat="server"  Text='<%#Eval ("目录项次") %>'  CssClass ="c14070203" ></asp:TextBox>                     
                </ItemTemplate>
                <HeaderStyle Width="2%" />
                 <ItemStyle Width="2%"  />
            </asp:TemplateField>
             
                             <asp:BoundField DataField="供应商代码" HeaderText="供应商代码" >
                             <ItemStyle Width="4%"  ForeColor="#595d5a" CssClass ="c15021101"/>
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="供应商名称" HeaderText="供应商名称" >
                             <ItemStyle Width="8%"  ForeColor="#595d5a" CssClass ="c15021101"/>
                                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                          </asp:BoundField>
                                     <asp:TemplateField HeaderText="入库(退货)单号">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox3" runat="server"  Text='<%#Eval ("入库退货单号") %>' CssClass ="c14070203" ></asp:TextBox>                     
                </ItemTemplate>
               <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  />
            </asp:TemplateField>
                          <asp:BoundField DataField="采购单号" HeaderText="采购单号" >
                             <ItemStyle Width="4%"  ForeColor="#595d5a" CssClass ="c15021101"/>
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="项次" HeaderText="项次" >
                          <ItemStyle Width="2%"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="2%" HorizontalAlign="Center" />
                          </asp:BoundField>
          <asp:BoundField DataField="ID" HeaderText="ID" >
                              <ItemStyle Width="4%"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                          </asp:BoundField>
              <asp:BoundField DataField="料号" HeaderText="料号" >
                               <ItemStyle Width="4%"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                          </asp:BoundField>
                         <asp:BoundField DataField="客户料号" HeaderText="客户料号" >
                               <ItemStyle Width="4%"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                          </asp:BoundField>
            <asp:BoundField DataField="品名" HeaderText="品名" >
                              <ItemStyle Width="6%"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="6%" HorizontalAlign="Center" />
                          </asp:BoundField> 

      
            <asp:BoundField DataField="采购数量" HeaderText="采购数量" >
                             <ItemStyle Width="4%"  ForeColor="#595d5a" CssClass ="c14071615"/>
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                          </asp:BoundField>
                          
            <asp:BoundField DataField="采购单价" HeaderText="采购单价" >
                             <ItemStyle Width="4%"  ForeColor="#595d5a" CssClass ="c14071615"/>
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                          </asp:BoundField>
            
                           <asp:BoundField DataField="入库退货数量" HeaderText="入库(退货)数量"  >
                             <ItemStyle Width="5%"  ForeColor="#595d5a" CssClass ="c14071615"/>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                          </asp:BoundField>
             <asp:BoundField DataField="税率" HeaderText="税率" >
                           <ItemStyle Width="2%"  ForeColor="#595d5a" CssClass ="c14071615"/>
                                    <HeaderStyle Width="2%" HorizontalAlign="Center" />
                          </asp:BoundField>
                       <asp:TemplateField HeaderText="未税金额">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox4" runat="server"  Text='<%#Eval ("未税金额") %>' CssClass ="c14070203" ></asp:TextBox>                     
                </ItemTemplate>
               <HeaderStyle Width="4%" />
                 <ItemStyle Width="4%"  />
            </asp:TemplateField>
             <asp:BoundField DataField="税额" HeaderText="税额"   DataFormatString="{0:0.00}">
                           <ItemStyle Width="3%"  ForeColor="#595d5a" CssClass ="c14071615"/>
                                    <HeaderStyle Width="3%" HorizontalAlign="Center" />
                          </asp:BoundField>
            <asp:BoundField DataField="含税金额" HeaderText="含税金额"   DataFormatString="{0:0.00}">
                          <ItemStyle Width="4%"  ForeColor="#595d5a" CssClass ="c14071615"/>
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                          </asp:BoundField>
                    
             <asp:BoundField DataField="采购日期" HeaderText="采购日期"  Visible ="false"   >
                           <ItemStyle Width="6%"  ForeColor="#595d5a"/>
                                    <HeaderStyle Width="6%" HorizontalAlign="Center" />
                          </asp:BoundField>
                               <asp:TemplateField HeaderText="索引">
                <ItemTemplate >
                 <asp:TextBox ID="TextBox202" runat="server"  Text='<%#Eval ("索引") %>'  CssClass ="c14070203" ></asp:TextBox>                     
                </ItemTemplate>
                <HeaderStyle Width="6%" />
                 <ItemStyle Width="6%"  />
            </asp:TemplateField>
                  
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
                </div>
             
       <script type ="text/javascript" >
           window.onload = function onload1() {
               var Invocation = document.getElementById("hint").value;
               var Invocation1 = document.getElementById("x").value;
               var Invocation3 = document.getElementById("x2").value;

               if (Invocation != "") {
                   document.getElementById("i13102301").style.display = "block";
                   document.all("prompt").innerText = Invocation;
               }
               else {
                   document.getElementById("i13102301").style.display = "none";
               }

               if (Invocation3 != "") {
                   document.getElementById("i14073101").style.display = "block";
                   document.getElementById("i14073102").style.display = "block";
               }
               else {
                   document.getElementById("i14073101").style.display = "none";
                   document.getElementById("i14073102").style.display = "none";
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
                      var url = "../PurchaseManage/supplierinfo.aspx?come=sn";
                      var dlgResult = myShowModalDialog(url, window, 900, 500);
                      if (navigator.userAgent.indexOf("Chrome") > 0) {

                      }
                      else {
                          document.getElementById("Suid").value = dlgResult[0];
                          document.getElementById("Text2").value = dlgResult[1];
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