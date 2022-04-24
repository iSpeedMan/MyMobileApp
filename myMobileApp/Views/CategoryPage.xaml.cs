using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        private CategoryViewModel _categoriesViewModel;
        public CategoryPage()
        {
            InitializeComponent();
            BindingContext = _categoriesViewModel = new CategoryViewModel();
        }
    }
}