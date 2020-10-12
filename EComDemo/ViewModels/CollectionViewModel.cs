
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


    public class CollectionViewModel : BaseViewModel
    {
        private INavigation navigation;
        public CollectionViewModel(INavigation navigation)
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




        private ObservableCollection<ProductData> _items = new ObservableCollection<ProductData>();
        public ObservableCollection<ProductData> Items
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
                Items = new ObservableCollection<ProductData>();
                string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.OrderUrl;

                var userinfo = await HttpRequest.GetRequest(url);
                var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);

                if (serviceResult.status)
                {

                    foreach (var item in serviceResult.data.Where(x => x.name == CollectionPage.name).ToList())
                    {
                        FavoriteItem objUser = App.Database.GetProduct(item.id);
                        string Favorite = "heartblack.png";
                        if (objUser != null)
                        {
                            Favorite = "redheart.png";
                        }



                        Items.Add(new ProductData { favorite = Favorite, category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                    }

                }
                Loader = false;
            }
            catch (Exception ex)
            {
                Loader = false;
            }
        }





        public Command SortCommand
        {
            get
            {
                return new Command(async (data) =>
                {



                });
            }
        }
















        private ObservableCollection<string> _item = new ObservableCollection<string>();
        public ObservableCollection<string> Item
        {
            get
            {
                return _item;
            }
            set
            {
                if (_item != value)
                {
                    _item = value;
                    OnPropertyChanged(nameof(Item));
                }
            }
        }
        public void PageList()
        {




            Item.Clear();
            Item = new ObservableCollection<string>();
            Item.Add("Looks I Love");
            Item.Add("Vacation-Ready");
            Item.Add("Office Approved Attrire");
            Item.Add("Celab Looks");
        }







    }
}
