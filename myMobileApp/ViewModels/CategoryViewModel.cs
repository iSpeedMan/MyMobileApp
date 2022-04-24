using myMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace myMobileApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class CategoryViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private string itemId;
        private string title;
        private string description;
        private float successRate = 50f;
        public int IdCategory { get; set; }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public float SuccessRate
        {
            get => successRate;
            set => SetProperty(ref successRate, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(Int32.Parse(value));
            }
        }

        public CategoryViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(description);
        }

        public async void LoadItemId(int IdCategory)
        {
            try
            {
                var category = await DataStoreCategories.GetItemAsync(IdCategory);

                //IdCategory = category.Id;
                Title = category.Title;
                Description = category.Description;
                SuccessRate = SuccessRate;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var categoriesList = await DataStoreCategories.GetItemsAsync();

            int curMaxId = -1;
            if (categoriesList?.Count() > 0)
            {
                curMaxId = categoriesList.Max(x => x.Id);
            }
                
            Category newCategory = new Category()
            {
                Id = curMaxId + 1,
                Title = Title,
                Description = Description,
                SuccessRate = successRate
            };

            await DataStoreCategories.AddItemAsync(newCategory);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

    }
}
