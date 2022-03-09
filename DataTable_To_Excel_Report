 [HttpGet]
        [Route("api/GetStoreDeliveryReportByAttr/{company_code}/{sdate}/{edate}/{store_code}/{delivery_to}/{item}")]
        public HttpResponseMessage GetStoreDeliveryReportByAttr(string company_code, DateTime sdate, DateTime edate, string store_code, string delivery_to, string item)
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

                    System.Data.DataTable items = service.GetStoreDeliveryReportByAttr(company_code, sdate, edate, store_code, delivery_to, item, username, password);
                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                    if (items.Rows.Count == 0)
                    {
                        Log.Debug("Not Found");
                        return new HttpResponseMessage(HttpStatusCode.NotFound);

                    }
                    else
                    {

      
                        DataRow totalsRow = items.NewRow();
                        
                        foreach (DataColumn col in items.Columns)
                        {
                            if (items.Columns.IndexOf(col) >= items.Columns.Count - 2)
                            {
                                decimal qtyTotal = 0;
                                decimal amtTotal = 0;
                                foreach (DataRow row in col.Table.Rows)
                                {
                                    if (items.Columns.IndexOf(col) == items.Columns.Count - 2)
                                        qtyTotal += Decimal.Parse(row[col].ToString());
                                    else
                                        amtTotal += Decimal.Parse(row[col].ToString());

                                }
                                if (items.Columns.IndexOf(col) == items.Columns.Count - 2)
                                    totalsRow["Qty"] = qtyTotal;
                                else
                                    totalsRow["Total"] = amtTotal;
                            }
                            else if (items.Columns.IndexOf(col) == 0)
                            {
                                //totalsRow[col.ColumnName] = "Total : ";
                            }
                            
                        }
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

