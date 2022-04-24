using myMobileApp.Models;
using myMobileApp.ViewModels;
using myMobileApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myMobileApp.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        private bool SlidingPanelIsShown = false;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();

            HideSlidingPanel();
        }

        private async void HideSlidingPanel()
        {
            while (this.Height == -1)
            {
                await Task.Delay(200);
                SlidingPanel.TranslationY = this.Height;
             }
            SlidingPanel.TranslationY = this.Height;
            SlidingPanelIsShown = false;
        }

        private async void ScaleButton_Clicked(object sender, EventArgs e)
        {
           if (SlidingPanelIsShown)
            {
                SlidingPanel.TranslateTo(0, this.Height, 450, Easing.SinIn);
                await ScaleButton.TranslateTo(0, -40, 450);
                SlidingPanelBackground.BackgroundColor = Color.Transparent;
                SlidingPanelBackground.InputTransparent = true;
            }
            else
            {
                SlidingPanel.TranslateTo(0, this.Height - QuikMenu.Height -30, 450, Easing.SinInOut);
                await ScaleButton.TranslateTo(0, -90, 450);
                SlidingPanelBackground.BackgroundColor = Color.FromRgba(55,55,55,99);
                SlidingPanelBackground.InputTransparent = false;
            }

            SlidingPanelIsShown = !SlidingPanelIsShown;

            
        }

        private void SlidingPanelBackground_OnCTapped(object sender, EventArgs e)
        {
            ScaleButton_Clicked(sender, e);
        }

        private async void SlidingPanel_OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Up:
                    SlidingPanel.TranslateTo(0, this.Height - QuikMenu.Height - 330, 450, Easing.SinInOut);
                    await ScaleButton.TranslateTo(0, -400, 450);
                    break;
                case SwipeDirection.Down:
                    SlidingPanel.TranslateTo(0, this.Height, 450, Easing.SinIn);
                    await ScaleButton.TranslateTo(0, -40, 450);
                    SlidingPanelBackground.BackgroundColor = Color.Transparent;
                    SlidingPanelBackground.InputTransparent = true; 
                    break;
            }
            
        }
    }

}