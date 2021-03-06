using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DemoSeven.WebApi.Controllers.Base;
using DemoSeven.WebApi.Models;
using DemoSeven.WebApi.Models.Dtos;
using DemoSeven.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoSeven.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public sealed class BooksController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        /*
    * Not using repository pattern.
    * This is okay if you are not writing any tests because this is not testable.
    */
        public BooksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            try
            {
                var books = await _context.Books.ToListAsync();
                var bookDtos = _mapper.Map<List<BookDto>>(books);

                return bookDtos;
            }
            catch (Exception e)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
  // GET: api/books/ab2bd817-98cd-4cf3-a80a-53ea0cd9c200
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookDto>> GetBook(Guid id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);

                if (book == null)
                    return NotFound();

                var bookDto = _mapper.Map<BookDto>(book);
                return bookDto;
            }
            catch (Exception e)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/books/ab2bd817-98cd-4cf3-a80a-53ea0cd9c200
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutBook(Guid id, Book book)
        {
            if (id != book.Id)
                return BadRequest();

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return !BookExists(id) 
                    ? NotFound() 
                    : new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                var bookDto = _mapper.Map<BookDto>(book);

                return CreatedAtAction("GetBook", new { id = bookDto.Id }, bookDto);
            }
            catch (Exception e)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/books/ab2bd817-98cd-4cf3-a80a-53ea0cd9c200
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                    return NotFound();

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        private bool BookExists(Guid id) => _context.Books.Any(e => e.Id == id);
    }

    }
