using System.Data.Linq;

namespace Chicken4WP.Entities
{
    public class ChickenDataContext : DataContext
    {
        private const string connectionString = @"isostore:/ChickenDataContext.sdf";

        public ChickenDataContext()
            : base(connectionString)
        { }

        public Table<Setting> Settings
        {
            get
            {
                return this.GetTable<Setting>();
            }
        }

        public Table<Temp> Temps
        {
            get
            {
                return this.GetTable<Temp>();
            }
        }

        public Table<CachedImage> CachedImages
        {
            get
            {
                return this.GetTable<CachedImage>();
            }
        }
    }
}
