using EComDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EComDemo
{
    public partial class MainPage : ContentPage
    {
        MainViewModel mainViewModel;
        public MainPage()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel(Navigation);
            mainViewModel.PageLoad();
            BindingContext = mainViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform.Equals("iOS"))
            {
                var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                safeInsets.Top = 0;
                safeInsets.Bottom = 10;
                mainlayout.Padding = safeInsets;
            }
        }

        void btnfilter_Clicked(System.Object sender, System.EventArgs e)
        {
            var animation = new Animation();
            filterview.IsVisible = true;
            var textFieldTranslate = new Animation(v => filterview.TranslationX = v, 300, 0);
            var textFieldChangeWidth = new Animation(v => filterview.WidthRequest = v, 0, 300);
            var textFieldChangeOpacity = new Animation(v => filterview.Opacity = v, 0, 1);

            animation.Add(0, 1, textFieldTranslate);
            animation.Add(0, 1, textFieldChangeWidth);
            animation.Add(0, 0.2, textFieldChangeOpacity);

            animation.Commit(this, "Slide", 16, 300, Easing.Linear);
        }

        void btnclear_Clicked(System.Object sender, System.EventArgs e)
        {
            filterview.IsVisible = false;
            mainViewModel.FilterItem("");
        }

        private void Kids_Tapped(object sender, EventArgs e)
        {
            filterview.IsVisible = false;
            mainViewModel.FilterItem("kids");
        }
        private void Female_Tapped(object sender, EventArgs e)
        {
            filterview.IsVisible = false;
            mainViewModel.FilterItem("women");
        }
        private void Male_Tapped(object sender, EventArgs e)
        {
            filterview.IsVisible = false;
            mainViewModel.FilterItem("men");
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var data = sender as ImageButton;
            var img = data.Source.ToString();
            if (img == "File: list.png")
            {
                imgBtn.Source = "list2.png";

                colView.IsVisible = true;
                lstView.IsVisible = false;
            }
            else
            {
                imgBtn.Source = "list.png";
                colView.IsVisible = false;
                lstView.IsVisible = true;
            }
        }
    }
}
