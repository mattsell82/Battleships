namespace sänkaskepp
{
    public interface IShipGrid : IGrids
    {
        void AddShip(IShip ship);
        int[] FindPosition(int shipLength, int shipOrientation, int shipId);
        int GetHits();
        int GetMaxScore();
        int GetScore();
        int MarkIncomingShot(int row, int col);
        void MarkShotOnShip(int shipId);
    }
}