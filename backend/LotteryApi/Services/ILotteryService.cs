using LotteryApi.Models;

namespace LotteryApi.Services
{
    public interface ILotteryService
    {
        LotteryResult GenerateSingleNumber(int min, int max);
        LotteryResult GenerateMultipleNumbers(int count, int min, int max, bool unique);
        LotteryResult GenerateCustomLottery(LotteryRequest request);
    }
}