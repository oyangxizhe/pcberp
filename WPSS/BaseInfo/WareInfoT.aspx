<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WareInfoT.aspx.cs" Inherits="WPSS.BaseInfo.WareInfoT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑品号信息</title>
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
    <input id="x2" type="hidden"  runat="server" />
        <input id="x3" type="hidden"  runat="server" />
         
                <div class ="c13101905">
      <div class="c13101906" id ="Div911">
>编辑品号信息</div>
     <div class="c13101907" id ="Div111">
 </div>
    </div>
       <div class="c13110501">
           <div id="Div923" class="c13110502">
               <input id="Submit1" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="添加品号" />
           </div>
           <div id="Div130" class="c13110510">
               <span id="Span4" class="c13110511"></span>
           </div>
           <div id="Div16" class="c13110502">
               <input id="Submit2" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="保存" />
           </div>
           <div id="Div17" class="c13110510">
               <span id="Span5" class="c13110511"></span>
           </div>
           <div id="Div18" class="c13110507">
               <input id="Submit3" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="退出" />
           </div>
           <div id="Div19" class="c13110510">
               <span id="Span6" class="c13110511"></span>
           </div>
           <div id="Div9" class="c13110510">
               <input id="Submit4" runat="server" class="c19012703" onmouseout="out(this,'Submit2s')" onmouseover="over(this,'Submit2s')" onserverclick="submit1_Click" type="submit" value="复制" />
           </div>
       </div>

<div  id="i13102301" class ="c13102101">
<span  class ="c13102102"><asp:Label ID="prompt" runat="server"  ForeColor="#f80707"></asp:Label></span>
</div> 
  <div class ="c13101902">
      <div class="c13122302" id ="Div2">
   ID</div>
     <div class="c14031403" id ="Div4">
<input id="Text1" type="text"  runat="server"   readonly ="readonly" class="c14031401"/> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Text1" Text="必填！" runat="server" /></div>
         <div class="c13122302" id ="Div5">
              <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
      </div>
     <div class="c14031404" id ="Div6">
   <input id="Text2" type="text"  runat ="server"  class ="c14031401" />
   </div>
                <div class="c13122302" id ="Div1">
                   &nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
      </div>
     <div class="c14031403" id ="Div3">
     <input id="Text3" type="text"  runat ="server"  class="c14033101"  />
 
    </div>      <div class="c13122302" id ="Div7">
      &nbsp;<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
   </div>
     <div class="c14120501" id ="Div8">
     <input id="Text4" type="text"  runat ="server" class="c14031401" />
 </div>
           </div>
             <div class ="c13101902">

          <div class="c13122302" id ="Div12">
                 层数</div>
