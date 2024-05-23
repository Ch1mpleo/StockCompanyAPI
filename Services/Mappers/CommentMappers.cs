using Porfolio_API.Models;
using Repository.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappers
{
    public static class CommentMappers
    {
        public static CommentDTO ToCommentDTO(this Comment dto)
        {
            return new CommentDTO
            {
                Id = dto.Id,
                Title = dto.Title,
                Content = dto.Content,
                CreatedOn = dto.CreatedOn,
                StockId = dto.StockId,
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDTO dto, int stockId)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDTO dto)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content,
            };
        }

    }
}
