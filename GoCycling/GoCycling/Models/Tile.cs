using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCycling.Models
{
    public class Tile
    {
        [Key]
        public int X { get; set; }
        [Key]
        public int Y { get; set; }

        public virtual ICollection<TileConquer> Conquers { get; set; } = null!;

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
