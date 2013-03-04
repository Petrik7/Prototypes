using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.App_SrcCode
{
    public class HardCodedData : IGasPurchasesData
    {
        public IEnumerable<GasPurchase> GetPurchasesForMonth(string userName, int month)
        {
            List<GasPurchase> purchases = new List<GasPurchase>();
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, month, 1);
            purchases.Add(new GasPurchase(date, 1.24F, 20.51F, 80));
            purchases.Add(new GasPurchase(date.AddDays(2), 1.23F, 30.88F, 95));
            purchases.Add(new GasPurchase(date.AddDays(3), 1.25F, 28.76F, 85));
            purchases.Add(new GasPurchase(date.AddDays(5), 1.24F, 6.55F, 100));

            return purchases;
        }
    }
}