<div class="c14031403" id ="Div13">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList1" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="DropDownList1" Text="必填" runat="server" /></span>
         </div>
                  <div class="c13122302" id ="Div11" >
                      客户</div>
     <div class="c14031403" id ="Div14">
   <input id="Text5" type="text"  runat ="server" class ="c14031405"  /> 
      <span style =" margin-left :5px"><a  href="javascript:f13100202('Text5','');">选择 </a></span> 
         </div>
               <div class="c13122302" id ="Div24">
   Line A PN</div>
     <div class="c14031403" id ="Div25">
   <input id="Text6" type="text"  runat ="server" class="c14031401" /> </div>
          <div class="c13122302" id ="Div26">
                 Line B PN</div>
     <div class="c14120501" id ="Div27">
   <input id="Text7" type="text"  runat ="server" class="c14031401" /> 
         </div>
           </div>
           <div class ="c13101902">

                  <div class="c13122302" id ="Div28" >
             Line C PN</div>
     <div class="c14031403" id ="Div29">
   <input id="Text8" type="text"  runat ="server" class ="c14031401"  /> 
         </div>
               <div class="c13122302" id ="Div30">
   Line D PN</div>
     <div class="c14031403" id ="Div31">
   <input id="Text9" type="text"  runat ="server" class="c14031401" /> </div>
          <div class="c13122302" id ="Div32">
                板子类型</div>
     <div class="c14031403" id ="Div33">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList2" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="DropDownList2" Text="必填" runat="server" /></span>
         </div>
                  <div class="c13122302" id ="Div34" >
             板厚</div>
     <div  class="c14120501" id ="Div35">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList3" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="DropDownList3" Text="必填" runat="server" /></span>
         </div>
           </div>
           
           <div class ="c13101902">
      <div class="c13122302" id ="Div36">
   板厚公差</div>
 <div class="c14031403" id ="Div37">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList4" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="DropDownList4" Text="必填" runat="server" /></span>
         </div>
          <div class="c13122302" id ="Div38">
                 板材</div>
     <div class="c14031404" id ="Div39">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList5" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="DropDownList5" Text="必填" runat="server" /></span>
         </div>
                  <div class="c13122302" id ="Div40" >
                       Pcs 长</div>
     <div class="c14031403" id ="Div41">
 <input id="Text10" type="text"  runat ="server" class ="c14031401"  />(mm)
         </div>
               <div class="c13122302" id ="Div42">
         Pcs 宽</div>
     <div  class="c14120501" id ="Div43">
 <input id="Text11" type="text"  runat ="server" class ="c14031401"  />(mm) </div>
           </div>
           
           <div class ="c13101902">

          <div class="c13122302" id ="Div44">
              Set 长</div>
     <div class="c14031404" id ="Div45">
 <input id="Text12" type="text"  runat ="server" class ="c14031401"  />(mm)
         </div>
                  <div class="c13122302" id ="Div46" >
               Set 宽</div>
     <div class="c14031403" id ="Div47">
 <input id="Text13" type="text"  runat ="server" class ="c14031401"  />(mm)
         </div>
              <div class="c13122302" id ="Div48">
    Set 排版数</div>
     <div class="c14031403" id ="Div49">
  <input id="Text14" type="text"  runat ="server" class ="c14031401"  />(pcs)</div>
          <div class="c13122302" id ="Div50">
                表面处理</div>
     <div  class="c14120501" id ="Div51">
  <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList6" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="DropDownList6" Text="必填" runat="server" /></span>
         </div>
           </div>
           <div class ="c13101902">
 
                  <div class="c13122302" id ="Div52" >
            表面处理厚度</div>
     <div class="c14031403" id ="Div53">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList7" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="DropDownList7" Text="必填" runat="server" /></span>
         </div>
               <div class="c13122302" id ="Div54">
    防焊颜色</div>
     <div class="c14031403" id ="Div55">
  <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList8" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator26" ControlToValidate="DropDownList8" Text="必填" runat="server" /></span> </div>
          <div class="c13122302" id ="Div56">
                   文字颜色</div>
