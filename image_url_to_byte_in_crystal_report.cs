 [HttpPost]
        [Route("api/GetBatchWiseStockReport")]
        public IHttpActionResult GetBatchWiseStockReport(ReportParameter param)
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
                    param.reportTitle = "Batch Wise Stock Report";

                    int incrementalid = SaveLog(username, password, "rptBatchwiseStockReport.rpt", param);

                    IList<ProductStock> items = service.GetBatchWiseStockReport(username, password, param);
                    reportLogService.UpdateReportLog(incrementalid, username, password);

                    IList<CloudPos.Model.ProductVarianceSequence> productVarianceSequence = null;
                    productVarianceSequence = productVarianceSequenceService.GetProductVarianceSequence(username, password);
                    string baseUrl = System.Configuration.ConfigurationManager.AppSettings["rptBaseUrl"];
                    foreach (var item in items)
                    {
                        try
                        {                            
                            WebClient wc = new WebClient();
                            item.ProudctImage = wc.DownloadData(baseUrl + item.IMAGE_URL);
                            //item.ProudctImage = wc.DownloadData(System.Web.Hosting.HostingEnvironment.MapPath(item.IMAGE_URL));
                        }
                        catch { }
                    }
                    if (items == null)
                    {
                        Log.Debug("Not Found");
                        return Content(HttpStatusCode.NotFound, "Sorrry!!! Data Not Found");

                    }
                    else
                    {
                        Guid guid = Guid.NewGuid();
                        using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new ReportDocument())
                        {
                            rd.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/") + "\\Reports\\rptBatchwiseStockReport.rpt");                            
                            rd.Database.Tables[0].SetDataSource(items);
                            rd.Database.Tables[1].SetDataSource(productVarianceSequence);
                            string store = "ALL";
                            string storeAddress = string.Empty;

                            var companyinfo = serviceCompany.Get(param.company, username, password);

                            IList<Store> storelist = storeservice.GetListStoreCodeTypeName(param.company, username, password);


                            var firstOrDefault = storelist.FirstOrDefault(a => a.STORE_CODE == param.store);
                            if (firstOrDefault != null)
                            {
                                store = firstOrDefault.STORE_NAME;
                                storeAddress = firstOrDefault.ADDRESS1;
                            }

                            rd.SetParameterValue("@Company", companyinfo.COMPANY_NAME);
                            rd.SetParameterValue("@Store", store);
                            rd.SetParameterValue("@storeAddress", param.store == "ALL" ? companyinfo.ADDRESS1 : storeAddress);

                            rd.SetParameterValue("@DEPARTMENT_LABEL", companyinfo.DEPARTMENT_LABEL);
                            rd.SetParameterValue("@SUB_DEPARTMENT_LABEL", companyinfo.SUB_DEPARTMENT_LABEL);
                            rd.SetParameterValue("@CATEGORY_LABEL", companyinfo.CATEGORY_LABEL);
                            rd.SetParameterValue("@SUB_CATEGORY_LABEL", companyinfo.SUB_CATEGORY_LABEL);
                            rd.SetParameterValue("@SUB_SUBCATEGORY_LABEL", companyinfo.SUB_SUBCATEGORY_LABEL);

                            string ffName = string.Empty;
                            if (param.type == "data")
                            {
                                ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + "." + "xls";

                            }
                            else
                            {
                                ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + "." + param.type;
                            }
                            CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                            dfo.DiskFileName = ffName;
                            switch (param.type)
                            {
                                case "pdf":
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                                    break;
                                case "xls":
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.Excel;
                                    break;
                                case "data":
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.ExcelRecord;
                                    param.type = "xls";
                                    break;
                            }
                            rd.ExportOptions.DestinationOptions = dfo;
                            rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                            rd.Export();
                            rd.Close();
                        }// Reports.rptDateWiseSalesReport();



                        string filename = "/CReports/" + guid.ToString() + "." + param.type;

                        return Content(HttpStatusCode.OK, filename);
                    }
                }
                else
                {
                    Log.Debug("Bad Sale Request");
                    return Content(HttpStatusCode.BadRequest, "Bad Sale Request");
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
