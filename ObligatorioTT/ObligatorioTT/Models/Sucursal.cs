using SQLite;

namespace ObligatorioTT.Models
{
    [Table("Sucursales")]
    public class Sucursal
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(250), NotNull, Unique]
        public string nombre { get; set; }

        [Unique, NotNull]
        public string direccion { get; set; }

        [Unique]
        public string telefono { get; set; }
    }
}
