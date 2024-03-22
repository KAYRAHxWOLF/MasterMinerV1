# Master Miner V1

This is a School examination project.
The Idea comes from the game "Cookie Clicker" + "RPG-TextGame" (bot from discord).
A collaboration will happen in GEOMETRY DASH as level and visual of this game among me and @IchorDragon, taking the concept from the GEOMETRY DASH Level: "Coin Temple".

You may dont want to copy my code since its not 100% working but for reviews and Concept stealing its allowed XD.
<br>
<br>
<br>
<br>
<br>





Task 1. UML-Milestone (create an UML-Diagram)

V V V V V V V V V V V V V V V V V V V V V

############################################

**Main program UML:**

############################################



```mermaid
classDiagram
    class Player {
        + String playerName
        + int totalOres
        - int clickValue
        - int totalClicks
        - List<Upgrade> upgrades
        + Player(playerName: String)
        + clickMine(): void
        + getClickValue(): int
        + purchaseUpgrade(upgrade: Upgrade): void
    }
    class Upgrade {
        - String upgradeName
        - int upgradeCost
        - int clickValueIncrease
        + Upgrade(upgradeName: String, upgradeCost: int, clickValueIncrease: int,)
        + getUpgradeName(): String
        + getUpgradeCost(): int
        + getClickValueIncrease(): int
    }
      class Database { 
      + Database() 
      + savePlayerData(player: Player): void 
      + loadPlayerData(playerName: String, password: String): Player
    }

    Player "1" --> "*" Upgrade : owns
    Database "1" --> "0..*" Player : contains
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
    class Database {
        - String playerName
        - int totalOres
        - int clickValue
        + Database()
        + savePlayerData(player: Player): void
        + loadPlayerData(playerId: int): Player
    }
```
