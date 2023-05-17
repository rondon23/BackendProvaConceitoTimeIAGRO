using System.Net;
using BookStore.Application.Interfaces;
using BookStore.Domain.Dtos;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookStoreController : ControllerBase
{
    private readonly IBookStoreService _bookStoreService;

    public BookStoreController(IBookStoreService bookStoreService)
    {
        _bookStoreService = bookStoreService;
    }


    [HttpGet("GetBookById/{id}")]
    public IActionResult GetBookById(int id)
    {
        try
        {
            var shippingCost = _bookStoreService.GetBookById(id);

            return Ok(shippingCost);
        }
        catch (ArgumentNullException)
        {
            return NotFound("no record found with that id!");
        }
    }

    [Route("GetBookByParameters")]
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public IActionResult Post([FromBody] BooksQueryParameters filter)
    {
        try
        {
            return Ok(_bookStoreService.GetByParameters(filter));
        }
        catch (ArgumentNullException)
        {
            return NotFound("Nothing was found with these parameters!");
        }
    }

    [HttpGet("shipping-cost/{id}")]
    public IActionResult GetShippingCost(int id)
    {
        try
        {
            var shippingCost = _bookStoreService.CalculateShippingCost(id);

            return Ok(shippingCost);
        }
        catch (ArgumentNullException)
        {
            return NotFound("book not found");
        }
    }
}

