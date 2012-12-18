using System.Web.Mvc;
using MvcIpAddressActionFilterExample.Filters;

namespace MvcIpAddressActionFilterExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string html = "Hello.<br /><br />";
            html += "<a href=\"Home\\RestrictedWebPage\">Try to access a restricted web page</a>";

            return Content( html );
        }

        [AuthorizeIpAddress]
        public ActionResult RestrictedWebPage()
        {
            // Get users IP Address
            string ipAddress = HttpContext.Request.UserHostAddress;

            string html = "Congratulations: You've been granted access.<br /><br />";
            html += string.Format( "You were granted access because your IP address '{0}' is in the ", ipAddress );
            html += "list of authorized IP addresses:<br />";

            var validIpAddresses = AuthorizeIpAddressAttribute.GetValidIpAddresses();

            html += "<ul>";

            foreach ( var validIpAddress in validIpAddresses )
            {
                html += string.Format( "<li>{0}</li>", validIpAddress );
            }

            html += "</ul>";

            return Content( html );
        }
    }
}