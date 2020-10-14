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
    public partial class ProductsPage : ContentPage
    {
        public static string name = "";
        ProductsViewModel productsViewModel;
        public ProductsPage()
        {
            InitializeComponent();
            productsViewModel = new ProductsViewModel(Navigation);
            productsViewModel.PageLoad();
            BindingContext = productsViewModel;
        }
    }
}