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
        private long upgradeClickCost = 1000000;
//a few changes must happen here
        private long prestigeCost = 10000000000000;
        private long overallIncomeMultiplier = 1;
        private bool loggedIn = false;
        private bool spacePressed = false;
        public void Start()
        {
            Console.WriteLine("Willkommen bei Master Miner!");

            
            LoginOrRegister();

            
            while (true)
            {
                UpdateConsole();
                HandleInput();
                Thread.Sleep(100); //  Aktualisierung
            }
        }

        private void UpdateConsole()
        {
            Console.Clear();
            Console.WriteLine($"Total Ores: {ores}      Cps={cps}\n" +
                              "########################################################\n" +
                              "Upgrade Click: C " +
                              "Upgrade machines: M\n" +
                              "Booster Shop: B\n" +
                              "Open Game menue: Y\n" +
                              "Log out: Q");
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
                        OpenUpgradeClickMenu();
                        break;
                    case ConsoleKey.M:
                        OpenMachineUpgradeMenu();
                        break;
                    case ConsoleKey.B:
                        OpenBoosterShop();
                        break;
                    case ConsoleKey.Y:
                        OpenGameMenue();
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

        private void OpenUpgradeClickMenu()
        {
            Console.WriteLine("Upgrade Click-Menü geöffnet!");
            Console.WriteLine("Optionen:");
            Console.WriteLine("1. Upgrade Clicker: Kosten = X Erze, CPS-Boost = Y");
            Console.WriteLine("2. Zurück");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    UpgradeClicker();
                    break;
                case "2":
                    // Zurück zum Hauptmenü
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe.");
                    break;
            }
        }

        private void UpgradeClicker()
        {
            // Hier fügst du die Logik hinzu, um den Clicker zu verbessern
            if (ores >= upgradeClickCost)
            {
                ores -= upgradeClickCost;
                cps++;
                Console.WriteLine("Clicker erfolgreich verbessert!");
            }
            else
            {
                Console.WriteLine("Nicht genug Erze für das Upgrade.");
            }
        }

        private void OpenMachineUpgradeMenu()
        {
            Console.WriteLine("Maschinen-Upgrade-Menü geöffnet!");
            Console.WriteLine("Optionen:");
            Console.WriteLine("1. Maschine 1: Kosten = X Erze, CPS-Boost = Y");
            Console.WriteLine("2. Maschine 2: Kosten = X Erze, CPS-Boost = Y");
            Console.WriteLine("3. Zurück");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    // Implementiere Logik für Maschine 1-Upgrade
                    break;
                case "2":
                    // Implementiere Logik für Maschine 2-Upgrade
                    break;
                case "3":
                    // Zurück zum Hauptmenü
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe.");
                    break;
            }
        }

        private void OpenBoosterShop()
        {
            Console.WriteLine("Booster-Shop geöffnet!");
            Console.WriteLine("Optionen:");
            Console.WriteLine("1. Booster 1: Kosten = X Erze, Boost = Y");
            Console.WriteLine("2. Booster 2: Kosten = X Erze, Boost = Y");
            Console.WriteLine("3. Zurück");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    // Implementiere Logik für Booster 1-Kauf
                    break;
                case "2":
                    // Implementiere Logik für Booster 2-Kauf
                    break;
                case "3":
                    // Zurück zum Hauptmenü
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe.");
                    break;
            }
        }

        private void OpenGameMenue()
        {
            Console.WriteLine("-Spiel Menü-");
            Console.WriteLine("Optionen:");
            Console.WriteLine("Einstellungen: X");
            Console.WriteLine("Spiel speichern: S");
            Console.WriteLine("Zurück zum spiel: V");
            var input = Console.ReadLine();
            switch (input)
            {
                case "X":
                    // Implementiere Logik für Einstellungen-Menü
                    break;
                case "S":
                    SaveGame();
                    break;
                case "V":
                    
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe.");
                    break;
            }
        }
        private async Task SaveGame()
        {
            // how to Logik zum Speichern des Spiels ?
        }

        private void ExitGame()
        {
            Console.WriteLine("Das Spiel wird beendet...(Drücken die eine zufällige taste)");
            Console.ReadKey();
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
