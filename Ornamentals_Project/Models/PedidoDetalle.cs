//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ornamentals_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PedidoDetalle
    {
        public int PedidoDetalle1 { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    
        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
