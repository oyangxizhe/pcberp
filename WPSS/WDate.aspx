<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WDate.aspx.cs" Inherits="WPSS.WDate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择日期</title>
    <base target ="_self" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
     <input id="hint" type="hidden"  runat="server" />
        <input id="come" type="hidden"  runat="server" />
    </div>
    <asp:Calendar ID="Calendar1" runat="server" 
        onselectionchanged="Calendar1_SelectionChanged" Height="230px" 
        Width="240px"></asp:Calendar>
  <script type="text/javascript" language="javascript">
      window.onload = function onload1() {
          var arr1 = new Array();
          var Invocation = document.getElementById("hint").value;
          arr1[0] = Invocation;

          if (navigator.userAgent.indexOf("Chrome") > 0) {
              if (Invocation != "") {

                  if (document.getElementById("come").value == "0") {
               
                      window.opener.document.getElementById("StartDate").value = Invocation;
                   
                   
                  }
                  else if (document.getElementById("come").value == "1") {
                      window.opener.document.getElementById("EndDate").value = Invocation;

                  }
                  else if (document.getElementById("come").value == "2") {
                  
                      window.opener.document.getElementById("StartDate").value = Invocation.substring(0,7)

                  }
                  var userAgent = navigator.userAgent;
                  if (userAgent.indexOf("Firefox") != -1 || userAgent.indexOf("Chrome") != -1) {
                      location.href = "about:blank";
                  } else {
                      window.opener = null;
                      window.open('', '_self');
                  }
              
                  window.close();

              }

          }
          else {

              if (Invocation != "") {
                  window.returnValue = Invocation;
                  window.close();

              }

          }
      }

</script>
    </form>
</body>
</html>
