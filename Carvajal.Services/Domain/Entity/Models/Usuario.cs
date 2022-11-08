using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entity.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Password { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
