using System.Data.Linq.Mapping;

namespace Chicken4WP.Entities
{
    [Table]
    public class Account
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Data { get; set; }
    }
}
