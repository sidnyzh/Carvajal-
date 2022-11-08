using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carvajal.Services.Shared
{
    public partial class Pedido
    {
        public int Id { get; set; }
        public int Usuario { get; set; }
        public int Producto { get; set; }
        public double Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public double Iva { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore]
        public virtual Producto Productos { get; set; } = null!;

        [JsonIgnore]
        public virtual Usuario Usuarios { get; set; } = null!;
    }
}
