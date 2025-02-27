using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyLifeAccount.Comm
{
    public class Notifications
    {
        #region 公共消息名称


        public static readonly string AddShow = Guid.NewGuid().ToString();
        public static readonly string UpdateShow = Guid.NewGuid().ToString();
        public static readonly string Close = Guid.NewGuid().ToString();
        public static readonly string Succeed = Guid.NewGuid().ToString();
        public static readonly string Error = Guid.NewGuid().ToString();
        public static readonly string Query = Guid.NewGuid().ToString();
        public static readonly string ChildrenShow = Guid.NewGuid().ToString();
        public static readonly string Submit = Guid.NewGuid().ToString();
        public static readonly string YearPayShow = Guid.NewGuid().ToString();
        public static readonly string TeamPayShow = Guid.NewGuid().ToString();
        public static readonly string Refresh = Guid.NewGuid().ToString();
        public static readonly string Cancel = Guid.NewGuid().ToString();
        public static readonly string Parameter = Guid.NewGuid().ToString();
        #endregion
    }
}
