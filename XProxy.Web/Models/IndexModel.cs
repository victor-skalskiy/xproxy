using XProxy.Domain;

namespace XProxy.Web.Models;

public class IndexModel
{
    public List<UserSettingsItem> Settings { get; set; }
    public List<AV100FilterItem> Filters { get; set; }
    public AV100ResponseProfile Profile { get; set; }
}