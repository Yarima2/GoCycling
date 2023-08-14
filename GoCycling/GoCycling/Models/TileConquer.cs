using GoCycling.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCycling.Models
{
    public class TileConquer
    {
		public int Id { get; set; }
        public int X { get; set; }
		public int Y { get; set; }
		public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public long ActivityId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Encircled { get; set; }

		internal Feature ToGeoJson()
		{
			return new Feature(Geometry.CreateRectangle(new Tuple<int, int>(X, Y)));
		}
	}
}
