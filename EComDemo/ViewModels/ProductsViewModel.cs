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
using EComDemo.Dependency;

namespace EComDemo.ViewModels
{
   
    public class ProductsViewModel : BaseViewModel
    {
        private INavigation navigation;
        public ProductsViewModel(INavigation navigation)
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

                    foreach (var item in serviceResult.data)
                    {

                        Items.Add(new ProductData { selectedImg = false, favorite = "ic_checkbox_silver.png", category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

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
                    string img = "ic_checkbox_silver.png";
                    if (item.selectedImg == false)
                    {
                        select = true;
                        img = "ic_checkbox_sliver.png";
                    }
                    Items.Insert(index, new ProductData { selectedImg = select, favorite = img, category = item.category, description = item.description, id = item.id, image = item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });
                });
            }
        }

        public Command AddProductCommand
        {
            get
            {
                return new Command(async (data) =>
                {

                    var cn = Items.Where(x => x.selectedImg == true).ToList();
                    if (cn.Count > 0)
                    {
                        try
                        {
                            if (!HttpRequest.CheckConnection())
                            {
                                return;
                            }

                            List<UBProductId> lst = new List<UBProductId>();
                            foreach (var item in cn)
                            {
                                lst.Add(new UBProductId { Id = item.id });
                            }

                            var postData = new UBProductSave() { Name = ProductsPage.name.Trim(), Data = lst };
                            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
                            var userinfo = await HttpRequest.PostRequest(ServiceConfigrations.BaseUrl, ServiceConfigrations.SaveProductUrl, jsonString);
                            var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);





                            if (serviceResult.status)
                            {


                                App.Current.MainPage = new NavigationPage(new GroupPage()) { BarBackgroundColor = Color.FromHex("#ffec19") };




                            }
                            else
                            {


                            }



                        }
                        catch (Exception ex)
                        {



                        }



                    }


                });


            }
        }



        






    }
}
