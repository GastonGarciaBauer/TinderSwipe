namespace TinderSwipe.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? LikeId { get; set; }
        public int? DislikeId { get; set; }
        
    }
}
