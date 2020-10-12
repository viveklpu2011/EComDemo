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
    public partial class AddCollectionPage : ContentPage
    {
        CollectionViewModel collectionViewModel;
        public AddCollectionPage()
        {
            InitializeComponent();
            collectionViewModel = new CollectionViewModel(Navigation);
            collectionViewModel.PageList();
            BindingContext = collectionViewModel;
        }

        private void itemLst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (itemLst.SelectedItem != null)
            {
                txtCollection.Text = itemLst.SelectedItem.ToString();
            }
            itemLst.SelectedItem = null;
        }
    }
}