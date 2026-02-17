<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeInfoT.aspx.cs" Inherits="WPSS.BaseInfo.EmployeeInfoT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑员工信息</title>
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
          &gt;编辑员工信息维护 </div>
     <div class="c13101907" id ="Div10">
 </div>
     </div>
       <div class="c13110501">
           <div id="Div923" class="c13110502">
               <input id="Submit1" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="添加员工" />
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
               <input id="Submit3" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="退出" />
           </div>
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
      <div class="c13101903" id ="Div2">
   员工编号 </div>
     <div class="c13101904" id ="Div4">
<input id="Text1" type="text"  runat="server"   readonly ="readonly" class="c13102103" /> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Text1" Text="必填" runat="server" /></div>
         <div class="c13101903" id ="Div5">
             姓名</div>
     <div class="c13101904" id ="Div6">
   <input id="Text2" type="text"  runat ="server" class ="c13110901" /> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="Text2" Text="必填" runat="server" /></div>
                <div class="c13101903" id ="Div1">
                    部门</div>
     <div class="c13101904" id ="Div3">
                  <asp:DropDownList ID="DropDownList1" runat="server"  CssClass ="c13102104">
                          <asp:ListItem></asp:ListItem>
                          <asp:ListItem>经理室</asp:ListItem>
                         <asp:ListItem>业务部</asp:ListItem>
                         <asp:ListItem>财务部财务科</asp:ListItem>
                         <asp:ListItem>财务部信息科</asp:ListItem>
                         <asp:ListItem>物流部生管科</asp:ListItem>
                         <asp:ListItem>物流部采购科</asp:ListItem>
                         <asp:ListItem>物流部资材科</asp:ListItem>
                         <asp:ListItem>工程部</asp:ListItem>
                         <asp:ListItem>品保部</asp:ListItem>
                         <asp:ListItem>生技部设备科</asp:ListItem>
                         <asp:ListItem>生技部生技科</asp:ListItem>
                         <asp:ListItem>管理部人事科</asp:ListItem>
                         <asp:ListItem>管理部总务科</asp:ListItem>
                         <asp:ListItem>体系科</asp:ListItem>
                         <asp:ListItem>生产部</asp:ListItem>
                          <asp:ListItem>驾驭员</asp:ListItem>
                          <asp:ListItem>保安部</asp:ListItem>
                    </asp:DropDownList>
    </div>
        <div class="c13101903" id ="Div7">
   职务 </div>
     <div class="c14120501" id ="Div8">
     <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList2" runat="server" class="c13102104">
                                 <asp:ListItem ></asp:ListItem>
                    <asp:ListItem>职员</asp:ListItem>
                                    <asp:ListItem>主管(科长)</asp:ListItem>
                                    <asp:ListItem>课长(部长)</asp:ListItem>
                                    <asp:ListItem>经理</asp:ListItem>
                                    <asp:ListItem>副总经理</asp:ListItem>
                                    <asp:ListItem>总经理</asp:ListItem>
                                     <asp:ListItem>特助</asp:ListItem>
                                    <asp:ListItem>董事长</asp:ListItem>
                    </asp:DropDownList></span> </div>
           </div>
             <div class ="c13101902">
  
                     <div class="c13101903" id ="Div16">
            电话</div>
     <div class="c13101904" id ="Div17">
   <input id="Text3" type="text"  runat ="server" class ="c13110901" /> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="Text3" Text="必填" runat="server" /></div>
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