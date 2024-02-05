using HouseStoreApi.Models;
using HouseStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HouseStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnerController : ControllerBase
{
    //private readonly HousesService _housesService;
    private readonly OwnerService _OwnerService;

    public OwnerController(OwnerService OwnerService){
        
        _OwnerService = OwnerService;

    }
        
    [HttpGet]
    public async Task<List<Retailor>> Get() =>
        await _OwnerService.GetAsync();
    
   
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Retailor>> Get(string id)
    {
        var retailor = await _OwnerService.GetAsync(id);

        if (retailor is null)
        {
            return NotFound();
        }

        return retailor;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Retailor newRetailor)
    {
        await _OwnerService.CreateAsync(newRetailor);

        return CreatedAtAction(nameof(Get), new { id = newRetailor.Id }, newRetailor);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Retailor updatedRetailor)
    {
        var retailor = await _OwnerService.GetAsync(id);

        if (retailor is null)
        {
            return NotFound();
        }

        updatedRetailor.Id = retailor.Id;

        await _OwnerService.UpdateAsync(id, updatedRetailor);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var retailor = await _OwnerService.GetAsync(id);

        if (retailor is null)
        {
            return NotFound();
        }

        await _OwnerService.RemoveAsync(id);

        return NoContent();
    }
}