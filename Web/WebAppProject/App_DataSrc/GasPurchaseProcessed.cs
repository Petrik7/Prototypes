using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.App_SrcCode
{
    public class GasPurchaseProcessed
    {
        private DateTime _when;
        private float _price;
        private float _amount;
        private int _distance;

        public enum MilageType {LitersPerKm, Miles};
        private MilageType _type;

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

        public float Milage
        {
            get 
            {
                if (_type == MilageType.LitersPerKm)
                    return (_amount * 100) / _distance;
                else
                    return ((_distance / 1.6F) * 3.785F) / _amount;
            }
        }

        public GasPurchaseProcessed(DateTime when, float price, float amount, int distance, MilageType type = MilageType.LitersPerKm)
        {
            _type = type;
            _when = when;
            _price = price;
            _amount = amount;
            _distance = distance;
        }

        public GasPurchaseProcessed(GasPurchase purchase, MilageType type = MilageType.LitersPerKm)
        {
            _type = type;
            _when = purchase.When;
            _price = purchase.Price;
            _amount = purchase.Amount;
            _distance = purchase.Distance;
        }

    }
}