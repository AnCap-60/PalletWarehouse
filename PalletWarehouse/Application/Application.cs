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

        public void Run()
        {

        }

        private int GetChoice() => int.Parse(Console.ReadKey().KeyChar.ToString());

        private void MenuPageWrapper(Action menuFunc)
        {
            Console.Clear();

            menuFunc();

            Console.WriteLine("Нажмите любую клавишу для продолжения");
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
                    return;
                case 1:
                    MenuPageWrapper(ShowAllPallets);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;

                default:
                    Console.WriteLine("Неверный ввод");
                    Console.ReadKey();
                    break;
            }
        }

        private void ShowPallets(IEnumerable<Pallet> pallets)
        {
            Console.WriteLine("Всего паллет загружено: " + pallets.Count());

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

        private void LoadPallets()
        {
            Console.WriteLine("Выберите способ получение паллет\n");
            Console.WriteLine("1 - Сгенерировать случайно");
            Console.WriteLine("2 - Ввести с клавиатуры (долго)");
            Console.WriteLine("3 - Загрузить из базы данных");
            Console.WriteLine("0 - Вернуться");

            int choice = GetChoice();
            switch (choice)
            {
                case 1:


                default:
                    Console.WriteLine("Неверный ввод");
                    return;
            }
        }

        private void GeneratePallets()
        {
            Console.Write("Сколько паллет сгенерировать?: ");
            if(!int.TryParse(Console.ReadLine(), out int count) || count < 1)
            {
                Console.WriteLine("Неверный ввод");
                return;
            }

            Random random = new();

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < random.Next(0, 10); j++) //Цикл генерации коробок
                {

                }
            }
        }
    }
}
