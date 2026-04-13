using CMSXData.Models;

namespace ICMSX;

public interface IEventPublisher
{
    Task PublicarPedidoAsync(Pedido pedido);
}
