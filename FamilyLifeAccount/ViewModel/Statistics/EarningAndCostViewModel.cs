using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using DataFactory.DAL;
using DataFactory.MODEL;
using System.Collections.ObjectModel;

namespace FamilyLifeAccount.ViewModel.Statistics
{
    public class EarningAndCostViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the CostClassChartViewModel class.
        /// </summary>
        public EarningAndCostViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
            }
            else
            {
                // Code runs "for real": Connect to service, etc...
            }
        }

        DALBase dal = new DALBase();

        private void InitLoad()
        {
            HeadLabel = dal.GetOneModel<navigation>(m => m.NavigationID.Equals(11)).Title;
            AccountList = dal.GetList<account>();
            PersionList = new ObservableCollection<persons>(dal.GetList<persons>().OrderByDescending(m=>m.UserID));
        }

        #region 属性初始化

        private ObservableCollection<persons> _PersionsList;
        public ObservableCollection<persons> PersionList
        {
            get { return _PersionsList; }
            set
            {
                _PersionsList = value;
                this.RaisePropertyChanged("PersionList");
            }
        }

        private List<account> _AccountList;
        public List<account> AccountList
        {
            get { return _AccountList; }
            set
            {
                _AccountList = value;
                this.RaisePropertyChanged("AccountList");
            }
        }

        private string _HeadLabel;
        public string HeadLabel
        {
            get { return _HeadLabel; }
            set
            {
                _HeadLabel = value;
                this.RaisePropertyChanged("HeadLable");
            }
        }

        #endregion

    }
}
