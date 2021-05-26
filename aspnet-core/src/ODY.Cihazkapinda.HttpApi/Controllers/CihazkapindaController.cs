using ODY.Cihazkapinda.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ODY.Cihazkapinda.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class CihazkapindaController : AbpController
    {
        protected CihazkapindaController()
        {
            LocalizationResource = typeof(CihazkapindaResource);
        }
    }
}