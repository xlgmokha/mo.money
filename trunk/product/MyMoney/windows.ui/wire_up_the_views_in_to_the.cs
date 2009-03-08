using System.ComponentModel;
using MoMoney.Infrastructure.Container.Windsor;
using MoMoney.Infrastructure.interceptors;
using MoMoney.Infrastructure.proxies;
using MoMoney.Presentation.Context;
using MoMoney.Presentation.Views;
using MoMoney.Presentation.Views.billing;
using MoMoney.Presentation.Views.dialogs;
using MoMoney.Presentation.Views.income;
using MoMoney.Presentation.Views.Menu.Help;
using MoMoney.Presentation.Views.Navigation;
using MoMoney.Presentation.Views.Shell;
using MoMoney.Presentation.Views.Startup;
using MoMoney.Presentation.Views.updates;
using MoMoney.Utility.Core;

namespace MoMoney.windows.ui
{
    internal class wire_up_the_views_in_to_the : ICommand
    {
        readonly WindsorDependencyRegistry register;

        public wire_up_the_views_in_to_the(WindsorDependencyRegistry registry)
        {
            register = registry;
        }

        public void run()
        {
            register.singleton<IShell, ApplicationShell>();
            //register.proxy(new SynchronizedViewProxyConfiguration<IShell>(), () => new ApplicationShell());
            register.singleton<the_application_context, the_application_context>();
            register.transient<IAboutApplicationView, AboutTheApplicationView>();
            register.transient<ISplashScreenView, SplashScreenView>();
            register.transient<INavigationView, NavigationView>();
            register.transient<IAddCompanyView, AddCompanyView>();
            register.transient<IViewAllBills, view_all_bills>();
            register.transient<IAddBillPaymentView, add_bill_payment>();
            register.transient<IMainMenuView, MainMenuView>();
            register.transient<IAddNewIncomeView, AddNewIncomeView>();
            register.transient<IViewIncomeHistory, ViewAllIncome>();
            register.transient<ISaveChangesView, SaveChangesView>();
            register.transient<ICheckForUpdatesView, CheckForUpdatesView>();
            register.singleton<INotificationIconView, NotificationIconView>();
            register.transient<IStatusBarView, StatusBarView>();
        }
    }

    internal class SynchronizedViewProxyConfiguration<T> : IConfiguration<IProxyBuilder<T>> where T : ISynchronizeInvoke
    {
        public void configure(IProxyBuilder<T> item)
        {
            item.add_interceptor<SynchronizedInterceptor<T>>();
        }
    }
}