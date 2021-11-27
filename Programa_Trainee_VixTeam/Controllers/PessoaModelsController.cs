using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Programa_Trainee_VixTeam.Data;
using Programa_Trainee_VixTeam.Models;

namespace Programa_Trainee_VixTeam.Controllers
{
    public class PessoaModelsController : Controller
    {
        private readonly Programa_Trainee_VixTeamContext _context;

        public PessoaModelsController(Programa_Trainee_VixTeamContext context)
        {
            _context = context;
        }

        // GET: PessoaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PessoaModel.ToListAsync());
        }

        // GET: PessoaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        [HttpPost,ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MudancaSituacao(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);

            if (pessoaModel.Situacao == "Ativo")
            {
                pessoaModel.Situacao = "Inativo";
            }
            else if (pessoaModel.Situacao == "Inativo")
            {
                pessoaModel.Situacao = "Ativo";
            }




            _context.PessoaModel.Update(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }


        // GET: PessoaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PessoaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Email,DatadeNascimento,QuantidadeFilhos,Salario")] PessoaModel pessoaModel)
        {
            //if (ModelState.IsValid)
            //{
            var emailsIguais = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email) && x.Codigo != pessoaModel.Codigo);


                if (pessoaModel.limitedeFilhos() == false)
                {
                    ModelState.AddModelError("Limite de Filhos", "É impossivel possuir uma quantidade de filhos menor que 0");
                    
                }

                if(pessoaModel.limiteSalario() == false)
                {
                    ModelState.AddModelError("Limite de Salarios", "Um Salario deve ser maior que 1200 e menor que 13000");
                }

                if (pessoaModel.emailsIguais(pessoaModel, _context))
                {
                    ModelState.AddModelError("Emails Duplicados", "Não pode haver mais de uma conta para o mesmo email");
                }

                else
                {
                    _context.Add(pessoaModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(pessoaModel);


            //}
            //return View(pessoaModel);
        }

        // GET: PessoaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }

        // POST: PessoaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Email,DatadeNascimento,QuantidadeFilhos,Salario, Situacao")] PessoaModel pessoaModel)
        {
            var emailsIguais = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email) && x.Codigo != pessoaModel.Codigo);

            if (id != pessoaModel.Codigo)
            {
                return NotFound();
            }

            if (pessoaModel.limiteSalario() == false)
            {
                ModelState.AddModelError("Limite de Salarios", "Um Salario deve ser maior que 1200 e menor que 13000");
            }

            if (pessoaModel.InativoEdicao())
            {
                ModelState.AddModelError("Inativo impossibilitado de Edição", "Não é permitido editar contas inativas");
            }

            if (pessoaModel.limitedeFilhos() == false)
            {
                ModelState.AddModelError("Limite de Filhos", "É impossivel possuir uma quantidade de filhos menor que 0");

            }

            if (pessoaModel.emailsIguais(pessoaModel, _context) == true)
            {
                ModelState.AddModelError("E-mails Repetidos", "Duas Contas Diferentes não podem ter e-mail iguais");
            }

            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(pessoaModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PessoaModelExists(pessoaModel.Codigo))
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
            }

            
            return View(pessoaModel);
        }

        // GET: PessoaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // POST: PessoaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if(pessoaModel.Situacao == "Ativo")
            {
                ModelState.AddModelError("Situação Ativo", "Contas Ativas não podem ser removidas");
            }
            else
            {
                _context.PessoaModel.Remove(pessoaModel);
                await _context.SaveChangesAsync();
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaModelExists(int id)
        {
            return _context.PessoaModel.Any(e => e.Codigo == id);
        }


    }
}
