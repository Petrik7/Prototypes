using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebAppProject.App_DataSrc
{
    public class GasPurchasesDBRetriever : IGasPurchasesRetriever
    {
        public IEnumerable<GasPurchase> GetPurchasesForMonth(string userName, int month)
        {
            List<GasPurchase> purchases = new List<GasPurchase>();
            
            DataTable purchasesTable = DB.GetPurchases(userName);
            if (purchasesTable.Rows.Count == 0 || purchasesTable.HasErrors)
                return purchases;
            foreach (DataRow purchaceRow in purchasesTable.Rows)
            {
                DateTime date = (DateTime)purchaceRow[DB.PurchaseTable.Date];
                decimal price = (decimal)purchaceRow[DB.PurchaseTable.Price];
                int amount = (int)purchaceRow[DB.PurchaseTable.Amount];
                int distance = (int)purchaceRow[DB.PurchaseTable.Distance];

                purchases.Add(new GasPurchase(date, price, amount, distance));
            }

            return purchases;
        }
    }
}