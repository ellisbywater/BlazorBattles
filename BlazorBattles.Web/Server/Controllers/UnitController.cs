using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBattles.Web.Server.Data;
using BlazorBattles.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Web.Server.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        [HttpGet]
        public async Task<IActionResult> GetUnits()
        {
            var units = await _dbContext.Units.ToListAsync();
            return Ok(units);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            await _dbContext.Units.AddAsync(unit);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Units.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, Unit unit)
        {
            Unit dbUnit = await _dbContext.Units.FirstOrDefaultAsync(unit => unit.Id == id);
            if (dbUnit == null)
            {
                return NotFound("Unit not found");
            }

            dbUnit.Title = unit.Title;
            dbUnit.Attack = unit.Attack;
            dbUnit.Defense = unit.Defense;
            dbUnit.HitPoints = unit.HitPoints;
            dbUnit.BananaCost = unit.BananaCost;

            await _dbContext.SaveChangesAsync();
            return Ok(dbUnit);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            Unit dbUnit = await _dbContext.Units.FirstOrDefaultAsync(unit => unit.Id == id);
            if (dbUnit == null)
            {
                return NotFound("Unit not found");
            }

            _dbContext.Units.Remove(dbUnit);
            await _dbContext.SaveChangesAsync();
            
            return Ok(await _dbContext.Units.ToListAsync());
        }
    }
}