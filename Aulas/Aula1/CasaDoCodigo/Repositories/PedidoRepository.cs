using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;

namespace CasaDoCodigo.Repositories
{
    public interface IPedidoRepository
    {

    }
    public class PedidoRepository: BaseRepository<Produto>, IPedidoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PedidoRepository(ApplicationContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
             _httpContextAccessor = httpContextAccessor;
        }
        private int? GetPedidoId()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("pedidoId");

        }
        private void SetPedido(int pedidoId)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32("pedidoId",pedidoId);
        }
    }
}
