using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PalletWarehouse.Model
{
    public class Pallet : WarehouseObject
    {
        private const int selfWeight = 30;

        private int id;
        public int Id => id;

        private List<Box> boxes;
        public Box[] Boxes => boxes.ToArray();

        public override DateOnly ExpirationDate => boxes.Min(box => box.ExpirationDate);

        public int Weight => boxes.Sum(box => box.Weight) + selfWeight;

        public override int Volume => boxes.Sum(box => box.Volume) + base.Volume;

        public Pallet(int id, int width, int height, int depth, List<Box> boxes) : base(width, height, depth)
        {
            this.id = id;
            this.boxes = boxes;
        }

        public void AddBox(Box box)
        {
            if (box.Width > Width || box.Depth > Depth)
                throw new ArgumentException("Box is bigger than pallet");

            boxes.Add(box);
        }

        public override string ToString()
        {
            return $"ID: {Id}" + base.ToString() + $" Weight: {Weight},\n" +
                $"  Volume: {Volume}, Expiration date: {ExpirationDate}";
        }
    }
}
