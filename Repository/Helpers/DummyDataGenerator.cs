using Bogus;
using Porfolio_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers
{
    public class DummyDataGenerator
    {
        private readonly Faker _faker = new Faker();
        public List<Stock> GenerateStocksWithComments(int numStocks, int commentsPerStock)
        {
            var stocks = new List<Stock>();
            int currentStockId = 1;
            int currentCommentId = 1;

            for (int i = 0; i < numStocks; i++)
            {
                var stock = new Stock
                {
                    Id = currentStockId++,
                    Symbol = _faker.Name.FirstName(),
                    CompanyName = _faker.Company.CompanyName(),
                    Purchase = _faker.Finance.Amount(10, 1000),
                    LastDiv = _faker.Finance.Amount(0, 100),
                    Industry = _faker.Commerce.Department(),
                    MarketCap = _faker.Random.Long(100000000, 100000000000),
                    Comments = new List<Comment>()
                };
                
                for (int j = 0; j < commentsPerStock; j++)
                {
                    var comment = new Comment
                    {
                        Id = currentCommentId++,
                        Title = _faker.Lorem.Sentence(),
                        Content = _faker.Lorem.Paragraphs(2),
                        CreatedOn = _faker.Date.Past(),
                        StockId = stock.Id
                    };

                    stock.Comments.Add(comment);
                }

                stocks.Add(stock);
            }

            return stocks;
        }

    }

}
