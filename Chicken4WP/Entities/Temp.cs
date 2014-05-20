using System.Data.Linq.Mapping;

namespace Chicken4WP.Entities
{
    [Table]
    public class Temp
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public TempType Type { get; set; }

        [Column]
        public string Data { get; set; }
    }

    public enum TempType
    {
        TweetDetail = 1,
        User = 2,
    }
}
