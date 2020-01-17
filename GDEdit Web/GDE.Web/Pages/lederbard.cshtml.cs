using GDE.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GDE.Web.Pages
{
    public class lederbardModel : PageModel
    {
        public void OnGet()
        {
            GlobalVariables.CurrentSection = LinkItems.rankings;
        }
    }
}