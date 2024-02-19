namespace MasterMinerV1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MasterMiner masterMiner = new MasterMiner();
            masterMiner.PrintSamePositionCounter();
        }
    }
    internal class MasterMiner {

        public void PrintSamePositionCounter()
        {
            //constant main console Visual
            Console.WriteLine("Total Ores:      Cps=" +
                "\n" +
                "\n########################################################" +
                "\nUpgrade Click:Press C Cost=(ClickerUpgradeCost_value)" +
                "\nUpgrade machines:Press M Cost=(MachineUpgradeCost_value)" +
                "\nBuy Boosters: Press B to open (BoosterShopMenue)" +
                "\nOpen menue: (Save file, Exit, Settings, back)");

            //cursor invisible
            Console.SetCursorPosition(0, 1);
            Console.CursorVisible = false;
            CounterAutomater(); // starts clocked code alongside following code
            Clicker();//following code
        }
        int count = 0;
        int cps = 1;
        internal async void Clicker()
        {
            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    count += cps;
                }
            }
        }
        internal async void CounterAutomater()
        {
            while (true)
            {
                // Use carriage return ('\r') to return the cursor to the beginning of the line
                // and overwrite the previous number with the new one
                Console.Write("\r" + count);


                count++;
                // Sleep for 1 second to control the speed of the counter
                Thread.Sleep(1000); // 1000 milliseconds = 1 second


            }
        }

    }
}