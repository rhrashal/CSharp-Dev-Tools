<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlySalaryFullDetailsReportViewer.aspx.cs" Inherits="PayRollMS.Views.Report.Viewers.MonthlySalaryFullDetailsReportViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salary Sheet Report</title>

    <style type="text/css">
        .tableStyle td 
        {
            width: 50px;
            
        }
         .tableStyle th 
        {
            width: 50px;
        }
        .tableStyle 
        {
            font-size:11px;
            width:800px;
            border:1px solid;
        }
        
        .tableStyle2
        {
            border:1px solid;
        }
    </style>


         <script type="text/javascript">

             function printPartOfPage(elementId) {
                 var printContent = document.getElementById(elementId);
                 var windowUrl = 'about:blank';
                 var uniqueName = new Date();
                 var windowName = 'Print' + uniqueName.getTime();
                 var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');

                 var styles = " <link href=\"salarySheet.css\" rel=\"stylesheet\" type=\"text/css\" /> ";

                 printWindow.document.writeln('<!DOCTYPE html> <head> ');
                 printWindow.document.writeln(styles);
                 printWindow.document.writeln('</head> <body>');
                 printWindow.document.write(printContent.innerHTML);
                 printWindow.document.writeln('</body> </html>');
                 printWindow.document.close();
                 printWindow.focus();
                 printWindow.print();
                 printWindow.close();
                 return false
             }

        </script>

