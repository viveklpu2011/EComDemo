using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EComDemo.Controls
{
    public class GridDataTemplateSelector : DataTemplateSelector
    {
       
        public DataTemplate GridTemplate;
        public DataTemplate ListTemplate;
        public GridDataTemplateSelector()
        {

            GridTemplate = new DataTemplate(typeof(ColumnListView));
            ListTemplate = new DataTemplate(typeof(RowListView));
        }
        protected override Xamarin.Forms.DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return MainPage.col==false? ListTemplate: GridTemplate;
        }
    }

}
