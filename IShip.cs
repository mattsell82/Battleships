namespace sänkaskepp
{
    public interface IShip
    {
        int Hits { get; set; }
        int ShipId { get; set; }
        int ShipLength { get; set; }
        string ShipType { get; set; }
    }
}