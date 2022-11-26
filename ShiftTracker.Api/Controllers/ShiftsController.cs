﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftTracker.Api.Data;
using ShiftTracker.Api.Entities;
using ShiftTracker.Api.Services;

namespace ShiftTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly ShiftTrackerApiDbContext _context;

    public ShiftsController(ShiftTrackerApiDbContext context)
    {
        _context = context;
    }

    // GET: api/Shifts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
    {
      if (_context.Shifts == null)
      {
          return NotFound();
      }
        return await _context.Shifts.ToListAsync();
    }

    // GET: api/Shifts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Shift>> GetShift(int id)
    {
      if (_context.Shifts == null)
      {
          return NotFound();
      }
        var shift = await _context.Shifts.FindAsync(id);

        if (shift == null)
        {
            return NotFound();
        }

        return shift;
    }

    // PUT: api/Shifts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShift(int id, Shift shift)
    {
        if (id != shift.ShiftId)
        {
            return BadRequest();
        }

        ShiftService shiftService = new();
        shift = shiftService.CalculateTimeAndPay(shift);
        _context.Entry(shift).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShiftExists(id))
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

    // POST: api/Shifts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Shift>> PostShift(Shift shift)
    {
      if (_context.Shifts == null)
      {
          return Problem("Entity set 'ShiftTrackerApiDbContext.Shifts'  is null.");
      }

        ShiftService shiftService = new();
        shift = shiftService.CalculateTimeAndPay(shift);
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetShift), new { id = shift.ShiftId }, shift);
    }

    // DELETE: api/Shifts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        if (_context.Shifts == null)
        {
            return NotFound();
        }
        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null)
        {
            return NotFound();
        }

        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShiftExists(int id)
    {
        return (_context.Shifts?.Any(e => e.ShiftId == id)).GetValueOrDefault();
    }
}
