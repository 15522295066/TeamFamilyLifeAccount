using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using FamilyLifeAccount.Comm;

namespace FamilyLifeAccount.ViewModel.Share
{
    /// <summary>
    /// 响应式的ViewModel模型
    /// </summary>
    public abstract class ViewModelResponsive : ViewModelBase
    {
        #region Fields

        private ViewModelStatus _status = ViewModelStatus.None;

        #endregion

        #region ViewModel Status

        /// <summary>
        /// 刷新UI数据
        /// </summary>
        public virtual void Refresh()
        {

        }

        /// <summary>
        /// ViewModel状态
        /// </summary>
        public ViewModelStatus Status
        {
            get
            {
                return _status;
            }
            protected set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged(@"Status");
                }
            }
        }

        #endregion
    }

}
