using System;

namespace CMSXData.Models;

public partial class Statuspedido
{
    public Guid Statuspedidoid { get; set; }
    public Guid Pedidoid { get; set; }
    public string? Status { get; set; }
    public string? Descricao { get; set; }
    public DateTime Datahora { get; set; }

    public virtual Pedido PedidoNavigation { get; set; } = null!;
}
