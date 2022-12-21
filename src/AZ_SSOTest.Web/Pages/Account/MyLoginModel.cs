
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Account.Settings;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Settings;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Account.Web.Pages.Account
{

    [ExposeServices(typeof(LoginModel))]
    public class MyLoginModel : IdentityServerSupportedLoginModel
    {

        public MyLoginModel(
            IAuthenticationSchemeProvider schemeProvider,
            IOptions<AbpAccountOptions> accountOptions,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IEventService identityServerEvents,
            IOptions<IdentityOptions> identityOptions
           ) : base(schemeProvider, accountOptions, interaction, clientStore, identityServerEvents, identityOptions)
        {

        }
        public override async Task<IActionResult> OnGetExternalLoginCallbackAsync(string returnUrl = "", string returnUrlHash = "", string remoteError = null)
        {
            if (remoteError != null)
            {
                Logger.LogWarning($"External login callback error: {remoteError}");
                return RedirectToPage("./Login");
            }

            await IdentityOptions.SetAsync();

            var loginInfo = await SignInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                Logger.LogWarning("External login info is not available");
                return RedirectToPage("./Login");
            }

            var result = await SignInManager.ExternalLoginSignInAsync(
                loginInfo.LoginProvider,
                loginInfo.ProviderKey,
                isPersistent: false,
                bypassTwoFactor: true
            );
            if (!result.Succeeded)
            {
                await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
                {
                    Identity = IdentitySecurityLogIdentityConsts.IdentityExternal,
                    Action = "Login" + result
                });
            }
            if (result.IsLockedOut)
            {
                Logger.LogWarning($"External login callback error: user is locked out!--58");
                throw new UserFriendlyException("Cannot proceed because user is locked out!--58");
            }
            if (result.IsNotAllowed)
            {
                Logger.LogWarning($"External login callback error: user is not allowed!--63");
                throw new UserFriendlyException("Cannot proceed because user is not allowed!--63");
            }
            var email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            //var kAccount = loginInfo.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

            Identity.IdentityUser user;
            //if (email.EndsWith(".net"))
            //{
            //    string kAccount = email.Split("@")[0];
            //    user = await _myIdentityUserAppService.GetUserByKAccount(kAccount);

            //    Logger.LogWarning($"Email EndsWith .net! --88");
            //}
            //else
            //{
            //    user = await UserManager.FindByEmailAsync(email);
            //    Logger.LogWarning($"Email EndsWith .com! --93");
            //}

            user = await UserManager.FindByEmailAsync(email);
            Logger.LogWarning($"Email EndsWith .com! --93");

            if (user == null)
            {
                Logger.LogWarning($"External login callback error: user is not allowed!--72");
                throw new UserFriendlyException("Cannot proceed because user is not allowed!--72");
            }
            else
            {
                if (await UserManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey) == null)
                {
                    CheckIdentityErrors(await UserManager.AddLoginAsync(user, loginInfo));
                }
            }
            await SignInManager.SignInAsync(user, false);
            await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
            {
                Identity = IdentitySecurityLogIdentityConsts.IdentityExternal,
                Action = result.ToIdentitySecurityLogAction(),
                UserName = user.Name
            });
            return RedirectSafely(returnUrl, returnUrlHash);

        }

        public override async Task<IActionResult> OnGetAsync()

        {

            LoginInput = new LoginInputModel();



            var context = await Interaction.GetAuthorizationContextAsync(ReturnUrl);



            if (context != null)

            {

                // TODO: Find a proper cancel way.

                // ShowCancelButton = true;



                LoginInput.UserNameOrEmailAddress = context.LoginHint;



                //TODO: Reference AspNetCore MultiTenancy module and use options to get the tenant key!

                var tenant = context.Parameters[TenantResolverConsts.DefaultTenantKey];

                if (!string.IsNullOrEmpty(tenant))

                {

                    CurrentTenant.Change(Guid.Parse(tenant));

                    Response.Cookies.Append(TenantResolverConsts.DefaultTenantKey, tenant);

                }

            }



            if (context?.IdP != null)

            {

                LoginInput.UserNameOrEmailAddress = context.LoginHint;

                ExternalProviders = new[] { new ExternalProviderModel { AuthenticationScheme = context.IdP } };

                return Page();

            }



            var providers = await GetExternalProviders();

            ExternalProviders = providers.ToList();



            EnableLocalLogin = await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin);



            if (context?.Client?.ClientId != null)

            {

                var client = await ClientStore.FindEnabledClientByIdAsync(context?.Client?.ClientId);

                if (client != null)

                {

                    // EnableLocalLogin = client.EnableLocalLogin;



                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())

                    {

                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();

                    }

                }

            }



            if (IsExternalLoginOnly)

            {

                return await base.OnPostExternalLogin(providers.First().AuthenticationScheme);

            }



            return Page();

        }
    }
}