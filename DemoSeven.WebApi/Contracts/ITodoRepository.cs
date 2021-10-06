using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoSeven.WebApi.Models.Dtos;
using DemoSeven.WebApi.Models.Entities;

namespace DemoSeven.WebApi.Contracts
{
    public interface ITodoRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<TodoDto>> GetAllAsync();
        Task<TodoDto> GetByIdAsync(Guid id);
        Task<TodoDto> CreateAsync(Todo todo);
        Task<TodoDto> UpdateAsync(Todo todo);
        Task DeleteAsync(Guid id);



    }
}