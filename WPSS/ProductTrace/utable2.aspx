<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="utable2.aspx.cs" Inherits="WPSS.PruductTrace.utable2" %>
<DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href ="/Css/S190320.css"  type ="text/css" rel ="Stylesheet" />
 <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" width="900px"  >
          <input id="come" type="hidden"  runat="server" />
         <input id="index" type="hidden"  runat="server" />
          <input id="count" type="hidden"  runat="server" />
  
         <input id="parent_nodeid" type="hidden"  runat="server" />
    <input id="nodeid" type="hidden"  runat="server" />
           <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                       <Services>
                           <asp:ServiceReference Path="~/WebService1.asmx"  />
                       </Services>
                   </asp:ScriptManager>
       <div >
                  <div class ="c13101905">
      <div class="c13101906" id ="Div9">
          &gt;表格管理<br /> </div>
     <div class="c13101907" id ="Div10">
 </div>
     </div>
       <div class ="c19030701">
          <div class="c18092613" id ="Div20">
              <input id="Text1" runat="server" class="c18092701" autocomplete="off"  placeholder="" type="text" /></div>
         <input id="Submit1" type="submit" value="搜索"  runat ="server"  onmouseover="over(this)" 
             onmouseout="out(this)" class ="c19010504"   onserverclick="submit1_Click" />
          
             &nbsp;
         
    </div>
  
             </div>
       <input id="prompt" type="submit" value="提示窗"  runat ="server"    class ="c19011410"   onserverclick="submit1_Click" />
    <div class ="c19011105">
     <div class="c19011101" id ="Div2">
              </div>
          <div class="c19011102" id ="Div1">
              </div>
           <div class="c19011101" id ="Div3">
               创建时间：</div>
          <div class="c19011102" id ="Div4">
                <asp:TextBox ID="TextBox1" runat="server" autocomplete="off" CssClass="c19011110" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd ',alwaysUseStartDate:true})"   ></asp:TextBox>
              </div>
        <div class="c19011120" id ="Div5">
            >> </div>

          <div class="c19011102" id ="Div6">
              <asp:TextBox ID="TextBox2" runat="server" autocomplete="off" CssClass="c19011110" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd ',alwaysUseStartDate:true})" ></asp:TextBox>
           </div>
         
    </div>
    <div class ="c18092614">
     <div class="c19011101" id ="Div7">
              </div>
          <div class="c19011102" id ="Div8">
        </div>
          <div class="c19011102" id ="Div12">
            </div>
       
         <div class="c19011101" id ="Div13">
            </div>
          <div class="c19011102" id ="Div14">
                 </div>
         
     <div class="c19011101" id ="Div17">
              排序：</div>
          <div class="c19022811" id ="Div18">
              <asp:DropDownList ID="DropDownList8" runat="server" CssClass="c19022810" 
                  AutoPostBack ="true"  OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged">
       <asp:ListItem Value ="mdate" >创建时间</asp:ListItem>
                    </asp:DropDownList></div>
          <div class="c19022807" id ="Div31">
              <asp:DropDownList ID="DropDownList9" runat="server" CssClass="c19022812"
                   AutoPostBack ="true"  OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged">
                   <asp:ListItem  Value ="asc" >升序</asp:ListItem>
       <asp:ListItem  Selected="True"  Value ="desc">降序</asp:ListItem>
                    </asp:DropDownList></div>
         
    </div>

                <div >
         
               <asp:GridView ID="GridView1" runat="server"
                    AllowPaging="True" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowdeleting="GridView1_RowDeleting" 
                    AllowSorting="True"   
                    onrowdatabound="GridView1_RowDataBound" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" 
                        AutoGenerateColumns="False" PageSize="15" 
                   
                         CssClass ="c13102001" BorderStyle="None" CellPadding="0" GridLines ="None"
                   
                   >
                   
                    <HeaderStyle Height="30px" />
                   
                    <RowStyle BackColor="#ffffff"  CssClass ="c19011720"   ForeColor="#333333"  />
                    <Columns >

             
                          <asp:BoundField DataField="序号" HeaderText="序号"  >
                              <ItemStyle  ForeColor="#595d5a"  HorizontalAlign ="Left"  />
                                    <HeaderStyle  HorizontalAlign ="Left" />
                          </asp:BoundField>
   
                  <asp:TemplateField HeaderText="表格名称" HeaderStyle-HorizontalAlign="Left" >
                            <ItemTemplate >
                              <div style ="height:10px;"></div>
                               
                          <a   onclick ="f12100302('<%#Eval ("utid") %>','<%#Eval ("utable_name") %>')" 
                      class="c19011711" ><%#Eval ("utable_name") %></a><br />
                         
                                <div style ="height:10px;"></div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="录入用户" HeaderStyle-HorizontalAlign="Left" >
                            <ItemTemplate >
                              <div style ="height:10px;"></div>
                             <%#Eval ("用户名") %>
                                <div style ="height:10px;"></div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="创建时间" HeaderStyle-HorizontalAlign="Left" >
                            <ItemTemplate >
                              <div style ="height:10px;"></div>
                             <%#Eval ("创建时间") %>
                                <div style ="height:10px;"></div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                  
             <asp:TemplateField HeaderText="操作"  HeaderStyle-Width="110px" >
                <ItemTemplate >
                    <div style="width :107px;height:30px;float:left;">
                         <div  id="select" onmouseover="over(this,'edit_top')" onmouseout="out(this,'edit_top')"    
                         class ="c19021902" 
                              onclick="f12100302('<%#Eval ("utid") %>','<%#Eval ("utable_name") %>')">选择</div>
                     
                        </div>
                     </ItemTemplate>
                 <HeaderStyle  HorizontalAlign ="Left" />
                 <ItemStyle   ForeColor="#595d5a"/>
            </asp:TemplateField> 
                        <asp:TemplateField>
                          <ItemTemplate>
                               <asp:Label ID="L1" Text ='<%#Eval ("id") %>' runat ="server"   Visible ="false" ></asp:Label>
                          </ItemTemplate>
                             
                        </asp:TemplateField>
                     
                    </Columns>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
                </div> 
          <div id="i14031701" class ="c13102303">
          <span class="c13102304"><asp:Label ID="lblRecordCount" runat="server"></asp:Label></span>
          <span class="c13102304"><asp:Label ID="lblPageCount" runat="server"></asp:Label></span>
          <span class="c13102304"><asp:Label ID="lblCurrentIndex" runat="server"></asp:Label></span>
          <span class="c13102304"><asp:LinkButton ID="btnFirst" runat="server" CommandArgument="First" onclick="PageButton_Click">首页</asp:LinkButton></span>
          <span class="c13102304"><asp:LinkButton ID="btnPrev" runat="server" CommandArgument="Prev" onclick="PageButton_Click">上一页</asp:LinkButton></span>  
          <span class="c13102304"><asp:LinkButton ID="btnNext" runat="server" CommandArgument="Next" onclick="PageButton_Click">下一页</asp:LinkButton></span>
          <span class="c13102304"><asp:LinkButton ID="btnLast" runat="server" CommandArgument="Last" onclick="PageButton_Click">尾页</asp:LinkButton></span>
          <span class="c13102304"> 转到<asp:TextBox ID="txtNum" runat="server"  Width="73px"></asp:TextBox></span>
          <span class="c13102304"> 页</span>
          <span class="c13102304"> <asp:Button ID="btngo" runat="server"  Text="GO！"   style="width:45px" onclick="btngo_Click" />&nbsp;</span>每页显示：
     <asp:DropDownList  ID="DropDownList5" runat="server"     AutoPostBack="true" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged"   >
      <asp:ListItem>10</asp:ListItem>
              <asp:ListItem >30</asp:ListItem>
               <asp:ListItem >50</asp:ListItem>
                  <asp:ListItem>全部</asp:ListItem>
            </asp:DropDownList> 条记录
               
