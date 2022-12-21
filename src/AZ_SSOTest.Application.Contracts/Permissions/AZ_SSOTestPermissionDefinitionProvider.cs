using AZ_SSOTest.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AZ_SSOTest.Permissions;

public class AZ_SSOTestPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AZ_SSOTestPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(AZ_SSOTestPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AZ_SSOTestResource>(name);
    }
}
