using GoCycling.Models;

namespace GoCycling.Models
{
    public class TileConquer
    {
        public string Id { get; set; } = null!;
        public Tile Tile { get; set; } = null!;
        public User User { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public bool Encircled { get; set; }
    }
}
