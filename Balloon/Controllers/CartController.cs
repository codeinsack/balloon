using Balloon.Abstract;
using Balloon.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace Balloon.Controllers
{

    public class CartController : Controller
    {
        private IGoodRepository repository;

        public CartController(IGoodRepository repo)
        {
            repository = repo;
        }

        [Authorize]
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int goodId, string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                Good good = repository.Goods
                    .FirstOrDefault(p => p.GoodId == goodId);

                if (good != null)
                {
                    cart.AddItem(good, 1);
                }

                TempData["Item"] = good;

                return RedirectToAction("Index", new { returnUrl });
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int goodId, string returnUrl)
        {
            Good good = repository.Goods
                .FirstOrDefault(p => p.GoodId == goodId);

            if (good != null)
            {
                cart.RemoveLine(good);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        ApplicationContext db = new ApplicationContext();

        public ActionResult Checkout(decimal total, DateTime? deliveryDate, string deliveryAddress, bool delivery = false)
        {
            if (total == 0)
            {
                return RedirectToAction("EmptyCart", "Cart");
            }

            if (delivery == true && deliveryDate == null)
            {
                TempData["Date"] = "Вы не ввели дату доставки!";
                return RedirectToAction("Index", "Cart");
            }

            if (delivery == true && deliveryAddress == "")
            {
                TempData["Address"] = "Вы не ввели адрес доставки!";
                return RedirectToAction("Index", "Cart");
            }

            Cart cart = (Cart)Session["Cart"];

            CartIndexViewModel cartIndex = new CartIndexViewModel()
            {
                Cart = cart
            };

            List<OrderItem> orderItems = new List<OrderItem>();
            Good good = new Good();

            foreach (var item in cartIndex.Cart.Lines)
            {
                orderItems.Add(new OrderItem { GoodId = item.Good.GoodId, Price = item.Good.Price, Quantity = item.Quantity });

                good = db.Goods.Find(item.Good.GoodId);
                good.InOrderCount++;
                double result;

                if (good.ClickCount != 0)
                {
                    result = (double)good.InOrderCount / (double)good.ClickCount * 100;
                }
                else
                {
                    if (good.InOrderCount == 0)
                    {
                        result = -1;
                    }
                    else
                    {
                        result = (double)good.InOrderCount * 100;
                    }
                }

                good.SellingSuccess = result;
                db.Entry(good).State = EntityState.Modified;
                db.SaveChanges();
            }

            Order order = new Order
            {
                OrderItems = orderItems,
                Date = DateTime.Now,
                UserId = User.Identity.GetUserId(),
                TotalSum = total
            };

            if (delivery == true)
            {
                order.Delivery = delivery;
                order.DeliveryDate = deliveryDate;
                order.DeliveryAddress = deliveryAddress;
            }

            db.Orders.Add(order);
            db.SaveChanges();

            TempData["OrderId"] = order.OrderId;
            Session.Abandon();

            SendEmail("admin@mail.ru", User.Identity.GetUserName() , String.Format("На сайте был оформлен заказ #{0}", order.OrderId));
            
            return View();
        }

        public RedirectToRouteResult Update(FormCollection fc, string returnUrl)
        {
            string[] quantities = fc.GetValues("quantity");
            Cart cart = (Cart)Session["Cart"];
            CartIndexViewModel cartIndex = new CartIndexViewModel()
            {
                Cart = cart
            };

            List<CartLine> lineCollection = new List<CartLine>();

            int k = 0;

            foreach (var item in cartIndex.Cart.Lines)
            {
                int temp = item.Quantity = Convert.ToInt32(quantities[k]);
                k++;
                CartLine line = lineCollection
                    .Where(p => p.Good.GoodId == item.Good.GoodId)
                    .FirstOrDefault();

                lineCollection.Add(new CartLine
                {
                    Good = item.Good,
                    Quantity = temp
                });
            }

            Session["Cart"] = cart;

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult ClearCart(FormCollection fc, string returnUrl)
        {
            Session.Abandon();
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult EmptyCart()
        {
            return View();
        }

        public void SendEmail(string recipientmail, string frommail, string message)
        {
            var body = "<p>Email From: {0} </p><p>Message:</p><p>{1}</p>";
            var msg = new MailMessage();
            msg.To.Add(new MailAddress(recipientmail));
            msg.From = new MailAddress(frommail);
            msg.Body = string.Format(body, frommail, message);
            msg.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "user@outlook.com",
                    Password = "password"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
            }
        }
    }
}