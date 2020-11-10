using Model.EF;
using Model.ModelForProject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CommentDao
    {
        EWBOK_DbContext db = null;
        public CommentDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<CommentModel> ListCommentByProductID(long productid)
        {
            object[] sqlparams =
            {
                new SqlParameter("@productid",productid)
            };
            return db.Database.SqlQuery<CommentModel>("[dbo].[sp_ListCommentByProductID] @productid", sqlparams).ToList();
        }
        public int Insert(Comment CommentEntity)
        {
            object[] sqlparams =
            {
                new SqlParameter("@userid",CommentEntity.UserID),
                new SqlParameter("@productid",CommentEntity.ProductID),
                new SqlParameter("@createdate",CommentEntity.CreateDate),
                new SqlParameter("@status",CommentEntity.Status),
                new SqlParameter("@content",CommentEntity.Content)
            };
            return db.Database.ExecuteSqlCommand("[dbo].[sp_InsertComment] @productid, @userid, @createdate, @status, @content", sqlparams);
        }
        //public List<Comment> ListCommentByProductID(long productid)
        //{
        //    return db.Comments.Where(x => x.ProductID == productid).ToList();
        //}
    }
}
