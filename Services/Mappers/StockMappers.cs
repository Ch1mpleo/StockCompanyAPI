using Porfolio_API.Models;
using Repository.DTOs.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappers
{
    public static class StockMappers
    {
        //phải dùng api.model
        public static StockDTO ToStockDTO(this Stock dto)
        {
            return new StockDTO
            {
                Id = dto.Id,
                Symbol = dto.Symbol,
                CompanyName = dto.CompanyName,
                Purchase = dto.Purchase,
                LastDiv = dto.LastDiv,
                Industry = dto.Industry,
                MarketCap = dto.MarketCap,
                Comments = dto.Comments.Select(c => c.ToCommentDTO()).ToList(),
            };
        }

        //Muốn submit xuống db thì data phải ở dạng Stock thuần ko chỉnh sửa 
        public static Stock ToCreateStockRequestDTO(this CreateStockRequestDTO dto)
        {
            return new Stock
            {
                Symbol = dto.Symbol,
                CompanyName = dto.CompanyName,
                Purchase = dto.Purchase,
                LastDiv = dto.LastDiv,
                Industry = dto.Industry,
                MarketCap = dto.MarketCap,
            };
        }
    }
}
