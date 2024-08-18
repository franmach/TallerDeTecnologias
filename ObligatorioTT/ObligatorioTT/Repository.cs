using ObligatorioTT.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioTT
{
    public class Repository
    {
        string rutaDB;
        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;

        private async Task Init()
        {
            if (conn is not null)
                return;
            conn = new SQLiteAsyncConnection(rutaDB);
            await conn.CreateTableAsync<Usuario>();
        }

        public Repository(string _rutaDB)
        {
            rutaDB = _rutaDB;
        }

        public async Task AddNewUsuarioAsync(Usuario usuario)
        {
            try
            {
                await Init();
                var result = await conn.InsertAsync(usuario);
                StatusMessage = $"{result} registro(s) agregado(s) (Nombre: {usuario.nombre})";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al agregar usuario: {ex.Message}";
            }
        }

        public async Task<List<Usuario>> GetAllUsuarios()
        {
            // TODO: Init then retrieve a list of Person objects from the database into a list
            try
            {
                await Init();
                return await conn.Table<Usuario>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Usuario>();
        }
    }
}