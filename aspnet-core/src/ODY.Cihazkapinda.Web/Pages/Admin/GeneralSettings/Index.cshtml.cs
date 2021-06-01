using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.SiteSettings;

namespace ODY.Cihazkapinda.Web.Pages.Admin.GeneralSettings
{
    public class IndexModel : CihazkapindaPageModel
    {
        public bool ResponseSuccessfully { get; set; }
        public IndexModel(ISiteSettingAppService _siteSettingAppService) : base(_siteSettingAppService)
        {
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            if(ResponseSuccessfully == true)
            {
                Alerts.Success(
                   text: "G�ncelleme i�leminiz ba�ar�yla ger�ekle�tirilmi�tir.",
                   title: "Ba�ar�l�"
               );
            }
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}
