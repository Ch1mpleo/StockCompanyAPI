using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs.Stock
{
    public class UpdateStockRequestDTO
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Symbol cannot be over than 15 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(30, ErrorMessage = "Company name cannot be over than 15 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.01, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Industry cannot be over than 30 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}
