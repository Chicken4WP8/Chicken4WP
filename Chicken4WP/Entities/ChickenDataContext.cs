using System.Data.Linq;

namespace Chicken4WP.Entities
{
    public class ChickenDataContext : DataContext
    {
        private const string connectionString = @"isostore:/ChickenDataContext.sdf";
        private static ChickenDataContext context;
        public static ChickenDataContext Instance
        {
            get
            {
                if (context == null)
                {
                    context = new ChickenDataContext();
                    if (!context.DatabaseExists())
                    {
                        context.CreateDatabase();
                        Initialize();
                    }
                }
                return context;
            }
        }

        public ChickenDataContext()
            : base(connectionString)
        { }

        private static void Initialize()
        {
            var proxy = new Setting
             {
                 Type = SettingType.ProxySetting,
                 IsEnabled = true,
                 Name = "Base",
             };
            context.Settings.InsertOnSubmit(proxy);
            var twip = new Setting
            {
                Type = SettingType.ProxySetting,
                IsEnabled = true,
                Name = "Twip4",
                Data = "https://wxt2005.org/tapi/o/6Z66RF"
            };
            context.Settings.InsertOnSubmit(twip);
            context.SubmitChanges();
        }

        public Table<Account> Accounts
        {
            get
            {
                return this.GetTable<Account>();
            }
        }

        public Table<Setting> Settings
        {
            get
            {
                return this.GetTable<Setting>();
            }
        }
    }
}
