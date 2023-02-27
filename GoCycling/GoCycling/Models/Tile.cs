using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GoCycling.Models
{
    public class Tile
    {
        public int Id { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public virtual ICollection<TileConquer> Conquers { get; set; } = null!;

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
