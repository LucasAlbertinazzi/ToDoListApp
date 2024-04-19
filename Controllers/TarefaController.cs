using AppToDoList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppToDoList.Controllers
{
    public class TarefaController : Controller
    {
        private readonly DbToDoListContext _context;

        public TarefaController(DbToDoListContext context)
        {
            _context = context;
        }

        // GET: Tarefa
        public async Task<IActionResult> Index()
        {
            var tarefas = await _context.TblTarefas
                                .Select(t => new TblTarefa
                                {
                                    IdTarefas = t.IdTarefas,
                                    Titulo = t.Titulo,
                                    Descricao = t.Descricao,
                                    Status = t.Status,
                                    DataCriacao = t.DataCriacao,
                                    DataConclusao = t.DataConclusao
                                })
                                .ToListAsync();

            return View(tarefas);
        }

        // GET: Tarefa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarefa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarefas,Titulo,Descricao,Status,DataCriacao,DataConclusao")] TblTarefa tarefaInfo)
        {
            if (ModelState.IsValid)
            {
                var novaTarefa = new TblTarefa
                {
                    Titulo = tarefaInfo.Titulo,
                    Descricao = tarefaInfo.Descricao,
                    DataCriacao = DateTime.Now
                };

                _context.Add(novaTarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefaInfo);
        }

        // GET: Tarefa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.TblTarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdTarefas,Titulo,Descricao,Status")] TblTarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Attach(tarefa); // Anexa a entidade ao contexto
                    _context.Entry(tarefa).Property("Titulo").IsModified = true; // Define que a propriedade Titulo será modificada
                    _context.Entry(tarefa).Property("Descricao").IsModified = true; // Define que a propriedade Descricao será modificada
                    _context.Entry(tarefa).Property("Status").IsModified = true; // Define que a propriedade Status será modificada

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.IdTarefas))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        // GET: Tarefa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.TblTarefas.FirstOrDefaultAsync(m => m.IdTarefas == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa); // Renderiza a view Delete com os detalhes da tarefa
        }

        // POST: Tarefa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.TblTarefas.FindAsync(id);

            // Verifica se a tarefa foi encontrada
            if (tarefa == null)
            {
                return NotFound(); // Ou qualquer outra ação apropriada
            }

            _context.TblTarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Redireciona para a página de índice após a exclusão
        }

        // POST: Tarefa/Concluir/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Concluir(int id)
        {
            var tarefa = await _context.TblTarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            tarefa.Status = true;
            tarefa.DataConclusao = DateTime.Now;
            _context.Update(tarefa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
            return _context.TblTarefas.Any(e => e.IdTarefas == id);
        }
    }
}
