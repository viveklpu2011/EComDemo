using EComDemo.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EComDemo.Controls
{
    public partial class ColumnListView : ContentView
    {
        MainViewModel mainViewModel;
        public ColumnListView()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel(Navigation);
        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var ss = sender as ImageButton;
            var dd = ss.Parent as StackLayout;
            var ff = dd.Children[0] as ImageButton;
            string img = ff.Source.ToString();

            int type = 0;
            if (img == "File: heartblack.png")
            {
                ff.Source = "redheart.png";
                type = 1;
            }
            else
            {
                ff.Source = "heartblack.png";
                type = 0;
            }

            var id = dd.Children[1] as Label;
            mainViewModel.Favorite(type, Convert.ToInt32(id.Text));
        }
    }
}
