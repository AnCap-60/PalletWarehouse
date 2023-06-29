namespace PalletWarehouse.Model
{
    public abstract class WarehouseObject
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }

        public virtual int Volume => Width * Height * Depth;

        public abstract DateOnly ExpirationDate { get; }

        public WarehouseObject(int width, int height, int depth)
        {
            Width = width; Height = height; Depth = depth;
        }

        public override string ToString()
        {
            return $"Width: {Width}, Height{Height}, Depth: {Depth}";
        }
    }
}
