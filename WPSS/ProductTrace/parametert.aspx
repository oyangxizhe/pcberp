<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="parametert.aspx.cs" Inherits="WPSS.ProductTrace.parametert" %>
<DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑层数信息</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
<meta name ="Description" content ="进销存管理系统" />
<meta name ="keywords" content ="进销存管理系统,进销存管理软件,ERP,小微企业管理系统,希哲软件" />
   
       <link href ="../Css/S190320.css"  type ="text/css" rel ="Stylesheet" />
    </head>
<body >
   <form id="form1" runat="server">
    <input id="hint" type="hidden"  runat="server" />
          <input id="usid" type="hidden"  runat="server" />
        <input id="right" type="hidden"  runat="server" />
               <input id="id" type="hidden"  runat="server" />
        <asp:Panel ID="Panel1" runat="server" width="1100px"  >
                <div class ="c13101905">
      <div class="c13101906" id ="Div9">
<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </div>
     <div class="c13101907" id ="Div10">
 </div>
    </div>
     <div class ="c19030701">
         
          <input id="Submit2s" type="submit" value="上一页"  runat ="server"  onmouseover="over(this,'Submit2s')" onmouseout="out(this,'Submit2s')" class ="c19012703"   onserverclick="submit1_Click" />
         <input id="Submit3" type="submit" value="返回参数管理"  runat ="server"  onmouseover="over(this,'Submit3s_left')"
               onmouseout="out(this,'Submit3s_left')" class ="c19021304"   onserverclick="submit1_Click" />
    </div>
 
        <input id="prompt"  type="submit" value="提示窗"  runat ="server"   class ="c19011410"   onserverclick="submit1_Click" />
  <div class ="c13101902">
         <div class="c13101903" id ="Div5"> 
               参数名称：</div> 
     <div class="c18091301" id ="Div6">
    <input id="Text1" type="text"  runat ="server"  autocomplete="off" class ="c18091401" /> </div>
              <div class="c19022102" id ="Div38">
                  单位：</div>
    <div class="c19022101" id ="Div13">
         <input id="Text2" type="text"  runat ="server" autocomplete="off" class ="c19051301"  /></div>
           </div>
        <div class ="c19010703">
         <input id="Submit1" type="submit" value="提交"  runat ="server"  onmouseover="over(this)" onmouseout="out(this)" class ="c19010506"   onserverclick="submit1_Click" />&nbsp;
         
    </div>
            </asp:Panel>
       <script language="javascript" type="text/javascript">

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
               else if (obj1 == "xwidth80_height22") {

                   obj.className = "c19022511";
               }
               else if (obj1 == "xwidth90_height22") {

                   obj.className = "c19022802";
               }
               else if (obj1 == "left_xwidth110_height30") {

                   obj.className = "c19031202";
               }
               else if (obj1 == "left_xwidth100_height30") {

                   obj.className = "c19032302";
               }
               else if (obj1 == "xwidth100_height30") {

                   obj.className = "c19031204";
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

                   obj.className = "c19010503";
               }
               else if (obj.value == "添加货币贸易公司" || obj.value == "返回货币贸易公司管理") {

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
               else if (obj1 == "xwidth80_height22") {

                   obj.className = "c19022510";
               }
               else if (obj1 == "xwidth90_height22") {

                   obj.className = "c19022801";
               }
               else if (obj1 == "left_xwidth110_height30") {

                   obj.className = "c19031201";
               }
               else if (obj1 == "left_xwidth100_height30") {

                   obj.className = "c19032301";
               }
               else if (obj1 == "xwidth100_height30") {

                   obj.className = "c19031203";
               }
               else if (obj1 == "xwidth110_height30") {

                   obj.className = "c19031501";
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
               else if (obj.value == "添加部门" || obj.value == "添加员工" || obj.value == "添加角色" || obj.value == "添加公告"
                   || obj.value == "添加供应商" || obj.value == "添加PI"
                    || obj.value == "添加VPO") {
                   obj.className = "c19010502";
               }
               else if (obj.value == "添加货币贸易公司" || obj.value == "返回货币贸易公司管理") {

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