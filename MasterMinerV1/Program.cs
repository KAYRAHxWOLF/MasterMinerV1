using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;
using MasterMinerV1.Database;

namespace MasterMiner
{
    internal class Program
    {
        //Verbindung zur datenbank
        public static DBconfig db = new DBconfig();
        static void Main(string[] args)
        {
            //Programmablauf
            Scripts.Config();
            Player player = null;
            do
            {
                int spielstand = Scripts.Spielstand.Auswahl();
                player = Scripts.Spielstand.CreateLoad(spielstand);
            } while (player == null);
            Scripts.GameLoop(player);

            //Hauptmenü


            //Testcode
            /*
            Player player = database.LoadPlayerData("TestPlayer", "password");
            MasterMiner masterMiner = new MasterMiner(player);
            masterMiner.Start();
            BigInteger bbc = new BigInteger();
            BigInteger.TryParse("564465456456564564564645496148194862864591268659265864125384929516967139765902476871223469082475689734565564654654654564", out bbc);
        */
        }
        internal class Scripts
        {
            public static void Config()
            {
                Console.CursorVisible = false;
            }
            internal class Spielstand
            {
                public static int Auswahl()
                {
                    Console.Clear();
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 1) != null)
                        Console.Write($"[1]Spielstand 1 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 1).Ores}");
                    else
                        Console.Write("[1]Spielstand 1 || EMPTY");
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 2) != null)
                        Console.Write($"\n[2]Spielstand 2 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 2).Ores}");
                    else
                        Console.Write("\n[2]Spielstand 2 || EMPTY");
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 3) != null)
                        Console.Write($"\n[3]Spielstand 3 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 3).Ores}");
                    else
                        Console.Write("\n[3]Spielstand 3 || EMPTY");
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 4) != null)
                        Console.Write($"\n[4]Spielstand 4 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 4).Ores}");
                    else
                        Console.Write("\n[4]Spielstand 4 || EMPTY");
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 5) != null)
                        Console.Write($"\n[5]Spielstand 5 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 5).Ores}");
                    else
                        Console.Write("\n[5]Spielstand 5 || EMPTY");
                    Console.Write($"\n[Esc]Programm Beenden");
                    Player spieler = null;
                    short slot = 0;
                    do
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.KeyChar)
                        {
                            case '1':
                                slot = 1;
                                break;
                            case '2':
                                slot = 2;
                                break;
                            case '3':
                                slot = 3;
                                break;
                            case '4':
                                slot = 4;
                                break;
                            case '5':
                                slot = 5;
                                break;
                            default:
                                if (key.Key == ConsoleKey.Escape)
                                    Environment.Exit(0);
                                break;
                        }
                    } while (slot == 0);
                    return slot;
                }
                public static Player CreateLoad(int gameslot)
                {
                    Player player = db.Players.FirstOrDefault(p => p.GameSlot == gameslot);
                    if (player == null)
                    {
                        Console.Clear();
                        Console.Write($"Slot {gameslot}");
                        Console.Write("\n[1]New Game");
                        Console.Write("\n[Esc]Return to selection");
                        while (true)
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            switch (key.KeyChar)
                            {
                                case '1':
                                    player = new Player() { GameSlot = gameslot };
                                    db.Players.Add(player);
                                    foreach (Upgrade upgrade in db.Upgrades)
                                    {
                                        Link link = new Link() { upgrade = upgrade, upgradeId = upgrade.Id, player = player, playerId = player.Id, price = upgrade.Cost.ToString(), increasePercent = upgrade.IncreasePercent, clickval = upgrade.ClickVal };
                                        db.Links.Add(link);
                                        player.Links.Add(link);
                                    }
                                    db.SaveChanges();
                                    return player;
                                    break;
                                default:
                                    if (key.Key == ConsoleKey.Escape)
                                        return null;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write($"Slot {gameslot}");
                        Console.Write("\n[1]Continue");
                        Console.Write("\n[2]Delete Savegame");
                        Console.Write("\n[Esc]Return to selection");
                        while (true)
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            switch (key.KeyChar)
                            {
                                case '1':
                                    return player;
                                case '2':
                                    db.Players.Remove(player);
                                    db.SaveChanges();
                                    return null;
                                default:
                                    if (key.Key == ConsoleKey.Escape)
                                        return null;
                                    break;
                            }
                        }
                    }

                }
            }
            public static void GameLoop(Player player)
            {
                BigInteger playerOres = new BigInteger();
                playerOres = BigInteger.Parse(player.Ores);
                BigInteger playerClickVal = new BigInteger();
                playerClickVal = BigInteger.Parse(player.ClickVal);
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("you currently posess {0} ores", playerOres);
                    Console.WriteLine("[SPACE] to Mine ores with {0} ores per click", playerClickVal);
                    Console.WriteLine("[M] for Market");
                    Console.WriteLine("[ESC] save and quit");
                    check:
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey();
                        switch (key.Key)
                        {
                            case ConsoleKey.Spacebar:
                                playerOres += playerClickVal;
                                break;
                            case ConsoleKey.M:
                                player = Shop(player);
                                break;
                            case ConsoleKey.Escape:
                                player.ClickVal = playerClickVal.ToString();
                                player.Ores = playerOres.ToString();
                                db.SaveChanges();
                                Environment.Exit(0);
                                break;
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                        goto check;
                    }
                }

            }
            public static Player Shop(Player player)
            {
                BigInteger playerOres = new BigInteger();
                playerOres = BigInteger.Parse(player.Ores);
                while (true)
                {
                    Console.Clear();
                    Console.Write("Shop\nYou have {0} ores avalible\nuse [↑] and [↓] to select [Enter] to buy and [Esc] to exit", playerOres);
                    int i = 0;
                    int sel = 1;
                    Link selected = null;
                    if (sel < 1)
                        sel = 1;
                    if (sel > player.Links.Count)
                        sel = player.Links.Count;
                    foreach (Link link in player.Links)
                    {
                        i++;
                        if (i == sel)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            selected = link;
                        }
                        else
                            Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write($"\n{link.upgrade.Name} gives {link.clickval} for {link.price}");
                    }
                    if (selected == null)
                        Environment.Exit(404);
                    BigInteger selectedPrice = BigInteger.Parse(selected.price);
                    ConsoleKeyInfo keyinfo = Console.ReadKey();
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.Enter:
                            if (playerOres >= selectedPrice)
                            {
                                player.ClickVal += selected.clickval;
                                playerOres -= selectedPrice;
                                selectedPrice += selectedPrice / 100 * selected.increasePercent;
                                player.Ores = playerOres.ToString();
                                selected.price = selectedPrice.ToString();
                                db.SaveChanges();
                            }
                            break;
                        case ConsoleKey.Escape:
                            return player;
                        case ConsoleKey.UpArrow:
                            sel -= 1;
                            break;
                        case ConsoleKey.DownArrow:
                            sel += 1;
                            break;
                    }
                }
            }
        }
    }


