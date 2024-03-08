using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterMiner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            Player player = database.LoadPlayerData("TestPlayer", "password");
            MasterMiner masterMiner = new MasterMiner(player);
            masterMiner.Start();
        }
    }

    internal class Player
    {
        public string playerName;
        public int totalOres;
        private int clickValue;
        private int totalClicks;
        private List<Upgrade> upgrades;

        public Player(string playerName)
        {
            this.playerName = playerName;
            totalOres = 0;
            clickValue = 1;
            totalClicks = 0;
            upgrades = new List<Upgrade>();
        }

        public int GetTotalOres()
        {
            return totalOres;
        }

        public void ClickMine()
        {
            totalOres += clickValue;
            totalClicks++;
        }

        public int GetClickValue()
        {
            return clickValue;
        }

        public void PurchaseUpgrade(Upgrade upgrade)
        {
            upgrades.Add(upgrade);
            clickValue += upgrade.GetClickValueIncrease();
        }
    }


    internal class Upgrade
    {
        private string upgradeName;
        private int upgradeCost;
        private int clickValueIncrease;

        public Upgrade(string upgradeName, int upgradeCost, int clickValueIncrease)
        {
            this.upgradeName = upgradeName;
            this.upgradeCost = upgradeCost;
            this.clickValueIncrease = clickValueIncrease;
        }

        public string GetUpgradeName()
        {
            return upgradeName;
        }

        public int GetUpgradeCost()
        {
            long index = clickValueIncrease / 10; // Annehmen, dass Kosten linear mit clickValueIncrease steigen
            long[] upgradeCosts = new long[]
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

            if (index >= upgradeCosts.Length)
            {
                Console.WriteLine("Maximale Upgrades erreicht!");
                return int.MaxValue;
            }

            return (int)upgradeCosts[index];
        }



        public int GetClickValueIncrease()
            {
            return clickValueIncrease;
            }
    }

    internal class Database
    {
        public Database()
        {
            // Verbindungsinitialisierung, Datenbankaufbau usw.
        }

        public void SavePlayerData(Player player)
        {
            // Logik zum Speichern der Spielerdaten in der Datenbank
        }

        public Player LoadPlayerData(string playerName, string password)
        {
            // Logik zum Laden der Spielerdaten aus der Datenbank
            // Beispielrückgabe:
            return new Player(playerName);
        }
    }

    internal class MasterMiner
    {
        private Player player;

        public MasterMiner(Player player)
        {
            this.player = player;
        }

        public void Start()
        {
            Console.WriteLine("Willkommen bei Master Miner!");

            while (true)
            {
                UpdateConsole();
                HandleInput();
                Task.Delay(100).Wait(); // Aktualisierung
            }
        }

        private void UpdateConsole()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Total Ores: {player.GetTotalOres()}      CPS: {player.GetClickValue()}");
            Console.WriteLine("########################################################");
            Console.WriteLine("Upgrade Click: C \n Save game: S \n Log out: Q");
            Console.CursorVisible = false;
        }


        private void HandleInput()  // spiel logik
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Spacebar: // click logik
                        player.ClickMine();
                        break;
                    case ConsoleKey.C:
                        OpenUpgradeClickMenu();  //menü öffnen
                        break;
                    case ConsoleKey.Q: // quitting
                        ExitGame();
                        break;
                    default:
                        break;
                }
            }
        }

        private void OpenUpgradeClickMenu()
        {
            Console.WriteLine("Upgrade Click-Menü geöffnet!");
            Console.WriteLine("Optionen:");
            Console.WriteLine($"1. Upgrade Clicker: Kosten = {CalculateUpgradeCost()} Ores");
            Console.WriteLine("2. Zurück");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    UpgradeClicker();
                    break;
                case "2":
                    Console.Clear(); // menü schließen
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe.");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                    
            }
        }

        private void UpgradeClicker()
        {
            // Überprüfen, ob der Spieler genug Ores hat, um das Upgrade zu kaufen
            int upgradeCost = CalculateUpgradeCost();
            if (player.GetTotalOres() >= upgradeCost)
            {
                // Upgrade kaufen
                Upgrade upgrade = new Upgrade("Clicker Upgrade", upgradeCost, 1); // Annahme: Der Clicker erhöht sich um 1
                player.PurchaseUpgrade(upgrade);
                Console.WriteLine($"Clicker wurde verbessert! Neue CPS: {player.GetClickValue()}");

                // Spielerressourcen aktualisieren
                player.totalOres -= upgradeCost;
            }
            else
            {
                Console.WriteLine("Not enough Ores to purchase the upgrade! Press any key to close the menu...");
                Console.ReadKey(true); // Warten auf eine beliebige Taste, um das Menü zu schließen
            }

            Console.Clear(); // Menü schließen
        }


        private int CalculateUpgradeCost()
        {
            // Annahme: Die Kosten steigen linear mit der aktuellen CPS (Clicks pro Sekunde)
            return player.GetClickValue() * 10;
        }


        private async Task SaveGame()
        {
            // Logik zum Speichern des Spiels
        }

        private void ExitGame()
        {
            Console.WriteLine("Das Spiel wird beendet...(Drücken Sie eine beliebige Taste)");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}

