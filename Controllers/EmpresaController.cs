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
    public class EmpresaController : Controller
    {
        private readonly Contexto _context;

        public EmpresaController(Contexto contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Inicio()
        {
            return View(await _context.Empresa.Include(a => a.Endereco).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Cadastro()
        {
            var endereco = await _context.Endereco.ToListAsync();
            endereco.Insert(0, new Endereco()
            {
                Id = 0,
                Base = "Selecione uma Cidade"
            });
            ViewBag.Endereco2 = endereco;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cadastro([Bind("Nome", "NomeFantasia", "Cnpj", "Telefone", "Celular", "EnderecoId")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _context.Empresa.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Inicio));
            }
            return View(empresa);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var empresa = await _context.Empresa.SingleOrDefaultAsync(a => a.Id == id);

            _context.Endereco.Where(a => empresa.EnderecoId == a.Id).Load();
            return View(empresa);
        }
    }
}
