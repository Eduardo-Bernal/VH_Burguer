using VHBurguer.Interfaces;
using VHBurguer.DTOs.ProdutoDto;
using VHBurguer.Domains;
using VHBurguer.Applications.Conversoes;
using VHBurguer.Applications.Regras;
namespace VHBurguer.Applications.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public List<LerProdutoDto> Listar()
        {
            List<Produto> produtos = _repository.Listar();
            // SELECT Percorre cada produto e transdorma em dto ->  LerProdutoDTO
            List<LerProdutoDto> produtosDto = produtos.Select(ProdutoParaDto.ConverterParaDto).ToList();

            return produtosDto;
        }

        public LerProdutoDto ObterPorId(int id)
        {
            Produto produto = _repository.ObterPorId(id);
            if (produto == null)
            {
                throw new Exception("Produto não encontrado");
            }
            // Converte o produto encontrado para DTO e dvolve
            return ProdutoParaDto.ConverterParaDto(produto);
        }

        private static void ValidarCadastro(CriarProdutoDto produtoDto)
        {
            if (string.IsNullOrWhiteSpace(produtoDto.Nome))
            {
                throw new Exception("O nome do produto é obrigatório");
            }

            if (produtoDto.Preco <= 0)
            {
                throw new Exception("O preço do produto deve ser maior que zero");
            }

            if (string.IsNullOrWhiteSpace(produtoDto.Descricao))
            {
                throw new Exception("A Descrição do produto é obrigatório");
            }


            if (produtoDto.Imagem == null || produtoDto.Imagem.Length == 0)
            {
                throw new Exception("A Imagem do produto é obrigatório");
            }

            if (produtoDto.CategoriaIds == null || produtoDto.CategoriaIds.Count == 0)
            {
                throw new Exception("O produto deve pertencer a pelo menos uma categoria");
            }
        }

        public byte[] ObterImagem(int id)
        {
            byte[] imagem = _repository.ObterImagem(id);
            if (imagem == null || imagem.Length == 0)
            {
                throw new Exception("Imagem não encontrado");
            }
            return imagem;
        }

        public LerProdutoDto Adicionar(CriarProdutoDto produtoDto, int usuarioId)
        {
            ValidarCadastro(produtoDto);

            if (_repository.NomeExiste(produtoDto.Nome))
            {
                throw new Exception("Já existe um produto com esse nome");
            }


            Produto produto = new Produto
            {
                Nome = produtoDto.Nome,
                Preco = produtoDto.Preco,
                Descricao = produtoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(produtoDto.Imagem),
                StatusProduto = true,
                UsuarioID = usuarioId
            };

            _repository.Adicionar(produto, produtoDto.CategoriaIds);

            return ProdutoParaDto.ConverterParaDto(produto);
        }
        public LerProdutoDto Atualizar(int id, AtualizarProdutoDto produtoDto)
        {
            HorarioAlteracaoProduto.ValidarHorario();

            Produto produtoBanco = _repository.ObterPorId(id);

            if (produtoBanco == null)
            {
                throw new Exception("Produto não encontrado");
            }

            if (_repository.NomeExiste(produtoDto.Nome, produtoIdAtual: id))
            {
                throw new Exception("Já existe outro produto com esse nome.");
            }

            if (produtoDto.CategoriaIds == null || produtoDto.CategoriaIds.Count == 0)
            {
                throw new Exception("O produto deve pertencer a pelo menos uma categoria");
            }

            if (produtoDto.Preco <= 0)
            {
                throw new Exception("O preço do produto deve ser maior que zero");
            }

            produtoBanco.Nome = produtoDto.Nome;
            produtoBanco.Preco = produtoDto.Preco;
            produtoBanco.Descricao = produtoDto.Descricao;

            if (produtoDto.Imagem != null && produtoDto.Imagem.Length > 0)
            {
                produtoBanco.Imagem = ImagemParaBytes.ConverterImagem(produtoDto.Imagem);
            }

            if (produtoDto.StatusProduto.HasValue)
            {
                produtoBanco.StatusProduto = produtoDto.StatusProduto.Value;
            }

            _repository.Atualizar(produtoBanco, produtoDto.CategoriaIds);

            return ProdutoParaDto.ConverterParaDto(produtoBanco);
        }

        public void Excluir(int id)
        {
            HorarioAlteracaoProduto.ValidarHorario();
            Produto produtoBanco = _repository.ObterPorId(id);
            if (produtoBanco == null)
            {
                throw new Exception("Produto não encontrado");
            }
            _repository.Excluir(id);
        }
    }
}