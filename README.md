# Master Miner V1

This is a School examination project.
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
    }
    class Upgrade {
        - String upgradeName
        - int upgradeCost
        - int clickValueIncrease
    }

    Player "1" --> "0..*" Upgrade
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







□□□□□□□□□□□□□□□□□□

```mermaid
classDiagram
    class Link {
        Id: int
        player: Player
        playerId: int
        upgrade: Upgrade
        upgradeId: int
        price: string
        owned: int
        increasePercent: int
        clickval: int
    }

    class Player {
        Id: int
        GameSlot: int
        Ores: string
        ClickVal: string
        TotalClicks: long
        Links: ICollection<Link>
    }

    class Upgrade {
        Id: int
        Name: string
        Cost: int
        ClickVal: int
        IncreasePercent: int
    }

    Link "0..*" <-- "1" Player
    Link "0..*" --> "1" Upgrade

```
