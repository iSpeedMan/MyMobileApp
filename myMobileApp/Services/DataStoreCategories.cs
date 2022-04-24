using myMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myMobileApp.Services
{
    public class DataStoreCategories : IDataStore<Category, int>
    {
        readonly List<Category> items;

        public DataStoreCategories()
        {
            items = new List<Category>()
            {
                new Category { Id = 0, Title = "Хобби", Description="Любимые занятия", SuccessRate = 30 },
                new Category { Id = 1, Title = "Работа", Description="Рабочие дела", SuccessRate = 60 },
                new Category { Id = 2, Title = "Учеба", Description="Важные занятия", SuccessRate = 70 },
                new Category { Id = 3, Title = "Спорт", Description="Спортивные мероприятия", SuccessRate = 40 },
                new Category { Id = 4, Title = "Прочее", Description="Всякое разное", SuccessRate = 10 }
            };
        }

        public async Task<bool> AddItemAsync(Category item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Category item)
        {
            var oldItem = items.Where((Category arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Category arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Category> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}