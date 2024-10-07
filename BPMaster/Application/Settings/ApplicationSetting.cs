using Common.Application.Settings;
using RPMSMaster.Common.Application.Settings;

namespace Application.Settings
{
    public class ApplicationSetting : BaseAppSetting
    {
        public PasswordSetting PasswordSetting { get; set; } = new();
        public override BasePermissionSetting PermissionSetting { get => new PermissionSetting(); }
        public EmailSetting? EmailSetting { get; set; } 
    }
}
