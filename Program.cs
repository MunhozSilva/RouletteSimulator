using System;

public class RouletteGame
{
    private int initialAmount;
    private int desiredAmount;
    private int baseBet;
    private Random random;

    public RouletteGame(int initialAmount, int desiredAmount)
    {
        this.initialAmount = initialAmount;
        this.desiredAmount = desiredAmount;
        baseBet = 1;
        random = new Random();
    }

    public double Simulate(int numberOfSimulations)
    {
        int successfulSimulations = 0;

        for (int simulation = 1; simulation <= numberOfSimulations; simulation++)
        {
            int amount = initialAmount;
            int currentBet = baseBet;

            while (IsAmountValid(amount))
            {
                double rouletteResult = random.NextDouble();
                
                if (rouletteResult > 0.48)
                {
                    amount += currentBet; 
                    currentBet = baseBet;
                }
                else
                {
                    amount -= currentBet; 
                    currentBet *= 2;
                }

                if (IsTargetAmountReached(amount))
                {
                    successfulSimulations++;
                    break;
                }
            }
        }

        double successRate = (double)successfulSimulations / numberOfSimulations * 100;
        return successRate;
    }

    private bool IsAmountValid(int amount)
    {
        return amount > 0;
    }

    private bool IsTargetAmountReached(int amount)
    {
        return amount >= desiredAmount;
    }
}

public class Program
{
    public static void Main()
    {
        Console.Write("Enter your initial amount: $");
        int initialAmount = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter your desired amount: $");
        int desiredAmount = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the number of simulations to run: ");
        int numberOfSimulations = Convert.ToInt32(Console.ReadLine());

        RouletteGame game = new RouletteGame(initialAmount, desiredAmount);
        double successRate = game.Simulate(numberOfSimulations);

        Console.WriteLine($"Success rate after {numberOfSimulations} simulations: {successRate}%");
    }
}
