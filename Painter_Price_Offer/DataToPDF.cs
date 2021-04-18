using Painter_Price_Offer.Models;
using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace Painter_Price_Offer
{
    public class DataToPDF
    {
        Title windowTitle;
        OwnerModel _Owner = new OwnerModel();
        CustommerModel _Customer = new CustommerModel();
        List<WorkflowModel> _Workfow = new List<WorkflowModel>();
        List<ConsuptionModel> _Consuption = new List<ConsuptionModel>();
        string _Save = "";
        int _WSum = 0;
        int _CSum = 0;
        int _TotalSum = 0;
        string _Title = "";

        public DataToPDF(OwnerModel owner, CustommerModel customer, List<WorkflowModel> workfow, List<ConsuptionModel> consuption, string savePath, int wSum, int cSum, int totalSum)
        {
            this._Owner = owner;
            this._Customer = customer;
            this._Workfow = workfow;
            this._Consuption = consuption;
            this._Save = savePath;
            this._WSum = wSum;
            this._CSum = cSum;
            this._TotalSum = totalSum;
        }

        public void Print()
        {
            windowTitle = new Title();
            windowTitle.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowTitle.ShowDialog();
            _Title = windowTitle._title;
            //_Title = "teszt";//Tesztelés alatt!

            try
            {
                if (!string.IsNullOrEmpty(_Title))
                {
                    //Nyomtatás!!!
                    string savePath = _Save;

                    Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 40, 20);
                    BaseFont bffont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                    iTextSharp.text.Font TitleFont = new iTextSharp.text.Font(bffont, 20, 1);
                    iTextSharp.text.Font SubTitleFont = new iTextSharp.text.Font(bffont, 11, 1);
                    iTextSharp.text.Font DataFont = new iTextSharp.text.Font(bffont, 10, 0);

                    PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(_Save + @"\" + _Title + ".pdf", FileMode.Create, FileAccess.Write));
                    doc.Open();

                    PdfPTable table = new PdfPTable(1);
                    PdfPCell cell = new PdfPCell(new Phrase(_Owner._title, TitleFont));

                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    cell.Padding = 15;
                    cell.BorderWidth = 1; //0

                    table.AddCell(cell);
                    doc.Add(table);

                    //**************************************************************************************************************
                    //Munkavállaló adatai:

                    PdfPTable stamp = new PdfPTable(1);
                    PdfPCell stampCell = new PdfPCell(new Phrase("Bélyegző helye:", new iTextSharp.text.Font(bffont, 8, 1)));
                    stamp.AddCell(stampCell);

                    table = new PdfPTable(3);

                    PdfPCell stampPlace = new PdfPCell(stamp);
                    stampPlace.Rowspan = 5;
                    stampPlace.BorderWidth = 1;
                    table.AddCell(stampPlace);

                    table.AddCell(new PdfPCell(new Phrase(" ", DataFont)) { Colspan = 2 });
                    table.AddCell(new PdfPCell(new Phrase("Név:", DataFont)));
                    table.AddCell(new PdfPCell(new Phrase(_Owner._name, DataFont)));
                    table.AddCell(new PdfPCell(new Phrase("Cím:", DataFont)));
                    table.AddCell(new PdfPCell(new Phrase(_Owner._location, DataFont)));
                    table.AddCell(new PdfPCell(new Phrase("Tel:", DataFont)));
                    table.AddCell(new PdfPCell(new Phrase(_Owner._phoneNumber, DataFont)));
                    table.AddCell(new PdfPCell(new Phrase("Email:", DataFont)));
                    table.AddCell(new PdfPCell(new Phrase(_Owner._email, DataFont)));

                    table.SetWidthPercentage(new float[3] { 200, 36, 240f }, new Rectangle(iTextSharp.text.PageSize.A4));
                    table.SpacingAfter = 15f;
                    doc.Add(table);

                    //**************************************************************************************************************
                    //Megrendelő adatai:

                    if (_Customer._isActive)//Ha a checkBox ki van pipálva
                    {
                        table = new PdfPTable(2);

                        PdfPCell customerCell = new PdfPCell(new PdfPCell(new Phrase("Megrendelő adatai:", SubTitleFont)) { BorderWidth = 0f });
                        customerCell.Colspan = 2;
                        customerCell.PaddingBottom = 10f;
                        table.AddCell(customerCell);
                        int subtitleColumnWidth = 36;

                        if (!string.IsNullOrEmpty(_Customer._name))
                        {
                            table.AddCell(new PdfPCell(new Phrase("Név:", DataFont)));
                            table.AddCell(new PdfPCell(new Phrase(_Customer._name, DataFont)));
                        }
                        if (!string.IsNullOrEmpty(_Customer._location))
                        {
                            table.AddCell(new PdfPCell(new Phrase("Cím:", DataFont)));
                            table.AddCell(new PdfPCell(new Phrase(_Customer._location, DataFont)));
                        }
                        if (!string.IsNullOrEmpty(_Customer._workPlace))
                        {
                            table.AddCell(new PdfPCell(new Phrase("Munka megnevezése:", DataFont)));
                            table.AddCell(new PdfPCell(new Phrase(_Customer._workPlace, DataFont)));
                            subtitleColumnWidth = 100;
                        }
                        if (!string.IsNullOrEmpty(_Customer._phoneNumber))
                        {
                            table.AddCell(new PdfPCell(new Phrase("Tel:", DataFont)));
                            table.AddCell(new PdfPCell(new Phrase(_Customer._phoneNumber, DataFont)));
                        }
                        if (!string.IsNullOrEmpty(_Customer._email))
                        {
                            table.AddCell(new PdfPCell(new Phrase("Email:", DataFont)));
                            table.AddCell(new PdfPCell(new Phrase(_Customer._email, DataFont)));
                        }

                        table.SetWidthPercentage(new float[2] { subtitleColumnWidth, 440f }, new Rectangle(iTextSharp.text.PageSize.A4));
                        table.SpacingAfter = 15f;
                        doc.Add(table);
                    }

                    //**************************************************************************************************************
                    //Munkadíj:

                    List<string> getDataWorkflow = new List<string>();

                    foreach (var item in _Workfow)
                    {
                        if (!string.IsNullOrEmpty(item._Megnevezés.ToString()) && _Workfow.Count > 0)
                            getDataWorkflow.Add(item._Megnevezés);
                    }

                    if (_Workfow.Count > 0 && getDataWorkflow.Count > 0)
                    {
                        table = new PdfPTable(5);
                        table.AddCell(new PdfPCell(new Phrase("Munkadíj:", SubTitleFont)) { Colspan = 5, PaddingBottom = 10f, BorderWidth = 0 });
                        table.AddCell(new PdfPCell(new Phrase("MEGNEVEZÉS", SubTitleFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table.AddCell(new PdfPCell(new Phrase("MENNYISÉG", SubTitleFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table.AddCell(new PdfPCell(new Phrase(" ", DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table.AddCell(new PdfPCell(new Phrase("EGYSÉGÁR", SubTitleFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table.AddCell(new PdfPCell(new Phrase("MUNKADÍJ", SubTitleFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY, MinimumHeight = 15f, PaddingBottom = 5f });

                        foreach (var data in _Workfow)
                        {

                            table.AddCell(new PdfPCell(new Phrase(data._Megnevezés, DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 0 });
                            table.AddCell(new PdfPCell(new Phrase(data._Mennyiség + " " + data._Fm_m2, DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1 });
                            table.AddCell(new PdfPCell(new Phrase("*", DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1 });
                            table.AddCell(new PdfPCell(new Phrase(data._Egységár + " Ft/" + data._Fm_m2, DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1 });
                            table.AddCell(new PdfPCell(new Phrase(data._Munkadíj + " Ft", DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1 });

                        }

                        //Összegzés
                        table.AddCell(new PdfPCell(new Phrase("Munkadíj öszesen: ", DataFont)) { Colspan = 4, HorizontalAlignment = 2, PaddingTop = 10f, BorderWidth = 0 });
                        table.AddCell(new PdfPCell(new Phrase(_WSum.ToString() + " Ft", SubTitleFont)) { HorizontalAlignment = 0, BorderWidth = 0, PaddingTop = 10f });

                        table.SetWidthPercentage(new float[5] { 236f, 70f, 10f, 70f, 90f }, new Rectangle(iTextSharp.text.PageSize.A4));
                        doc.Add(table);
                    }
                    getDataWorkflow.Clear();

                    //**************************************************************************************************************
                    //Anyagdíj:

                    List<string> getDataConsuption = new List<string>();

                    foreach (var item in _Consuption)
                    {
                        if (!string.IsNullOrEmpty(item._Megnevezés.ToString()) && _Consuption.Count > 0)
                            getDataConsuption.Add(item._Megnevezés);
                    }

                    if (_Consuption.Count > 0 && getDataConsuption.Count > 0)
                    {
                        table = new PdfPTable(5);
                        table.AddCell(new PdfPCell(new Phrase("Anyagdíj:", SubTitleFont)) { Colspan = 5, PaddingBottom = 10f, BorderWidth = 0 });
                        table.AddCell(new PdfPCell(new Phrase("MEGNEVEZÉS", SubTitleFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table.AddCell(new PdfPCell(new Phrase("MENNYISÉG", SubTitleFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table.AddCell(new PdfPCell(new Phrase(" ", DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table.AddCell(new PdfPCell(new Phrase("EGYSÉGÁR", SubTitleFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table.AddCell(new PdfPCell(new Phrase("ANYAGDÍJ", SubTitleFont)) { VerticalAlignment = 1, HorizontalAlignment = 1, BackgroundColor = BaseColor.LIGHT_GRAY, MinimumHeight = 15f, PaddingBottom = 5f });

                        foreach (var data in _Consuption)
                        {
                            if (!string.IsNullOrEmpty(data._Megnevezés.ToString()))
                            {
                                table.AddCell(new PdfPCell(new Phrase(data._Megnevezés, DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 0 });
                                table.AddCell(new PdfPCell(new Phrase(data._Mennyiség + " db", DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1 });
                                table.AddCell(new PdfPCell(new Phrase("*", DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1 });
                                table.AddCell(new PdfPCell(new Phrase(data._Egységár + " Ft/db", DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1 });
                                table.AddCell(new PdfPCell(new Phrase(data._Anyagdíj + " Ft", DataFont)) { VerticalAlignment = 1, HorizontalAlignment = 1 });
                            }
                        }

                        //Összegzés
                        table.AddCell(new PdfPCell(new Phrase("Anyagdíj öszesen: ", DataFont)) { Colspan = 4, HorizontalAlignment = 2, PaddingTop = 10f, BorderWidth = 0 });
                        table.AddCell(new PdfPCell(new Phrase(_CSum.ToString() + " Ft", SubTitleFont)) { HorizontalAlignment = 0, BorderWidth = 0, PaddingTop = 10f });

                        table.SetWidthPercentage(new float[5] { 236f, 70f, 10f, 70f, 90f }, new Rectangle(iTextSharp.text.PageSize.A4));
                        doc.Add(table);
                    }
                    getDataConsuption.Clear();

                    //**************************************************************************************************************
                    //Keltezés:

                    table = new PdfPTable(3);
                    if (_WSum > 0)
                    {
                        table.AddCell(new PdfPCell(new Phrase(" ", DataFont)) { BorderWidth = 0 });
                        table.AddCell(new PdfPCell(new Phrase("Munkadíj öszesen: ", DataFont)) { HorizontalAlignment = 2, BorderWidth = 0 });
                        table.AddCell(new PdfPCell(new Phrase(_WSum.ToString() + " Ft", DataFont)) { HorizontalAlignment = 0, BorderWidth = 0 });
                        _TotalSum += _WSum;
                    }
                    else
                        table.AddCell(new PdfPCell(new Phrase(" ", DataFont)) { Colspan = 3, BorderWidth = 0 });

                    if (_CSum > 0)
                    {
                        table.AddCell(new PdfPCell(new Phrase(" ", DataFont)) { BorderWidth = 0 });
                        table.AddCell(new PdfPCell(new Phrase("Anyagdíj öszesen: ", DataFont)) { HorizontalAlignment = 2, BorderWidth = 0 });
                        table.AddCell(new PdfPCell(new Phrase(_CSum.ToString() + " Ft", DataFont)) { HorizontalAlignment = 0, BorderWidth = 0 });
                        _TotalSum += _CSum;
                    }
                    else
                        table.AddCell(new PdfPCell(new Phrase(" ", DataFont)) { Colspan = 3, BorderWidth = 0 });

                    table.AddCell(new PdfPCell(new Phrase("Az árajánlat végösszege az áfát nem tartalmazza!", SubTitleFont)) { BorderWidth = 0 });
                    table.AddCell(new PdfPCell(new Phrase("A végösszeg: ", SubTitleFont)) { HorizontalAlignment = 2, BorderWidth = 0 });
                    table.AddCell(new PdfPCell(new Phrase(_TotalSum.ToString() + " Ft", SubTitleFont)) { HorizontalAlignment = 0, BorderWidth = 0 });

                    table.AddCell(new PdfPCell(new Phrase(" ", DataFont)) { BorderWidth = 0f });
                    table.AddCell(new PdfPCell(new Phrase(" ", DataFont)) { Colspan = 2, BorderWidth = 0, BorderWidthBottom = 1f, PaddingTop = 30f });

                    table.AddCell(new PdfPCell(new Phrase("Ászár, " + DateTime.Now.ToString("yyyy.M.dd"), DataFont)) { BorderWidth = 0 });
                    table.AddCell(new PdfPCell(new Phrase("Aláírás", DataFont)) { Colspan = 2, BorderWidth = 0f, HorizontalAlignment = 1 });


                    table.KeepTogether = true;
                    table.SpacingBefore = 30f;
                    table.SetWidthPercentage(new float[3] { 296f, 90f, 90f }, new Rectangle(iTextSharp.text.PageSize.A4));
                    doc.Add(table);

                    doc.Close();

                    _WSum = 0;
                    _CSum = 0;
                    _TotalSum = 0;

                    Process.Start(_Save + @"\" + _Title + ".pdf");
                }
        }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
}
    }
}