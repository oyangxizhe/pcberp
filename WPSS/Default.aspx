<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WPSS._Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 5.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>希哲管理软件V1.0</title>
 <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
<meta name ="Description" content ="希哲管理软件V1.0" />
<meta name ="keywords" content ="希哲管理软件V1.0,希哲管理软件V1.0软件,ERP,小微企业管理系统,希哲软件" />
  <link href ="Css/SSBase.css"  type ="text/css" rel ="Stylesheet" />
  <link href ="Css/S190320.css"  type ="text/css" rel ="Stylesheet" />
</head>
<body style="border-top:0px;margin-top:0px;">
    <form id="form1" runat="server">
    <input id="hint" type="hidden"  runat="server" />
         <input id="width" type="hidden"  runat="server" />
         <input id="height" type="hidden"  runat="server" />   
        <div class="Default_top">
            <span>登陆ERP MS</span></div>
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> 
    <Services> 
        <asp:ServiceReference Path="../Detail/WebService1.asmx"  /> 
    </Services> 
    </asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" 
                 UpdateMode="Conditional"
                 runat="server">
    <ContentTemplate>
     
        <div style="height:360px;margin-top:40px; margin-bottom:0px;">
            <div style="width:45%; float:left; border-right:solid 1px #ccc; height:100%; text-align:right; padding-right:30px;">
                <br />
                <br />
                <img id="Img1" src="Image/logo_index.png" runat="server" alt =""/><br />
          
                <div style ="display :none "><img src="image/logo.jpg" alt ="" /></div>
                      <div style=" color :#990033 "> 
                          <asp:Panel ID="p1" runat ="server" ><div></div></asp:Panel>

                      </div>
                <div style="height:36px; line-height:36px; display:none ;"><a href="http://www.oyxxi.com" target="_blank">帮助</a></div>
            </div>
            <div style=" width:49%; float:right; height: 261px;">
                
             
      
      
        <div class ="c18102501">
     <div class="c18102503" id ="Div6">
            </div>
           </div>
                <div class ="c18102510">
     <div class="c18102511" id ="Div3">
   <asp:Label ID="prompt" runat="server" ForeColor="Red"  CssClass ="c18102514"  Visible ="True"  ></asp:Label></div>
           </div>
                <div class ="c18102513">
     <div class="c18103001" id ="Div9">
   <input id="Text1" type="text"  runat ="server"   placeholder="请输入帐号"  class ="c18102505" /> </div>
           </div>
              
                <div class ="c18102501">
     <div class="c18103001" id ="Div13">
   <input id="Text2"  runat ="server" type="password" placeholder="输入密码"  class ="c18102505" /></div>
           </div>
                         
                <div class ="c18102501">
     <div class="c18103001" id ="Div15">
   <input id="Submit1" type="submit" value="登录"  runat ="server"   onmouseover="over(this)" onmouseout="out(this)" class ="c19010508"   onserverclick="submit1_Click" />
 
                    </div>
           </div>   
                                                  
            </div>
            &nbsp;</div>
      <div class="c19032801" id ="Div1">XizheMS Administration Designed for IE Explorer </div>
         <div class="c1903280101" id ="Div2">软件支持 苏州好用软件有限公司</div>
        <div class ="c1903280101">
                       <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                               </div> 
     
          </ContentTemplate>
        </asp:UpdatePanel>

              <script type ="text/javascript" >

                  function keyin(obj) {

                      if (obj = "用户名登录") {
                          document.getElementById("Text1").placeholder = "请输入手机号";
                          document.getElementById("Text2").placeholder = "请输入验证码";
                          document.getElementById("Text2").type = "text";
                      }
                      else {
                          document.getElementById("Text1").placeholder = "请输入用户名";
                          document.getElementById("Text2").placeholder = "请输入密码";
                          document.getElementById("Text2").type = "password";
                      }
                  }
                  function enter2tab(e) {
                      if (window.event.keyCode == 13) document.getElementById("<%= this.Submit1.ClientID %>").click();

                  }
                  document.onkeydown = enter2tab;

                  window.onload = function onlaod1() {


                      document.getElementById("Text1").focus();


                  }
                  function over(obj) {

                      obj.className = "c19010509";

                  }
                  function out(obj) {

                      obj.className = "c19010508";

                  }
              </script>
           

    </form>
</body>
</html>
