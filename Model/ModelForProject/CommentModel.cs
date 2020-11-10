using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelForProject
{
    public class CommentModel
    {
        public string ImagePath { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
