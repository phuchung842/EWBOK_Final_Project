using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDao
    {
        EWBOK_DbContext db = null;
        public OrderDao()
        {
            db = new EWBOK_DbContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public bool Update(Order OrderEntity)
        {
            var order = db.Orders.Find(OrderEntity.ID);
            order.PctDiscount = OrderEntity.PctDiscount;
            order.ShipAddress = OrderEntity.ShipAddress;
            order.CustomerID = OrderEntity.CustomerID;
            order.ShipEmail = OrderEntity.ShipEmail;
            order.ShipMobile = OrderEntity.ShipMobile;
            order.ShipName = OrderEntity.ShipName;
            order.Total = OrderEntity.Total;
            order.TotalDiscount = OrderEntity.TotalDiscount;
            db.SaveChanges();
            return true;
        }
        public List<Order> ListAllOrder()
        {
            return db.Orders.OrderByDescending(x => x.CreateDate).ToList();
        }
        public Order GetOrderById(long id)
        {
            return db.Orders.Find(id);
        }
        public List<Order> ListOrderByCustomerId(long id)
        {
            return db.Orders.Where(x => x.CustomerID == id).OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Order> ListOrderByCustomerId_Implement(long id)
        {
            return db.Orders.Where(x => x.CustomerID == id && x.Status == -1).OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Order> ListOrderByCustomerId_Ship(long id)
        {
            return db.Orders.Where(x => x.CustomerID == id && x.Status == 0).OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Order> ListOrderByCustomerId_Cancelled(long id)
        {
            return db.Orders.Where(x => x.CustomerID == id && x.Status == -2).OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Order> ListOrderByCustomerId_Decline(long id)
        {
            return db.Orders.Where(x => x.CustomerID == id && x.Status == -3).OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Order> ListByMonthAndYear(int month, int year)
        {
            if (month == 1)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 31);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else if (month == 2)
            {
                if (IsLeapYear(year))
                {
                    DateTime? start = new DateTime(year, month, 1);
                    DateTime? end = new DateTime(year, month, 29);
                    return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
                }
                else
                {
                    DateTime? start = new DateTime(year, month, 1);
                    DateTime? end = new DateTime(year, month, 28);
                    return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
                }
            }
            else if (month == 3)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 31);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else if (month == 4)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 30);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else if (month == 5)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 31);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else if (month == 6)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 30);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else if (month == 7)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 31);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else if (month == 8)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 31);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else if (month == 9)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 30);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else if (month == 10)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 31);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else if (month == 11)
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 30);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
            else
            {
                DateTime? start = new DateTime(year, month, 1);
                DateTime? end = new DateTime(year, month, 31);
                return db.Orders.Where(x => x.CreateDate >= start && x.CreateDate <= end).ToList();
            }
        }
        private bool IsLeapYear(int year)
        {
            if ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0))
            {
                return true;
            }
            return false;
        }
    }
}
