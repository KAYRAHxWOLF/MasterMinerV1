using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MasterMiner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MasterMiner masterMiner = new MasterMiner();
            masterMiner.Start();
        }
    }

    internal class MasterMiner
    {
        private long ores = 0;
        private long cps = 1;
        private long cpsUpgradeLevel = 0;
        private long[] cpsUpgradeCosts = new long[] 
        {
            25, 50, 100, 250, 500, 750, 1000, 1250, 1500, 1750,
            2000, 2250, 2500, 2750, 3000, 3250, 3500, 3750, 4000, 4250,
            4500, 4750, 5000, 5500, 6000, 6500, 7000, 7500, 8000, 8500,
            9000, 9500, 10000, 11000, 12000, 13000, 14000, 15000, 17500, 20000,
            22500, 25000, 27500, 30000, 35000, 40000, 45000, 50000, 75000, 100000,
            110000, 125000, 150000, 175000, 200000, 250000, 500000, 750000, 1000000,
            1500000, 2000000, 2500000, 5000000, 10000000, 15000000, 25000000, 35000000, 50000000,
            65000000, 75000000, 100000000, 125000000, 150000000, 175000000, 200000000, 225000000, 250000000,
            275000000, 300000000, 400000000, 500000000, 600000000, 700000000, 800000000, 900000000, 1000000000,
            1250000000, 1500000000, 1750000000, 2000000000, 2500000000, 5000000000, 10000000000, 25000000000, 50000000000,
            100000000000
        };
        private long prestigeCost = 10000000000000;
        private long overallIncomeMultiplier = 1;
        private bool loggedIn = false;
        private bool spacePressed = false;
        public void Start()
        {
            Console.WriteLine("Willkommen bei Master Miner!");

            // Anmeldungs- oder Registrierungsfunktion aufrufen
            LoginOrRegister();

            // Hauptspiel-Loop
            while (true)
            {
                UpdateConsole();
                HandleInput();
                Thread.Sleep(100); // Kurze Verzögerung für Aktualisierung
            }
        }

        private void UpdateConsole()
        {
            Console.Clear();
            Console.WriteLine($"Total Ores: {ores}      Cps={cps}\n" +
                              "########################################################\n" +
                              "Upgrade Click: Press C Cost=(ClickerUpgradeCost_value)\n" +
                              "Upgrade machines: Press M Cost=(MachineUpgradeCost_value)\n" +
                              "Buy Boosters: Press B to open (BoosterShopMenue)\n" +
                              "Open menue: (Save file, Exit, Settings, back)");
            Console.CursorVisible = false;
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Spacebar:
                        if (!spacePressed)
                        {
                            Click();
                            spacePressed = true;
                        }
                        break;
                    case ConsoleKey.C:
                        OpenUpgradeMenu();
                        break;
                    case ConsoleKey.M:
                        OpenMachineUpgradeMenu();
                        break;
                    case ConsoleKey.B:
                        OpenBoosterShop();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                spacePressed = false;
            }
        }

        private void Click()
        {
            ores += cps * overallIncomeMultiplier;
        }

        private void OpenUpgradeMenu()
        {
            // Implementieren Sie Logik für das Upgrade-Menü hier
            // Beispiel: Console.WriteLine("Upgrade-Menü geöffnet!");
        }

        private void OpenMachineUpgradeMenu()
        {
            // Implementieren Sie Logik für das Maschinen-Upgrade-Menü hier
            // Beispiel: Console.WriteLine("Maschinen-Upgrade-Menü geöffnet!");
        }

        private void OpenBoosterShop()
        {
            // Implementieren Sie Logik für den Booster-Shop hier
            // Beispiel: Console.WriteLine("Booster-Shop geöffnet!");
        }

        private async Task SaveGame()
        {
            // Implementieren Sie Logik zum Speichern des Spiels hier
        }

        private void ExitGame()
        {
            Environment.Exit(0);
        }

        private void LoginOrRegister()
        {
            // Implementieren Sie Logik für die Anmeldung oder Registrierung hier
        }

        internal async void Prestige()
        {
            if (ores >= prestigeCost)
            {
                Console.WriteLine("Du hast dein Prestige erhöht!");
                ores -= prestigeCost;
                overallIncomeMultiplier *= 2;
                prestigeCost *= 2; // Beispiel für Anpassung der Prestigekosten
            }
            else
            {
                Console.WriteLine($"Nicht genug Erze für Prestige. Du benötigst mindestens {prestigeCost} Erze.");
            }
            await Task.Delay(2000); // Beispiel für Verzögerung nach Prestige
        }
    }
}
