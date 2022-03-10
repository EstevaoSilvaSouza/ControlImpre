using Control_Impressora.Context;
using Control_Impressora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly Contexto _context;

        public EnderecoController(Contexto contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Inicio()
        {
            return View(await _context.Endereco.ToListAsync());
        }

        
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cadastro([Bind("Rua","Numero","Logradouro","Bairro","Cidade","Estado","Base")]Endereco endereco )
        {
            if (ModelState.IsValid)
            {
                _context.Endereco.Add(endereco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Inicio));
            }
            return View(endereco);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            var dado = await _context.Endereco.FindAsync(id);

            if(dado == null)
            {
                return RedirectToAction(nameof(Inicio));
            }

            return View(dado);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(endereco).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Inicio));
            }
            return View(endereco);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            var dados = await _context.Endereco.FindAsync(id);
            if(dados == null)
            {
                return RedirectToAction(nameof(Inicio));
            }
            return View(dados);
        }
    }
}
