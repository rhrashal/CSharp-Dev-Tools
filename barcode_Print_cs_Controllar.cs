[HttpPost]
        [Route("api/PrintBarcode")]
        public IHttpActionResult PrintBarcode(List<BarcodePrintReport> barcodelist)
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

                    List<BarcodePrintReport> items = new List<BarcodePrintReport>();

                    string barcode_print_opt = comService.Get(barcodelist[0].COMPANY_CODE, username, password).BARCODE_PRINT_OPT;

                    ZXing.Common.EncodingOptions option = new ZXing.Common.EncodingOptions();
                    option.Height = 40;
                    option.Width = 120;
                    //option.PureBarcode = false;
                    option.PureBarcode = true;

                    ZXing.IBarcodeWriter writer = new ZXing.BarcodeWriter { Format = ZXing.BarcodeFormat.CODE_128, Options = option };

                    if (barcodelist[0].type == "SHELF_TAG")
                    {
                        BarcodePrintReport b;
                        foreach (BarcodePrintReport s in barcodelist)
                        {
                            Product prd = prdService.GetProduct(s.COMPANY_CODE, s.SAL_BARCODE, username, password);

                            b = new BarcodePrintReport();
                            b.DISPLAY_NAME = s.DISPLAY_NAME;
                            b.PUR_QTY = s.PUR_QTY;

                            b.SAL_PRICE = s.SAL_PRICE;
                            b.FREE_TEXT1 = s.FREE_TEXT1;
                            b.FREE_TEXT2 = s.BRAND_NAME;
                            b.FREE_TEXT4 = s.FREE_TEXT4;
                            b.FREE_TEXT3 = s.FREE_TEXT3;
                            b.SAL_BARCODE = s.SAL_BARCODE;
                            b.CATEGORY_NAME = s.CATEGORY_NAME;
                            b.SUB_CATEGORY_NAME = s.SUB_CATEGORY_NAME;

                            var result = writer.Write(b.SAL_BARCODE);
                            Bitmap barcodeBitmap = new Bitmap(result);
                            ImageConverter converter = new ImageConverter();
                            b.BARCODE = (byte[])converter.ConvertTo(barcodeBitmap, typeof(byte[]));


                            b.MANUFACTURE_DATE = s.MANUFACTURE_DATE;
                            b.EXPIRE_DATE = s.EXPIRE_DATE;
                            b.VENDOR_NAME = s.VENDOR_NAME;


                            if (prd != null)
                            {
                                if (prd.ProductAttributeList.Count > 0)
                                {
                                    var pvList = (from p in prd.ProductAttributeList
                                                  where p.SHOW_BARCODE == true
                                                  select p).ToList();

                                    foreach (var pv in pvList)
                                    {
                                        switch (pv.SERIAL)
                                        {
                                            case 1:
                                                b.Att1 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 2:
                                                b.Att2 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 3:
                                                b.Att3 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 4:
                                                b.Att4 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 5:
                                                b.Att5 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 6:
                                                b.Att6 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 7:
                                                b.Att7 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 8:
                                                b.Att8 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 9:
                                                b.Att9 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 10:
                                                b.Att10 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 11:
                                                b.Att11 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 12:
                                                b.Att12 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 13:
                                                b.Att13 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 14:
                                                b.Att14 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 15:
                                                b.Att15 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 16:
                                                b.Att16 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 17:
                                                b.Att17 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 18:
                                                b.Att18 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 19:
                                                b.Att19 = pv.ATTRIBUTE_NAME;
                                                break;
                                            case 20:
                                                b.Att20 = pv.ATTRIBUTE_NAME;
                                                break;
                                        }
                                    }
                                }

                                if (prd.UserBarcodeList.Count > 0)
                                {
                                    b.FREE_TEXT5 = prd.UserBarcodeList[0];
                                }

                                b.PrintDate = CloudPos.Service.DateToStringConverterService.GeneratePrintDateString(DateTime.Today);

                                b.TEXT1 = prd.FREE_TEXT1;
                                b.TEXT2 = prd.FREE_TEXT2;
                                b.TEXT3 = prd.FREE_TEXT3;
                                b.TEXT4 = prd.FREE_TEXT4;
                                b.TEXT5 = prd.FREE_TEXT5;

                                b.SAL_VAT_PERCENT = prd.SAL_VAT_PERCENT;
                                b.IS_PRICE_INCLD_VAT = prd.IS_PRICE_INCLD_VAT == 1 ? true : false;
                            }

                            items.Add(b);
                        }

                        Guid guid = Guid.NewGuid();

                        using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
                        {
                            rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports") + "/" + "rptShelfTag.rpt");

                            rd.SetDataSource(items);

                            string ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + ".pdf";
                            CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                            dfo.DiskFileName = ffName;
                            rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                            rd.ExportOptions.DestinationOptions = dfo;
                            rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                            rd.Export();
                            rd.Close();
                        }

                        string filename = "/CReports/" + guid.ToString() + ".pdf";

                        return Content(HttpStatusCode.OK, filename);

                    }
                    else
                    {
                        BarcodePrintReport b;
                        foreach (BarcodePrintReport s in barcodelist)
                        {
                            Product prd = null;
                            if (barcodelist[0].type == "CARTON_BARCODE")
                            {
                                prd = prdService.GetProductByCartonBarcode(s.COMPANY_CODE, s.CODE, username, password);
                            }
                            else
                            {
                                prd = prdService.GetProduct(s.COMPANY_CODE, s.SAL_BARCODE, username, password);
                            }
                            if (prd != null)
                            {
                                s.FREE_TEXT4 = prd.PRICE1.ToString("0.00");
                                if (prd.ProductVendorList.Count > 0)
                                {
                                    s.FREE_TEXT3 = prd.ProductVendorList.FirstOrDefault().VENDOR_CODE;
                                }
                                s.DISPLAY_NAME = prd.BARCODE_NAME;
                                s.BRAND_NAME = prd.BRAND_NAME;
                            }

                            if (barcode_print_opt.Equals("4x11v2"))
                            {
                                s.DISPLAY_NAME = s.DISPLAY_NAME.ToUpper().Replace("N/A", "");
                                s.DISPLAY_NAME = s.DISPLAY_NAME.ToUpper().Replace("-", " ");
                            }



                            if (s.type.Equals("default"))
                            {
                                if (s.USER_BARCODE != s.CODE)
                                {
                                    continue;
                                }

                                for (int i = 1; i <= s.PUR_QTY; i++)
                                {
                                    b = new BarcodePrintReport();
                                    b.DISPLAY_NAME = s.DISPLAY_NAME;
                                    b.PUR_QTY = s.PUR_QTY;

                                    b.SAL_PRICE = s.SAL_PRICE;
                                    b.FREE_TEXT1 = s.FREE_TEXT1;
                                    b.FREE_TEXT2 = s.BRAND_NAME;
                                    b.FREE_TEXT4 = s.FREE_TEXT4;
                                    b.FREE_TEXT3 = s.FREE_TEXT3;
                                    b.SAL_BARCODE = s.SAL_BARCODE;
                                    b.CATEGORY_NAME = s.CATEGORY_NAME;
                                    b.SUB_CATEGORY_NAME = s.SUB_CATEGORY_NAME;
                                    b.SUB_SUBCATEGORY_NAME = prd.SUB_SUBCATEGORY_NAME;
                                    b.PACK_SIZE = prd.PACK_SIZE;
                                    b.STYLE_CODE = prd.STYLE_CODE;
                                    b.BRAND_NAME = s.BRAND_NAME;
                                    b.CPU = NumberToStringConvertor(prd.LAST_PUR_PRICE);
                                    var result = writer.Write(b.SAL_BARCODE);
                                    Bitmap barcodeBitmap = new Bitmap(result);
                                    ImageConverter converter = new ImageConverter();
                                    b.BARCODE = (byte[])converter.ConvertTo(barcodeBitmap, typeof(byte[]));

                                    b.MANUFACTURE_DATE = s.MANUFACTURE_DATE;
                                    b.EXPIRE_DATE = s.EXPIRE_DATE;
                                    b.VENDOR_NAME = s.VENDOR_NAME;



                                    if (prd.ProductAttributeList.Count > 0)
                                    {
                                        var pvList = (from p in prd.ProductAttributeList
                                                      where p.SHOW_BARCODE == true
                                                      select p).ToList();

                                        foreach (var pv in pvList)
                                        {
                                            switch (pv.SERIAL)
                                            {
                                                case 1:
                                                    b.Att1 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 2:
                                                    b.Att2 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 3:
                                                    b.Att3 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 4:
                                                    b.Att4 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 5:
                                                    b.Att5 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 6:
                                                    b.Att6 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 7:
                                                    b.Att7 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 8:
                                                    b.Att8 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 9:
                                                    b.Att9 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 10:
                                                    b.Att10 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 11:
                                                    b.Att11 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 12:
                                                    b.Att12 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 13:
                                                    b.Att13 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 14:
                                                    b.Att14 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 15:
                                                    b.Att15 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 16:
                                                    b.Att16 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 17:
                                                    b.Att17 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 18:
                                                    b.Att18 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 19:
                                                    b.Att19 = pv.ATTRIBUTE_NAME;
                                                    break;
                                                case 20:
                                                    b.Att20 = pv.ATTRIBUTE_NAME;
                                                    break;
                                            }
                                        }
                                    }

                                    if (prd.UserBarcodeList.Count > 0)
                                    {
                                        b.FREE_TEXT5 = prd.UserBarcodeList[0];
                                    }

                                    b.PrintDate = CloudPos.Service.DateToStringConverterService.GeneratePrintDateString(DateTime.Today);

                                    b.TEXT1 = prd.FREE_TEXT1;
                                    b.TEXT2 = prd.FREE_TEXT2;
                                    b.TEXT3 = prd.FREE_TEXT3;
                                    b.TEXT4 = prd.FREE_TEXT4;
                                    b.TEXT5 = prd.FREE_TEXT5;

                                    b.SAL_VAT_PERCENT = prd.SAL_VAT_PERCENT;
                                    b.IS_PRICE_INCLD_VAT = prd.IS_PRICE_INCLD_VAT == 1 ? true : false;

                                    items.Add(b);

                                    if (barcode_print_opt.Equals("1x2"))
                                    {
                                        items.Add(b);
                                    }
                                }
                            }
                            else
                            {


                                for (int i = 1; i <= s.PUR_QTY; i++)
                                {
                                    b = new BarcodePrintReport();
                                    b.DISPLAY_NAME = s.DISPLAY_NAME;
                                    b.PUR_QTY = s.PUR_QTY;

                                    b.SAL_PRICE = s.SAL_PRICE;
                                    b.FREE_TEXT1 = s.FREE_TEXT1;
                                    b.FREE_TEXT2 = s.BRAND_NAME;
                                    b.FREE_TEXT4 = s.FREE_TEXT4;
                                    b.FREE_TEXT3 = s.FREE_TEXT3;
                                    b.CATEGORY_NAME = s.CATEGORY_NAME;
                                    b.SUB_CATEGORY_NAME = s.SUB_CATEGORY_NAME;
                                    b.CPU = NumberToStringConvertor(prd.LAST_PUR_PRICE);
                                    if (prd != null)
                                    {
                                        b.SUB_SUBCATEGORY_NAME = prd.SUB_SUBCATEGORY_NAME;
                                        b.PACK_SIZE = prd.PACK_SIZE;
                                        b.STYLE_CODE = prd.STYLE_CODE;
                                    }


                                    b.BRAND_NAME = s.BRAND_NAME;

                                    b.MANUFACTURE_DATE = s.MANUFACTURE_DATE;
                                    b.EXPIRE_DATE = s.EXPIRE_DATE;
                                    b.VENDOR_NAME = s.VENDOR_NAME;

                                    if (s.type.Equals("BARCODE"))
                                    {
                                        b.SAL_BARCODE = s.SAL_BARCODE;
                                        var result = writer.Write(b.SAL_BARCODE);
                                        Bitmap barcodeBitmap = new Bitmap(result);
                                        ImageConverter converter = new ImageConverter();
                                        b.BARCODE = (byte[])converter.ConvertTo(barcodeBitmap, typeof(byte[]));
                                    }
                                    else
                                    {
                                        b.SAL_BARCODE = s.USER_BARCODE;
                                        var result = writer.Write(s.USER_BARCODE);
                                        Bitmap barcodeBitmap = new Bitmap(result);
                                        ImageConverter converter = new ImageConverter();
                                        b.BARCODE = (byte[])converter.ConvertTo(barcodeBitmap, typeof(byte[]));
                                    }

                                    if (prd != null)
                                    {

                                        if (prd.ProductAttributeList.Count > 0)
                                        {
                                            var pvList = (from p in prd.ProductAttributeList
                                                          where p.SHOW_BARCODE == true
                                                          select p).ToList();

                                            foreach (var pv in pvList)
                                            {
                                                switch (pv.SERIAL)
                                                {
                                                    case 1:
                                                        b.Att1 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 2:
                                                        b.Att2 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 3:
                                                        b.Att3 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 4:
                                                        b.Att4 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 5:
                                                        b.Att5 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 6:
                                                        b.Att6 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 7:
                                                        b.Att7 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 8:
                                                        b.Att8 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 9:
                                                        b.Att9 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 10:
                                                        b.Att10 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 11:
                                                        b.Att11 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 12:
                                                        b.Att12 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 13:
                                                        b.Att13 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 14:
                                                        b.Att14 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 15:
                                                        b.Att15 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 16:
                                                        b.Att16 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 17:
                                                        b.Att17 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 18:
                                                        b.Att18 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 19:
                                                        b.Att19 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                    case 20:
                                                        b.Att20 = pv.ATTRIBUTE_NAME;
                                                        break;
                                                }
                                            }
                                        }

                                        if (prd.UserBarcodeList.Count > 0)
                                        {
                                            b.FREE_TEXT5 = prd.UserBarcodeList[0];
                                        }
                                    }


                                    b.PrintDate = CloudPos.Service.DateToStringConverterService.GeneratePrintDateString(DateTime.Today);

                                    if (prd != null)
                                    {
                                        b.TEXT1 = prd.FREE_TEXT1;
                                        b.TEXT2 = prd.FREE_TEXT2;
                                        b.TEXT3 = prd.FREE_TEXT3;
                                        b.TEXT4 = prd.FREE_TEXT4;
                                        b.TEXT5 = prd.FREE_TEXT5;
                                    }

                                    b.SAL_VAT_PERCENT = prd.SAL_VAT_PERCENT;
                                    b.IS_PRICE_INCLD_VAT = prd.IS_PRICE_INCLD_VAT == 1 ? true : false;

                                    items.Add(b);

                                    if (barcode_print_opt.Equals("1x2"))
                                    {
                                        items.Add(b);
                                    }
                                }
                            }
                        }

                        if (items.Count > 0)
                        {
                            #region log

                            var data = items
                                        .GroupBy(l => l.SAL_BARCODE)
                                        .Select(cl => new
                                        {
                                            Barcode = cl.First().SAL_BARCODE,
                                            Qty = cl.Count().ToString()
                                        }).ToList();

                            DateTime dt = DateTime.Now;

                            List<BarcodePrintLog> logList = new List<BarcodePrintLog>();
                            foreach (var d in data)
                            {
                                BarcodePrintLog l = new BarcodePrintLog();

                                l.Barcode = d.Barcode;
                                l.IsShelfTag = barcodelist[0].shelfTag;
                                l.PrintDate = dt;
                                l.Qty = Convert.ToInt32(d.Qty);
                                l.Ref = string.Empty;
                                l.UserName = username;

                                logList.Add(l);
                            }

                            IBarcodePrintLogService logService = new BarcodePrintLogService();
                            Hangfire.BackgroundJob.Enqueue(() => logService.InsertBarcodePrintLog(logList));

                            #endregion


                            Guid guid = Guid.NewGuid();

                            if (barcodelist[0].shelfTag == true)
                            {
                                using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
                                {
                                    rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports") + "/" + "rptShelfTag.rpt");
                                    rd.SetDataSource(items);

                                    string ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + ".pdf";
                                    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                                    dfo.DiskFileName = ffName;
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                                    rd.ExportOptions.DestinationOptions = dfo;
                                    rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                                    rd.Export();
                                    rd.Close();
                                }
                            }
                            else if (barcode_print_opt.Equals("5x13"))
                            {
                                //using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new rptBarcodePrint65())                            
                                using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
                                {
                                    rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports") + "/" + "rptBarcodePrint65.rpt");
                                    rd.SetDataSource(items);

                                    string ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + ".pdf";
                                    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                                    dfo.DiskFileName = ffName;
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                                    rd.ExportOptions.DestinationOptions = dfo;
                                    rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                                    rd.Export();
                                    rd.Close();
                                }
                            }
                            else if (barcode_print_opt.Equals("4x11"))
                            {
                                //using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new rptBarcodePrint44())
                                using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
                                {
                                    rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports") + "/" + "rptBarcodePrint44.rpt");

                                    rd.SetDataSource(items);

                                    string ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + ".pdf";
                                    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                                    dfo.DiskFileName = ffName;
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                                    rd.ExportOptions.DestinationOptions = dfo;
                                    rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                                    rd.Export();
                                    rd.Close();
                                }
                            }
                            else if (barcode_print_opt.Equals("4x11v2"))
                            {
                                //using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new rptBarcodePrint44v2())
                                using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
                                {
                                    rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports") + "/" + "rptBarcodePrint44v2.rpt");

                                    rd.SetDataSource(items);

                                    string ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + ".pdf";
                                    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                                    dfo.DiskFileName = ffName;
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                                    rd.ExportOptions.DestinationOptions = dfo;
                                    rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                                    rd.Export();
                                    rd.Close();
                                }
                            }
                            else if (barcode_print_opt.Equals("1x1"))
                            {
                                //using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new rptBarcodePrint1())
                                using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
                                {
                                    rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports") + "/" + "rptBarcodePrint1.rpt");

                                    rd.SetDataSource(items);

                                    string ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + ".pdf";
                                    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                                    dfo.DiskFileName = ffName;
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                                    rd.ExportOptions.DestinationOptions = dfo;
                                    rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                                    rd.Export();
                                    rd.Close();
                                }
                            }
                            else if (barcode_print_opt.Equals("1x1v2"))
                            {
                                //using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new rptBarcodePrint1())
                                using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
                                {
                                    rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports") + "/" + "rptBarcodePrint1v2.rpt");

                                    rd.SetDataSource(items);

                                    string ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + ".pdf";
                                    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                                    dfo.DiskFileName = ffName;
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                                    rd.ExportOptions.DestinationOptions = dfo;
                                    rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                                    rd.Export();
                                    rd.Close();
                                }
                            }
                            else if (barcode_print_opt.Equals("1x2"))
                            {
                                //using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new rptBarcodePrint1())
                                using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
                                {
                                    rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports") + "/" + "rptBarcodePrint2.rpt");

                                    rd.SetDataSource(items);

                                    string ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + ".pdf";
                                    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                                    dfo.DiskFileName = ffName;
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                                    rd.ExportOptions.DestinationOptions = dfo;
                                    rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                                    rd.Export();
                                    rd.Close();
                                }
                            }
                            else if (barcode_print_opt.Equals("1x2v2"))
                            {
                                //using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new rptBarcodePrint1())
                                using (CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
                                {
                                    rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports") + "/" + "rptBarcodePrint2v2.rpt");

                                    rd.SetDataSource(items);

                                    string ffName = System.Web.HttpContext.Current.Server.MapPath("~/CReports") + "/" + guid.ToString() + ".pdf";
                                    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                                    dfo.DiskFileName = ffName;
                                    rd.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                                    rd.ExportOptions.DestinationOptions = dfo;
                                    rd.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                                    rd.Export();
                                    rd.Close();
                                }
                            }

                            string filename = "/CReports/" + guid.ToString() + ".pdf";

                            return Content(HttpStatusCode.OK, filename);
                        }
                        else
                        {
                            return Content(HttpStatusCode.NotFound, "Preview Print Failed !!!!!");
                        }
                    }


                }
                else
                {
                    Log.Debug("Bad Promotion Request");
                    return Content(HttpStatusCode.BadRequest, "Bad Promotion Request");
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
                //return   NotFound();
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
---------------------------------------
public class BarcodePrintReport
    {
        public string CODE { get; set; }
        public string COMPANY_CODE { get; set; }
        public string USER_BARCODE { get; set; }
        public string SAL_BARCODE { get; set; }
        public string DISPLAY_NAME { get; set; }
        public decimal SAL_PRICE { get; set; }
        public decimal PUR_QTY { get; set; }
        public string FREE_TEXT1 { get; set; }
        public string FREE_TEXT2 { get; set; }
        public string FREE_TEXT3 { get; set; }
        public string FREE_TEXT4 { get; set; }
        public string FREE_TEXT5 { get; set; }
        public byte[] BARCODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string SUB_CATEGORY_NAME { get; set; }
        public string type { get; set; }

        public string EXPIRE_DATE { get; set; }
        public string MANUFACTURE_DATE { get; set; }


        public string Att1 { get; set; }
        public string Att2 { get; set; }
        public string Att3 { get; set; }
        public string Att4 { get; set; }
        public string Att5 { get; set; }
        public string Att6 { get; set; }
        public string Att7 { get; set; }
        public string Att8 { get; set; }
        public string Att9 { get; set; }
        public string Att10 { get; set; }
        public string Att11 { get; set; }
        public string Att12 { get; set; }
        public string Att13 { get; set; }
        public string Att14 { get; set; }
        public string Att15 { get; set; }
        public string Att16 { get; set; }
        public string Att17 { get; set; }
        public string Att18 { get; set; }
        public string Att19 { get; set; }
        public string Att20 { get; set; }
        public string PrintDate { get; set; }
        public bool shelfTag { get; set; }

        public string TEXT1 { get; set; }
        public string TEXT2 { get; set; }
        public string TEXT3 { get; set; }
        public string TEXT4 { get; set; }
        public string TEXT5 { get; set; }

        public string STYLE_CODE { get; set; }        

        public string SUB_SUBCATEGORY_NAME { get; set; }

        public string PACK_SIZE { get; set; }

        public string VENDOR_NAME { get; set; }
        public string CPU { get; set; }

        public bool IS_PRICE_INCLD_VAT { get; set; }

        public decimal SAL_VAT_PERCENT { get; set; }
    }
