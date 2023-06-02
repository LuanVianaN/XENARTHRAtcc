using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XeMobile.Models
{
    public class FotoDatabase
    {
        readonly SQLiteAsyncConnection database;

        public FotoDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Foto>().Wait();
        }

        public Task<List<Foto>> GetPhotosAsync()
        {
            //seleciona as fotos
            return database.Table<Foto>().ToListAsync();
        }

        public Task<Foto> GetPhotoAsync(int id)
        {
            // seleciona a foto
            return database.Table<Foto>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SavePhotoAsync(Foto _photo)
        {
            if (_photo.ID != 0)
            {
                // atualiza a foto
                return database.UpdateAsync(_photo);
            }
            else
            {
                // salva a foto
                return database.InsertAsync(_photo);
            }
        }

        public Task<int> DeletePhotoAsync(Foto _photo)
        {
            // deleta a foto
            return database.DeleteAsync(_photo);
        }
    }
}
