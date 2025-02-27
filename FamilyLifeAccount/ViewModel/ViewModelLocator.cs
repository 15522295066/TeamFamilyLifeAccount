using GalaSoft.MvvmLight;
using FamilyLifeAccount.ViewModel.EveryDay;
using FamilyLifeAccount.ViewModel.Settings;
using FamilyLifeAccount.ViewModel.Statistics;

namespace FamilyLifeAccount.ViewModel
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view models
            }
            else
            {
                CreateMain();
            }
        }

        #region 主框架初始化

        private static MainViewModel _main;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static MainViewModel MainStatic
        {
            get
            {
                if (_main == null)
                {
                    CreateMain();
                }
                return _main;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearMain()
        {
            _main.Cleanup();
            _main.Dispose();
            _main = null;
            
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateMain()
        {
            if (_main == null)
            {
                _main = new MainViewModel();
            }
        }
        #endregion

        #region 公司记账

        #region 支付主界面

        private static CompanyCostMainViewModel _companyCostMainViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static CompanyCostMainViewModel CompanyCostMainStatic
        {
            get
            {
                if (_companyCostMainViewModel == null)
                {
                    CreateCompanyCostMain();
                }
                return _companyCostMainViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CompanyCostMainViewModel CompanyCostMainLocator
        {
            get
            {
                return CompanyCostMainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearCompanyCostMain()
        {
            _companyCostMainViewModel.Cleanup();
            _companyCostMainViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateCompanyCostMain()
        {
            if (_companyCostMainViewModel == null)
            {
                _companyCostMainViewModel = new CompanyCostMainViewModel();
            }
        }

        #endregion

        #region 编辑/添加 支付界面

        private static CompanyEditCostMainViewModel _companyEditCostMainViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static CompanyEditCostMainViewModel CompanyEditCostMainStatic
        {
            get
            {
                if (_companyEditCostMainViewModel == null)
                {
                    CreateCompanyEditCostMain();
                }

                return _companyEditCostMainViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CompanyEditCostMainViewModel CompanyEditCostMainLocator
        {
            get
            {
                return CompanyEditCostMainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearCompanyEditCostMain()
        {
            _companyEditCostMainViewModel.Cleanup();
            _companyEditCostMainViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateCompanyEditCostMain()
        {
            if (_companyEditCostMainViewModel == null)
            {
                _companyEditCostMainViewModel = new CompanyEditCostMainViewModel();
            }
        }

        #endregion

        #region 收入
        private static CompanyEarningViewModel _CompanyEarningViewModel;

        public static CompanyEarningViewModel CompanyEarningStatic
        {
            get
            {
                if (_CompanyEarningViewModel == null)
                {
                    CreateCompanyEarningViewModel();
                }
                return _CompanyEarningViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CompanyEarningViewModel CompanyEarningLocator
        {
            get
            {
                return CompanyEarningStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearCompanyEarningViewModel()
        {
            if (_CompanyEarningViewModel == null)
            {
                return;
            }

            _CompanyEarningViewModel.Cleanup();
            _CompanyEarningViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateCompanyEarningViewModel()
        {
            if (_CompanyEarningViewModel == null)
            {
                _CompanyEarningViewModel = new CompanyEarningViewModel();
            }
        }
        #endregion

        #region 编辑/添加 收入

        private static CompanyEditEarningViewModel _companyEditEarningViewModel;

        public static CompanyEditEarningViewModel CompanyEditEarningStatic
        {
            get
            {
                if (_companyEditEarningViewModel == null)
                {
                    CreateCompanyEditEarningViewModel();
                }
                return _companyEditEarningViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CompanyEditEarningViewModel CompanyEditEarningLocator
        {
            get
            {
                return CompanyEditEarningStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearCompanyEditEarningViewModel()
        {
            if (_companyEditEarningViewModel == null)
            {
                return;
            }

            _companyEditEarningViewModel.Cleanup();
            _companyEditEarningViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateCompanyEditEarningViewModel()
        {
            if (_companyEditEarningViewModel == null)
            {
                _companyEditEarningViewModel = new CompanyEditEarningViewModel();
            }
        }
        #endregion

        #endregion


        #region 生活记账模块
        #region 支付主界面

        private static CostMainViewModel _costMainViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static CostMainViewModel CostMainStatic
        {
            get
            {
                if (_costMainViewModel == null)
                {
                    CreateCostMain();
                }
                return _costMainViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CostMainViewModel CostMainLocator
        {
            get
            {
                return CostMainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearCostMain()
        {
            _costMainViewModel.Cleanup();
            _costMainViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateCostMain()
        {
            if (_costMainViewModel == null)
            {
                _costMainViewModel = new CostMainViewModel();
            }
        }

        #endregion

        #region 编辑/添加 支付界面

        private static EditCostMainViewModel _editCostMainViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static EditCostMainViewModel EditCostMainStatic
        {
            get
            {
                if (_editCostMainViewModel == null)
                {
                    CreateEditCostMain();
                }

                return _editCostMainViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditCostMainViewModel EditCostMainLocator
        {
            get
            {
                return EditCostMainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearEditCostMain()
        {
            _editCostMainViewModel.Cleanup();
            _editCostMainViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateEditCostMain()
        {
            if (_editCostMainViewModel == null)
            {
                _editCostMainViewModel = new EditCostMainViewModel();
            }
        }

        #endregion

        #region 收入
        private static EarningViewModel _earningViewModel;

        public static EarningViewModel EarningStatic
        {
            get
            {
                if (_earningViewModel == null)
                {
                    CreateEarningViewModel();
                }
                return _earningViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EarningViewModel EarningLocator
        {
            get
            {
                return EarningStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearEarningViewModel()
        {
            if (_earningViewModel == null)
            {
                return;
            }

            _earningViewModel.Cleanup();
            _earningViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateEarningViewModel()
        {
            if (_earningViewModel == null)
            {
                _earningViewModel = new EarningViewModel();
            }
        }
        #endregion

        #region 编辑/添加 收入

        private static EditEarningViewModel _editEarningViewModel;

        public static EditEarningViewModel EditEarningStatic
        {
            get
            {
                if (_editEarningViewModel == null)
                {
                    CreateEditEarningViewModel();
                }
                return _editEarningViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditEarningViewModel EditEarningLocator
        {
            get
            {
                return EditEarningStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearEditEarningViewModel()
        {
            if (_editEarningViewModel == null)
            {
                return;
            }

            _editEarningViewModel.Cleanup();
            _editEarningViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateEditEarningViewModel()
        {
            if (_editEarningViewModel == null)
            {
                _editEarningViewModel = new EditEarningViewModel();
            }
        }
        #endregion


        #region 红岭回款
        private static HonglingViewModel _honglingViewModel;

        public static HonglingViewModel HonglingStatic
        {
            get
            {
                if (_honglingViewModel == null)
                {
                    CreateHonglingViewModel();
                }
                return _honglingViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public HonglingViewModel HonglingLocator
        {
            get
            {
                return HonglingStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearHonglingViewModel()
        {
            if (_honglingViewModel == null)
            {
                return;
            }

            _honglingViewModel.Cleanup();
            _honglingViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateHonglingViewModel()
        {
            if (_honglingViewModel == null)
            {
                _honglingViewModel = new HonglingViewModel();
            }
        }
        #endregion

        #region 编辑/添加 收入

        private static EditHonglingViewModel _editHonglingViewModel;

        public static EditHonglingViewModel EditHonglingStatic
        {
            get
            {
                if (_editHonglingViewModel == null)
                {
                    CreateEditHonglingViewModel();
                }
                return _editHonglingViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditHonglingViewModel EditHonglingLocator
        {
            get
            {
                return EditHonglingStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearEditHonglingViewModel()
        {
            if (_editHonglingViewModel == null)
            {
                return;
            }

            _editHonglingViewModel.Cleanup();
            _editHonglingViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateEditHonglingViewModel()
        {
            if (_editHonglingViewModel == null)
            {
                _editHonglingViewModel = new EditHonglingViewModel();
            }
        }
        #endregion


        #region 月支付主界面

        private static MonthCostMainViewModel _monthCostMainViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static MonthCostMainViewModel MonthCostMainStatic
        {
            get
            {
                if (_monthCostMainViewModel == null)
                {
                    CreateMonthCostMain();
                }
                return _monthCostMainViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MonthCostMainViewModel MonthCostMainLocator
        {
            get
            {
                return MonthCostMainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearMonthCostMain()
        {
            _monthCostMainViewModel.Cleanup();
            _monthCostMainViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateMonthCostMain()
        {
            if (_monthCostMainViewModel == null)
            {
                _monthCostMainViewModel = new MonthCostMainViewModel();
            }
        }



        #endregion

        #region 编辑/添加 月支付界面

        private static EditMonthCostMainViewModel _editMonthCostMainViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static EditMonthCostMainViewModel EditMonthCostMainStatic
        {
            get
            {
                if (_editMonthCostMainViewModel == null)
                {
                    CreateEditMonthCostMain();
                }

                return _editMonthCostMainViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditMonthCostMainViewModel EditMonthCostMainLocator
        {
            get
            {
                return EditMonthCostMainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearEditMonthCostMain()
        {
            _editMonthCostMainViewModel.Cleanup();
            _editMonthCostMainViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateEditMonthCostMain()
        {
            if (_editMonthCostMainViewModel == null)
            {
                _editMonthCostMainViewModel = new EditMonthCostMainViewModel();
            }
        }

        #endregion
        #endregion

        #region 统计报表模块
        #region 支付统计
        private static CostChartViewModel _costChartVM;

        public static CostChartViewModel CostChartVMStatic
        {
            get
            {
                if (_costChartVM == null)
                {
                    CreateCostChartViewModel();
                }
                return _costChartVM;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CostChartViewModel CostChartLocator
        {
            get
            {
                return CostChartVMStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearCostChartViewModel()
        {
            if (_costChartVM == null)
            {
                return;
            }

            _costChartVM.Cleanup();
            _costChartVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateCostChartViewModel()
        {
            if (_costChartVM == null)
            {
                _costChartVM = new CostChartViewModel();
            }
        }
        #endregion

        #region 支付分类统计
        private static CostClassChartViewModel _costClassChartVM;

        public static CostClassChartViewModel CostClassChartVMStatic
        {
            get
            {
                if (_costClassChartVM == null)
                {
                    CreateCostClassChartViewModel();
                }
                return _costClassChartVM;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CostClassChartViewModel CostClassChartLocator
        {
            get
            {
                return CostClassChartVMStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearCostClassChartViewModel()
        {
            if (_costClassChartVM == null)
            {
                return;
            }

            _costClassChartVM.Cleanup();
            _costClassChartVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateCostClassChartViewModel()
        {
            if (_costClassChartVM == null)
            {
                _costClassChartVM = new CostClassChartViewModel();
            }
        }
        #endregion

        #region 成员收支统计
        private static EarningAndCostViewModel _EarningAndCostChartVM;

        public static EarningAndCostViewModel EarningAndCostChartVMStatic
        {
            get
            {
                if (_EarningAndCostChartVM == null)
                {
                    CreateEarningAndCostChartViewModel();
                }
                return _EarningAndCostChartVM;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EarningAndCostViewModel EarningAndCostChartLocator
        {
            get
            {
                return EarningAndCostChartVMStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearEarningAndCostChartViewModel()
        {
            if (_EarningAndCostChartVM == null)
            {
                return;
            }

            _EarningAndCostChartVM.Cleanup();
            _EarningAndCostChartVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateEarningAndCostChartViewModel()
        {
            if (_EarningAndCostChartVM == null)
            {
                _EarningAndCostChartVM = new EarningAndCostViewModel();
            }
        }
        #endregion

        #endregion

        #region 系统设置模块
        #region 支付类别管理主界面

        private static CostClassManageViewModel _costClassManageViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static CostClassManageViewModel CostClassManageStatic
        {
            get
            {
                if (_costClassManageViewModel == null)
                {
                    CreateCostClassManage();
                }

                return _costClassManageViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CostClassManageViewModel CostClassManageLocator
        {
            get
            {
                return CostClassManageStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearCostClassManage()
        {
            _costClassManageViewModel.Cleanup();
            _costClassManageViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateCostClassManage()
        {
            if (_costClassManageViewModel == null)
            {
                _costClassManageViewModel = new CostClassManageViewModel();
            }
        }

        #endregion

        #region 编辑/添加 支付类别管理主界面

        private static EditCostClassManageViewModel _editCostClassManageViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static EditCostClassManageViewModel EditCostClassManageStatic
        {
            get
            {
                if (_editCostClassManageViewModel == null)
                {
                    CreateEditCostClassManage();
                }

                return _editCostClassManageViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditCostClassManageViewModel EditCostClassManageLocator
        {
            get
            {
                return EditCostClassManageStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearEditCostClassManage()
        {
            _editCostClassManageViewModel.Cleanup();
            _editCostClassManageViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateEditCostClassManage()
        {
            if (_editCostClassManageViewModel == null)
            {
                _editCostClassManageViewModel = new EditCostClassManageViewModel();
            }
        }

        #endregion

        #region 支付商家管理主界面

        private static CostShopManageViewModel costShopManageViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static CostShopManageViewModel CostShopManageViewModelStatic
        {
            get
            {
                if (costShopManageViewModel == null)
                {
                    CreateCostShopManage();
                }

                return costShopManageViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CostShopManageViewModel CostShopManageLocator
        {
            get
            {
                return CostShopManageViewModelStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearCostShopManage()
        {
            costShopManageViewModel.Cleanup();
            costShopManageViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateCostShopManage()
        {
            if (costShopManageViewModel == null)
            {
                costShopManageViewModel = new CostShopManageViewModel();
            }
        }

        #endregion

        #region 编辑/添加 支付商家管理界面

        private static EditCostShopManageViewModel _editCostShopManageViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static EditCostShopManageViewModel EditCostShopManageStatic
        {
            get
            {
                if (_editCostShopManageViewModel == null)
                {
                    CreateEditCostShopManage();
                }

                return _editCostShopManageViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditCostShopManageViewModel EditCostShopManageLocator
        {
            get
            {
                return EditCostShopManageStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearEditCostShopManage()
        {
            _editCostShopManageViewModel.Cleanup();
            _editCostShopManageViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateEditCostShopManage()
        {
            if (_editCostShopManageViewModel == null)
            {
                _editCostShopManageViewModel = new EditCostShopManageViewModel();
            }
        }

        #endregion

        #region 收入分类

        private static EarningClassManageViewModel _earningClassViewModel;

        public static EarningClassManageViewModel EarningClassManageStatic
        {
            get
            {
                if (_earningClassViewModel == null)
                {
                    CreateEarningClassManageViewModel();
                }
                return _earningClassViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EarningClassManageViewModel EarningClassManageLocator
        {
            get
            {
                return EarningClassManageStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearEarningClassManageViewModel()
        {
            if (_earningClassViewModel == null)
            {
                return;
            }

            _earningClassViewModel.Cleanup();
            _earningClassViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateEarningClassManageViewModel()
        {
            if (_earningClassViewModel == null)
            {
                _earningClassViewModel = new EarningClassManageViewModel();
            }
        }
        #endregion

        #region 编辑/添加 收入分类

        private static EditEarningClassManageViewModel _editEarningClassViewModel;

        public static EditEarningClassManageViewModel EditEarningClassStatic
        {
            get
            {
                if (_editEarningClassViewModel == null)
                {
                    CreateEditEarningClassViewModel();
                }
                return _editEarningClassViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditEarningClassManageViewModel EditEarningClassLocator
        {
            get
            {
                return EditEarningClassStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ExchangeVM property.
        /// </summary>
        public static void ClearEditEarningClassViewModel()
        {
            if (_editEarningClassViewModel == null)
            {
                return;
            }

            _editEarningClassViewModel.Cleanup();
            _editEarningClassViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ExchangeVM property.
        /// </summary>
        public static void CreateEditEarningClassViewModel()
        {
            if (_editEarningClassViewModel == null)
            {
                _editEarningClassViewModel = new EditEarningClassManageViewModel();
            }
        }
        #endregion
        #endregion

        #region 人员管理界面

        private static PersonsManageViewModel _personsManageViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static PersonsManageViewModel PersonsManageStatic
        {
            get
            {
                if (_personsManageViewModel == null)
                {
                    CreatePersonsManage();
                }

                return _personsManageViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PersonsManageViewModel PersonsManageLocator
        {
            get
            {
                return PersonsManageStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearPersonsManage()
        {
            _personsManageViewModel.Cleanup();
            _personsManageViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreatePersonsManage()
        {
            if (_personsManageViewModel == null)
            {
                _personsManageViewModel = new PersonsManageViewModel();
            }
        }

        #endregion

        #region 编辑/添加 人员管理界面

        private static EditPersonsManageViewModel _editPersonsManageViewModel;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static EditPersonsManageViewModel EditPersonsManageStatic
        {
            get
            {
                if (_editPersonsManageViewModel == null)
                {
                    CreateEditPersonsManage();
                }

                return _editPersonsManageViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditPersonsManageViewModel EditPersonsManageLocator
        {
            get
            {
                return EditPersonsManageStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearEditPersonsManage()
        {
            _editPersonsManageViewModel.Cleanup();
            _editPersonsManageViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateEditPersonsManage()
        {
            if (_editPersonsManageViewModel == null)
            {
                _editPersonsManageViewModel = new EditPersonsManageViewModel();
            }
        }

        #endregion
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearMain();
        }

        
    }
}