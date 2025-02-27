using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;
using DataFactory.MODEL;
using DataFactory.DAL;

namespace FamilyLifeAccount.ConvertFormart
{
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class ConvertSettings : IValueConverter
    {
        #region IValueConverter 成员

        DALBase dal = new DALBase();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int id = System.Convert.ToInt32(value);
                return dal.GetOneModel<costclass>(m => m.CostClassID.Equals(id)).ClassName;
            }
            catch {
                return "/";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "/";
        }

        #endregion
    }
}
