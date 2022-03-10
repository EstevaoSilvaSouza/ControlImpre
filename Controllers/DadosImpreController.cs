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
    public class DadosImpreController : Controller
    {
        private readonly Contexto _context;

        public DadosImpreController (Contexto contexto)
        {
            _context = contexto;
        }

        public async Task<IActionResult> InsertDatesImpre(FileImpressora fileImpressora)
        {
            if (ModelState.IsValid)
            {
                var obj = new ImpressoraDate()
                {
                    NumberOne = fileImpressora.impressoraDate.NumberOne,
                    NumberTwo = fileImpressora.impressoraDate.NumberTwo,
                    QtdPapel = fileImpressora.impressoraDate.QtdPapel,
                    QtdPapelUtil = fileImpressora.impressoraDate.QtdPapelUtil,
                    Volume = fileImpressora.impressoraDate.Volume,
                    ImpressoraId = fileImpressora.Impressora.Id,
                };

                _context.impressoraDate.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DateImpressora));



            }
            return View(fileImpressora);
        }

        [HttpGet]
        public async Task<IActionResult> DateImpressora(int id)
        {

            if(id == null)
            {
                return RedirectToRoute("Impressora");
            }

            var dados = new FileImpressora();

            dados.Impressora = await _context.Impressora.FirstOrDefaultAsync(a => a.Id == id);
            dados.ImpressoraDate = await _context.impressoraDate.Where(b => b.ImpressoraId == dados.Impressora.Id).ToListAsync();
            return View(dados);


        }

    }
}
