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
            await conn.CreateTableAsync<Sucursal>();
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

        public async Task AddNewSucursalAsync(Sucursal sucursal)
        {
            try
            {
                await Init();
                var result = await conn.InsertAsync(sucursal);
                StatusMessage = $"{result} registro(s) agregado(s) (Nombre: {sucursal.nombre})";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al agregar sucursal: {ex.Message}";
            }
        }

        // Obtener todas las sucursales
        public async Task<List<Sucursal>> GetAllSucursalesAsync()
        {
            try
            {
                await Init();
                return await conn.Table<Sucursal>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al obtener sucursales: {ex.Message}";
                return new List<Sucursal>();
            }
        }

        // Eliminar una sucursal por ID
        public async Task DeleteSucursalAsync(int id)
        {
            try
            {
                await Init();
                var result = await conn.DeleteAsync<Sucursal>(id);
                StatusMessage = $"{result} registro(s) eliminado(s)";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar sucursal: {ex.Message}";
            }
        }
    }

}