</head>
<body>
    <form id="form1" runat="server">
     <div id="sheet" runat="server">
     <div style="width:1100px; font-size:12px;">
     
      <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
      
      <div style="width:1100px;">
        <h2 align="center" style="font-size:22px;">Lavender Convenience Store Ltd.</h2>
        
        <h2 align="center" style="font-size:15px;">
           SALARY SHEET    -      <asp:Label ID="lblMonthName" runat="server" Text="Jan of 2013"></asp:Label>
         </h2>
        <br />
        <div style="float:left">
        <table class="tableStyle2">
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblShopName" runat="server" Text="Shop"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border-right:1px solid;">
                    <asp:Label ID="lblTitle" runat="server" Text="Jan of 2013"></asp:Label>
                </td>
                <td style="border-right:1px solid;">
                    Working Days
                </td>
                <td>
                    <asp:Label ID="lblWorkDays" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <div style="float:right; margin-bottom:3px;">
            <table class="tableStyle2">
                <tr>
                    <td style="border-right:1px solid;">Start</td>
                    <td style="border-right:1px solid;">
                        <asp:Label ID="lblstartDate" runat="server" Text=""></asp:Label>
                        </td>
                    <td style="border-right:1px solid;">End</td>
                    
                    <td>
                    <asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
      </div>
      <br />
         <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>

         <table width="1100" class="tableStyle">
            <thead>
            <tr >
                <asp:Literal ID="LiteralHead1" runat="server"></asp:Literal>
            </tr>
            <tr>
                <asp:Literal ID="LiteralHead2" runat="server"></asp:Literal>
                
            </tr>
            </thead>

            <tbody>
             <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <asp:Literal ID="Literaldata" runat="server" Text='<%# Bind("Data") %>'></asp:Literal>
                    </tr>
                </ItemTemplate>
             </asp:Repeater>
             </tbody>
         </table>
         <%-- <asp:GridView ID="GridView1" runat="server" EnableTheming="false" Width="100%" ShowHeader="false" CssClass="tableStyle">
          </asp:GridView>--%>
      <%--<table style="border:1px solid;">
        <tr style="border-bottom:1px solid;">
            <th colspan="2">Allownace</th>
            <asp:Repeater ID="RepAlHeader" runat="server">
                <ItemTemplate>
                    <th style="border-bottom:1px solid; border-left:1px solid;">
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("ALID") %>' />
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ALOWANCE_TITLE") %>'></asp:Label>
                    </th>
                </ItemTemplate>
            </asp:Repeater>
            <th  style="border-bottom:1px solid; border-left:1px solid;">Total Allowance</th>
            <th  style="border-bottom:1px solid; border-left:1px solid;">Gross Amount</th>
            <th  style="border-bottom:1px solid; border-left:1px solid;">Over Time</th>
            <th  style="border-bottom:1px solid; border-left:1px solid;">Net Amount</th>
        </tr>

        <tr>
            <th colspan="2" style="border-bottom:1px solid;">Deduction</th>
            <asp:Repeater ID="RepDedHeader" runat="server">
                <ItemTemplate>
                    <th style="border-bottom:1px solid; border-left:1px solid;">
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("ALID") %>' />
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ALOWANCE_TITLE") %>'></asp:Label>
                    </th>
                </ItemTemplate>
            </asp:Repeater>
            <th style="border-bottom:1px solid; border-left:1px solid;">&nbsp;</th>
            <th style="border-bottom:1px solid; border-left:1px solid;">&nbsp;</th>
            <th style="border-bottom:1px solid; border-left:1px solid;">&nbsp;</th>
            <th style="border-bottom:1px solid; border-left:1px solid;">&nbsp;</th>

        </tr>
          <asp:Repeater ID="RepBranch" runat="server">
              <ItemTemplate>
                  <tr style="background-color:#eb3;">
                      <td colspan="2" >
                          <asp:HiddenField ID="hfBranchID" runat="server" Value='<%# Bind("BID") %>' />
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("BRANCH_NAME") %>'></asp:Label>
                      </td>
                      <td>
                            <asp:Repeater ID="RepAiDataBranchHeader" runat="server">
                                <ItemTemplate>
                                    <td>
                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("ALID") %>' />
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                      </td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>

                  </tr>
                  <asp:Repeater ID="RepDepartment" runat="server">
                      <ItemTemplate>
                          <tr style="background:#de3;">
                          
                              <td colspan="2" style="border-top:1px solid;">
                                  <asp:HiddenField ID="HFDID" runat="server" Value='<%# Bind("DID") %>' />
                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:Label ID="Label3" runat="server" Text='<%# Bind("DEP_NAME") %>'></asp:Label>
                              </td>
                              
                                    <asp:Repeater ID="RepAiDataDepHeader" runat="server">
                                        <ItemTemplate>
                                            <td style="border-top:1px solid;">
                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("ALID") %>' />
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                              
                              <td style="border-top:1px solid;">&nbsp;</td>
                              <td style="border-top:1px solid;">&nbsp;</td>
                              <td style="border-top:1px solid;">&nbsp;</td>
                              <td style="border-top:1px solid;">&nbsp;</td>

                          </tr>
                          <asp:Repeater ID="RepEmployee" runat="server">
                            <ItemTemplate>
                                <tr>
                                    
                                    <td colspan="2" style="border-right:1px solid; border-top:1px solid;">
                                     <asp:HiddenField ID="HFEID" runat="server" Value='<%# Bind("EID") %>' />
                                        <asp:HiddenField ID="HFJTID" runat="server" Value='<%# Bind("JTID") %>' />
                                     <asp:Label ID="Label3s" runat="server" Text='<%# Bind("EMP_NAME") %>'></asp:Label>
                                     </td>
                                    <asp:Repeater ID="RepAlData" runat="server">
                                        <ItemTemplate>
                                            <td  style="border-right:1px solid; border-top:1px solid; border-bottom:1px solid;">
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                   
                                    <td style="border-right:1px solid; border-top:1px solid; border-bottom:1px solid;">
                                        <asp:Label ID="lblTotalAllowance" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td style="border-right:1px solid; border-top:1px solid; border-bottom:1px solid;">
                                        <asp:Label ID="lblGrossAllowance" runat="server" Text="0"></asp:Label>
                                    </td>
                                     <td style="border-right:1px solid; border-top:1px solid; border-bottom:1px solid;">
                                        <asp:Label ID="lblOverTimeAmnt" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td style=" border-top:1px solid; border-bottom:1px solid;">
                                        <asp:Label ID="lblNetAmnt" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-right:1px solid;">&nbsp;</td>
                                     <td style="border-right:1px solid;">
                                        <asp:Label ID="lblAdvanceAmnt" runat="server" Text="0"></asp:Label>
                                    </td>
                                     <td style="border-right:1px solid;">
                                        <asp:Label ID="lblLoanAmnt" runat="server" Text="0"></asp:Label>
                                    </td>
                                     <td style="border-right:1px solid;">
                                        <asp:Label ID="lblAbscentAmnt" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                          </asp:Repeater>
                      </ItemTemplate>
                  </asp:Repeater>
                  <tr style="background-color:#F396AE;">
                    <td colspan="2" style="border-top:1px solid; border-bottom:1px solid; border-right:1px solid;">Department Total</td>

                     <asp:Repeater ID="RepAlData" runat="server">
                        <ItemTemplate>
                            <td style="border-top:1px solid; border-bottom:1px solid; border-right:1px solid;">
                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("ALID") %>' />
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                    <td style="border-top:1px solid; border-bottom:1px solid; border-right:1px solid;">
                        <asp:Label ID="lblDepAlloTotal" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="border-top:1px solid; border-bottom:1px solid; border-right:1px solid;">
                        <asp:Label ID="lblDepGrossTotal" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="border-top:1px solid; border-bottom:1px solid; border-right:1px solid;">&nbsp;</td>
                    <td style="border-top:1px solid; border-bottom:1px solid; border-right:1px solid;">
                        <asp:Label ID="lblDepNetTotal" runat="server" Text="0"></asp:Label>
                    </td>
                  </tr>
              </ItemTemplate>
          </asp:Repeater>

          <tr style="background-color:#D62323;color:#fff;">
            <td colspan="2">Grand Total</td>
              <asp:Repeater ID="RepAlData2" runat="server">
                  <ItemTemplate>
                      <td>
                          <%--<asp:Label ID="Label3" runat="server" Text='<%# Bind("ALID") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Bind("ALID") %>' />
                      </td>
                  </ItemTemplate>
              </asp:Repeater>
              <td>
                  <asp:Label ID="lblAllowanceTotal" runat="server" Text="0"></asp:Label>
              </td>
              <td>
                  <asp:Label ID="lblGrossTotal" runat="server" Text="0"></asp:Label>
              </td>
              <td>&nbsp;</td>
              <td>
                  <asp:Label ID="lblNetTotal" runat="server" Text="0"></asp:Label>
              </td>
          </tr>
      </table>--%>
        
        <br />
        <br />
        <br />
        <table width="1100">
            <tr>
                <td>
                   <hr /> Prepared By
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                 <hr />   Checked By
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <hr />Approved By
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>

            </tr>
        </table>

        <br />
        <hr />
        
    </div>
     </div>

     <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="printPartOfPage('sheet');" />
     <asp:Button ID="btnExport" runat="server" Text="Excel" OnClick="btnExcel_Click" />


    </form>
</body>
</html>
