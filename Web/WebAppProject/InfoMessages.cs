using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject
{
    public class InfoMessages
    {
        private static int baseId = 1003;

        public static readonly int PurchaseNotFound                 = ++baseId;
        public static readonly int PurchaseWasSuccessfullyModified  = ++baseId;
        public static readonly int PurchaseWasNotModified           = ++baseId;
        public static readonly int PurchaseWasSuccessfullyDeleted   = ++baseId;
        
    }
}