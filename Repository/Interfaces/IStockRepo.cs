using Porfolio_API.Models;
using Repository.DTOs;
using Repository.DTOs.Stock;
using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetAllAsync(QueryObjectStock query);

        //? để tránh null khi sử dụng FirstOrDefault
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO dto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExist(int id);
    }
}
