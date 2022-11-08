using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entity.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal Valor { get; set; }

        [JsonIgnore]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
