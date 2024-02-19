# Master Miner V1

This is a School examination project.
The Idea comes from the game "Cookie Clicker" + "RPG-TextGame" (bot from discord).
A collaboration will happen in GEOMETRY DASH as level and visual of this game among me and @IchorDragon, taking the concept from the GEOMETRY DASH Level: "Coin Temple".

You may dont want to copy my code since its not 100% working but for reviews and Concept stealing its allowed XD.

Task 1. UML-Milestone (create an UML-Diagram)
|     |     |     |     |
V     V     V     V     V

############################################

**Main program UML:**

############################################



```mermaid
classDiagram
    class Player {
        - String playerName
        - int totalOres
        - int clickValue
        - int autoCPS
        - int totalClicks
        - int totalAutoClicks
        - List<Upgrade> upgrades
        - List<Booster> boosters
        - List<Prestige> prestiges
        + Player(playerName: String)
        + clickMine(): void
        + addAutoCPS(cps: int): void
        + getTotalOres(): int
        + getTotalClicks(): int
        + getTotalAutoClicks(): int
        + getClickValue(): int
        + getAutoCPS(): int
        + purchaseUpgrade(upgrade: Upgrade): void
        + purchaseBooster(booster: Booster): void
        + resetPlayer(): void
    }
    class Upgrade {
        - String upgradeName
        - int upgradeCost
        - int clickValueIncrease
        - int autoCPSIncrease
        + Upgrade(upgradeName: String, upgradeCost: int, clickValueIncrease: int, autoCPSIncrease: int)
        + getUpgradeName(): String
        + getUpgradeCost(): int
        + getClickValueIncrease(): int
        + getAutoCPSIncrease(): int
    }
    class Booster {
        - String boosterName
        - int boosterCost
        - int boosterMultiplier
        + Booster(boosterName: String, boosterCost: int, boosterMultiplier: int)
        + getBoosterName(): String
        + getBoosterCost(): int
        + getBoosterMultiplier(): int
    }
    class Prestige {
        - String prestigeName
        - int prestigeCost
        - int prestigeMultiplier
        + Prestige(prestigeName: String, prestigeCost: int, prestigeMultiplier: int)
        + getPrestigeName(): String
        + getPrestigeCost(): int
        + getPrestigeMultiplier(): int
     } 
      class Database { 
      + Database() 
      + savePlayerData(player: Player): void 
      + loadPlayerData(playerName: String, password: String): Player
    }

    Player "1" --> "*" Upgrade : owns
    Player "1" --> "*" Booster : owns
    Player "1" --> "*" Prestige : owns
    Database "1" --> "1" Player : contains
```


############################################

**Main Data structure UML:**

############################################

```mermaid
classDiagram
    class ClickerCounter {
        - int clicks
        + ClickerCounter()
        + incrementClicks(): void
        + getClicks(): int
    }
    class AutoClickerMachine {
        - int cps
        + AutoClickerMachine(cps: int)
        + getCPS(): int
    }
    class MachineUpgrade {
        - String upgradeName
        - int upgradeCost
        - int cpsIncrease
        + MachineUpgrade(upgradeName: String, upgradeCost: int, cpsIncrease: int)
        + getUpgradeName(): String
        + getUpgradeCost(): int
        + getCPSIncrease(): int
    }
    class Database {
        - int playerId
        - String playerName
        - int totalOres
        - int clickValue
        - int autoCPS
        - int totalClicks
        - int totalAutoClicks
        + Database()
        + savePlayerData(player: Player): void
        + loadPlayerData(playerId: int): Player
    }
```