/*

// UML PLAYER
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






















// UML UPGRADE
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
            long index = clickValueIncrease / 10; 
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
                Console.WriteLine("Max Upgrades !");
                return int.MaxValue;
            }

            return (int)upgradeCosts[index];
        }



        public int GetClickValueIncrease()
        {
        return clickValueIncrease;
        }
    }







// UML ClickerCounter
    internal class ClickerCounter
    {
        private int clicks;

        public ClickerCounter()
        {
            clicks = 0;
        }

        public void incrementClicks()
        {
            clicks++;
        }

        public int getClicks()
        {
            return clicks;
        }
        
    }









// UML DATABASE
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













// GAME CONSOLE VISUAL
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
            Console.WriteLine($"Player:{player.playerName}");
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
            Console.WriteLine("########################################################");
            Console.WriteLine("Upgrade Click-Menu !");
            Console.WriteLine("Options:");
            Console.WriteLine($"1. Upgrade Clicker: Cost = {CalculateUpgradeCost()} Ores");
            Console.WriteLine("2. Back");
            Console.WriteLine("########################################################");
            Console.WriteLine("input:");
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
    // Annahme: Die Kosten steigen exponentiell mit der aktuellen CPS (Clicks pro Sekunde)
    // Die Kosten können angepasst werden, um den gewünschten Exponenten zu erzielen
    int baseCost = 5; // Grundkosten für das Upgrade
    int exponent = 2; // Exponent für den Kostenanstieg
    return (int)(baseCost * Math.Pow(player.GetClickValue(), exponent));
}












        private async Task SaveGame()
        {
            // Logik zum Speichern des Spiels
        }














        private void ExitGame()
        {
            Console.WriteLine("The Game has ended!...(press any button to end)");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
*/
}

