namespace VHBurguer.DTOs.ProdutoDto
{
    public class AtualizarProdutoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!;
        // Imagem via multipart/form-data, ideal para upload de arquivo

        public List<int> CategoriaIds { get; set; } = new();
        public bool? StatusProduto { get; set; }
    }
}

