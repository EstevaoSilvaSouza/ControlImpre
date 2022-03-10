using Control_Impressora.Context;
using Control_Impressora.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Controllers
{
    public class ImpressoraController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly Contexto _contexto;

        public ImpressoraController(UserManager<ApplicationUser> _userManager, Contexto contexto)
        {
            _contexto = contexto;
            userManager = _userManager;
        }

        public async Task<IActionResult> Inicio()
        {
            var Lista = await _contexto.Impressora.Include(i => i.Empresa).Include(a => a.Endereco).AsNoTracking().ToListAsync();
            
            return View(Lista);
        }

       [HttpGet]
        public async Task<IActionResult> Cadastro()
        {
            var eempresa = await _contexto.Empresa.ToListAsync();
            var eendereco = await _contexto.Endereco.ToListAsync();
            var usuario = userManager.Users.ToList();

            eempresa.Insert(0, new Empresa
            {
                Id = 0,
                Nome = "Selecione uma empresa"
            });

            eendereco.Insert(0, new Endereco()
            {
                Id = 0,
                Base = "Selecione uma base"
            });
    
            ViewBag.Empresa = eempresa;
            ViewBag.Endereco = eendereco;
            ViewBag.Usuario = usuario;
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(Impressora impressora)
        {
            if (ModelState.IsValid)
            {
                _contexto.Impressora.Add(impressora);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Inicio));
            }
            return View(impressora);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            var impreesora = await _contexto.Impressora.SingleOrDefaultAsync(i => i.Id == id);
            if(impreesora == null)
            {
                return RedirectToAction(nameof(Inicio));
            }
          
            var enderecoo = _contexto.Endereco.SingleOrDefault(a => impreesora.EnderecoId == a.Id);
            var empresaa = _contexto.Empresa.SingleOrDefault(a => impreesora.EnderecoId == a.Id);
         //   _contexto.Users.SingleOrDefault(b => b.Id == impreesora.UsuarioId);
           
            var t = new Impressora()
            {
            Id = impreesora.Id,
            Marca = impreesora.Marca, 
            Modelo = impreesora.Modelo, 
            VersaoDrive = impreesora.VersaoDrive,
            Mac = impreesora.Mac, 
            IpImpre = impreesora.IpImpre, 
            QtdEstoqueToner = impreesora.QtdEstoqueToner,
            QtdEstoqueUsoToner = impreesora.QtdEstoqueUsoToner,
            Empresa = empresaa,
            Endereco = enderecoo,
            applicationUser = _contexto.Users.SingleOrDefault(b => b.Id == impreesora.UsuarioId)
        };
            return View(t);
            
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            var impressoraa = await _contexto.Impressora.FirstOrDefaultAsync(i => i.Id == id);
            if(impressoraa == null)
            {
                return RedirectToAction(nameof(Inicio));
            }

            ViewBag.Empresa = new SelectList(_contexto.Empresa,"Id", "Nome", impressoraa.Id);
            ViewBag.Endereco = new SelectList(_contexto.Endereco, "Id", "Base", impressoraa.Id);
            ViewBag.Tecnico = new SelectList(_contexto.Users, "Id", "Nome", impressoraa.Id);

            return View(impressoraa);

        }
        [HttpPost]
        public async Task<IActionResult> Editar(Impressora impressora)
        {
            if (ModelState.IsValid)
            {
                _contexto.Entry(impressora).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Inicio));
            }

            ViewBag.Empresa = new SelectList(_contexto.Empresa, "Id", "Nome", impressora.Id);
            ViewBag.Endereco = new SelectList(_contexto.Endereco, "Id", "Base", impressora.Id);
            ViewBag.Tecnico = new SelectList(_contexto.Users, "Id", "Nome", impressora.Id);

            return View(impressora);
        }

        [HttpGet]
        public async Task<IActionResult> Arquivos(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Inicio));
            }

            
            var dados = new FileImpressora();
            dados.Impressora = await _contexto.Impressora.FirstOrDefaultAsync(t => t.Id == id);
            dados.FileOnDatabaseModel =  _contexto.FileOnDatabaseModels.Where(i => i.ImpressoraId == dados.Impressora.Id).ToList();
            return View(dados);
        }

        [HttpPost]
        public async Task<IActionResult> Arquivos(List<IFormFile> files, string descricao,FileImpressora fileImpressora)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var verifica = fileName;
                //string[] check = verifica.Split(".");
                //foreach(string t in check)
                //{
                if (verifica.Contains("."))
                {
                    TempData["Message1"] = "Arquivo invalido!!, use um arquivo .pdf";
                    break;
                }
                //}
                if (extension != ".pdf")
                {
                    TempData["Message1"] = "Arquivo invalido!!, use um arquivo .pdf";
                    break;
                }
                else
                {
                    
                    var fileModel = new FileOnDatabaseModel
                    {
                        CriadoEm = DateTime.UtcNow,
                        TipoArquivo = file.ContentType,
                        Extensao = extension,
                        Nome = fileName,
                        Descricao = descricao,
                        ImpressoraId = fileImpressora.Impressora.Id,
                    };
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        fileModel.Data = dataStream.ToArray();
                    }
                    _contexto.FileOnDatabaseModels.Add(fileModel);
                    await _contexto.SaveChangesAsync();
                    TempData["Message"] = "Arquivo Enviado com Sucesso!! aguarde para ser aprovado!!";
                }
            }
            return RedirectToAction(nameof(Arquivos));
        }

        public async Task<IActionResult> Download(int? id)
        {
            if(id == null) { return NotFound("Id Invalido!");}
            var file = await _contexto.FileOnDatabaseModels.FirstOrDefaultAsync(a => a.Id == id);
            if(file == null) { return NotFound("Objeto Invalido!");}
            return File(file.Data, file.TipoArquivo, file.Nome + file.Extensao);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var obj = await _contexto.Impressora.FirstOrDefaultAsync(a => a.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Inicio));
            }

            _contexto.Impressora.Remove(obj);
            return RedirectToAction(nameof(Inicio));

        }

    }
}
