using GoCycling.Models;

namespace GoCycling.Models
{
    public class TileConquer
    {
        public string Id { get; set; } = null!;
        public virtual Tile Tile { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public long ActivityId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Encircled { get; set; }
    }
}
