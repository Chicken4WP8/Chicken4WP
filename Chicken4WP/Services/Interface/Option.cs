namespace Chicken4WP.Services.Interface
{
    public abstract class Option
    {
        public int? Count { get; set; }
        public long? SinceId { get; set; }
        public long? MaxId { get; set; }
    }

    public class HomeTimelineTweetOption : Option
    {
        //public int? Count { get; set; }
        //public long? SinceId { get; set; }
        //public long? MaxId { get; set; }
    }
}
