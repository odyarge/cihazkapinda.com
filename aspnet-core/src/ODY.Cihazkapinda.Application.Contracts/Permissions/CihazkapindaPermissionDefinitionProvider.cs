using ODY.Cihazkapinda.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ODY.Cihazkapinda.Permissions
{
    public class CihazkapindaPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CihazkapindaPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(CihazkapindaPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CihazkapindaResource>(name);
        }
    }
}
