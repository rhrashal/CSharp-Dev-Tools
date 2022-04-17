 ###########################################################################################		
		[HttpPost]
        [Route("api/GetSalePaymentSummaryReport")]
        public HttpResponseMessage GetSalePaymentSummaryReport(ReportParameter param)
        {

            try
            {
                var request = Request;
                if (request.Headers.Contains("Authorization"))
                {
                    var auth = request.Headers.GetValues("Authorization").ToList();
                    var headers = auth[0].Split(':');
                    string username = headers[0];
                    string password = headers[1];

                    System.Data.DataTable items = saleService.GetSalePaymentSummary(username,password, param.fromDate,param.endDate, param.store, param.type);
                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                    if (items.Rows.Count == 0)
                    {
                        Log.Debug("Not Found");
                        return new HttpResponseMessage(HttpStatusCode.NotFound);

                    }
                    else
                    {
                        DataRow totalsRow = items.NewRow();

                        //foreach (DataColumn col in items.Columns)
                        //{
                        //    if (items.Columns.IndexOf(col) >= items.Columns.Count - 2)
                        //    {
                        //        decimal qtyTotal = 0;
                        //        decimal amtTotal = 0;
                        //        foreach (DataRow row in col.Table.Rows)
                        //        {
                        //            if (items.Columns.IndexOf(col) == items.Columns.Count - 2)
                        //                qtyTotal += Decimal.Parse(row[col].ToString());
                        //            else
                        //                amtTotal += Decimal.Parse(row[col].ToString());

                        //        }
                        //        if (items.Columns.IndexOf(col) == items.Columns.Count - 2)
                        //            totalsRow["Qty"] = qtyTotal;
                        //        else
                        //            totalsRow["Total"] = amtTotal;
                        //    }
                        //    else if (items.Columns.IndexOf(col) == 0)
                        //    {
                        //        //totalsRow[col.ColumnName] = "Total : ";
                        //    }

                        //}
                        //items.Rows.Add(emptyRow);
                        items.Rows.Add(totalsRow);
                        Guid guid = Guid.NewGuid();
                        string filename = "/CReports/" + guid.ToString() + "." + ".xls";


                        using (StringWriter sw = new StringWriter())
                        {
                            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                            {
                                DataGrid dgGrid = new DataGrid();
                                dgGrid.DataSource = items;
                                dgGrid.DataBind();

                                //foreach (DataGridColumn dgc in dgGrid)
                                //{
                                //    foreach (DataGridItem dgi in dgc)
                                //    {

                                //    }
                                //}


                                dgGrid.RenderControl(htw);
                                result.Content = new StringContent(sw.ToString());
                                result.Content = new StringContent(sw.ToString());

                            }
                        }

                        return result;

                    }
                }

                else
                {
                    Log.Debug("Bad Sale Request");
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
		
		
		
		
#################################################################################

        $scope.showSalePayReport = function () {
            var fromDate = $scope.FROM_DATE
            var endDate = $scope.TO_DATE

            var obj = { 'Jan': 1, 'Feb': 2, 'Mar': 3, 'Apr': 4, 'May': 5, 'Jun': 6, 'Jul': 7, 'Aug': 8, 'Sep': 9, 'Oct': 10, 'Nov': 11, 'Dec': 12 }
            d1 = fromDate.split("-")
            d2 = endDate.split("-")
            m1 = obj[d1[1]];
            m2 = obj[d2[1]];
            if (d1[2] > d2[2] || (d1[2] == d2[2] && m1 > m2) || (d1[2] == d2[2] && m1 == m2 && d1[0] > d2[0])) {
                alert("From date can not be greater than To Date !!");
                return;
            }
            var store = 'ALL'

            if ($scope.STORE_CODE) {
                store = $scope.STORE_CODE
            }
            var type = "date";
            if ($scope.RPT_TYPE == "summary") {
                type = "shop";
            }
            var param = {
                type: type,
                fromDate: $scope.FROM_DATE,
                endDate: $scope.TO_DATE,
                store: store
            }

            var promiseGet = paymentMethodService.salePaymentSummaryReport(param);
            promiseGet.then(function (pl) {
                $scope.gotData = pl.data;
                //console.log($scope.gotData.length);
                if (!pl.data) {
                    alert("No Data matches these filters!");
                    return;
                }
                var tempDiv = document.createElement('table');
                tempDiv.innerHTML = pl.data;
                //console.log(tempDiv);
                var cols = tempDiv.rows[0].cells.length;
                var Store = "ALL";
                if ($scope.STORE_CODE != "ALL")
                    Store = tempDiv.rows[1].cells[6].innerHTML;
                var Company = tempDiv.rows[1].cells[7].innerHTML;;
                debugger;

                if (type == 'Periodical') {
                    type = 'From ' + sdate + ' To ' + edate;
                }
                if (store == 'ALL') {
                    $scope.gotData = $scope.gotData.replace('<table cellspacing="0" rules="all" border="1" style="border-collapse:collapse;">',
                        '<table cellspacing="0" rules="all" border="1" style="border-collapse:collapse;"><tr><td colspan="'
                        + cols + '" style="font-size: 20px;text-align:center;font-weight:bold">' + Company + '</td></tr>'
                        + '<tr><td colspan="' + cols + '" style="font-size: 18px;text-align:center;font-weight:bold">Stock Report by Attribute</tr>'
                        + '<tr><td colspan="' + cols + '" style="font-size: 16px;text-align:center;font-weight:bold">' + type + ' </td></tr>');
                }
                else {
                    $scope.gotData = $scope.gotData.replace('<table cellspacing="0" rules="all" border="1" style="border-collapse:collapse;">',
                    '<table cellspacing="0" rules="all" border="1" style="border-collapse:collapse;"><tr><td colspan="'
                    + cols + '" style="font-size: 20px;text-align:center;font-weight:bold">' + Company + '</td></tr>'
                    + '<tr><td colspan="' + cols + '" style="font-size: 18px;text-align:center;font-weight:bold">Stock Report by Attribute</td></tr>'
                    + '<tr><td colspan="' + cols + '" style="font-size: 16px;text-align:center;font-weight:bold">' + Store + ' </td></tr>'
                    + '<tr><td colspan="' + cols + '" style="font-size: 16px;text-align:center;font-weight:bold">' + type + ' </td></tr>');
                }
                //console.log($scope.gotData);
                var extensions = {
                    "application/pdf": ".pdf",
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8": "xls"
                }
                var result = "success";
                //console.log(result);
                var blob = new Blob([$scope.gotData], {
                    type: "application/vnd.ms-excel;charset=charset=utf-8"
                });
                var uuid = guid();
                saveAs(blob, uuid + ".xls");
            }, function (errPl) {
                $log.error('failure loading Class', errPl);
            });


        }

        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
              s4() + '-' + s4() + s4() + s4();
        }		
		
		
		
		
###################################################################################
    this.salePaymentSummaryReport = function (obj) {
        var username = sessionStorage.getItem("UserName");
        var password = sessionStorage.getItem("PW");

        var request = $http({
            method: "post",
            url: _urlBase + "api/GetSalePaymentSummaryReport",
            data: obj,
            headers: { 'Authorization': username + ':' + password }
        });
        return request;
    }
##################################################################################	
