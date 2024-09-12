using InAndEx.Models;
using SQLite;

namespace InAndEx.Data
{
    public class ListDatabase
    {
        SQLiteAsyncConnection _database;

        async Task Init()
        {
            if (_database is not null)
                return;

            _database = new SQLiteAsyncConnection(
                Constants.DatabasePath,
                Constants.Flags);
            
            await _database.CreateTableAsync<List>();
        }

        public async Task<List<List>> GetListsAsync()
        {
            await Init();
            return await _database.Table<List>().ToListAsync();
        }

        public async Task<List> GetListsAsync(int pListId)
        {
            await Init();
            return await _database
                .Table<List>()
                .Where(n => n.ID == pListId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveListAsync(List pList)
        {
            await Init();
            if (pList.ID != 0)
            {
                return await _database.UpdateAsync(pList);
            }
            else
            {
                return await _database.InsertAsync(pList);
            }
        }
        
        public async Task<int> DeleteListAsync(List pDelList)
        {
            await Init();
            return await _database.DeleteAsync(pDelList);
        }
    }
}