<div class="c14031403" id ="Div57">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList9" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="DropDownList9" Text="必填" runat="server" /></span>
         </div>
                  <div class="c13122302" id ="Div58" >
               板材厂商要求</div>
     <div  class="c14120501" id ="Div59">
 <input id="Text15" type="text"  runat ="server" class ="c14031401"  />
         </div>
           </div>
           
           <div class ="c13101902">
      <div class="c13122302" id ="Div60">
  阻抗</div>
     <div class="c14031403" id ="Div61">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList10" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator27" ControlToValidate="DropDownList10" Text="必填" runat="server" /></span> </div>
          <div class="c13122302" id ="Div62">
               指定叠构</div>
     <div class="c14031404" id ="Div63">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList11" runat="server" CssClass="c14031402">
                               
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="DropDownList11" Text="必填" runat="server" /></span>
         </div>
                  <div class="c13122302" id ="Div64" >
          内层成品铜厚</div>
     <div class="c14031403" id ="Div65">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList12" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="DropDownList12" Text="必填" runat="server" /></span>
         </div>
               <div class="c13122302"id ="Div641" > 
              
   外层成品铜厚</div>
     <div  class="c14120501" id ="Div67">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList13" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="DropDownList13" Text="必填" runat="server" /></span> </div>
           </div>
           <div class ="c13101902">

          <div class="c13122302" id ="Div68">
                  内外线路规格</div>
     <div class="c14031404" id ="Div69">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList14" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="DropDownList14" Text="必填" runat="server" /></span>
         </div>
                  <div class="c13122302" id ="Div70" >
          厚铜板</div>
     <div class="c14031403" id ="Div71">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList15" runat="server" CssClass="c14031402">
                                
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="DropDownList15" Text="必填" runat="server" /></span>
         </div>
               <div class="c13122302" id ="Div72">
 是否有BGA设计</div>
     <div class="c14031403" id ="Div73">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList16" runat="server" CssClass="c14031402">
                     
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="DropDownList16" Text="必填" runat="server" /></span> </div>
          <div class="c13122302" id ="Div74">
                   最小BGA PAD大小</div>
     <div  class="c14120501" id ="Div75">
 <input id="Text16" type="text"  runat ="server" class ="c14031401"  />
         </div>
           </div>
           <div class ="c13101902">

                  <div class="c13122302" id ="Div76" >
             孔铜要求</div>
     <div class="c14031403" id ="Div77">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList17" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="DropDownList17" Text="必填" runat="server" /></span>
         </div>
               <div class="c13122302" id ="Div78">
    最小孔</div>
     <div class="c14031403" id ="Div79">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList18" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="DropDownList18" Text="必填" runat="server" /></span> </div>
          <div class="c13122302" id ="Div80">
                  PCS 内孔数</div>
     <div class="c14031404" id ="Div81">
  <input id="Text17" type="text"  runat ="server" class ="c14031401"  />
  <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="Text17" Text="必填" runat="server" />
         </div>
                  <div class="c13122302" id ="Div82" >
            成型方式</div>
     <div  class="c14120501" id ="Div83">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList19" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="DropDownList19" Text="必填" runat="server" /></span>
         </div>
           </div>
           
           <div class ="c13101902">
      <div class="c13122302" id ="Div84">
   成型公差</div>
     <div class="c14031403" id ="Div85">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList20" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="DropDownList20" Text="必填" runat="server" /></span> </div>
          <div class="c13122302" id ="Div86">
                   V-cut set刀数</div>
     <div class="c14031404" id ="Div87">
 <input id="Text18" type="text"  runat ="server" class ="c14031401"  />
   <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ControlToValidate="Text18" Text="必填" runat="server" />
         </div>
                  <div class="c13122302" id ="Div88" >
               V-cut 角度</div>
     <div class="c14031403" id ="Div89">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList21" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="DropDownList21" Text="必填" runat="server" /></span>
         </div>
               <div class="c13122302" id ="Div90">
  V-cut 残厚</div>
     <div  class="c14120501" id ="Div91">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList27" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator28" ControlToValidate="DropDownList27" Text="必填" runat="server" /></span></div>
           </div>
           <div class ="c13101902">

          <div class="c13122302" id ="Div92">
                金手指是否斜边</div>
     <div class="c14031404" id ="Div93">
   <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList22" runat="server" CssClass="c14031402" >
              
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="DropDownList22" Text="必填" runat="server" /></span>
         </div>
                  <div class="c13122302" id ="Div94" >
           金手指斜边角度</div>
     <div class="c14031403" id ="Div95">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList23" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="DropDownList23" Text="必填" runat="server" /></span>
         </div>
              <div class="c13122302" id ="Div96">
 金手指斜边深度要求</div>
     <div class="c14031403" id ="Div97">
 <input id="Text20" type="text"  runat ="server" class ="c14031401"  />  </div>
          <div class="c13122302" id ="Div98">
                   测试方式</div>
     <div  class="c14120501" id ="Div99">
  <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList24" runat="server" CssClass="c14031402">
                                 <asp:ListItem >飞针</asp:ListItem>
                    <asp:ListItem>治具</asp:ListItem>
                    </asp:DropDownList></span>
         </div>
           </div>
                <div class ="c13101902">
      <div class="c13122302" id ="Div100">
   状态</div>
     <div class="c14031403" id ="Div101">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList25" runat="server" CssClass="c14031402">
   <asp:ListItem >正常</asp:ListItem>
        <asp:ListItem>Hold</asp:ListItem>
                    <asp:ListItem>作废</asp:ListItem>
                    </asp:DropDownList></span>
         </div>
      <div class="c13122302" id ="Div102">
   超大小板</div>
     <div class="c14031403" id ="Div103">
 <span style =" margin-right :8px;">
   <asp:DropDownList ID="DropDownList26" runat="server" CssClass="c14031402">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator25" ControlToValidate="DropDownList26" Text="必填" runat="server" /></span>
         </div>
                 <div class="c13122302" id ="Div20">
             终端</div>
     <div class="c13102904" id ="Div23">
   <input id="Text19" type="text"  runat ="server"  class ="c14031601" /></div>
                    <asp:Panel ID ="p1" runat="server">
                          <div class="c13122302" id ="Div10">
                              产品工艺流程</div>
     <div class="c13102904" id ="Div66">
   <input id="Text21" type="text"  runat ="server"  class ="c14031401" />      <span style =" margin-left :5px"><a  href="javascript:f13100202('Text21','');">选择 </a></span> </div></asp:Panel>
           </div>

                                  <div class ="c13122402">

          <div class="c13122302" id ="Div108">
                 备注</div>
     <div class="c13122401" id ="Div109">

         <asp:TextBox ID="TextBox1" runat="server"   TextMode="MultiLine" CssClass ="c13122403"></asp:TextBox>
         </div>
                
           </div>
           
           <div class ="c13102201">
               <div class="c13122302" id ="Div21">
         上传资料
                 </div>
          
     <div class="c13102203" id ="Div22">
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
            <div class="c13102301" id ="Div15">
            <span style =" float :left ; margin-left :30px;">   <asp:Button ID="Button1" runat="server" onclick="btnOnloadFile_Click" 
               Text="上传" /></span>
        <span style=" margin-left :20px; color :Red ;">注：上传的单个附件大小需小于20M</span>
                 </div>
