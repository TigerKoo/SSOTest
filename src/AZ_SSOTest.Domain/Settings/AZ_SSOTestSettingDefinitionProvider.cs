using Volo.Abp.Settings;

namespace AZ_SSOTest.Settings;

public class AZ_SSOTestSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AZ_SSOTestSettings.MySetting1));
    }
}
