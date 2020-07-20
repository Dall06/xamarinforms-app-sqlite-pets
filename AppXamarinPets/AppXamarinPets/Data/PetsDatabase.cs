using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using System.Threading.Tasks;
using AppXamarinPets.Models;
using AppXamarinPets.Extensions;

namespace AppXamarinPets.Data
{
    public class PetsDatabase
    {
        // Lazy nos ayuda para que al conectar nuestra base de datos con SQLite no se bloquee la app
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        // Nos regresa la conexión de la base de datos de SQLite
        static SQLiteAsyncConnection Database => lazyInitializer.Value;

        static bool initialized = false;

        public PetsDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }


        // Método para inicializar la table de tasks, si no existe la crea
        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(PetsModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(PetsModel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<PetsModel>> GetAllPetsAsync()
        {
            return Database.Table<PetsModel>().ToListAsync();
        }

        public Task<PetsModel> GetPetsAsync(int id)
        {
            return Database.Table<PetsModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SavePetsAsync(PetsModel item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeletePetsAsync(PetsModel item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
