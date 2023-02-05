using GoCycling.Model;

namespace GoCycling.Queries
{
    public class TileQueries
    {

        public static int TestCreate(GoCycleDbContext dbContext)
        {
            Tile newTile = new Tile(0, 1);
            dbContext.Add(newTile);
            dbContext.SaveChanges();

            return dbContext.Tiles.First().Y;
        }

    }
}
