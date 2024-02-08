using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPICrudOperation.Models;

namespace WEBAPICrudOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _context;

        public BrandController(BrandContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if(_context.Brands == null)
            {
                return NotFound();
            }
            return await _context.Brands.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Brand>> GetBrands(int id)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            var brand = await _context.Brands.FindAsync(id);    
            if (brand == null)
            {
                return NotFound();
            }
            return brand;
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand); 
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrands), new { id = brand.ID }, brand);   
        }

        [HttpPut("id")]
        public async Task<ActionResult> PutBrand(int id, Brand brand)
        {
            if(id != brand.ID)
            {
                return BadRequest();
            }
            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();  
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool BrandAvailable(int id)
        {
            return ( _context.Brands?.Any(x => x.ID == id)).GetValueOrDefault();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            if(_context.Brands == null)
            {
                return NotFound();
            }
            var brand = await _context.Brands.FindAsync(id);
            if(brand == null)
            {
                return NotFound();
            }
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
