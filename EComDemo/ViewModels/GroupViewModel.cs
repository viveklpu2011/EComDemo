
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;




using EComDemo.ServiceConfigration;

using System.Collections.ObjectModel;
using EComDemo.ResponseModels;
using Plugin.Toasts;
using Newtonsoft.Json;
using EComDemo.Utils;
using System.Linq;
using EComDemo.Models;
using System.Threading.Tasks;

namespace EComDemo.ViewModels
{

    public class GroupViewModel : BaseViewModel
    {
        private INavigation navigation;
        public GroupViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

        private bool loader
        {
            get;
            set;
        }
        public bool Loader
        {
            get { return loader; }
            set
            {
                if (loader != value)
                {
                    loader = value;
                    OnPropertyChanged("Loader");
                }
            }
        }




        private ObservableCollection<UBProduct> _items = new ObservableCollection<UBProduct>();
        public ObservableCollection<UBProduct> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }
        public async void PageLoad()
        {

            Loader = true;

            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Device is not connected with Internet. Please check your network connection", TimeSpan.FromSeconds(2));

                    return;
                }



                Items.Clear();
                Items = new ObservableCollection<UBProduct>();
                string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.BProductUrl;

                var userinfo = await HttpRequest.GetRequest(url);
                var serviceResult = JsonConvert.DeserializeObject<UBProductList>(userinfo.Result);

                if (serviceResult.Status)
                {

                    foreach (var item in serviceResult.Data)
                    {

                        Items.Add(new UBProduct { NewCollection = false, ProductCollection = true, TotalProduct = item.TotalProduct, PName = item.PName, Id = item.Id, PImg1 = ServiceConfigrations.BaseImg + item.PImg1, PImg2 = ServiceConfigrations.BaseImg + item.PImg2, PImg3 = ServiceConfigrations.BaseImg + item.PImg3, PImg4 = ServiceConfigrations.BaseImg + item.PImg4 });

                    }
                    Items.Add(new UBProduct { NewCollection = true, ProductCollection = false, PName = "New Collection", Id = 0, PImg1 = "add.png" });
                }
                Loader = false;
            }
            catch (Exception ex)
            {
                Loader = false;
            }
        }





        public Command SelectCommand
        {
            get
            {
                return new Command((data) =>
                {

                    var result = data as UBProduct;

                    if (result.Id == 0)
                    {
                        navigation.PushAsync(new AddCollectionPage());
                    }
                    else
                    {
                        if (result.TotalProduct != 0)
                        {
                            CollectionPage.name = result.PName;
                            navigation.PushAsync(new CollectionPage());
                        }
                        else
                        {
                            ProductsPage.name = result.PName;
                            App.Current.MainPage = new NavigationPage(new ProductsPage()) { BarBackgroundColor = Color.FromHex("#ffec19") };

                        }
                    }
                });
            }
        }































    }
}
