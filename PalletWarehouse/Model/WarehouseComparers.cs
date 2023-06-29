using System.Linq;

namespace PalletWarehouse.Model
{
    public class PalletExpirationComparer : IComparer<Pallet>
    {
        int compareMultiplier;

        public PalletExpirationComparer()
        {
            compareMultiplier = 1;
        }

        public PalletExpirationComparer(bool ascending)
        {
            compareMultiplier = ascending ? 1 : -1;
        }

        public int Compare(Pallet? x, Pallet? y) => compareMultiplier * x.ExpirationDate.CompareTo(y.ExpirationDate);
    }

    public class PalletVolumeComparer : IComparer<Pallet>
    {
        int compareMultiplier;

        public PalletVolumeComparer()
        {
            compareMultiplier = 1;
        }

        public PalletVolumeComparer(bool ascending)
        {
            compareMultiplier = ascending ? 1 : -1;
        }

        public int Compare(Pallet? x, Pallet? y) => compareMultiplier * x.Volume.CompareTo(y.Volume);
    }
}
