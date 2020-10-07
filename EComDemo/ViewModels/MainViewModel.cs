﻿
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

namespace EComDemo.ViewModels
{

    public class MainViewModel : BaseViewModel
    {
        private INavigation navigation;
        public MainViewModel(INavigation navigation)
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
        private bool order
        {
            get;
            set;
        }
        public bool Order
        {
            get { return order; }
            set
            {
                if (order != value)
                {
                    order = value;
                    OnPropertyChanged("Order");
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
            CountFilter = "Filters(0)";
            Loader = true;


            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Device is not connected with Internet. Please check your network connection", TimeSpan.FromSeconds(2));

                    return;
                }

                // LoggedIn objUser = App.Database.GetLoggedInUser();

                Items.Clear();
                Items = new ObservableCollection<ProductData>();
                string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.OrderUrl;

                var userinfo = await HttpRequest.GetRequest(url);
                var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);

                if (serviceResult.status)
                {

                    foreach (var item in serviceResult.data)
                    {

                        Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

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
                    //Loader = true;


                    //try
                    //{
                    //    if (!HttpRequest.CheckConnection())
                    //    {
                    //        await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Device is not connected with Internet. Please check your network connection", TimeSpan.FromSeconds(2));

                    //        return;
                    //    }



                    //    Items.Clear();
                    //    Items = new ObservableCollection<ProductData>();
                    //    string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.OrderUrl;

                    //    var userinfo = await HttpRequest.GetRequest(url);
                    //    var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);

                    //    if (serviceResult.status)
                    //    {
                    //        if (Order == false)
                    //        {
                    //            foreach (var item in serviceResult.data.OrderBy(x => x.title))
                    //            {

                    //                Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                    //            }
                    //            Order = true;
                    //        }
                    //        else
                    //        {
                    //            foreach (var item in serviceResult.data.OrderByDescending(x => x.title))
                    //            {

                    //                Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                    //            }
                    //            Order = false;
                    //        }

                    //    }
                    //    Loader = false;
                    //}
                    //catch (Exception ex)
                    //{
                    //    Loader = false;
                    //}

                    Loader = true;


                    try
                    {
                        if (!HttpRequest.CheckConnection())
                        {
                            await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Device is not connected with Internet. Please check your network connection", TimeSpan.FromSeconds(2));

                            return;
                        }

                        // LoggedIn objUser = App.Database.GetLoggedInUser();

                        Items.Clear();
                        Items = new ObservableCollection<ProductData>();
                        string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.OrderUrl;

                        var userinfo = await HttpRequest.GetRequest(url);
                        var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);

                        if (serviceResult.status)
                        {
                            var cn = serviceResult.data;
                            CountFilter = "Filters(0)";
                            //
                            if (WomenFilter)
                            {
                                CountFilter = "Filters(1)";
                                cn = serviceResult.data.Where(x => x.category == "women").ToList();
                            }
                            if (MenFilter)
                            {
                                CountFilter = "Filters(1)";
                                cn = serviceResult.data.Where(x => x.category == "men").ToList();
                            }
                            if (KidFilter)
                            {
                                CountFilter = "Filters(1)";
                                cn = serviceResult.data.Where(x => x.category == "kids").ToList();
                            }
                            if (WomenFilter)
                            {
                                CountFilter = "Filters(1)";
                                cn = serviceResult.data.Where(x => x.category == "women").ToList();
                            }

                            //
                            if (WomenFilter && MenFilter)
                            {
                                CountFilter = "Filters(2)";
                                cn = serviceResult.data.Where(x => x.category == "women" || x.category == "men").ToList();
                            }
                            if (WomenFilter && KidFilter)
                            {
                                CountFilter = "Filters(2)";
                                cn = serviceResult.data.Where(x => x.category == "women" || x.category == "kids").ToList();
                            }

                            //

                            if (MenFilter && KidFilter)
                            {
                                CountFilter = "Filters(2)";
                                cn = serviceResult.data.Where(x => x.category == "men" || x.category == "kids").ToList();
                            }
                            if (MenFilter && KidFilter && WomenFilter)
                            {
                                CountFilter = "Filters(3)";
                                cn = serviceResult.data.Where(x => x.category == "men" || x.category == "kids" || x.category == "women").ToList();
                            }



                            if (Order == false)
                            {
                                foreach (var item in cn.OrderBy(x => x.title))
                                {

                                    Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                                }
                                Order = true;
                            }
                            else
                            {
                                foreach (var item in cn.OrderByDescending(x => x.title))
                                {

                                    Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                                }
                                Order = false;
                            }

                        }
                        Loader = false;
                    }
                    catch (Exception ex)
                    {
                        Loader = false;
                    }
                });
            }
        }


        public async void KidsItem()
        {
            Loader = true;


            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Device is not connected with Internet. Please check your network connection", TimeSpan.FromSeconds(2));

                    return;
                }

                // LoggedIn objUser = App.Database.GetLoggedInUser();

                Items.Clear();
                Items = new ObservableCollection<ProductData>();
                string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.OrderUrl;

                var userinfo = await HttpRequest.GetRequest(url);
                var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);

                if (serviceResult.status)
                {
                    if (Order == true)
                    {
                        foreach (var item in serviceResult.data.Where(x => x.category == "kids").OrderBy(x => x.title))
                        {

                            Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                        }

                    }
                    else
                    {
                        foreach (var item in serviceResult.data.Where(x => x.category == "kids").OrderByDescending(x => x.title))
                        {

                            Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                        }
                        Order = false;
                    }

                }
                Loader = false;
            }
            catch (Exception ex)
            {
                Loader = false;
            }
        }

        public async void FemaleItem()
        {
            Loader = true;


            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Device is not connected with Internet. Please check your network connection", TimeSpan.FromSeconds(2));

                    return;
                }

                // LoggedIn objUser = App.Database.GetLoggedInUser();

                Items.Clear();
                Items = new ObservableCollection<ProductData>();
                string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.OrderUrl;

                var userinfo = await HttpRequest.GetRequest(url);
                var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);

                if (serviceResult.status)
                {
                    if (Order == true)
                    {
                        foreach (var item in serviceResult.data.Where(x => x.category == "women").OrderBy(x => x.title))
                        {

                            Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                        }

                    }
                    else
                    {
                        foreach (var item in serviceResult.data.Where(x => x.category == "women").OrderByDescending(x => x.title))
                        {

                            Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                        }
                        Order = false;
                    }

                }
                Loader = false;
            }
            catch (Exception ex)
            {
                Loader = false;
            }
        }
        public async void MaleItem()
        {
            Loader = true;


            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Device is not connected with Internet. Please check your network connection", TimeSpan.FromSeconds(2));

                    return;
                }

                // LoggedIn objUser = App.Database.GetLoggedInUser();

                Items.Clear();
                Items = new ObservableCollection<ProductData>();
                string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.OrderUrl;

                var userinfo = await HttpRequest.GetRequest(url);
                var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);

                if (serviceResult.status)
                {
                    if (Order == true)
                    {
                        foreach (var item in serviceResult.data.Where(x => x.category == "men").OrderBy(x => x.title))
                        {

                            Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                        }

                    }
                    else
                    {
                        foreach (var item in serviceResult.data.Where(x => x.category == "men").OrderByDescending(x => x.title))
                        {

                            Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                        }
                        Order = false;
                    }

                }
                Loader = false;
            }
            catch (Exception ex)
            {
                Loader = false;
            }
        }




        private bool womenFilter
        {
            get;
            set;
        }
        public bool WomenFilter
        {
            get { return womenFilter; }
            set
            {
                if (womenFilter != value)
                {
                    womenFilter = value;
                    OnPropertyChanged("WomenFilter");
                }
            }
        }
        private bool menFilter
        {
            get;
            set;
        }
        public bool MenFilter
        {
            get { return menFilter; }
            set
            {
                if (menFilter != value)
                {
                    menFilter = value;
                    OnPropertyChanged("MenFilter");
                }
            }
        }

        private bool kidFilter
        {
            get;
            set;
        }
        public bool KidFilter
        {
            get { return kidFilter; }
            set
            {
                if (kidFilter != value)
                {
                    kidFilter = value;
                    OnPropertyChanged("KidFilter");
                }
            }
        }


        private string countFilter
        {
            get;
            set;
        }
        public string CountFilter
        {
            get { return countFilter; }
            set
            {
                if (countFilter != value)
                {
                    countFilter = value;
                    OnPropertyChanged("CountFilter");
                }
            }
        }

        public async void FilterItem(string type)
        {

            if (type == "women")
            {
                if(WomenFilter == false)
                {
                    WomenFilter = true;
                }
                else
                {
                    WomenFilter = false;
                }
            }
           else if (type == "men")
            {
               
                if (MenFilter == false)
                {
                    MenFilter = true;
                }
                else
                {
                    MenFilter = false;
                }
            }
            else if (type == "kids")
            {
                if (KidFilter == false)
                {
                    KidFilter = true;
                }
                else
                {
                    KidFilter = false;
                }
            }
            else
            {
                KidFilter = false;
                MenFilter = false;
                WomenFilter = false;
            }


            Loader = true;


            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Device is not connected with Internet. Please check your network connection", TimeSpan.FromSeconds(2));

                    return;
                }

                // LoggedIn objUser = App.Database.GetLoggedInUser();

                Items.Clear();
                Items = new ObservableCollection<ProductData>();
                string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.OrderUrl;

                var userinfo = await HttpRequest.GetRequest(url);
                var serviceResult = JsonConvert.DeserializeObject<ProductList>(userinfo.Result);

                if (serviceResult.status)
                {
                    var cn = serviceResult.data;
                    CountFilter = "Filters(0)";
                    //
                    if (WomenFilter)
                    {
                        CountFilter = "Filters(1)";
                        cn = serviceResult.data.Where(x => x.category == "women").ToList();
                    }
                    if (MenFilter)
                    {
                        CountFilter = "Filters(1)";
                        cn = serviceResult.data.Where(x => x.category == "men").ToList();
                    }
                    if (KidFilter)
                    {
                        CountFilter = "Filters(1)";
                        cn = serviceResult.data.Where(x => x.category == "kids").ToList();
                    }
                    if (WomenFilter)
                    {
                        CountFilter = "Filters(1)";
                        cn = serviceResult.data.Where(x => x.category == "women").ToList();
                    }

                    //
                    if (WomenFilter && MenFilter)
                    {
                        CountFilter = "Filters(2)";
                        cn = serviceResult.data.Where(x => x.category == "women" || x.category == "men").ToList();
                    }
                    if (WomenFilter && KidFilter)
                    {
                        CountFilter = "Filters(2)";
                        cn = serviceResult.data.Where(x => x.category == "women" || x.category == "kids").ToList();
                    }
                    
                    //

                    if (MenFilter && KidFilter)
                    {
                        CountFilter = "Filters(2)";
                        cn = serviceResult.data.Where(x => x.category == "men" || x.category == "kids").ToList();
                    }
                    if (MenFilter && KidFilter&& WomenFilter)
                    {
                        CountFilter = "Filters(3)";
                        cn = serviceResult.data.Where(x => x.category == "men" || x.category == "kids" || x.category == "women").ToList();
                    }



                    if (Order == true)
                    {
                        foreach (var item in cn.OrderBy(x => x.title))
                        {

                            Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                        }

                    }
                    else
                    {
                        foreach (var item in cn.OrderByDescending(x => x.title))
                        {

                            Items.Add(new ProductData { category = item.category, description = item.description, id = item.id, image = ServiceConfigrations.BaseImg + item.image, name = item.name, price = item.price, ratecount = item.ratecount, title = item.title, });

                        }
                      
                    }

                }
                Loader = false;
            }
            catch (Exception ex)
            {
                Loader = false;
            }
        }

    }
}