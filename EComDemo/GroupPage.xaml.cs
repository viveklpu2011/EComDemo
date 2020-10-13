using EComDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EComDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        GroupViewModel groupViewModel;
        public GroupPage()
        {
            InitializeComponent();
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            groupViewModel = new GroupViewModel(Navigation);
            groupViewModel.PageLoad();
            BindingContext = groupViewModel;
        }
    }
}