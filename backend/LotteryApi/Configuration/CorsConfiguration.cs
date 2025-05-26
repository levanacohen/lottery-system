namespace LotteryApi.Configuration
{
    public static class CorsConfiguration
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsSettings = configuration.GetSection("CorsSettings");
            var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>() 
                ?? new[] { 
                    "http://localhost:8080", 
                    "http://localhost:3000",
                    "http://127.0.0.1:5500",
                    "http://localhost:5500"
                };

            services.AddCors(options =>
            {
                options.AddPolicy("LotteryPolicy", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials()
                          .SetIsOriginAllowed(_ => true); // Allow for development
                });
            });
        }
    }
}