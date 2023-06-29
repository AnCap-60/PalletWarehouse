using PalletWarehouse.Model;
using static PalletWarehouse.Application.Application;
using System.Collections;
using System.Linq;

namespace PalletWarehouseTests
{
    public class Tests
    {
        Random random = new();

        [SetUp]
        public void Setup() { }

        [Test]
        public void OneVolumePalletTest()
        {
            Pallet p = new(1, 1, 1, 1, new()); //without boxes
            Assert.That(p.Volume, Is.EqualTo(1));
        }

        [Test]
        public void VolumeOfPalletWithBoxTest()
        {
            int width = 2; int height = 3; int depth = 4;
            Pallet p = new(0, width, height, depth, new());
            p.AddBox(new());

            Assert.That(p.Volume, Is.EqualTo(width * height * depth + 1));
        }

        [Test]
        public void AddBoxWhichBiggerThanPalletTest()
        {
            Pallet pallet = new();

            TestDelegate testDelegate = () => pallet.AddBox(new Box(2, 2, 2, 2, new()));
            Assert.Throws<ArgumentException>(testDelegate);
        }

        [Test]
        public void AddNormalBoxPalletTest()
        {
            Pallet pallet = new();

            TestDelegate testDelegate = () => pallet.AddBox(new Box(1, 1, 1, 1, new()));
            Assert.DoesNotThrow(testDelegate);
        }

        private DateOnly GetRandomDate() => new(random.Next(0, 2222), random.Next(1, 12), random.Next(1, 28));

        [Test]
        public void BoxExpirationTimeTest()
        {
            DateOnly date = GetRandomDate();

            Box box = new(1, 1, 1, 1, date);
            Assert.That(box.ExpirationDate, Is.EqualTo(date.AddDays(100)));
            Assert.That(box.ExpirationDate, Is.EqualTo(box.ProductionDate.AddDays(100)));
        }

        [Test]
        public void SuccesGeneratingPalletsTest()
        {
            Assert.DoesNotThrow(() => GeneratePallets(50));
        }

        [Test]
        public void GroupedPalletsQueryTest()
        {
            Pallet[] pallets = GeneratePallets(50);

            var groupedPallets = GroupedPalletsQuery(pallets).ToArray();

            var orderedPallets = pallets
                .OrderBy(p => p.ExpirationDate)
                .ThenBy(p => p.Weight);

            Assert.That(Enumerable.SequenceEqual(groupedPallets, orderedPallets));
        }

        [Test]
        public void MostDurablePaletsQueryTest()
        {
            Pallet[] pallets = GeneratePallets(50);

            var palletsByQuery = MostDurablePaletsQuery(pallets).ToArray();

            var palletsBySort = (Pallet[])pallets.Clone();
            Array.Sort(palletsBySort, new PalletExpirationComparer(false));
            palletsBySort = palletsBySort.Take(3).ToArray();
            Array.Sort(palletsBySort, new PalletVolumeComparer());

            Assert.That(Enumerable.SequenceEqual(palletsByQuery, palletsBySort));
        }
    }
}