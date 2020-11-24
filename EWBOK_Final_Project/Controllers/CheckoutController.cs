using EWBOK_Final_Project.Common;
using EWBOK_Final_Project.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            var preurl = System.Web.HttpContext.Current.Request.UrlReferrer.AbsolutePath.ToString();
            if (preurl != "/thanh-toan")
            {
                Session[Constants.CHECKOUT_DISCOUNT] = null;
                Session[Constants.DISCOUNT_NOTICE3PCT] = null;
                Session[Constants.DISCOUNT_NOTICE5PCT] = null;
            }
            var product = (List<CartItem>)Session[Constants.CART_SESSION];
            List<CartItem> listcart = new List<CartItem>();
            if(product!=null)
            {
                listcart = product;
            }
            if ((User)Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Check out", null, ((User)Session[Constants.USER_INFO]).ID);
            }
            return View(listcart);
        }
        [HttpPost]
        public ActionResult Payment(string receiver,string email,string phone, string address)
        {
            long idtempuser = -1;
            if ((User)Session[Constants.USER_INFO] == null)
            {
                var user = new User();
                user.Name = receiver;
                user.Email = email;
                user.Phone = phone;
                user.Address = address;
                user.CreateDate = DateTime.Now;
                user.UserName = "tempuser";
                user.Status = false;
                idtempuser = new UserDao().Insert(user);
            }

            Order order = new Order();
            order.ShipName = receiver;
            order.ShipEmail = email;
            order.ShipMobile = phone;
            order.ShipAddress = address;
            if (Session[Constants.CHECKOUT_DISCOUNT] != null)
            {
                order.PctDiscount = (short)Session[Constants.CHECKOUT_DISCOUNT];
            }
            else
            {
                order.PctDiscount = 0;
            }
            if ((User)Session[Constants.USER_INFO] == null)
            {
                order.CustomerID = idtempuser;
            }
            else
            {
                order.CustomerID = ((User)Session[Constants.USER_INFO]).ID;
            }
            order.CreateDate = DateTime.Now;
            order.Status = -1;
            try
            {
                var productDao = new ProductDao();
                var id = new OrderDao().Insert(order);
                order.ID = id;
                var cart = (List<CartItem>)Session[Constants.CART_SESSION];
                var detailDao = new OrderDetailDao();
                string listhtml = "";
                string row = "";
                decimal? total = 0;
                short discount = 0;
                if (Session[Constants.CHECKOUT_DISCOUNT] != null)
                {
                    discount = (short)Session[Constants.CHECKOUT_DISCOUNT];
                }
                decimal? totaldiscount = 0;
                for (int i = 0; i < cart.Count; i++)
                {
                    var product = productDao.GetDetail(cart[i].Product.ID);
                    product.Quantity = product.Quantity - cart[i].Quantity;
                    product.SellerCount = product.SellerCount + cart[i].Quantity;
                    var orderdetail = new OrderDetail();
                    orderdetail.ProductID = cart[i].Product.ID;
                    orderdetail.OrderID = id;
                    orderdetail.Price = cart[i].Product.Price;
                    orderdetail.Quantity = cart[i].Quantity;
                    if (cart[i].Product.ProductStatus > 0)
                    {
                        orderdetail.PromotionPrice = cart[i].Product.PromotionPrice;
                        total = total + cart[i].Product.PromotionPrice * cart[i].Quantity;
                        row = "<tr><td>" + cart[i].Product.Name + "</td><td>" + cart[i].Product.Price + "</td><td>" + cart[i].Product.PromotionPrice + "</td><td>" + cart[i].Quantity + "</td><td>" + cart[i].Product.PromotionPrice * cart[i].Quantity + "</td></tr> ";
                    }
                    else
                    {
                        total = total + cart[i].Product.Price * cart[i].Quantity;
                        row = "<tr><td>" + cart[i].Product.Name + "</td><td>" + cart[i].Product.Price + "</td><td>" + cart[i].Product.PromotionPrice + "</td><td>" + cart[i].Quantity + "</td><td>" + cart[i].Product.Price * cart[i].Quantity + "</td></tr> ";
                    }
                    detailDao.Insert(orderdetail);
                    productDao.Update(product);
                    listhtml = listhtml + row;
                }
                totaldiscount = Math.Round(Convert.ToDecimal(total * (100 - discount) / 100));
                order.Total = total;
                order.TotalDiscount = totaldiscount;
                new OrderDao().Update(order);
                if (discount == 3)
                {
                    ((User)Session[Constants.USER_INFO]).CumulativePoint = ((User)Session[Constants.USER_INFO]).CumulativePoint - 300;
                }
                else if (discount == 5)
                {
                    ((User)Session[Constants.USER_INFO]).CumulativePoint = ((User)Session[Constants.USER_INFO]).CumulativePoint - 500;
                }
                else
                { }
                if ((User)Session[Constants.USER_INFO] != null)
                {
                    long? cumulativepoint = Convert.ToInt64(total / 1000);
                    ((User)Session[Constants.USER_INFO]).CumulativePoint = ((User)Session[Constants.USER_INFO]).CumulativePoint + cumulativepoint;
                    new UserDao().Update((User)Session[Constants.USER_INFO]);
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/client/email_html/Email_checkout.html"));
                content = content.Replace("{{CustomerName}}", receiver);
                content = content.Replace("{{Phone}}", phone);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString());
                content = content.Replace("{{Discount}}", discount.ToString());
                content = content.Replace("{{TotalDiscount}}", totaldiscount.ToString());
                content = content.Replace("{{OrderDetail}}", listhtml);

                var ToEmailAdmin = ConfigurationManager.AppSettings["ToEmail"].ToString();
                new MailHelper().SendMail(ToEmailAdmin, "EWBOK_Đơn hàng", "Đơn hàng đến từ EWBOK Bookstore", content);
                new MailHelper().SendMail(email, "EWBOK_Đơn hàng", "Đơn hàng đến từ EWBOK Bookstore", content);
            }
            catch
            {
                if ((User)Session[Constants.USER_INFO] != null)
                {
                    new LogDao().SetLog("Payment", "Thất bại", ((User)Session[Constants.USER_INFO]).ID);
                }
                return View("PaymentError");
            }
            if ((User)Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Payment", "Thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            return View("PaymentSuccess");
        }

        public ActionResult Discount3Pct()
        {
            if (((User)Session[Constants.USER_INFO]).CumulativePoint < 300)
            {
                Session[Constants.DISCOUNT_NOTICE3PCT] = "Không đủ điểm tích luỹ";
                Session[Constants.DISCOUNT_NOTICE5PCT] = null;
            }
            else
            {
                Session[Constants.CHECKOUT_DISCOUNT] = (short)3;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Discount5Pct()
        {
            if (((User)Session[Constants.USER_INFO]).CumulativePoint < 500)
            {
                Session[Constants.DISCOUNT_NOTICE3PCT] = null;
                Session[Constants.DISCOUNT_NOTICE5PCT] = "Không đủ điểm tích luỹ";
            }
            else
            {
                Session[Constants.CHECKOUT_DISCOUNT] = (short)5;
            }
            return RedirectToAction("Index");
        }
    }
}