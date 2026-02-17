<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stept.aspx.cs" Inherits="WPSS.ProductTrace.stept" %>

   <DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑参数信息</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
<meta name ="Description" content ="进销存管理系统" />
<meta name ="keywords" content ="进销存管理系统,进销存管理软件,ERP,小微企业管理系统,希哲软件" />
      
       <link href ="/Css/S190320.css"  type ="text/css" rel ="Stylesheet" />
    </head>
<body >
   <form id="form1" runat="server">
       <asp:Panel ID="Panel1" runat="server" width="1100px"  >
          <input id="hint" type="hidden"  runat="server" />
          <input id="usid" type="hidden"  runat="server" />
        <input id="right" type="hidden"  runat="server" />
               <input id="id" type="hidden"  runat="server" />
          <input id="newfilename" type="hidden"  runat="server" />
          <input id="if_delete" type="hidden"  runat="server" />
          <input id="mdate" type="hidden"  runat="server" />
           <input id="last_value" type="hidden"  runat="server" />
           <input id="big_path" type="hidden"  runat="server" />
          <input id="initial_path" type="hidden"  runat="server" />
          <input id="file_path" type="hidden"  runat="server" />
            <input id="select" type="hidden"  runat="server" />
          <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> 
    <Services> 
        <asp:ServiceReference Path="../Detail/WebService1.asmx"  /> 
    </Services> 
    </asp:ScriptManager>
             <asp:UpdatePanel ID="UpdatePanel2" 
                 UpdateMode="Conditional"
                
                 runat="server">
    <ContentTemplate>
                <div class ="c13101905">
      <div class="c13101906" id ="Div9">
          &gt;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </div>
     <div class="c13101907" id ="Div10">
 </div>
    </div>
                    <div class ="c19030701">
         
          <input id="Submit2s" type="submit" value="上一页"  runat ="server"  onmouseover="over(this,'Submit2s')"
               onmouseout="out(this,'Submit2s')" class ="c19012703"   onserverclick="submit1_Click" />

           <input id="Submit31" type="submit" value="返回工序管理"  runat ="server"  onmouseover="over(this,'Submit2s_left')"
               onmouseout="out(this,'Submit2s_left')" class ="c19021302"   onserverclick="submit1_Click" />
                        &nbsp;&nbsp;
    </div>
        
                 
               
          
     <input id="prompt"  type="submit" value="提示窗"  runat ="server"   visible ="false"   class ="c19011410"   onserverclick="submit1_Click" />
 
         
                         <div class ="c19021803">
         <div class="c19011402" id ="Div22"> 
               基本信息
                             </div> 
                 <div class="c19011402" id ="Div56"> 
                             </div>
           </div>
  <div class ="c19010701">
         <div class="c19022102" id ="Div5"> 
               工序名称：</div> 
     <div class="c19022101" id ="Div6">
    <input id="Text1" type="text"  runat ="server" autocomplete="off" class ="c19022103"  /> 
         </div>
                 
           </div>
          
  
       <div class ="c19021803">
         <div class="c19011402" id ="Div63">
               表格信息：</div> 
     <div id="i19022210"  class="c19022003" onmouseover="over(this,'i4')"  onmouseout="out(this,'i4')" onclick ="f13100203(this)"> 
                                          <span class="c19022004" id ="Span4"> 

                                              <img id="i4" src="/image/xico/1.png " runat="server"  class="c19041310"/>
                             </span> 添加表格
                             </div>
           </div>
         
               <asp:GridView ID="GridView1" runat="server"
                    AllowPaging="False" 
             
                 
                    AllowSorting="False"   
                
                      
                        AutoGenerateColumns="False" PageSize="15" 
                   ShowHeader="false" 
                         CssClass ="c19022202" BorderStyle="None" CellPadding="0" GridLines ="None" BackColor="#9c9c9c" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
                    
                   >
                   
                    <HeaderStyle Height="21px" />
                   
                    <RowStyle BackColor="#cccccc"  CssClass ="c19011720"   ForeColor="#333333" Height="21px"  />
                    <Columns >


                        <asp:TemplateField HeaderText="基本信息1" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px" >
                            <ItemTemplate  >
                            
               <div style ="width:340px; Height:32px  ">
                   <div style ="float :left ;width :70px ;line-height:30px;height:30px;">表格名称：</div>
                   <div style ="float :left;width :250px;" >
                      
                        <input id="Text1" type="Text"  value='<%#Eval("utable_name") %>' runat ="server" autocomplete="off" class="c19022320" />
                   </div>
               </div>
           
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="基本信息2" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px" >
                            <ItemTemplate >
                          <div style ="height :32px;width:140px">


                         
                        
