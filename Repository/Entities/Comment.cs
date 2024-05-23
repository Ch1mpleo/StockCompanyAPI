namespace Porfolio_API.Models
{
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
    }
}
