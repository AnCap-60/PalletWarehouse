using PalletWarehouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalletWarehouse.Application
{
    public class Application
    {
        private Pallet[] loadedPallets;

        private bool run = true;

        public void Run()
        {
            while (run)
            {
                MainMenu();
            }
        }

        private static int GetChoice() => int.Parse(Console.ReadKey().KeyChar.ToString());
        private static int GetInt() => int.Parse(Console.ReadLine());

        private void MenuPageWrapper(Action menuFunc)
        {
            Console.Clear();

            menuFunc();

            Console.WriteLine("\nНажмите любую клавишу для продолжения");
            Console.ReadKey();
        }

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать на склад");
            Console.WriteLine("Для навигации нажимайте соответствующие клавиши\n");
            Console.WriteLine("1 - Вывести все известные паллеты");
            Console.WriteLine("2 - Получить информацию о паллетах");
            Console.WriteLine("3 - Вывести сгруппированные палеты по сроку годности, сортированные в группе по весу");
            Console.WriteLine("4 - Вывести 3 паллеты, которые содержат коробки с наибольшим сроком годности,\n    отсортированные по возрастанию объема");
            Console.WriteLine("0 - Выйти");

            int choice = GetChoice();
            switch (choice)
            {
                case 0:
                    run = false;
                    return;
                case 1:
                    MenuPageWrapper(ShowAllPallets);
                    break;
                case 2:
                    MenuPageWrapper(LoadPalletsPage);
                    break;
                case 3:
                    MenuPageWrapper(ShowGroupedPalletsPage);
                    break;
                case 4:
                    break;

                default:
                    Console.WriteLine("\nНеверный ввод");
                    Console.ReadKey();
                    break;
            }
        }

        private void ShowPallets(IEnumerable<Pallet> pallets)
        {
            Console.WriteLine("Паллет: " + pallets.Count());

            foreach (var pallet in pallets)
                Console.WriteLine(pallet);
        }

        private void ShowAllPallets()
        {
            if (loadedPallets == null || loadedPallets.Length == 0)
            {
                Console.WriteLine("Паллет не загружено");
                return;
            }
            
            ShowPallets(loadedPallets);            
        }

        private void LoadPalletsPage()
        {
            Console.WriteLine("Выберите способ получение паллет\n");
            Console.WriteLine("1 - Сгенерировать случайно");
            Console.WriteLine("2 - Загрузить из базы данных");
            Console.WriteLine("0 - Вернуться");

            int choice = GetChoice();
            switch (choice)
            {
                case 1:
                    MenuPageWrapper(GeneratePalletsPage);
                    break;
                case 2:
                    MenuPageWrapper(LoadPalletsFromDBPage);
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Неверный ввод");
                    return;
            }
        }

        private void ShowGroupedPalletsPage()
        {
            ShowPallets(loadedPallets
                .GroupBy(p => p.ExpirationDate)
                .OrderBy(group => group.First().ExpirationDate)
                .Select(g => g
                    .OrderBy(p => p.Weight))
                .SelectMany(p => p));
        }

        private void GeneratePalletsPage()
        {
            Console.Write("Сколько паллет сгенерировать?: ");
            if(!int.TryParse(Console.ReadLine(), out int count) || count < 1)
            {
                Console.WriteLine("Неверный ввод");
                return;
            }

            loadedPallets = GeneratePallets(count);
        }

        public Pallet[] GeneratePallets(int count)
        {
            Random random = new();

            var pallets = new Pallet[count];
            for (int i = 0; i < count; i++) //Цикл генерации самих паллет
            {
                int countOfBoxes = random.Next(0, 10);
                List<Box> boxes = new(countOfBoxes);
                for (int j = 0; j < countOfBoxes; j++) //Цикл генерации коробок для них
                {
                    int year = random.Next(1930, 2023);
                    int month = random.Next(1, 12);
                    int day = random.Next(1, 28);
                    boxes.Add(new(random.Next(1, 20), random.Next(1, 20), random.Next(1, 20), random.Next(1, 20),
                        new DateOnly(year, month, day)));
                }
                    

                pallets[i] = new(i, random.Next(20, 40), random.Next(20, 40), random.Next(20, 40), boxes);
            }

            return pallets;
        }

        private void LoadPalletsFromDBPage()
        {

        }
    }
}
