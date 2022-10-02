private void btnPreview_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Barcode = txtBarcode.Text.Trim();

            decimal qty = 0;


            #region Barcode Print

            List<BarcodePrintReport> Barcodeitems = new List<BarcodePrintReport>();
            ZXing.Common.EncodingOptions option = new ZXing.Common.EncodingOptions();
            option.Height = 40;
            option.Width = 120;
            option.PureBarcode = true;
            //option.PureBarcode = false;
            ZXing.IBarcodeWriter writer = new ZXing.BarcodeWriter { Format = ZXing.BarcodeFormat.CODE_128, Options = option };
            

            BarcodePrintReport b;
            Product prd = null;
            ProductStock stk = ProductStockController.GetProductStockListByBarcode(GlobalClass.Terminal.COMPANY_CODE, GlobalClass.Terminal.Store_Code, Barcode, false).FirstOrDefault();
            if (stk == null)
            {
                MessageBox.Show("Invalid Barcode!!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            prd = ProductController.GetProductForBarcode(GlobalClass.Terminal.COMPANY_CODE, GlobalClass.Terminal.Store_Code, stk.BARCODE).FirstOrDefault();


            decimal count = qty;

            if (prd != null)
            {
                for (int i = 1; i <= count; i++)
                {
                    b = new BarcodePrintReport();
                    b.DISPLAY_NAME = prd.DISPLAY_NAME;
                    b.SAL_PRICE = prd.MRP;
                    b.FREE_TEXT1 = prd.COMPANY_NAME;
                    b.FREE_TEXT2 = prd.BRAND_NAME;
                    b.FREE_TEXT4 = prd.PRICE1.ToString("0.00"); ;
                    b.FREE_TEXT3 = prd.VENDOR_CODE;//prd.ProductVendorList.FirstOrDefault().VENDOR_CODE;
                    b.CATEGORY_NAME = prd.CATEGORY_NAME;
                    b.SUB_CATEGORY_NAME = prd.SUB_CATEGORY_NAME;
                    b.BRAND_NAME = prd.BRAND_NAME;
                    b.SAL_BARCODE = Barcode;
                    var result = writer.Write(Barcode);
                    Bitmap barcodeBitmap = new Bitmap(result);
                    
                    if (rbLandscape.Checked) ////if need landscape
                    {
                        barcodeBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone); ///  add this line for landscape the image
                    }
                    
                    ImageConverter converter = new ImageConverter();
                    b.BARCODE = (byte[])converter.ConvertTo(barcodeBitmap, typeof(byte[]));
                    

                  
            }
            if (Barcodeitems.Count > 0)
            {
                //ReportDocument rpt = new ReportDocument();

                //rpt.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\rptSerialTrackingBarcodePrint.rpt");
                //rpt.SetDataSource(Barcodeitems);
                //rpt.PrintToPrinter(1, false, 0, 0);

                if (rbPortrait.Checked)
                {
                    ReportViewerForm reportViewer = new ReportViewerForm();
                    reportViewer.PreviewBarcode(Barcodeitems);    
                }
                else if (rbLandscape.Checked)
                {
                    ReportViewerForm reportViewer = new ReportViewerForm();
                    reportViewer.PreviewBarcodeLandscape(Barcodeitems);
                }
            }
            else
            {

            }
            Cursor.Current = Cursors.Default;
            #endregion

        }
