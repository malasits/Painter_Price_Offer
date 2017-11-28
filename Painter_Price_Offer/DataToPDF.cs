using Painter_Price_Offer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}