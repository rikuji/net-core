using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public interface IPedidoRepository
    {
        Pedido GetPedido();
        void AddItem(string codigo);
    }
    public class PedidoRepository: BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PedidoRepository(ApplicationContext context,IHttpContextAccessor httpContextAccessor)
            :base(context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddItem(string codigo)
        {
            var produto = _context.Set<Produto>()
                .Where(p => p.Codigo == codigo).SingleOrDefault();
            
            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var pedido = GetPedido();

            var itemPedido = _context.Set<ItemPedido>()
                                .Where(i => i.Produto.Codigo == codigo
                                        && i.Pedido.Id == pedido.Id)
                                .SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                _context.Set<ItemPedido>()
                    .Add(itemPedido);

                _context.SaveChanges();
            }
        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();
            var pedido = dbSet.Where(p => p.Id == pedidoId).Include(p => p.Itens).ThenInclude(i => i.Produto).SingleOrDefault();
            if (pedido == null)
            {
                pedido = new Pedido();
                dbSet.Add(pedido);
                _context.SaveChanges();
                SetPedidoId(pedido.Id);
            }
            return pedido;
        }

        private int? GetPedidoId()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("pedidoId");

        }
        private void SetPedidoId(int pedidoId)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32("pedidoId",pedidoId);
        }
    }
}
