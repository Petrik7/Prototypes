using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppProject.App_DataSrc;


namespace WebAppProject
{
    public class GradeUiMapping
    {
        private SortedDictionary<int, KeyValuePair<GasPurchase.Grade, string>> _indexToGradeAndText = new SortedDictionary<int, KeyValuePair<GasPurchase.Grade, string>>();
        private SortedDictionary<GasPurchase.Grade, int> _gradeToIndex = new SortedDictionary<GasPurchase.Grade, int>();

        public GradeUiMapping()
        {
            InitGradeMaps();
        }

        public void PopulateGradeDropDownList(System.Web.UI.WebControls.DropDownList dropDownListGrade)
        {
            foreach (KeyValuePair<GasPurchase.Grade, string> grade in _indexToGradeAndText.Values)
                dropDownListGrade.Items.Add(grade.Value);
        }

        public int GradeToIndex(GasPurchase.Grade gradeInt)
        {
            GasPurchase.Grade grade = (GasPurchase.Grade)gradeInt;
            return _gradeToIndex[grade];
        }

        public GasPurchase.Grade IndexToGrade(int index)
        {
            return _indexToGradeAndText[index].Key; 
        }

        private void InitGradeMaps()
        {
            int index = 0;
            _indexToGradeAndText.Add(index++, new KeyValuePair<GasPurchase.Grade, string>(GasPurchase.Grade.MidGrade, "Mid-grade(89)")); // Will be selected By Default
            _indexToGradeAndText.Add(index++, new KeyValuePair<GasPurchase.Grade, string>(GasPurchase.Grade.Regular, "Regular (87)"));
            _indexToGradeAndText.Add(index++, new KeyValuePair<GasPurchase.Grade, string>(GasPurchase.Grade.Premium, "Premium (92)"));
            _indexToGradeAndText.Add(index++, new KeyValuePair<GasPurchase.Grade, string>(GasPurchase.Grade.Diesel, "Diesel"));

            foreach(KeyValuePair<int, KeyValuePair<GasPurchase.Grade, string>> indexToGrade in _indexToGradeAndText)
            {
                _gradeToIndex.Add(indexToGrade.Value.Key, indexToGrade.Key);
            }
        }




    }
}