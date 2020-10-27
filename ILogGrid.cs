namespace sänkaskepp
{
    public interface ILogGrid : IGrids
    {
        void MarkShot(int row, int col, bool status);
        int[] RandomCoordinate();
        bool ValidateShot(int row, int col);
    }
}