namespace Exam_3_Sept
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    class GreedyTimes
    {
        static void Main(string[] args)
        {
            long bagCapacity = long.Parse(Console.ReadLine());

            var input = Console
                .ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            Gem gem = new Gem();
            Gold gold = new Gold();
            Cash cash = new Cash();
            Bag bag = new Bag();

            long currentBagValue = 0;
            long currentQuantity = 0;


            for (int i = 0; i < input.Length; i += 2)
            {
                var type = input[i];

                var currentTotalGold = bag.GoldItems.Sum(q => q.Quantity);
                var currentTotalGem = bag.GemItems.Sum(q => q.Quantity);
                var currentTotalCash = bag.CashAmount.Sum(q => q.Quantity);


                bool isLong = long.TryParse(input[i + 1], out currentQuantity);

                if (isLong)
                {
                    //bool isGoldMoreOrEqualToGemAmount = bag.GoldItems.Where(a => a.Quantity >= bag.GemItems);

                    if (bagCapacity >= currentBagValue)
                    {
                        if (input[i].ToLower().Equals("gold"))
                        {
                            // it's gold

                            gold = new Gold
                            {
                                TypeName = input[i],
                                Quantity = currentQuantity
                            };

                            bag.GoldItems.Add(gold);

                            currentBagValue += gold.Quantity;
                            bagCapacity -= gold.Quantity;
                        }

                        if (input[i].ToLower().EndsWith("gem") && input[i].Length >= 4)
                        {
                            // it's gem

                            if (currentTotalGem + currentQuantity <= currentTotalGold)
                            {
                                gem = new Gem
                                {
                                    TypeName = input[i],
                                    Quantity = currentQuantity
                                };

                                bag.GemItems.Add(gem);

                                currentBagValue += gem.Quantity;
                                bagCapacity -= gem.Quantity;
                            }
                        }

                        if (input[i].ToLower().All(Char.IsLetter) && input[i].Length == 3)
                        {
                            // it's cash

                            if (currentTotalCash + currentQuantity <= currentTotalGem)
                            {
                                cash = new Cash
                                {
                                    Currency = input[i],
                                    Quantity = currentQuantity
                                };

                                bag.CashAmount.Add(cash);

                                currentBagValue += cash.Quantity;
                                bagCapacity -= cash.Quantity; 
                            }
                        }
                    }
                }
            }
   

            bool isGoldInBag = bag.GoldItems.Any();
            bool isGemInBag = bag.GemItems.Any();
            bool isCashInBag = bag.CashAmount.Any();


            if (isGoldInBag)
            {
                // print gold items in bag

                Console.WriteLine($"<Gold> ${bag.GoldItems.Sum(g => g.Quantity)}");
                Console.WriteLine($"##Gold - {bag.GoldItems.Sum(g => g.Quantity)}");
            }


            if (isGemInBag)
            {
                // print total sum of gem and then gem types

                Console.WriteLine($"<Gem> ${bag.GemItems.Sum(g => g.Quantity)}");

                foreach (var item in bag.GemItems.OrderByDescending(t => t.TypeName).ThenBy(q => q.Quantity))
                {
                    Console.WriteLine($"##{item.TypeName} - {item.Quantity}");
                }
            }

            if (isCashInBag)
            {
                // print total amount of cash and then currencies

                Console.WriteLine($"<Cash> ${bag.CashAmount.Sum(c => c.Quantity)}");

                foreach (var item in bag.CashAmount.OrderByDescending(t => t.Currency).ThenBy(q => q.Quantity))
                {
                    Console.WriteLine($"##{item.Currency} - {item.Quantity}");
                }
            }
        }
    }
}

public class Gem
{
    public string TypeName { get; set; }
    public long Quantity { get; set; }
}

public class Gold
{
    public string TypeName { get; set; }
    public long Quantity { get; set; }
}

public class Cash
{
    public string Currency { get; set; }
    public long Quantity { get; set; }
}

public class Bag
{
    public Bag()
    {
        this.GemItems = new List<Gem>();
        this.GoldItems = new List<Gold>();
        this.CashAmount = new List<Cash>();
    }

    public List<Gem> GemItems { get; set; }
    public List<Gold> GoldItems { get; set; }
    public List<Cash> CashAmount { get; set; }
}