</div>
        </asp:Panel>        
<script type="text/javascript" >

    var come = document.getElementById("come").value;

    var parent_nodeid = document.getElementById("parent_nodeid").value;
    var nodeid = document.getElementById("nodeid").value;
    function f19012702(obj,obj1) {
        window.open("/producttrace/utablet.aspx?id="+obj1+"&parent_nodeid=" + parent_nodeid + "&nodeid=" + nodeid, target = '_self');
    }
    function f12100302(obj, obj1) {
        var arr1 = new Array();
        arr1[0] = obj;//paid
        arr1[1] = obj1;//parameter_name
        if (navigator.userAgent.indexOf("Chrome") > 0) {
            var table = document.getElementById('<%=GridView1.ClientID%>');
            var index = document.getElementById("index").value;
            var count = document.getElementById("count").value;/*来自订单的总项次*/
            var tr = table.getElementsByTagName("tr");
          
            for (i = 1; i <= count; i++) {
                //alert("index=" + index + ",i=" + i+",count="+tr.length);
                if (index == i) {
                    if (i == 1) {
                        window.opener.document.getElementById("GridView1_ctl02_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl02_Text1").value = arr1[1];
                        break;
                        /*Text1 产品名称 Text2 颜色 Text5 尺寸 Text6 材质*/
                    }
                    else if (i == 2) {
                        window.opener.document.getElementById("GridView1_ctl03_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl03_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 3) {
                        window.opener.document.getElementById("GridView1_ctl04_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl04_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 4) {
                        window.opener.document.getElementById("GridView1_ctl05_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl05_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 5) {
                        window.opener.document.getElementById("GridView1_ctl06_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl06_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 6) {
                        window.opener.document.getElementById("GridView1_ctl07_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl07_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 7) {
                        window.opener.document.getElementById("GridView1_ctl08_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl08_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 8) {
                        window.opener.document.getElementById("GridView1_ctl09_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl09_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 9) {
                        window.opener.document.getElementById("GridView1_ctl10_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl10_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 10) {
                        window.opener.document.getElementById("GridView1_ctl11_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl11_Text1").value = arr1[1];
                        break;
                    }
                   else if (i == 11) {
                        window.opener.document.getElementById("GridView1_ctl12_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl12_Text1").value = arr1[1];
                        break;
                        /*Text1 产品名称 Text2 颜色 Text5 尺寸 Text6 材质*/
                    }
                    else if (i == 12) {
                        window.opener.document.getElementById("GridView1_ctl13_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl13_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 13) {
                        window.opener.document.getElementById("GridView1_ctl14_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl14_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 14) {
                        window.opener.document.getElementById("GridView1_ctl15_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl15_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 15) {
                        window.opener.document.getElementById("GridView1_ctl16_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl16_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 16) {
                        window.opener.document.getElementById("GridView1_ctl17_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl17_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 17) {
                        window.opener.document.getElementById("GridView1_ctl18_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl18_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 18) {
                        window.opener.document.getElementById("GridView1_ctl19_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl19_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 19) {
                        window.opener.document.getElementById("GridView1_ctl20_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl20_Text1").value = arr1[1];
                        break;
                    }
                    else if (i == 20) {
                        window.opener.document.getElementById("GridView1_ctl21_Text29").value = arr1[0];
                        window.opener.document.getElementById("GridView1_ctl21_Text1").value = arr1[1];
                        break;
                    }
                }
            }
            window.close();
        }
        else {
            if (window.opener = undefined) {
                //for chrome
                window.opener.returnValue = arr1;
            }
            else {
                window.returnValue = arr1;
            }
            window.close();
        }
    }
    window.onload = function onload1() {


    }
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
            document.getElementById("0_ContentPlaceHolder1_i1").src = "/image/6.png";
        }
        else if (obj1 == "i2") {
            obj.className = "c19022006";
            document.getElementById("0_ContentPlaceHolder1_i2").src = "/image/8.png";
        }
        else if (obj1 == "i3") {
            obj.className = "c19022006";
            document.getElementById("0_ContentPlaceHolder1_i3").src = "/image/x3.png";
        }
        else if (obj1 == "i3_g") {
            obj.className = "c19022006";
            document.getElementById("i3_g").src = "/image/x3.png";
        }
        else if (obj1 == "i4") {
            obj.className = "c19022006";

            document.getElementById("0_ContentPlaceHolder1_i4").src = "/image/2.png";
        }
        else if (obj1 == "i5") {
            obj.className = "c19022006";
            document.getElementById("0_ContentPlaceHolder1_i5").src = "/image/2.png";
        }
        else if (obj1 == "i6") {
            obj.className = "c19022006";
            document.getElementById("0_ContentPlaceHolder1_i6").src = "/image/x6.png";
        }
        else if (obj1 == "i7") {
            obj.className = "c19022006";
            document.getElementById("0_ContentPlaceHolder1_i7").src = "/image/4.png";
        }
        else if (obj1 == "i8") {
            obj.className = "c19022006";
            document.getElementById("0_ContentPlaceHolder1_i8").src = "/image/2.png";
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
            document.getElementById("0_ContentPlaceHolder1_i1").src = "/image/5.png";
        }
        else if (obj1 == "i2") {
            obj.className = "c19022005";
            document.getElementById("0_ContentPlaceHolder1_i2").src = "/image/7.png";
        }
        else if (obj1 == "i3") {
            obj.className = "c19022005";
            document.getElementById("0_ContentPlaceHolder1_i3").src = "/image/x2.png";
        }
        else if (obj1 == "i3_g") {
            obj.className = "c19022005";
            document.getElementById("i3_g").src = "/image/x2.png";
        }
        else if (obj1 == "i4") {
            obj.className = "c19022005";
            document.getElementById("0_ContentPlaceHolder1_i4").src = "/image/1.png";
        }
        else if (obj1 == "i5") {
            obj.className = "c19022005";
            document.getElementById("0_ContentPlaceHolder1_i5").src = "/image/1.png";
        }
        else if (obj1 == "i6") {
            obj.className = "c19022005";
            document.getElementById("0_ContentPlaceHolder1_i6").src = "/image/x5.png";
        }
        else if (obj1 == "i7") {
            obj.className = "c19022005";
            document.getElementById("0_ContentPlaceHolder1_i7").src = "/image/3.png";
        }
        else if (obj1 == "i8") {
            obj.className = "c19022005";
            document.getElementById("0_ContentPlaceHolder1_i8").src = "/image/1.png";
        }

        //alert("obj.id=" + obj.value + "," + obj1 + "," + obj.className + ",obj2=" + obj2 + ",obj2 classname=" + obj2.className);
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

    function enter2tab(e) {
        if (window.event.keyCode == 13) window.event.keyCode = 9
    }
    document.onkeydown = enter2tab;

</script>
        
    </form>
</body>
</html>

