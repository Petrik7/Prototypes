using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.App_DataSrc
{
    [Serializable]
    public class GasPurchase
    {
        private int _id;
        private DateTime _when;
        private decimal _price;
        private int _amount;
        private int _distance;
        Grade _grade;
        string _note;

        public enum Grade { Diesel = 1, Regular = 87, MidGrade = 89, Premium = 92};
        public enum MilageType { LitersPerKm, Miles };
        private MilageType _type;

        public int ID
        {
            get { return _id; }
        }

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

        public MilageType Type
        {
            set { _type = value; }
            get { return _type; }
        }

        public Grade GradeOfFuel
        {
            get { return _grade; }
        }

        public string Note
        {
            get { return _note; }
        }

        public float Milage
        {
            get
            {
                if (_distance == 0)
                    _distance = 1;
                if (_type == MilageType.LitersPerKm)
                    return (_amount * 100) / _distance;
                else
                    return ((_distance / 1.6F) * 3.785F) / _amount;
            }
        }

        public GasPurchase(
            int id, 
            DateTime when, 
            decimal price, 
            int amount, 
            int distance, 
            Grade grade,
            string note,
            MilageType type = MilageType.LitersPerKm)
        {
            _id = id;
            _type = type;
            _when = when;
            _price = price;
            _amount = amount;
            _distance = distance;
            _grade = grade;
            _note = note;
        }

    }
}