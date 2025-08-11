using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Services.Panel.Discount;
using OwncmsCore.Services.Panel.Settings;

namespace OwncmsCore.Controllers.Admin.Shop
{
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminShopDiscountController : Controller
    {

        //Discount for all start
        [Route("Store/Discount")]
        public async Task<ActionResult> DiscountAll()
        {

            ViewBag.discount_value = Setting_Service.GetSettings().discountall;
            return View("../Admin/Dashboard/store_discountall");
        }

        [HttpPost]
        [Route("Store/Discount/New")]
        public async Task<ActionResult> AddAllRemove(string discount)
        {
            Setting_Service.SetDiscount(discount);
            return Redirect(Request.Headers.Referer.ToString());
        }

        [Route("Store/Discount/Remove")]
        public async Task<ActionResult> DiscountAllRemove()
        {
            Setting_Service.SetDiscount("0");
            return Redirect(Request.Headers.Referer.ToString());
        }
        //Discount for all end




        //Coupon start
        [Route("Store/Coupon")]
        public async Task<ActionResult> Coupon()
        {

            ViewBag.DiscountCodes = Discount_Service.GetAllDiscountCode();
            return View("../Admin/Dashboard/coupon");
        }

        [HttpPost]
        [Route("Store/Coupon/New")]
        public async Task<ActionResult> NewCoupon(string count)
        {
            Discount_Service.Generate(int.Parse(count));
            return Redirect(Request.Headers.Referer.ToString());
        }

        [HttpGet]
        [Route("Store/Coupon/Remove/{discount_code}")]
        public async Task<ActionResult> RemoveCoupon(string discount_code)
        {
            Discount_Service.GetRmeoveCode(long.Parse(discount_code));
            return Redirect(Request.Headers.Referer.ToString());
        }
        //Coupon end





    }
}
