using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EComDemo
{
   
    public partial class MainTabbedPage : ExtendedTabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();
            this.SelectedItem = this.Children[2];
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            if (CurrentPage.Title == "App")
            {
                home.Icon = "app_logo.png";
            }
            else
            {
                home.Icon = "app_logo_unselected.png";
            }
            this.Title = CurrentPage.Title;
        }
    }
}