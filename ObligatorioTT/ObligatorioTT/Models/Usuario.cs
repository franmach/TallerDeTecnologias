using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
 

namespace ObligatorioTT.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(250), NotNull]
        public string nombre { get; set; }

        [MaxLength(400)]
        public string telefono { get; set; }

        [Unique, NotNull]
        public string email { get; set; }

        [NotNull]
        public string password { get; set; }

        public string rutaFoto { get; set; }

    }
}
