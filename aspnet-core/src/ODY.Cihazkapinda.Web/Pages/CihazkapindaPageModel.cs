using ODY.Cihazkapinda.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ODY.Cihazkapinda.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class CihazkapindaPageModel : AbpPageModel
    {
        protected CihazkapindaPageModel()
        {
            LocalizationResourceType = typeof(CihazkapindaResource);
        }
    }
}