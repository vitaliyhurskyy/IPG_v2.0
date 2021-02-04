using IPG_v2._0.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPG_v2._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IPG_v2._0.Controllers
{
    public class ClientesController : Controller
    {

        private readonly ProjetosDbContext _context;
        private readonly UserManager<IdentityUser> _gestorUtilizadores;
        public ClientesController(ProjetosDbContext context, UserManager<IdentityUser> gestorUtilizadores)
        {
            _context = context;
            _gestorUtilizadores = gestorUtilizadores;
        }

        // GET: Clientes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            Clientes cliente;

            if (id != null)
            {
                cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.ClienteId == id);

                if (cliente == null)
                {
                    return NotFound();
                }
            }
            else
            {
                if (!User.IsInRole("Cliente"))
                {
                    return NotFound();
                }

                cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.Email == User.Identity.Name);

                if (cliente == null)
                {
                    // todo: Enviar para uma página a explicar o problema
                    return NotFound();
                }
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistoClientesViewModel infoCliente)
        {
            IdentityUser utilizador = await _gestorUtilizadores.FindByNameAsync(infoCliente.Email);

            if (utilizador != null)
            {
                ModelState.AddModelError("Email", "Já existe um cliente/utilizador com o email que especificou.");
            }

            utilizador = new IdentityUser(infoCliente.Email);
            IdentityResult resultado = await _gestorUtilizadores.CreateAsync(utilizador, infoCliente.Password);
            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível fazer o registo. Por favor tente mais tarde novamente e se o problema persistir contacte a assistência.");
            }
            else
            {
                await _gestorUtilizadores.AddToRoleAsync(utilizador, "Cliente");
            }

            if (!ModelState.IsValid)
            {
                return View(infoCliente);
            }

            Clientes cliente = new Clientes
            {
                Nome = infoCliente.Nome,
                Email = infoCliente.Email,
                Telefone = infoCliente.Telefone
            };

            _context.Add(cliente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
        }

        // GET: Clientes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nome,Telefone,Email")] Clientes cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
