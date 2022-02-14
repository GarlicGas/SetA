using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetA.Server.Data;
using SetA.Server.IRepository;
using SetA.Shared.Domain;

namespace SetA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensessController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ExpensessController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Expensess
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Expenses>>> GetExpenses()
        public async Task<IActionResult> GetExpensess()
        {
            //return await _context.Expenses.ToListAsync();
            var Expensess = await _unitOfWork.Expensess.GetAll();
            return Ok(Expensess);
        }

        // GET: api/Expensess/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Expenses>> GetExpenses(int id)
        public async Task<IActionResult> GetExpenses(int id)
        {
            var Expenses = await _unitOfWork.Expensess.Get(q => q.Id == id);

            if (Expenses == null)
            {
                return NotFound();
            }

            return Ok(Expenses);
        }

        // PUT: api/Expensess/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenses(int id, Expenses Expenses)
        {
            if (id != Expenses.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Expenses).State = EntityState.Modified;
            _unitOfWork.Expensess.Update(Expenses);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExpensesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Expensess
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expenses>> PostExpenses(Expenses Expenses)
        {
            //_context.Expenses.Add(Expenses);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Expensess.Insert(Expenses);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetExpenses", new { id = Expenses.Id }, Expenses);
        }

        // DELETE: api/Expensess/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenses(int id)
        {
            //var Expenses = await _context.Expenses.FindAsync(id);
            var Expenses = await _unitOfWork.Expensess.Get(q => q.Id == id);
            if (Expenses == null)
            {
                return NotFound();
            }

            //_context.Expenses.Remove(Expenses);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Expensess.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool ExpensesExists(int id)
        private async Task<bool> ExpensesExists(int id)
        {
            //return _context.Expenses.Any(e => e.Id == id);
            var make = await _unitOfWork.Expensess.Get(q => q.Id == id);
            return make != null;
        }
    }
}
