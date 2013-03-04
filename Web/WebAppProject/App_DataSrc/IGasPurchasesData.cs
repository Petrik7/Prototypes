using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAppProject.App_SrcCode
{
    public interface IGasPurchasesData
    {
        IEnumerable<GasPurchase> GetPurchasesForMonth(string userName, int month);
    }
}
