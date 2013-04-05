using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAppProject.App_DataSrc
{
    public interface IGasPurchasesRetriever
    {
        IEnumerable<GasPurchase> GetPurchasesForMonth(string userName, int month);
    }
}
