using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FamilyLifeAccount.Model
{
    public class MenuCostClass
    {
        public string ClassName { get; set; }
        public int ClassID { get; set; }

        public List<MenuCostClass> Children{ get; set; }


    }
}
