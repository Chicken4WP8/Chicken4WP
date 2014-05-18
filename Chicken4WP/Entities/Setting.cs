using System.Data.Linq.Mapping;

namespace Chicken4WP.Entities
{
    [Table]
    public class Setting
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public SettingType Type { get; set; }

        [Column]
        public bool IsEnabled { get; set; }

        [Column]
        public bool IsInUsed { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Data { get; set; }
    }

    public enum SettingType
    {
        ProxySetting = 1,
        LanguageSetting = 2,
        CurrentUser = 3,
    }
}
