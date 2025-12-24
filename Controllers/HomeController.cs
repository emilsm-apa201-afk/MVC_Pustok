using Microsoft.AspNetCore.Mvc;
using MVC_Pustokkk.Models;
using MVC_Pustokkk.ViewModels;

namespace MVC_Pustokkk.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            List<Featured> featured = new List<Featured>();
            {
                new Featured()
                {
                    Id = 1,
                    Author = "Apple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-1.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
                new Featured()
                {
                    Id = 2,
                    Author = "wpple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-2.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
                new Featured()
                {
                    Id = 3,
                    Author = "epple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-3.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
                new Featured()
                {
                    Id = 4,
                    Author = "tpple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-4.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
                new Featured()
                {
                    Id = 5,
                    Author = "ypple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-5.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
                new Featured()
                {
                    Id = 6,
                    Author = "opple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-6.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
                new Featured()
                {
                    Id = 7,
                    Author = "gpple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-7.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
                new Featured()
                {
                    Id = 8,
                    Author = "vpple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-8.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
                new Featured()
                {
                    Id = 9,
                    Author = "spple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-9.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
                new Featured()
                {
                    Id = 10,
                    Author = "hpple",
                    Details = "Lorem ipsun dolar",
                    Image = "product-10.jpg",
                    Price = 500,
                    PriceOld = 800,
                    PriceDiscount = 30
                };
            }
            HomeVM vm = new HomeVM()
            {
                //Featureds = (object)featured
            };
            return View(vm);
            
        }
    }
}
