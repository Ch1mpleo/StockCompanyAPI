using Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Porfolio_API.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;


        //Link stock với comment thông qua key StockId 
        public int? StockId { get; set; }
        //- Navigation property
        public Stock? Stock { get; set; }

        //1 - 1 với user 
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
