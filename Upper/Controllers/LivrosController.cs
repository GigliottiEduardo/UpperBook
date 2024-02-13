using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Upper.Models;
using UpperSLN.Models;

namespace Upper.Controllers
{
    public class LivrosController : Controller
    {
        private readonly Contexto _context;

        public LivrosController(Contexto context)
        {
            _context = context;
        }

        // GET: Livros
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<Livro> livro = _context.Livro.ToList();

            const int pageSize = 6;
            if (pg < 1)
                pg = 1;

            int recsCount = livro.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = livro.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            if (livro.Foto is not null)
            livro.ImagemLivroBase64 = Convert.ToBase64String(livro.Foto);


            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Genero,Autor,Editora,Foto,ImagemLivro")] Livro livro)
        {
            if (ModelState.IsValid)       
            {
                if (livro.ImagemLivro is not null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await livro.ImagemLivro.CopyToAsync(ms);
                        livro.Foto = ms.ToArray();
                    }
                }
                _context.Add(livro);
                await _context.SaveChangesAsync();
                TempData["Mensagem"] = "Livro adicionado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            if (livro.Foto is not null)

                // Cria um MemoryStream a partir do array de bytes
                using (MemoryStream memoryStream = new MemoryStream(livro.Foto))
            {
            // Inicializa um objeto FormFile usando o MemoryStream e o nome do arquivo
                livro.ImagemLivro = new FormFile(memoryStream, 0, livro.Foto.Length, null, livro.Name);
            }

            if (livro.ImagemLivro is not null)
            livro.ImagemLivroBase64 = Convert.ToBase64String(livro.Foto);

            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genero,Autor,Editora,Foto,ImagemLivro")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    if (livro.ImagemLivro is null)
                    {
                        var livroDb = _context.Livro.AsNoTracking().FirstOrDefault(c => c.Id == livro.Id);
                        livro.Foto = livroDb.Foto;
                    }
                    else
                    {
                        using (var ms = new MemoryStream())
                        {
                            await livro.ImagemLivro.CopyToAsync(ms);
                            livro.Foto = ms.ToArray();
                        }
                    }

                    var entry = _context.Entry(livro);
                    entry.State = EntityState.Modified;

                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Mensagem"] = "Livro editado com sucesso!";
                return RedirectToAction(nameof(Index));

            }

            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livro.FindAsync(id);
            if (livro != null)
            {
                _context.Livro.Remove(livro);
            }

            await _context.SaveChangesAsync();
            TempData["Mensagem"] = "Livro excluido com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.Id == id);
        }
    }
}
