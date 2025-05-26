using Microsoft.AspNetCore.Mvc;
using LotteryApi.Models;
using LotteryApi.Services;
using System.ComponentModel.DataAnnotations;

namespace LotteryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotteryController : ControllerBase
    {
        private readonly ILotteryService _lotteryService;
        private readonly ILogger<LotteryController> _logger;

        public LotteryController(ILotteryService lotteryService, ILogger<LotteryController> logger)
        {
            _lotteryService = lotteryService;
            _logger = logger;
        }

        [HttpGet("single")]
        public ActionResult<LotteryResult> GetSingleNumber(
            [FromQuery][Range(1, int.MaxValue)] int min = 1, 
            [FromQuery][Range(2, int.MaxValue)] int max = 100)
        {
            try
            {
                if (min >= max)
                {
                    return BadRequest("Min must be less than Max");
                }

                _logger.LogInformation($"Generating single number between {min} and {max}");
                var result = _lotteryService.GenerateSingleNumber(min, max);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating single number");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("multiple")]
        public ActionResult<LotteryResult> GetMultipleNumbers(
            [FromQuery][Range(1, 50)] int count = 6,
            [FromQuery][Range(1, int.MaxValue)] int min = 1,
            [FromQuery][Range(2, int.MaxValue)] int max = 37,
            [FromQuery] bool unique = true)
        {
            try
            {
                if (min >= max)
                {
                    return BadRequest("Min must be less than Max");
                }

                if (unique && count > (max - min + 1))
                {
                    return BadRequest("Cannot generate more unique numbers than available range");
                }

                _logger.LogInformation($"Generating {count} numbers between {min} and {max}, unique: {unique}");
                var result = _lotteryService.GenerateMultipleNumbers(count, min, max, unique);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating multiple numbers");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("custom")]
        public ActionResult<LotteryResult> GenerateCustomLottery([FromBody] LotteryRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!request.IsValid())
                {
                    return BadRequest("Invalid lottery request parameters");
                }

                _logger.LogInformation($"Generating custom lottery: {request.Count} numbers between {request.Min} and {request.Max}");
                var result = _lotteryService.GenerateCustomLottery(request);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating custom lottery");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("health")]
        public ActionResult<object> HealthCheck()
        {
            return Ok(new { 
                Status = "Healthy", 
                Timestamp = DateTime.Now,
                Version = "1.0.0"
            });
        }
    }
}