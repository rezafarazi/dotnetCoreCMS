using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;

namespace OwncmsCore.Middleware.Language
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LanguageMiddle:Attribute
    {

        public override bool Match(object? obj)
        {
            var cultureInfo = new CultureInfo("fa-IR");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            return base.Match(obj);
        }

    }
}
