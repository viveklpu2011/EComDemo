
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
using Xamarin.Essentials;

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



                        Items.Add(new ProductData { selectedImg = false, favorite = Favorite, category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                    }

                }
                Loader = false;
            }
            catch (Exception ex)
            {
                Loader = false;
            }
        }




        private string collectionTxt
        {
            get;
            set;
        }
        public string CollectionTxt
        {
            get { return collectionTxt; }
            set
            {
                if (collectionTxt != value)
                {
                    collectionTxt = value;
                    OnPropertyChanged("CollectionTxt");
                }
            }
        }
        public Command AddCommand
        {
            get
            {
                return new Command(async (data) =>
                {


                    try
                    {
                        if (!HttpRequest.CheckConnection())
                        {
                            return;
                        }
                        string error = string.Empty;


                        if (string.IsNullOrWhiteSpace(CollectionTxt))
                        {
                            error += "Please provide collection";
                        }
                        if (!string.IsNullOrWhiteSpace(error))
                        {

                            return;
                        }



                        var postData = new UBProduct() { PName = CollectionTxt.Trim() };
                        var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
                        var userinfo = await HttpRequest.PostRequest(ServiceConfigrations.BaseUrl, ServiceConfigrations.SaveCollectionUrl, jsonString);
                        var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);





                        if (serviceResult.status)
                        {





                            await navigation.PopAsync();

                        }
                        else
                        {


                        }



                    }
                    catch (Exception ex)
                    {



                    }

                });
            }
        }






        public Command SelectCommand
        {
            get
            {
                return new Command(async (data) =>
                {
                    var item = data as ProductData;
                    var index = Items.IndexOf(Items.Where(x => x.id == item.id).FirstOrDefault());
                    Items.RemoveAt(index);
                    bool select = false;
                    if (item.selectedImg == false)
                    {
                        select = true;
                    }
                    Items.Insert(index, new ProductData { selectedImg = select, favorite = item.favorite, category = item.category, description = item.description, id = item.id, image = item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });
                });
            }
        }

        public Command ShareCommand
        {
            get
            {
                return new Command(async (data) =>
                {
                var result = Items.Where(x => x.selectedImg == true).ToList();
                    if (result.Count > 0)
                    {
                        string img = "";
                        foreach (var item in result)
                        {
                            img += item.image + "\n";
                        }

                        ShareUri(img);
                    }
                });
             
                 
            }
        }



        public  void ShareUri(string uri)
        {
             Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = ""
            });
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
