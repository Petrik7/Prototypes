using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.App_SrcCode
{
    [Serializable]
    public class GasPurchase
    {
        private DateTime _when;
        private float _price;
        private float _amount;
        private int _distance;

        public DateTime When
        {
            get { return _when; }
        }

        public float Price
        {
            get { return _price; }
        }

        public float Amount
        {
            get { return _amount; }
        }

        public int Distance
        {
            get { return _distance; }
        }

        public GasPurchase(DateTime when, float price, float amount, int distance)
        {
            _when = when;
            _price = price;
            _amount = amount;
            _distance = distance;
        }

    }
}