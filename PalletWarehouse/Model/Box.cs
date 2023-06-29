using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalletWarehouse.Model
{
    public class Box : WarehouseObject
    {
        private const int expirationTime = 100;

        public Box(int width, int height, int depth, int weight, DateOnly productionDate) : base(width, height, depth)
        {
            this.Weight = weight;
            this.ProductionDate = productionDate;
        }

        public DateOnly ProductionDate { get; set; }

        public override DateOnly ExpirationDate => ProductionDate.AddDays(expirationTime);

        public int Weight { get; set; }
    }
}