</div>
<div class ="c13102201">
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
               
<script type="text/javascript" language="javascript">
    function f13100302(result) {
        if (window.opener != undefined) {
            //for chrome
            window.opener.returnValue = result;
        }
        else {
            window.returnValue = result;
        }
        window.close();
    }
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
    function f13100202(obj) {

        if (navigator.userAgent.indexOf("Chrome") > 0) {
            if (obj == "Text5") {
                var dlgResult = myShowModalDialog("../SellManage/Customerinfo.aspx?come=wa", window, 1200, 490);
            }
            else if (obj == "Text21") {
                var dlgResult = myShowModalDialog("../producttrace/flow.aspx?come=wareinfo", window, 1200, 490);
            }
        }
        else {
            if (obj == "Text5") {
                var dlgResult = window.showModalDialog("../SellManage/Customerinfo.aspx", window, "dialogWidth:970px; dialogHeight:490px; status:0");
                if (dlgResult != undefined) {
                    document.getElementById("Text5").value = dlgResult[1];

                }
            }
            else if (obj == "Text21") {
                var dlgResult = myShowModalDialog("../producttrace/flow.aspx?come=wareinfo", window, 1200, 490);
                if (dlgResult != undefined) {
                    document.getElementById("Text21").value = dlgResult[1];

                }
            }
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
        else if (obj.value == "添加货币贸易公司" || obj.value == "返回货币贸易公司管理" || obj.value == "返回查看个人信息") {

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
        else if (obj.value == "添加货币贸易公司" || obj.value == "返回货币贸易公司管理" || obj.value == "返回查看个人信息") {

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