<div id="i19022210"  class="c19041301" onclick ="f13100204(this,'<%#Eval ("sn") %>')" onmouseover="over(this,'i5')"    onmouseout="out(this,'i5')" > 
                                          <span class="c19041304" id ="Span4"> 

                                              <img id="i5" src="/image/xico/x2.png " runat="server"   alt="" class="c19041302"/>
                             </span>选择
                             </div>

<div id="Div1"  class="c19041305" onclick ="f13100203(this)"  > 
                                          <span class="c19041304" id ="Span1"> 

                                              <img id="i6" src="/image/xico/x5.png " runat="server"  alt="" class="c19041302"/>
                                          </span>
                <span class="c19041306" id ="Span2"> 

                                                 <asp:LinkButton ID="LinkButton1" CommandName="Select"  CssClass="c19041307"   runat="server">删除</asp:LinkButton>
                             </span>
     
                             </div>
                              <input id="Text29" type="Text"  value='<%#Eval("utid") %>' runat ="server" autocomplete="off" class="c19022701" /> 
               </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                          <ItemTemplate>
                               <asp:Label ID="L1" Text ='<%#Eval ("sn") %>' runat ="server"    Visible ="False" ></asp:Label>
                          </ItemTemplate>
                             
                        </asp:TemplateField>
                     
                    </Columns>
                    <AlternatingRowStyle BackColor="#cccccc" ForeColor="#284775" Font-Bold="False" />   
                </asp:GridView>
            <div class ="c19010703">
           <input id="Submit1" type="submit" value="提交"  runat ="server"  
               onmouseover="over(this)" onmouseout="out(this)" class ="c19010506"   onserverclick="submit1_Click" />
         
         
    </div>
         <input id="prompt1"  type="submit" value="提示窗"  runat ="server"   
         visible ="false"   class ="c19011410"   onserverclick="submit1_Click" />

   
          <div  style="visibility:hidden">
             
      
             <input id="Submit2" type="submit" value="添加"  runat ="server"  
               onmouseover="over(this)" onmouseout="out(this)" class ="c19010506"   
                   visible="true"  onserverclick="submit1_Click" />
                <input id="Submit4" type="submit" value="添加附加费用"  runat ="server"  
               onmouseover="over(this)" onmouseout="out(this)" class ="c19010506"   
                   visible="true"  onserverclick="submit1_Click" />
                         <input id="Submit20" type="submit" value="提示修改附加成本成功"  runat ="server"   onmouseover="over(this,'2s1')" onmouseout="out(this,'2s1')"   class ="c19011801" 
                           onclick='<%#Eval("修改附加成本")%>'
                 onserverclick="submit1_Click" /> 
                    <input id="Submit22" type="submit" value="提示修改发票成功"  runat ="server"   onmouseover="over(this,'2s1')" onmouseout="out(this,'2s1')"   class ="c19011801" 
                         
                 onserverclick="submit1_Click" /> 
             </div>
                  
   

    
 
       </ContentTemplate>
                    <Triggers >
                        <asp:PostBackTrigger ControlID="Submit1" />
                     <asp:PostBackTrigger ControlID="Submit2s" />
                    </Triggers>
