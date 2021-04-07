using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcuraCarros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcuraCarros.Controllers
{
    public class CarrosController : Controller
    {
        private readonly Contexto _contexto;

        public CarrosController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Carros.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string txtCarro)
        {
            if (!String.IsNullOrEmpty(txtCarro))
            {
                return View(await _contexto.Carros.Where(c => c.Nome.ToUpper().Contains(txtCarro.ToUpper())).ToListAsync());
            }

            return View(await _contexto.Carros.ToListAsync());
        }

        [HttpGet]
        public IActionResult NovoCarro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovoCarro(Carro carro)
        {
            await _contexto.Carros.AddAsync(carro);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
