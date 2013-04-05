using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.App_DataSrc
{
    [Serializable]
    public class GasPurchase
    {
        private DateTime _when;
        private decimal _price;
        private int _amount;
        private int _distance;

        public DateTime When
        {
            get { return _when; }
        }

        public decimal Price
        {
            get { return _price; }
        }

        public int Amount
        {
            get { return _amount; }
        }

        public int Distance
        {
            get { return _distance; }
        }

        public GasPurchase(DateTime when, decimal price, int amount, int distance)
        {
            _when = when;
            _price = price;
            _amount = amount;
            _distance = distance;
        }

    }
}