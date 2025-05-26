using LotteryApi.Models;

namespace LotteryApi.Services
{
    public class LotteryService : ILotteryService
    {
        private static readonly Random _random = new Random();

        public LotteryResult GenerateSingleNumber(int min, int max)
        {
            var number = _random.Next(min, max + 1);
            
            return new LotteryResult
            {
                Numbers = new[] { number },
                GeneratedAt = DateTime.Now,
                Range = $"{min}-{max}",
                Count = 1,
                IsUnique = true,
                Type = "Single"
            };
        }

        public LotteryResult GenerateMultipleNumbers(int count, int min, int max, bool unique)
        {
            var numbers = GenerateNumbers(count, min, max, unique);

            return new LotteryResult
            {
                Numbers = numbers,
                GeneratedAt = DateTime.Now,
                Range = $"{min}-{max}",
                Count = count,
                IsUnique = unique,
                Type = "Multiple"
            };
        }

        public LotteryResult GenerateCustomLottery(LotteryRequest request)
        {
            var numbers = GenerateNumbers(request.Count, request.Min, request.Max, request.Unique);

            return new LotteryResult
            {
                Numbers = numbers,
                GeneratedAt = DateTime.Now,
                Range = $"{request.Min}-{request.Max}",
                Count = request.Count,
                IsUnique = request.Unique,
                Type = "Custom"
            };
        }

        private int[] GenerateNumbers(int count, int min, int max, bool unique)
        {
            var numbers = new List<int>();

            if (unique)
            {
                var availableNumbers = Enumerable.Range(min, max - min + 1).ToList();
                for (int i = 0; i < count; i++)
                {
                    var index = _random.Next(availableNumbers.Count);
                    numbers.Add(availableNumbers[index]);
                    availableNumbers.RemoveAt(index);
                }
                numbers.Sort();
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    numbers.Add(_random.Next(min, max + 1));
                }
            }

            return numbers.ToArray();
        }
    }
}