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
        public IEnumerable<GasPurchaseProcessed> GetPurchasesForMonth(string userName, int month, GasPurchaseProcessed.MilageType type)
        {
            List<GasPurchaseProcessed> viewPurchases = new List<GasPurchaseProcessed>();
            
            foreach (GasPurchase purchase in _gasPurchasesData.GetPurchasesForMonth(userName, month))
            {
                viewPurchases.Add(new GasPurchaseProcessed(purchase, type));
            }

            return viewPurchases;
        }
    }
}