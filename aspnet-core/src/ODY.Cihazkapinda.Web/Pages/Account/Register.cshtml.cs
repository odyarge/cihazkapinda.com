using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ODY.Cihazkapinda.SiteSettings;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Settings;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace ODY.Cihazkapinda.Web.Pages.Account
{
    public class RegisterModel : AccountPageModel
    {

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrlHash { get; set; }

        [BindProperty]
        public PostInput Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsExternalLogin { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ExternalLoginAuthSchema { get; set; }


        private readonly ISiteSettingAppService _siteSettingAppService;


        public RegisterModel(IAccountAppService accountAppService, ISiteSettingAppService siteSettingAppService)
        {
            AccountAppService = accountAppService;
            _siteSettingAppService = siteSettingAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync(string reference)
        {
            await CheckToActivated();
            await CheckSelfRegistrationAsync();
            await TrySetEmailAsync();
            return Page();
        }

        private async Task TrySetEmailAsync()
        {
            if (IsExternalLogin)
            {
                var externalLoginInfo = await SignInManager.GetExternalLoginInfoAsync();
                if (externalLoginInfo == null)
                {
                    return;
                }

                if (!externalLoginInfo.Principal.Identities.Any())
                {
                    return;
                }

                var identity = externalLoginInfo.Principal.Identities.First();
                var emailClaim = identity.FindFirst(ClaimTypes.Email);

                if (emailClaim == null)
                {
                    return;
                }

                Input = new PostInput { EmailAddress = emailClaim.Value };
            }
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await CheckSelfRegistrationAsync();

                if (IsExternalLogin)
                {
                    var externalLoginInfo = await SignInManager.GetExternalLoginInfoAsync();
                    if (externalLoginInfo == null)
                    {
                        Logger.LogWarning("External login info is not available");
                        return RedirectToPage("./Login");
                    }

                    await RegisterExternalUserAsync(externalLoginInfo, Input.EmailAddress);
                }
                else
                {
                    await RegisterLocalUserAsync();
                }

                return Redirect(ReturnUrl ?? "~/"); //TODO: How to ensure safety? IdentityServer requires it however it should be checked somehow!
            }
            catch (BusinessException e)
            {
                Alerts.Danger(e.Message);
                return Page();
            }
        }

        protected virtual async Task RegisterLocalUserAsync()
        {
            ValidateModel();

            var userDto = await AccountAppService.RegisterAsync(
                new RegisterDto
                {
                    AppName = "MVC",
                    EmailAddress = Input.EmailAddress,
                    Password = Input.Password,
                    UserName = Input.UserName
                }
            );

            var user = await UserManager.GetByIdAsync(userDto.Id);
            await SignInManager.SignInAsync(user, isPersistent: true);
        }

        protected virtual async Task RegisterExternalUserAsync(ExternalLoginInfo externalLoginInfo, string emailAddress)
        {
            await IdentityOptions.SetAsync();

            var user = new IdentityUser(GuidGenerator.Create(), emailAddress, emailAddress, CurrentTenant.Id);

            (await UserManager.CreateAsync(user)).CheckErrors();
            (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();

            var userLoginAlreadyExists = user.Logins.Any(x =>
                x.TenantId == user.TenantId &&
                x.LoginProvider == externalLoginInfo.LoginProvider &&
                x.ProviderKey == externalLoginInfo.ProviderKey);

            if (!userLoginAlreadyExists)
            {
                (await UserManager.AddLoginAsync(user, new UserLoginInfo(
                    externalLoginInfo.LoginProvider,
                    externalLoginInfo.ProviderKey,
                    externalLoginInfo.ProviderDisplayName
                ))).CheckErrors();
            }

            await SignInManager.SignInAsync(user, isPersistent: true);
        }

        protected virtual async Task CheckSelfRegistrationAsync()
        {
            if (!await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled) ||
                !await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
            {
                throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
            }
        }

        public async Task CheckToActivated()
        {
            if (CurrentTenant.Id != null)
            {
                var checkActivated = await _siteSettingAppService.GetAsyncByTenantName(CurrentTenant.Name);
                if(checkActivated != null)
                {
                    bool activated = checkActivated.SITE_ACTIVATED;
                    if (activated == false)
                    {
                        Response.Redirect("/Error/Activate/");
                    }
                }
                else
                {
                    Response.Redirect("/Error/404");
                }
            }
        }

        public class PostInput
        {
            [Required]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
            public string UserName { get; set; }

            [Required]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxNameLength))]
            public string Name { get; set; }

            [Required]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxSurnameLength))]
            public string Surname { get; set; }

            [Required]
            [EmailAddress]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
            public string EmailAddress { get; set; }

            [Required]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
            [DataType(DataType.Password)]
            [DisableAuditing]
            public string Password { get; set; }
        }
    }
}