</asp:UpdatePanel>
      <script type ="text/javascript" >
      
             
          function f13100203(obj) {
              if (obj.id == "i19022210") {
                  document.getElementById("Submit2").click();//添加产品
              }
              else if (obj.id == "i19041501") {
                  document.getElementById("Submit4").click();//添加上传档案文件名
              }
          }
        
          function enter2tab(e) {
              if (window.event.keyCode == 13) window.event.keyCode = 9
          }
          document.onkeydown = enter2tab;
          function f13100204(obj, obj1) {
              var dlgResult;
              /*取得参数资料*/
          
              if (navigator.userAgent.indexOf("Chrome") > 0) {
                  var table = document.getElementById('<%=GridView1.ClientID%>');

                  var tr = table.getElementsByTagName("tr");

                  var url = "/producttrace/utable2.aspx?come=xtable&index=" + obj1 + "&count=" + tr.length;
                  var dlgResult = myShowModalDialog(url, window, 1000, 500);
              }
              else {

                  var url = "/producttrace/utable2.aspx";
                  var dlgResult = myShowModalDialog(url, window, 1000, 500);
                  if (dlgResult!= null) {

                      var table = document.getElementById('<%=GridView1.ClientID%>');

                          var tr = table.getElementsByTagName("tr");

                          for (i = 1; i <= tr.length; i++) {

                              if (obj1 == i) {

                             
                                  if (i == 1) {
                                      document.getElementById("GridView1_ctl02_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl02_Text1").value = dlgResult[1];
                                      break;
                                    
                                  }
                                  else if (i == 2) {
                                      document.getElementById("GridView1_ctl03_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl03_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 3) {
                                      document.getElementById("GridView1_ctl04_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl04_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 4) {
                                      document.getElementById("GridView1_ctl05_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl05_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 5) {
                                      document.getElementById("GridView1_ctl06_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl06_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 6) {
                                      document.getElementById("GridView1_ctl07_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl07_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 7) {
                                      document.getElementById("GridView1_ctl08_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl08_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 8) {
                                      document.getElementById("GridView1_ctl09_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl09_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 9) {
                                      document.getElementById("GridView1_ctl10_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl10_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 10) {
                                      document.getElementById("GridView1_ctl11_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl11_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 11) {
                                      document.getElementById("GridView1_ctl12_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl12_Text1").value = dlgResult[1];
                                      break;
                                      /*Text1 产品名称 Text2 颜色 Text5 尺寸 Text6 材质*/
                                  }
                                  else if (i == 12) {
                                      document.getElementById("GridView1_ctl13_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl13_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 13) {
                                      document.getElementById("GridView1_ctl14_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl14_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 14) {
                                      document.getElementById("GridView1_ctl15_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl15_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 15) {
                                      document.getElementById("GridView1_ctl16_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl16_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 16) {
                                      document.getElementById("GridView1_ctl17_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl17_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 17) {
                                      document.getElementById("GridView1_ctl18_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl18_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 18) {
                                      document.getElementById("GridView1_ctl19_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl19_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 19) {
                                      document.getElementById("GridView1_ctl20_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl20_Text1").value = dlgResult[1];
                                      break;
                                  }
                                  else if (i == 20) {
                                      document.getElementById("GridView1_ctl21_Text29").value = dlgResult[0];
                                      document.getElementById("GridView1_ctl21_Text1").value = dlgResult[1];
                                      break;
                                  }
                              }
                          }
                      }


                  }

              }

function over(obj) {
    if (obj.value == "选择") {
        obj.className = "c19012512";
    }
    else {
        obj.className = "c19010507";
    }
}
function out(obj) {

    if (obj.value == "选择") {
        obj.className = "c19012511";
    }
    else {
        obj.className = "c19010506";
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
        document.getElementById("i1").src = "/image/xico/6.png";
    }
    else if (obj1 == "i2") {
        obj.className = "c19022006";
        document.getElementById("i2").src = "/image/xico/8.png";
    }
    else if (obj1 == "i3") {
        obj.className = "c19022006";
        document.getElementById("i3").src = "/image/xico/x3.png";
    }
    else if (obj1 == "i3_g") {
        obj.className = "c19022006";
        document.getElementById("GridView1_ctl02_i3_g").src = "/image/xico/x3.png";
    }
    else if (obj1 == "i4") {
        obj.className = "c19022006";

        document.getElementById("i4").src = "/image/xico/2.png";
    }
    else if (obj1 == "i5") {
        obj.className = "c19041311";
        //document.getElementById("i5").src = "/image/xico/2.png";
    }
    else if (obj1 == "i6") {
       
        obj.className = "c19041312";
        //document.getElementById("i6").src = "/image/xico/x6.png";
    }
    else if (obj1 == "i7") {
        obj.className = "c19022006";
        document.getElementById("i7").src = "/image/xico/4.png";
    }
    else if (obj1 == "i8") {
        obj.className = "c19022006";
        document.getElementById("i8").src = "/image/xico/2.png";
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
        document.getElementById("i1").src = "/image/xico/5.png";
    }
    else if (obj1 == "i2") {
        obj.className = "c19022005";
        document.getElementById("i2").src = "/image/xico/7.png";
    }
    else if (obj1 == "i3") {
        obj.className = "c19022005";
        document.getElementById("i3").src = "/image/xico/x2.png";
    }
    else if (obj1 == "i3_g") {
        obj.className = "c19022005";
        document.getElementById("GridView1_ctl02_i3_g").src = "/image/xico/x2.png";
    }
    else if (obj1 == "i4") {
        obj.className = "c19022005";
        document.getElementById("i4").src = "/image/xico/1.png";
    }
    else if (obj1 == "i5") {
        obj.className = "c19041301";
        //document.getElementById("i5").src = "/image/xico/1.png";
    }
    else if (obj1 == "i6") {
        obj.className = "c19041305";
        //document.getElementById("i6").src = "/image/xico/x5.png";
    }
    else if (obj1 == "i7") {
        obj.className = "c19022005";
        document.getElementById("i7").src = "/image/xico/3.png";
    }
    else if (obj1 == "i8") {
        obj.className = "c19022005";
        document.getElementById("i8").src = "/image/xico/1.png";
    }

    //alert("obj.id=" + obj.value + "," + obj1 + "," + obj.className + ",obj2=" + obj2 + ",obj2 classname=" + obj2.className);
}

        </script>
       
       </asp:Panel>
    </form>
</body>
</html>

