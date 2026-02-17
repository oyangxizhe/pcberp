<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SellUnitPriceT.aspx.cs" Inherits="WPSS.SellManage.SellUnitPriceT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑销售核价单</title>
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
                <div class ="c13101905">
      <div class="c13101906" id ="Div9">
>编辑销售核价单 </div>
     <div class="c13101907" id ="Div10">
 </div>
    </div>
       <div class="c13110501">
           <div id="Div923" class="c13110502">
               <input id="Submit1" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="添加销售核价" />
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
           <div id="Div927" class="c13110507">
               <input id="Submit3" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="退出" />
           </div>
           <div id="Div928" class="c13110510">
               <span id="Span6" class="c13110511"></span>
           </div>
           <div id="Div926" class="c13110510">
&nbsp;</div>
       </div>

<div  id="i13102301" class ="c13102101">
<span  class ="c13102102"><asp:Label ID="prompt" runat="server"  ForeColor="#f80707"></asp:Label></span>
</div> 

           <div class ="c13101902">
      <div class="c13122302" id ="Div1">
          销售单价代码</div>
     <div class="c14031403" id ="Div3">
<input id="Text1" type="text"  runat="server"   readonly ="readonly" class="c14031401"/> 
         </div>
                 <div class="c13122302" id ="Div12">
              ID</div>
     <div class="c14031403" id ="Div13">
   <input id="Text2" type="text"    runat ="server" class ="c14031405" /> 
        <span style =" margin-left :5px"> <a  href="javascript:f13100202('Text2');">选择</a></span> 
        </div>
                  <div class="c13122302" id ="Div11">
                      <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
               </div>
     
  <div class="c14031403" id ="Div14">
   <input id="Text3" type="text"  runat ="server" class ="c14040704" /> </div>
         <div class="c13122302" id ="Div7">
             <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
               </div>
     <div  class="c14120501" id ="Div8">
<input id="Text4" type="text"  runat="server"   readonly ="readonly" class="c14040704"/> 
         </div>
           </div>
           <div class ="c13101902">

                 <div class="c13122302" id ="Div18">
                     <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                 </div>
     <div class="c14031403" id ="Div19">
   <input id="Text5" type="text"    runat ="server" class ="c14040704" /> 
     </div>
                  <div class="c13122302" id ="Div20">
                      客户</div>
     
  <div class="c14031403" id ="Div22">
   <input id="Text6" type="text"  runat ="server" class ="c14040705" /> 
           </div>
                            <div class="c13122302" id ="Div16">
                     销售单价</div>
     <div class="c14031403" id ="Div17">
   <input id="Text7" type="text"    runat ="server" class ="c14031405" /> (未税)
         </div>
              <div class="c13122302" id ="Div26">
              量产单价</div>
     <div  class="c14120501" id ="Div27">
   <input id="Text8" type="text"    runat ="server" class ="c14040704" /> 
     </div>
           </div> 
           <div class ="c13101902">

                   <div class="c13122302" id ="Div28">
              量产数量</div>
     <div class="c14031403" id ="Div29">
   <input id="Text9" type="text"    runat ="server" class ="c14040704" /> 
     </div>
                      <div class="c13122302" id ="Div30">
                    Sample单价</div>
     <div class="c14031403" id ="Div31">
   <input id="Text10" type="text"    runat ="server" class ="c14040704"/> 
         </div>
              <div class="c13122302" id ="Div32">
              Sample数量</div>
     <div class="c14031403" id ="Div33">
   <input id="Text11" type="text"    runat ="server" class ="c14040704" /> 
     </div>
                   <div class="c13122302" id ="Div34">
              小量单价</div>
     <div  class="c14120501" id ="Div35">
   <input id="Text12" type="text"    runat ="server" class ="c14040704" /> 
     </div>
           </div>
       <div class ="c13101902">

                   <div class="c13122302" id ="Div45">
              Set长</div>
     <div class="c14031403" id ="Div46">
   <input id="Text16" type="text"    runat ="server" class ="c14040704" /> 
     </div>
                      <div class="c13122302" id ="Div47">
                    Set宽</div>
     <div class="c14031403" id ="Div48">
   <input id="Text17" type="text"    runat ="server" class ="c14040704"/> 
         </div>
              <div class="c13122302" id ="Div49">
              Set排版数</div>
     <div class="c14031403" id ="Div50">
   <input id="Text18" type="text"    runat ="server" class ="c14040704" /> 
     </div>
                   <div class="c13122302" id ="Div51">
             批量报价</div>
     <div  class="c14120501" id ="Div52">
   <input id="Text19" type="text"    runat ="server" class ="c14040704" />&nbsp;&nbsp;&nbsp;&nbsp; 平方</div>
           </div>
           <div class ="c13101902">
                 <div class="c13122302" id ="Div36">
                     小量数量</div>
     <div class="c14031403" id ="Div37">
   <input id="Text13" type="text"    runat ="server" class ="c14040705" /> 
         </div>
              <div class="c13122302" id ="Div38">
               工程费</div>
     <div class="c14031403" id ="Div39">
   <input id="Text14" type="text"    runat ="server" class ="c14040704" /> 
     </div>  
                <div class="c13122302" id ="Div43">
               报价单号</div>
     <div class="c14031403" id ="Div44">
   <input id="Text15" type="text"    runat ="server" class ="c14040704" /> 
     </div>
      <div class="c13122302" id ="Div40">
                <asp:Button ID="Button2" runat="server" 
               Text="产生单价" Width="76px" onclick="GenerateUNITPRICE_Click" /></div>

           </div>
       <div class ="c13101902">
                 <div class="c13122302" id ="Div53">
                     含税单价</div>
     <div class="c14031403" id ="Div54">
   <input id="Text20" type="text"    runat ="server" class ="c14040705" /> 
         </div>
              <div class="c13122302" id ="Div55">
                  单位面积价格
               </div>
     <div class="c14031403" id ="Div56">
   <input id="Text21" type="text"    runat ="server" class ="c14040704" /></div>  
                <div class="c13122302" id ="Div57">
               </div>
     <div class="c14031403" id ="Div58">
   &nbsp;</div>
      <div class="c13122302" id ="Div59">
                 </div>

           </div>
           <div class ="c13122402">

          <div class="c13122302" id ="Div108">
                 备注</div>
     <div class="c13122401" id ="Div109">

         <asp:TextBox ID="TextBox1" runat="server"   TextMode="MultiLine" CssClass ="c13122403"></asp:TextBox>
         </div>
            <div class ="c14031406">
               <div class="c13122302" id ="Div23">
         上传资料
                 </div>
          
     <div class="c13102203" id ="Div24">
             <asp:DataList ID="DataList1" runat="server" RepeatColumns="1"   >
                　<ItemTemplate >    
