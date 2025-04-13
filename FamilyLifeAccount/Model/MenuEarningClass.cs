using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyLifeAccount.Model
{
    public class MenuEarningClass
    {
        public string ClassName { get; set; }
        public int ClassID { get; set; }

        public List<MenuEarningClass> Children { get; set; }
    }
}
