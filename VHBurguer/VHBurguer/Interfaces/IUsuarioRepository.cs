using VHBurguer.Domains;

namespace VHBurguer.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();

        // Pode ser nulo por isso "?"
        Usuario? ObterPorId(int id);
        Usuario? ObterPorEmail(string email);
        bool EmailExistes(string email);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Remover(int id);
    }
}
