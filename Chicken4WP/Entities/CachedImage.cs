using System;
using System.Data.Linq.Mapping;

namespace Chicken4WP.Entities
{
    [Table]
    public class CachedImage
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "bigint IDENTITY(1,1)")]
        public long Id { get; set; }

        [Column]
        public string ImageUrl { get; set; }

        [Column(DbType = "image")]
        public byte[] Data { get; set; }
    }
}
