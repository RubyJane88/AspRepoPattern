using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DemoSeven.WebApi.Contracts;
using DemoSeven.WebApi.Models;
using DemoSeven.WebApi.Models.Dtos;
using DemoSeven.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoSeven.WebApi.Repositories
{

    public sealed  class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TodoRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        public async Task<IEnumerable<TodoDto>> GetAllAsync()
        {
            try
            {
                var todos = await _context.Todos.ToListAsync(); //creates a session with the database to get the data list asynchronously
                var todoDtos = _mapper.Map<IEnumerable<TodoDto>>(todos); //IEnumerable iterates over the todo list while (AutoMapper) maps it to a new destination: todoDto

                return todoDtos;
            }
            catch (Exception e)
            {
                throw new Exception("Error in getting all the Todo list");
            }
        }

        public async Task<TodoDto> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                var todoDto = _mapper.Map<TodoDto>(todo);
                return todoDto;

            }
            catch (Exception e)
            {

                throw new Exception("Error getting todo list with that id");
            }
        }

        public async Task<TodoDto> CreateAsync(Todo todo)
        {
            try
            {
             todo.Id = Guid.NewGuid();
             _context.Todos.Add(todo);

             await _context.SaveChangesAsync(); //save all changes made in this context to the database 

             var todoDto = _mapper.Map<TodoDto>(todo);
             return todoDto;
            }
            catch (Exception e)
            {

                throw new Exception("Error creating new todo");
            }
        }
        
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var exists = await ExistsAsync(id);
                if (!exists) throw new Exception("Not found");

                _context.Remove(await _context.Todos.FindAsync(id));
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error deleting todo item");
            }
        }
        
        

        public async Task<TodoDto> UpdateAsync(Todo todo)
        {
            try
            {
                var exists = await ExistsAsync(todo.Id);
                if (!exists) throw new Exception("Not found");

                _context.Update(todo);
                await _context.SaveChangesAsync();
                var todoDto = _mapper.Map<TodoDto>(todo);
                return todoDto;
            }
            catch (Exception e)
            {

                throw new Exception("Error updating the todo item");
            }
        }

       

        public async Task<bool> ExistsAsync(Guid id) => await _context.Todos.AnyAsync(t => t.Id == id);

    }
}