using Microsoft.EntityFrameworkCore;
using Porfolio_API.Models;
using Repository.DTOs.Comment;
using Repository.Helpers;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Repository.Repositories
{
    public class CommentRepository : ICommentRepo
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllAsync(QueryObjectComment query)
        {
            var comments = _context.Comments.Include(a => a.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title) )
            {
                comments = comments.Where(c => c.Title.Contains(query.Title));
            }
            if (!string.IsNullOrWhiteSpace(query.Content) )
            {
                comments = comments.Where(c => c.Content.Contains(query.Content));  
            }
            
            //Sort cmt gần nhất
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                if (query.SortBy.Equals("DateTime", StringComparison.OrdinalIgnoreCase))
                {
                    comments = query.IsDescending
                        ? comments.OrderByDescending(c => c.CreatedOn)
                        : comments.OrderBy(c => c.CreatedOn);
                }
            }


            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await comments.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Comment?> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
