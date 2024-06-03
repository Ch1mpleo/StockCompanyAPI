using Microsoft.EntityFrameworkCore;
using Porfolio_API.Models;
using Repository.DTOs.Stock;
using Repository.Helpers;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StockRepository : IStockRepo
    {
        private readonly ApplicationDBContext _context;
        
        //Contructor
        public StockRepository (ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync(QueryObjectStock query)
        {
            var stocks = _context.Stocks.Include(s => s.Comments).ThenInclude(a => a.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName) )
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol) )
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            //So sánh theo bảng chữ cái giảm dần
            if (!string.IsNullOrWhiteSpace(query.SortBy) )
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending 
                           ? stocks.OrderByDescending(s => s.Symbol)
                           : stocks.OrderBy(s => s.Symbol);
                }
            }

            //Paging
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Stock?> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO dto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            //2. Có thì add vào stockModel
            stockModel.Symbol = dto.Symbol;
            stockModel.CompanyName = dto.CompanyName;
            stockModel.Purchase = dto.Purchase;
            stockModel.LastDiv = dto.LastDiv;
            stockModel.Industry = dto.Industry;
            stockModel.MarketCap = dto.MarketCap;

            //3. SaveChanges
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<bool> StockExist(int id)
        {
            return _context.Stocks.Any(s => s.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }
    }
}
