using Porfolio_API.Models;
using Repository.DTOs.Comment;
using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICommentRepo
    {
        Task<List<Comment>> GetAllAsync(QueryObjectComment query);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, Comment comment);
        Task<Comment?> DeleteAsync(int id);
    }
}
