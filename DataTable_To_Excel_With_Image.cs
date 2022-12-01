 //// first 



public void GenerateExcel(DataTable DT, string fullFileName)
        {
            
            var file = new FileInfo(fullFileName);
            //string currentFileName = System.IO.Path.GetFileName(fullFileName);

            ExcelPackage excel = new ExcelPackage(file);
            var sheetcreate = excel.Workbook.Worksheets.Add("Sheet1");

            int col = 0;
            foreach (DataColumn column in DT.Columns)  //printing column headings
            {
                sheetcreate.Cells[1, ++col].Value = column.ColumnName;
                sheetcreate.Cells[1, col].Style.Font.Bold = true;
                sheetcreate.Cells[1, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheetcreate.Cells[1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["imageUrl"];
            if (DT.Rows.Count > 0)
            {
                int row = 1;
                decimal checkDecimal;
                for (int eachRow = 0; eachRow < DT.Rows.Count; )    //looping each row
                {
                    for (int eachColumn = 1; eachColumn <= col; eachColumn++)   //looping each column in a row
                    {
                        try
                        {
                            if (eachColumn==15)
                            {
                                
                            }
                        
                        var eachRowObject = sheetcreate.Cells[row + 1, eachColumn];
                        eachRowObject.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        if (DT.Columns[eachColumn - 1].ColumnName == "IMAGE_URL")
                        {
                            //Image logo = Image.FromFile(baseUrl+DT.Rows[eachRow][(eachColumn )].ToString());
                            using (var webClient = new WebClient())
                            {
                                byte[] imageBytes = webClient.DownloadData(baseUrl + DT.Rows[eachRow][(eachColumn - 1)].ToString());
                                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(imageBytes));
                                if (x != null)
                                {
                                    sheetcreate.Row(row + 1).Height = 39.00D;                                    
                                    var picture = sheetcreate.Drawings.AddPicture("x" + (eachColumn + eachRow + 1).ToString(), x);
                                    picture.From.Column = eachColumn - 1;
                                    picture.From.Row = row;
                                    picture.SetSize(60, 55);
                                    
                                    //   picture.SetPosition(, 0, 2, 0);
                                }
                            }
                        }
                        else {
                            eachRowObject.Value = DT.Rows[eachRow][(eachColumn - 1)].ToString();
                        }
                        if (decimal.TryParse(DT.Rows[eachRow][(eachColumn - 1)].ToString(), out checkDecimal))      //verifying value is number to make it right align
                        {
                            eachRowObject.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        eachRowObject.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);     // adding border to each cells
                        if (eachRow % 2 == 0)       //alternatively adding color to each cell.
                            eachRowObject.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#e0e0e0"));
                        else
                            eachRowObject.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#ffffff"));
                        }
                        catch (Exception ex)
                        {

                           /// throw;
                        }
                    }
                    eachRow++;
                    row++;

                }
            }
            sheetcreate.Cells.AutoFitColumns();
            excel.Save();
        }

///or first

 public static void GenerateExcelWithImageFromPath(DataTable DT, string fullFileName,string companyName,string headerName,string headerDetails, string imgColumnName)
        {

            var file = new FileInfo(fullFileName);
            //string currentFileName = System.IO.Path.GetFileName(fullFileName);

            ExcelPackage excel = new ExcelPackage(file);
            var sheetcreate = excel.Workbook.Worksheets.Add(headerName);

            sheetcreate.Cells[1, 1, 1, DT.Columns.Count].Merge = true;
            sheetcreate.Cells[1, 1].Value = companyName;
            sheetcreate.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheetcreate.Cells[1, 1].Style.Font.Bold = true;
            sheetcreate.Cells[1, 1].Style.Font.Size = 16; ;
            //sheetcreate.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);


            sheetcreate.Cells[2, 1, 2, DT.Columns.Count].Merge = true;
            sheetcreate.Cells[2, 1].Value = headerName;
            sheetcreate.Cells[2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheetcreate.Cells[2, 1].Style.Font.Bold = true;
            sheetcreate.Cells[2, 1].Style.Font.Size = 14; ;
            //sheetcreate.Cells[2, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            sheetcreate.Cells[3, 1, 3, DT.Columns.Count].Merge = true;
            sheetcreate.Cells[3, 1].Value = headerDetails;
            sheetcreate.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheetcreate.Cells[3, 1].Style.Font.Bold = true;
            sheetcreate.Cells[3, 1].Style.Font.Size = 12; ;
            //sheetcreate.Cells[3, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            int col = 0;
            foreach (DataColumn column in DT.Columns)  //printing column headings
            {
                sheetcreate.Cells[4, ++col].Value = column.ColumnName;
                sheetcreate.Cells[4, col].Style.Font.Bold = true;
                sheetcreate.Cells[4, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheetcreate.Cells[4, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["imageUrl"];
            if (DT.Rows.Count > 0)
            {
                int row = 4;
                decimal checkDecimal;
                for (int eachRow = 0; eachRow < DT.Rows.Count; )    //looping each row
                {
                    for (int eachColumn = 1; eachColumn <= col; eachColumn++)   //looping each column in a row
                    {
                        try
                        {
                            if (eachColumn == 15)
                            {

                            }

                            var eachRowObject = sheetcreate.Cells[row + 1, eachColumn];
                            eachRowObject.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            if (DT.Columns[eachColumn - 1].ColumnName == imgColumnName)
                            {
                                //Image logo = Image.FromFile(baseUrl+DT.Rows[eachRow][(eachColumn )].ToString());
                                using (var webClient = new WebClient())
                                {
                                    byte[] imageBytes = webClient.DownloadData(baseUrl + DT.Rows[eachRow][(eachColumn - 1)].ToString());
                                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(imageBytes));
                                    if (x != null)
                                    {
                                        sheetcreate.Row(row + 1).Height = 39.00D;
                                        var picture = sheetcreate.Drawings.AddPicture("x" + (eachColumn + eachRow + 1).ToString(), x);
                                        picture.From.Column = eachColumn - 1;
                                        picture.From.Row = row;
                                        picture.SetSize(50, 50);
                                    }
                                }
                            }
                            else
                            {
                                eachRowObject.Value = DT.Rows[eachRow][(eachColumn - 1)].ToString();
                            }
                            if (decimal.TryParse(DT.Rows[eachRow][(eachColumn - 1)].ToString(), out checkDecimal))      //verifying value is number to make it right align
                            {
                                eachRowObject.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            eachRowObject.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);     // adding border to each cells
                            if (eachRow % 2 == 0)       //alternatively adding color to each cell.
                                eachRowObject.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#e0e0e0"));
                            else
                                eachRowObject.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#ffffff"));
                        }
                        catch (Exception ex)
                        {

                            /// throw;
                        }
                    }
                    eachRow++;
                    row++;

                }
            }
            sheetcreate.Cells.AutoFitColumns();
            excel.Save();
        }





///secend
        [HttpGet]
        [Route("api/GetArticalWiseStockReport/{type}/{store_code}/{category}/{subcategory}")]
        public IHttpActionResult GetArticalWiseStockReport(string type, string store_code, string category, string subcategory)
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
                    decimal total = 0;
                    DataTable items = service.GetInventoryPhysicalStockReport(store_code, category, subcategory, username, password);
                    //DataTable items = service.GetArticalWiseStockReport(store_code, category, subcategory, username, password);
                    //DataTable items2 = service.GetArticalWiseStockReportSizeList(store_code, category, subcategory, username, password);
                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                    if (items.Rows.Count == 0)
                    {
                        Log.Debug("Not Found");
                        return NotFound();
                    }
                    else
                    {

                        DataRow totalsRow = items.NewRow();
                        for (int i = 0; i < items.Columns.Count; i++)
                        {
                            try
                            {
                                string columnName = items.Columns[i].ColumnName.ToString();
                                totalsRow[i] = items.AsEnumerable().Sum(row => (row.Field<decimal>(columnName)));
                            }
                            catch (Exception)
                            {
                            }
                        }
                        items.Rows.Add(totalsRow);

                        Guid guid = Guid.NewGuid();
                       ///string fullFileName = System.Web.Hosting.HostingEnvironment.MapPath("~") + "CReports/" + guid.ToString() + "." + "xls";
                      string fullFileName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + "." + "xls";
                       GenerateExcel(items, fullFileName);
                       string filename = "/CReports/" + guid.ToString() + ".xls";
                       return Content(HttpStatusCode.OK, filename);
                    }
                }
                else
                {
                    Log.Debug("Bad Sale Request");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
