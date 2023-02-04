namespace GoCycling.Model
{
    public class Tile
    {
        public int TileId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime FirstConquered { get; set; }

        public TileConquer LastConquer { get; set; }

        public virtual List<TileConquer> Conquers { get; set; }

    }
}
