using System.ComponentModel.DataAnnotations;

namespace LotteryApi.Models
{
    public class LotteryResult
    {
        public int[] Numbers { get; set; } = Array.Empty<int>();
        public DateTime GeneratedAt { get; set; }
        public string Range { get; set; } = string.Empty;
        public int Count { get; set; }
        public bool IsUnique { get; set; }
        public string Type { get; set; } = string.Empty;
    }

    public class LotteryRequest
    {
        [Range(1, 50, ErrorMessage = "Count must be between 1 and 50")]
        public int Count { get; set; } = 6;

        [Range(1, int.MaxValue, ErrorMessage = "Min must be greater than 0")]
        public int Min { get; set; } = 1;

        [Range(2, int.MaxValue, ErrorMessage = "Max must be greater than 1")]
        public int Max { get; set; } = 37;

        public bool Unique { get; set; } = true;

        // Custom validation
        public bool IsValid()
        {
            if (Min >= Max) return false;
            if (Unique && Count > (Max - Min + 1)) return false;
            return true;
        }
    }
}