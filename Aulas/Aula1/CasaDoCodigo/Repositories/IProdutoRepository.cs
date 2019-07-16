using System.Collections.Generic;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.Entities;

namespace CasaDoCodigo.Repositories
{
    public interface IProdutoRepository
    {
        void SaveProdutos(List<Livro> livros);
        List<Produto> GetProdutos();
    }
}