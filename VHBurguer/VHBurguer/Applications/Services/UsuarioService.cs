using VHBurguer.DTOs;
using VHBurguer.Interfaces;
using VHBurguer.Domains;
namespace VHBurguer.Applications.Services
{
    // Service Concentra o "como fazer"
    public class UsuarioService
    {
     // repository e o canal para acessar os dados 
     private readonly IUsuarioRepository _repository;
        // injeção de dependência
        //implementamos o repositorio e o service sp  depende da interface do repositorio

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
            
        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            LerUsuarioDto usuarioDto = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario ?? true
            };
            return usuarioDto;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuarioBanco => LerDto(usuarioBanco)).ToList();
            return usuariosDto;
        }
    }
}
