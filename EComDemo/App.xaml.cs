using EComDemo.Database;
using EComDemo.Dependency;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EComDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new GroupPage()) { BarBackgroundColor=Color.FromHex("#ffec19") };
        }
        static DbCls database;
        public static DbCls Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new DbCls(DependencyService.Get<IFileHelper>().GetLocalFilePath("DbCls.db"));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return database;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
