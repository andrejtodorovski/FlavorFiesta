using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DataContext _context;

        public ShoppingCartRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCartDTO> GetShoppingCartById(int Id)
        {
            var shoppingCart = await _context.ShoppingCarts.Include(user => user.AppUser).Include(cart => cart.CartItems).ThenInclude(item => item.Food).FirstOrDefaultAsync(cart => cart.Id == Id);
            var cartItemsDTO = shoppingCart.CartItems.Select(cartItem =>
            {
                var foodDTO = new FoodDTO
                {
                    Id = cartItem.Food.Id,
                    Name = cartItem.Food.Name,
                    Description = cartItem.Food.Description,
                    Price = cartItem.Food.Price,
                    AverageRating = cartItem.Food.AverageRating,
                    ViewCount = cartItem.Food.ViewCount,
                    DateCreated = cartItem.Food.DateCreated,
                    Calories = cartItem.Food.Calories
                };

                return new CartItemDTO
                {
                    Id = cartItem.Id,
                    Quantity = cartItem.Quantity,
                    Subtotal = cartItem.Subtotal,
                    Food = foodDTO
                };
            }).ToList();

            var shoppingCartDTO = new ShoppingCartDTO
            {
                Id = shoppingCart.Id,
                Status = shoppingCart.Status,
                TotalPrice = shoppingCart.TotalPrice,
                AppUserId = shoppingCart.AppUserId,
                AppUserName = shoppingCart.AppUser.UserName,
                CartItems = cartItemsDTO
            };

            return shoppingCartDTO;
        }

        public async Task<ShoppingCartDTO> GetShoppingCartForUserAsync(int userId)
        {
            var shoppingCart = await _context.ShoppingCarts
            .Include(user => user.AppUser)
            .Include(cart => cart.CartItems)
                .ThenInclude(item => item.Food)
            .FirstOrDefaultAsync(cart => cart.AppUserId == userId);

            if (shoppingCart == null)
            {
                return null;
            }

            var cartItemsDTO = shoppingCart.CartItems.Select(cartItem =>
            {
                var foodDTO = new FoodDTO
                {
                    Id = cartItem.Food.Id,
                    Name = cartItem.Food.Name,
                    Description = cartItem.Food.Description,
                    Price = cartItem.Food.Price,
                    AverageRating = cartItem.Food.AverageRating,
                    ViewCount = cartItem.Food.ViewCount,
                    DateCreated = cartItem.Food.DateCreated,
                    Calories = cartItem.Food.Calories
                };

                return new CartItemDTO
                {
                    Id = cartItem.Id,
                    Quantity = cartItem.Quantity,
                    Subtotal = cartItem.Subtotal,
                    Food = foodDTO
                };
            }).ToList();

            var shoppingCartDTO = new ShoppingCartDTO
            {
                Id = shoppingCart.Id,
                Status = shoppingCart.Status,
                TotalPrice = shoppingCart.TotalPrice,
                AppUserId = shoppingCart.AppUserId,
                AppUserName = shoppingCart.AppUser.UserName,
                CartItems = cartItemsDTO
            };

            return shoppingCartDTO;
        }

        public async void PlaceOrder(int userId, string phoneNumber, string address)
        {
            var shoppingCart = await _context.ShoppingCarts
            .Include(user => user.AppUser)
            .Include(cart => cart.CartItems)
                .ThenInclude(item => item.Food)
            .FirstOrDefaultAsync(cart => cart.AppUserId == userId);

            if (shoppingCart == null)
            {
                return;
            }

            shoppingCart.Status = "Ordered";
            _context.ShoppingCarts.Update(shoppingCart);
            _context.SaveChanges();

            var order = new Order{
                PhoneNumber = phoneNumber,
                Address = address,
                OrderStatus = "Created",
                DateCreated = DateTime.Now,
                AppUserId = userId,
                ShoppingCartId = shoppingCart.Id
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

        }
    }
}