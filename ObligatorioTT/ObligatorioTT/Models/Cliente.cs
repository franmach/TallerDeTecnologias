using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ObligatorioTT.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(250)]
        public string nombre { get; set; }
        public string telefono { get; set; }

        [Unique]
        public string email { get; set; }
        public string password { get; set; }
        public string rutaFoto { get; set; }

        public Cliente() { }




    }
}
