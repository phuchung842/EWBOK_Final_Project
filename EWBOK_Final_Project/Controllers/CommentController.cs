using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Controllers
{
    public class CommentController : BaseController
    {
        // GET: Comment
        [HttpPost]
        public ActionResult Create(Comment comment,long productID)
        {
            comment.UserID = ((User)Session[Constants.USER_INFO]).ID;
            comment.ProductID = productID;
            comment.CreateDate = DateTime.Now;
            comment.Status = true;
            var result = new CommentDao().Insert(comment);
            if (result < 0)
            {
                ModelState.AddModelError("", "Đăng bình luận thất bại");
            }
            new LogDao().SetLog("Create Comment", null, ((User)Session[Constants.USER_INFO]).ID);
            return Redirect((string)Session[Constants.CURRENT_URL]);
        }
    }
}