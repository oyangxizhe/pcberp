<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WPSS.Main" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>进销存管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
<meta name ="Description" content ="进销存管理系统" />
<meta name ="keywords" content =<"B/S架构进销存管理系统","进销存管理系统","进销存管理软件" />
 
    <link rel="Stylesheet" href="Css/view.css" type ="text/css" />
</head>
<body  class ="c131010701"  onload ="onload1()">
<form id="Form1" runat ="server"  >
    <input id="usido" type="hidden"  runat="server" />
  <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> 
    <Services> 
        <asp:ServiceReference Path="WebService1.asmx"  /> 
    </Services> 
    </asp:ScriptManager>  
<div  class="c13042401">
             <div class ="c13051001">
      <div class="c13052503" id ="Div2">
   <span class="c13052504" id ="Span1"><img src ="Image/logo_index.png" alt ="" /></span>
       </div>
                 
       <div class="c13052906" id ="Div1">
             <asp:Panel ID="p1" runat="server"><span class="c13052504" id ="Span2"><img src ="Image/logo euj2.jpg" alt ="" /></span></asp:Panel>
       </div>
          <div class="c13052908" id ="Div3">
<img id="i13052801"  alt=""  style =" display :none ; float :left ; margin-top :16px; color :Blue "/>
              <span style =" margin-right :10px;"><asp:Label ID="Label1" runat="server" ></asp:Label></span>
         <span style =" margin-right  :10px;"><asp:Label ID="L3" runat="server" ></asp:Label></span>
                 </div>
   
 
    </div> 
  <div id="13102901" class="c13102901" />
      <div class="c13051201" id="i13053105">
          <span class="c13051005" >  
            
              <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton1_Click" CssClass ="c19042601"  >退出</asp:LinkButton>
          </span>
          <span class="c13051005" >  
          <asp:Label ID="Label2" runat="server" Text="Label" CssClass="c18102801"></asp:Label>
              
          </span>
          <span class="c13051005" >  
          <asp:Label ID="Label3" runat="server" Text="Label" CssClass="c18102801"></asp:Label>
          <asp:DataList ID="DataList1" runat="server" RepeatColumns="1"  width="100%" Height ="100%">
                　<ItemTemplate >

<div id='<%#Eval ("TEMP") %>' class ="c15060804" onclick ="f15060702('<%#Eval ("nodeid")%>')">
    <span  class ="c15060805"><%#Eval ("NODE_NAME") %></span></div>         
</ItemTemplate> 　
</asp:DataList>
          </span>
       </div>
        <div class="c13051202" id ="i13053106">
<iframe   id="wk" name="ContentP" frameborder="0"  
        style="height: 100%; width :100% "  ></iframe>
    </div>
      </div>

  </div>
   <div style ="height:300px;width:100%"></div>
      <script type ="text/javascript" >
          var arr1 = new Array();
          window.onload = function onload1() {
              var Invocation = document.getElementById("wk");
              Invocation.target = "ContentP";
              Invocation.src = "view.aspx?&parent_nodeid=1&usid=" + document.getElementById("usido").value + "";

              document.getElementById(11).className = "c15060806";
              var usidt = document.getElementById("usido").value;
              WPSS.WebService1.GET_PARENT_NODEID(usidt, onSuccess, onFail);

          }

          function onSuccess(value, context) {
              arr1 = value;

          }
          function onFail(value) {
              alert(value);
          }
          function f15060702(obj) {
              for (var i = 0; i < arr1.length; i++) {

                  if (obj == arr1[i]) {

                      document.getElementById(arr1[i] + "1").className = "c15060806";
                  }
                  else {


                      document.getElementById(arr1[i] + "1").className = "c15060804";
                  }

              }
              var Invocation = document.getElementById("wk");
              Invocation.target = "ContentP";
              Invocation.src = "view.aspx?usid=" + document.getElementById("usido").value + "&parent_nodeid=" + obj + "";
          }
      </script>


          </form> 
</body>

</html>