using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TesteVolvo.Data;
using TesteVolvo.Models;

namespace TesteVolvo.Controllers
{
    public class CaminhaoController : Controller
    {
        private readonly ILogger<CaminhaoController> _logger;

        public CaminhaoController(ILogger<CaminhaoController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromServices] AppDbContext context)
        {
            var caminhoes = await context.Caminhoes
                .Include(i => i.Modelo)
                .AsNoTracking().ToListAsync();
            return View(caminhoes);
        }

        public async Task<IActionResult> Adicionar([FromServices] AppDbContext context)
        {
            var modelos = context.Modelos.Where(w => w.Ativo).ToList();
            ViewData["IdModelo"] = new SelectList(
                modelos, 
                "IdModelo", "Nome");

            var anos = new Dictionary<int, string>
                {
                    { DateTime.Now.Year, DateTime.Now.Year.ToString() },
                    { DateTime.Now.Year + 1, (DateTime.Now.Year + 1).ToString()}
                };
            ViewData["AnoModelo"] = new SelectList(
                anos,
                "Key", "Value");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromServices] AppDbContext context, 
            Caminhao caminhao)
        {
            try
            {
                caminhao.AnoFabricacao = DateTime.Now.Year;
                ModelState.Remove("Modelo");
                if (ModelState.IsValid && (caminhao.AnoModelo == DateTime.Now.Year || caminhao.AnoModelo == DateTime.Now.Year + 1))
                {
                    await context.Caminhoes.AddAsync(caminhao);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                var modelos = context.Modelos.Where(w => w.Ativo).ToList();
                ViewData["IdModelo"] = new SelectList(
                    modelos,
                    "IdModelo", "Nome", caminhao.IdModelo);

                var anos = new Dictionary<int, string>
                {
                    { DateTime.Now.Year, DateTime.Now.Year.ToString() },
                    { DateTime.Now.Year + 1, (DateTime.Now.Year + 1).ToString()}
                };
                ViewData["AnoModelo"] = new SelectList(
                    anos,
                    "Key", "Value", caminhao.AnoModelo);

                return View(caminhao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }           
        }

        public async Task<IActionResult> Editar(int idCaminhao, [FromServices] AppDbContext context)
        {
            if (idCaminhao > 0)
            {
                var caminhao = await context.Caminhoes
                    .FirstOrDefaultAsync(x => x.IdCaminhao == idCaminhao);
                if (caminhao == null) return NotFound();
                

                var modelos = context.Modelos.Where(w => w.Ativo).ToList();
                ViewData["IdModelo"] = new SelectList(
                    modelos,
                    "IdModelo", "Nome", caminhao.IdModelo);

                var anos = new Dictionary<int, string>
                {
                    { DateTime.Now.Year, DateTime.Now.Year.ToString() },
                    { DateTime.Now.Year + 1, (DateTime.Now.Year + 1).ToString()}
                };
                ViewData["AnoModelo"] = new SelectList(
                    anos,
                    "Key", "Value", caminhao.AnoModelo);

                return View(caminhao);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Editar([FromServices] AppDbContext context,
            Caminhao caminhao)
        {
            try
            {
                ModelState.Remove("Modelo");
                if (ModelState.IsValid && (caminhao.AnoModelo == DateTime.Now.Year || caminhao.AnoModelo == DateTime.Now.Year + 1))
                {
                    context.Caminhoes.Update(caminhao);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                var modelos = context.Modelos.Where(w => w.Ativo).ToList();
                ViewData["IdModelo"] = new SelectList(
                    modelos,
                    "IdModelo", "Nome", caminhao.IdModelo);

                var anos = new Dictionary<int, string>
                {
                    { DateTime.Now.Year, DateTime.Now.Year.ToString() },
                    { DateTime.Now.Year + 1, (DateTime.Now.Year + 1).ToString()}
                };
                ViewData["AnoModelo"] = new SelectList(
                    anos,
                    "Key", "Value", caminhao.AnoModelo);

                return View(caminhao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int idCaminhao, [FromServices] AppDbContext context)
        {
            var caminhao = await context.Caminhoes.FirstOrDefaultAsync(x => x.IdCaminhao == idCaminhao);
            if (caminhao == null)
                return NotFound();

            try
            {
                context.Caminhoes.Remove(caminhao);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}