using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.SiteSettings;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.EventBus.Local;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.PermissionManagement.Web.Utils;

namespace ODY.Cihazkapinda.Web.Pages.AbpPermissionManagement
{
    public class PermissionManagementModal : CihazkapindaPageModel
    {
        [Required]
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ProviderName { get; set; }

        [Required]
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ProviderKey { get; set; }

        [BindProperty]
        public List<PermissionGroupViewModel> Groups { get; set; }

        public string EntityDisplayName { get; set; }

        public bool SelectAllInThisTab { get; set; }

        public bool SelectAllInAllTabs { get; set; }

        protected IPermissionAppService PermissionAppService { get; }

        protected ILocalEventBus LocalEventBus { get; }

        public List<string> ignoreList { get; set; }
        public List<PermissionGroupDto> newList { get; set; }

        public PermissionManagementModal(
        ISiteSettingAppService siteSettingAppService,
        IPermissionAppService permissionAppService,
        ILocalEventBus localEventBus) : base(siteSettingAppService)
        {
            //ObjectMapperContext = typeof(AbpPermissionManagementWebModule);

            PermissionAppService = permissionAppService;
            LocalEventBus = localEventBus;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            ValidateModel();

            var result = await PermissionAppService.GetAsync(ProviderName, ProviderKey);

            if (CurrentTenant.Id != null)
            {
                ignoreList = new List<string>() {
                    "SiteSettings",
                    "ThemeSettings",
                    "Licenses",
                    "OperatorSettings",
                    "ProductPropertyTemplate",
                    "ProductPropertySubTemplate"
                };

                newList = new List<PermissionGroupDto>();

                int count = result.Groups.Count;
                for (int i = 0; i < count; i++)
                {
                    var name = result.Groups[i].Name;
                    if (!ignoreList.Contains(name))
                    {
                        newList.Add(result.Groups[i]);
                    }
                }
            }
            EntityDisplayName = result.EntityDisplayName;

            Groups = ObjectMapper
                .Map<List<PermissionGroupDto>, List<PermissionGroupViewModel>>(CurrentTenant.Id != null ? newList : result.Groups)
                .OrderBy(g => g.DisplayName)
                .ToList();

            foreach (var group in Groups)
            {
                new FlatTreeDepthFinder<PermissionGrantInfoViewModel>().SetDepths(group.Permissions);
            }

            foreach (var group in Groups)
            {
                group.IsAllPermissionsGranted = group.Permissions.All(p => p.IsGranted);
            }

            SelectAllInAllTabs = Groups.All(g => g.IsAllPermissionsGranted);

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var updatePermissionDtos = Groups
                .SelectMany(g => g.Permissions)
                .Select(p => new UpdatePermissionDto
                {
                    Name = p.Name,
                    IsGranted = p.IsGranted
                })
                .ToArray();

            await PermissionAppService.UpdateAsync(
                ProviderName,
                ProviderKey,
                new UpdatePermissionsDto
                {
                    Permissions = updatePermissionDtos
                }
            );

            await LocalEventBus.PublishAsync(
                new CurrentApplicationConfigurationCacheResetEventData()
            );

            return NoContent();
        }

        public class PermissionGroupViewModel
        {
            public string Name { get; set; }

            public bool IsAllPermissionsGranted { get; set; }

            public string DisplayName { get; set; }

            public List<PermissionGrantInfoViewModel> Permissions { get; set; }

            public string GetNormalizedGroupName()
            {
                return Name.Replace(".", "_");
            }
        }

        public class PermissionGrantInfoViewModel : IFlatTreeItem
        {
            [Required]
            [HiddenInput]
            public string Name { get; set; }

            public string DisplayName { get; set; }

            public int Depth { get; set; }

            public string ParentName { get; set; }

            public bool IsGranted { get; set; }

            public List<string> AllowedProviders { get; set; }

            public List<ProviderInfoViewModel> GrantedProviders { get; set; }

            public bool IsDisabled(string currentProviderName)
            {
                return IsGranted && GrantedProviders.All(p => p.ProviderName != currentProviderName);
            }

            public string GetShownName(string currentProviderName)
            {
                if (!IsDisabled(currentProviderName))
                {
                    return DisplayName;
                }

                return string.Format(
                    "{0} <span class=\"text-muted\">({1})</span>",
                    DisplayName,
                    GrantedProviders
                        .Where(p => p.ProviderName != currentProviderName)
                        .Select(p => p.ProviderName)
                        .JoinAsString(", ")
                );
            }
        }

        public class ProviderInfoViewModel
        {
            public string ProviderName { get; set; }

            public string ProviderKey { get; set; }
        }
    }
}
