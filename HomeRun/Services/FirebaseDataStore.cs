using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeRun.Services
{
    public class FirebaseDataStore<T> : IDataStore<T>
    {

        public Task<bool> AddItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(string id, T item)
        {
            throw new NotImplementedException();
        }
    }
}
