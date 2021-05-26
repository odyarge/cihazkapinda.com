using System;
using System.Collections.Generic;
using System.Text;
using ODY.Cihazkapinda.Localization;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda
{
    /* Inherit your application services from this class.
     */
    public abstract class CihazkapindaAppService : ApplicationService
    {
        protected CihazkapindaAppService()
        {
            LocalizationResource = typeof(CihazkapindaResource);
        }
    }
}
