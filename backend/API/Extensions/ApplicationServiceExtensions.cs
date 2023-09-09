using API.Data;
using API.Interfaces;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlite(config.GetConnectionString("DefaultConnection")));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors();

            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrderService, OrderService>();
            // services.AddScoped<ICartItemService, CartItemService>();
            // services.AddScoped<ICategoryService, CategoryService>();
            // services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
        
    }
}