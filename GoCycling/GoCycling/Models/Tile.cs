using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCycling.Models
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int? LastConquerActivity { get; set; }
        public bool Encircled { get; set; }


        public Tile(int x, int y, int? lastConquerActivity = null, bool encircled = false)
        {
            X = x;
            Y = y;
            LastConquerActivity = lastConquerActivity;
            Encircled = encircled;
        }

    }
}
