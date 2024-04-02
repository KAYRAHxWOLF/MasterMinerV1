using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;
using MasterMinerV1.Database;
using Microsoft.EntityFrameworkCore;

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
        }
        internal class Scripts
        {
            public static void Config()
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.CursorVisible = false;
            }
            internal class Spielstand
            {
                public static int Auswahl()
                {
                    Console.Clear();
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 1) != null)
                        Console.Write($"[1] Spielstand 1 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 1).Ores}");
                    else
                        Console.Write("[1] Spielstand 1 || EMPTY");
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 2) != null)
                        Console.Write($"\n[2] Spielstand 2 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 2).Ores}");
                    else
                        Console.Write("\n[2] Spielstand 2 || EMPTY");
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 3) != null)
                        Console.Write($"\n[3] Spielstand 3 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 3).Ores}");
                    else
                        Console.Write("\n[3] Spielstand 3 || EMPTY");
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 4) != null)
                        Console.Write($"\n[4] Spielstand 4 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 4).Ores}");
                    else
                        Console.Write("\n[4] Spielstand 4 || EMPTY");
                    if (db.Players.FirstOrDefault(p => p.GameSlot == 5) != null)
                        Console.Write($"\n[5] Spielstand 5 || Ores: {db.Players.FirstOrDefault(p => p.GameSlot == 5).Ores}");
                    else
                        Console.Write("\n[5] Spielstand 5 || EMPTY");
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
                    Player player = db.Players
                    .Include(p => p.Links)
                    .ThenInclude(l => l.upgrade)
                    .FirstOrDefault(p => p.GameSlot == gameslot);
                    if (player == null)
                    {
                        Console.Clear();
                        Console.Write($"Slot {gameslot}");
                        Console.Write("\n[1] New Game");
                        Console.Write("\n[Esc] Return to selection");
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
                        Console.Write("\n[1] Continue");
                        Console.Write("\n[2] Delete Savegame");
                        Console.Write("\n[Esc] Return to selection");
                        while (true)
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            switch (key.KeyChar)
                            {
                                case '1':
                                    return player;
                                case '2':
                                    foreach (Link link in player.Links)
                                    {
                                        player.Links.Remove(link);
                                    }
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
                                player.ClickVal = playerClickVal.ToString();
                                player.Ores = playerOres.ToString();
                                player = Shop(player);
                                playerOres = BigInteger.Parse(player.Ores);
                                playerClickVal = BigInteger.Parse(player.ClickVal);
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
                BigInteger playerClickVal = new BigInteger();
                playerClickVal = BigInteger.Parse(player.ClickVal);
                int sel = 0;
                int i = 0;
                while (true)
                {
                    Console.Clear();
                    Console.Write("Shop\nYou have {0} ores avalible\nuse [↑] and [↓] to select [Enter] to buy and [Esc] to exit", playerOres);
                    Link selected = null;
                    i = 0;
                    if (sel < 1)
                        sel = 1;
                    if (sel > player.Links.Count)
                        sel = player.Links.Count;
                    foreach (Link link in player.Links)
                    {
                        i++;
                        if (i == sel)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            selected = link;
                        }
                        else
                            Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write($"\n{link.upgrade.Name} gives {link.clickval} for {link.price}");
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (selected == null)
                        Environment.Exit(404);
                    BigInteger selectedPrice = BigInteger.Parse(selected.price);
                    ConsoleKeyInfo keyinfo = Console.ReadKey();
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.Enter:
                            if (playerOres >= selectedPrice)
                            {
                                playerClickVal += selected.clickval;
                                playerOres -= selectedPrice;
                                selectedPrice += selectedPrice / 100 * selected.increasePercent;
                                selected.owned++;
                                player.Ores = playerOres.ToString();
                                player.ClickVal = playerClickVal.ToString();
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
}

