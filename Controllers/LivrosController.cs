using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
namespace Chapter.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivroRepository _livroRepository;
        public LivrosController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_livroRepository.Listar());
        }

        // get /api/livros/{id}
        [HttpGet("{id}")] // busca pelo id
        public IActionResult BuscarPorId(int id)
        {
            /* O método BuscarPorId conversa com o banco de dados buscando
            esse id na base de dados ou ele pega diretamente daqui
            do servidor?
            */
            Livro livro = _livroRepository.BuscaPorId(id);
            if(livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpPut("{id}")]
        
        public IActionResult Atualizar(int id, Livro livro)
        {
            _livroRepository.Atualizar(id, livro);
            return StatusCode(204);
        }

        [HttpPost]
        public IActionResult Cadastrar(Livro livro)
        {
            _livroRepository.Cadastrar(livro);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            _livroRepository.Deletar(id);
            return StatusCode(204);
        }
    }
}