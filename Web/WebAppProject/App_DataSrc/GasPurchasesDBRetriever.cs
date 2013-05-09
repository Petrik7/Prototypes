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
                purchases.Add(GetGasPurchaseFromRow(purchaceRow));
            }

            return purchases;
        }

        public static GasPurchase GetPurchase(string userName, int purchaseID)
        {
            DataTable purchaseTable = DB.GetPurchase(userName, purchaseID);
            if (purchaseTable.Rows.Count == 0 || purchaseTable.HasErrors)
            {
                return null;
            }

            DataRow purchaceRow = purchaseTable.Rows[0];
            return GetGasPurchaseFromRow(purchaceRow);
        }

        private static GasPurchase GetGasPurchaseFromRow(DataRow purchaceRow)
        {
            int id = (int)purchaceRow[DB.PurchaseTable.ID];
            DateTime date = (DateTime)purchaceRow[DB.PurchaseTable.Date];
            decimal price = (decimal)purchaceRow[DB.PurchaseTable.Price];
            int amount = (int)purchaceRow[DB.PurchaseTable.Amount];
            int distance = (int)purchaceRow[DB.PurchaseTable.Distance];
            GasPurchase.Grade grade = (GasPurchase.Grade)purchaceRow[DB.PurchaseTable.Grade];
            string note = string.Empty;
            object noteObj = purchaceRow[DB.PurchaseTable.Note];
            if (!(noteObj is System.DBNull))
                note = (string)noteObj;

            return new GasPurchase(id, date, price, amount, distance, grade, note);
        }
    }
}