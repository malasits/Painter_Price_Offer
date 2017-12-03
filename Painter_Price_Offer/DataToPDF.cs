using Painter_Price_Offer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows;
using System.IO;

namespace Painter_Price_Offer
{
    public class DataToPDF
    {
        OwnerModel _Owner = new OwnerModel();
        CustommerModel _Customer = new CustommerModel();
        List<WorkflowModel> _Workfow = new List<WorkflowModel>();
        List<ConsuptionModel> _Consuption = new List<ConsuptionModel>();

        public DataToPDF(OwnerModel owner, CustommerModel customer, List<WorkflowModel> workfow, List<ConsuptionModel> consuption)
        {
            this._Owner = owner;
            this._Customer = customer;
            this._Workfow = workfow;
            this._Consuption = consuption;
        }

        public void Print()
        {
            //Nyomtatás!!!

            Document doc = new Document(iTextSharp.text.PageSize.A4, 25, 25, 20, 10);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Test.pdf",FileMode.Create,FileAccess.Write));
            doc.Open();
            doc.AddTitle(_Owner._title);
            Paragraph p = new Paragraph(_Owner._name);
            doc.Add(new Header("asd", "asd"));
            doc.Add(new Paragraph(_Owner._phoneNumber));
            doc.Add(new Paragraph(_Owner._email));
            doc.Add(p);
            doc.Close();
        }
    }
}