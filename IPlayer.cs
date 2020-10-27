namespace sänkaskepp
{
    public interface IPlayer
    {
        ILogGrid LogGrid { get; set; }
        string Name { get; set; }
        IShipGrid ShipGrid { get; set; }
    }
}