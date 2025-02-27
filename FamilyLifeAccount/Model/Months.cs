using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyLifeAccount.Model
{
    public class Months
    {
        public int Index { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int year { get; set; }


        public static List<Months> GetOneYear(int year)
        {
            List<Months> list = new List<Months> {
                new Months{ Index=1, year=year, startDate="1-1", endDate="1-31"},
                new Months{ Index=2, year=year,startDate="2-1", endDate="2-28"},
                new Months{ Index=3, year=year,startDate="3-1", endDate="3-31"},
                new Months{ Index=4, year=year,startDate="4-1", endDate="4-30"},
                new Months{ Index=5, year=year,startDate="5-1", endDate="5-31"},
                new Months{ Index=6, year=year,startDate="6-1", endDate="6-30"},
                new Months{ Index=7, year=year,startDate="7-1", endDate="7-31"},
                new Months{ Index=8, year=year,startDate="8-1", endDate="8-31"},
                new Months{ Index=9, year=year,startDate="9-1", endDate="9-30"},
                new Months{ Index=10, year=year,startDate="10-1", endDate="10-31"},
                new Months{ Index=11, year=year,startDate="11-1", endDate="11-30"},
                new Months{ Index=12, year=year,startDate="12-1", endDate="12-31"},
              
        };
            return list;
        }
    }
}