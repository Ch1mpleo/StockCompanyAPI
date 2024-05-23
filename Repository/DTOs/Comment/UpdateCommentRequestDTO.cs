using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs.Comment
{
    public class UpdateCommentRequestDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be atleast 5 characters")]
        [MaxLength(150, ErrorMessage = "Title cannot be over 150 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Content must be atleast 5 characters")]
        [MaxLength(300, ErrorMessage = "Content cannot be over 300 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
