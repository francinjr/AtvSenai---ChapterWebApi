using Chapter.WebApi.Contexts;
using Chapter.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
namespace Chapter.WebApi.Repositories
{
    public class UsuarioRepository
    {
        private readonly ChapterContext _context;
        public UsuarioRepository(ChapterContext context)
        {
            _context = context;
        }
        public Usuario Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha
            == senha);
        }
        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }
        public void Cadastrar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
        public Usuario BuscaPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }




        public void Atualizar(int id, Usuario usuario)
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id);
            if (usuarioBuscado != null)
            {
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha = usuario.Senha;
            }
            _context.Usuarios.Update(usuarioBuscado);
            _context.SaveChanges();
        }
        public void Deletar(int id)
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id);
            _context.Usuarios.Remove(usuarioBuscado);
            _context.SaveChanges();
        }
    }
}
