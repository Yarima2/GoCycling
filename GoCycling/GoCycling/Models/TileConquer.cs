using GoCycling.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCycling.Models
{
    public class TileConquer
    {
		public string Id { get; set; } = null!;
        public virtual Tile Tile { get; set; } = null!;
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public long ActivityId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Encircled { get; set; }
    }
}
