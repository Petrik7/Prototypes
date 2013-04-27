using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.App_DataSrc
{
    public class GasData
    {
        public GasData(IGasPurchasesRetriever gasPurchasesData)
        {
            _gasPurchasesData = gasPurchasesData;
        }

        IGasPurchasesRetriever _gasPurchasesData;

        // date-time price amount miles
        public IEnumerable<GasPurchase> GetPurchasesForMonth(string userName, int month, GasPurchase.MilageType type)
        {
            List<GasPurchase> viewPurchases = new List<GasPurchase>();
            viewPurchases.AddRange(_gasPurchasesData.GetPurchasesForMonth(userName, month));
            foreach (GasPurchase purchase in viewPurchases)
                purchase.Type = type;
            return viewPurchases;
        }
    }
}