<div style="float:left; width:30px; height:30px; border:0px solid #0000FF; display:none ;">
<%#Eval ("C") %></div>
<input id="File2" type="file" name="File" runat="server" style="width: 300px;  margin-top :5px; margin-left :5px;   border-style: groove; border-width: thin;
"/>
   </div>  
</ItemTemplate> 　
</asp:DataList>
</div>
            <div class="c13102301" id ="Div25">
            <span style =" float :left ; margin-left :30px;">   <asp:Button ID="Button1" runat="server" onclick="btnOnloadFile_Click" 
               Text="上传" /></span>
        <span style=" margin-left :20px; color :Red ;">注：上传的单个附件大小需小于20M</span>
                 </div>
</div>
<div class ="c14031406">
     <asp:GridView ID="GridView1" runat="server" Width="58%" 
                    onrowdeleting="GridView1_RowDeleting" 
                    AllowSorting="True"   
                    onrowdatabound="GridView1_RowDataBound" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" 
                        AutoGenerateColumns="False" style="margin-left: 8px" PageSize="15" 
                        CssClass ="c13102001"
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
                            <asp:BoundField DataField="FLKEY" HeaderText="文件"   Visible ="false" >
                              <ItemStyle Width="500px" ForeColor="#595d5a" />
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                          </asp:BoundField>
             <asp:TemplateField HeaderText="点击打开文件">
                <ItemTemplate >
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" 
                        Text='<%# Bind("oldfilename") %>'></asp:LinkButton>                     
                </ItemTemplate>
                 <HeaderStyle Width="150px" />
                 <ItemStyle Width="150px"  ForeColor="#595d5a"/>
            </asp:TemplateField>   
               
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
                </div>
                </div>
      <script type ="text/javascript" >
          window.onload = function onload1() {
              var Invocation = document.getElementById("hint").value;
              if (Invocation != "") {
                  document.getElementById("i13102301").style.display = "block";
                  document.all("prompt").innerText = Invocation;
              }
              else {
                  document.getElementById("i13102301").style.display = "none";
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
          function f13100202(obj) {
              var dlgResult;
              if (obj == "Text2") {

                
    
                  if (navigator.userAgent.indexOf("Chrome") > 0) {
                      var url = "../baseinfo/wareinfo.aspx?come=sellunitprice";
                      //传入参数示例

                       dlgResult = myShowModalDialog(url, window, 1000, 500);
                  }
                  else {
                       dlgResult = window.showModalDialog("../baseinfo/wareinfo.aspx", window, "dialogWidth:970px; dialogHeight:490px; status:0");
                      if (dlgResult != undefined) {

                          document.getElementById("Text2").value = dlgResult[0];
                          document.getElementById("Text3").value = dlgResult[1];
                          document.getElementById("Text4").value = dlgResult[2];
                          document.getElementById("Text5").value = dlgResult[3];
                          document.getElementById("Text6").value = dlgResult[4];

                          document.getElementById("Text16").value = dlgResult[8];
                          document.getElementById("Text17").value = dlgResult[9];
                          document.getElementById("Text18").value = dlgResult[10];

                      }
                  }

              }

              else if (obj == "Text3") {
                  dlgResult = window.showModalDialog("../SELLManage/CUSTOMERinfo.aspx", window, "dialogWidth:970px; dialogHeight:490px; status:0");
                  if (dlgResult != undefined) {

                      document.getElementById("Text3").value = dlgResult[1];
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