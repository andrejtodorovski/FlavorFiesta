using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DataContext _context;

        public ShoppingCartRepository(DataContext context)
        {
            _context = context;
        }

        public ShoppingCart Create(AppUser user)
        {
            return _context.ShoppingCarts.Add(new ShoppingCart
            {
                AppUser = user,
                AppUserId = user.Id,
                Status = "Active",
            }).Entity;
        }

        public async Task<ShoppingCartDTO> GetShoppingCartById(int Id)
        {
            var shoppingCart = await _context.ShoppingCarts.Include(user => user.AppUser).Include(cart => cart.CartItems).ThenInclude(item => item.Food).FirstOrDefaultAsync(cart => cart.Id == Id);

            var cartItemsDTO = shoppingCart.CartItems.Select(cartItem =>
            {
                var food = _context.Foods.Find(cartItem.Food.Id);
                var foodIngredients = _context.FoodIngredients.Include(fi => fi.Ingredient).Where(fi => fi.FoodId == cartItem.Food.Id).ToList();
                var ingredients = foodIngredients.Select(fi => fi.Ingredient).ToList();
                var categories = _context.Categories.Where(c => c.Foods.Any(f => f.Id == cartItem.Food.Id)).ToList();
                var reviews = _context.Reviews.Where(r => r.FoodId == cartItem.Food.Id).ToList();
                var foodDTO = new FoodDTO
                {
                    Id = food.Id,
                    Name = food.Name,
                    Description = food.Description,
                    Price = food.Price,
                    AverageRating = food.AverageRating,
                    ViewCount = food.ViewCount,
                    DateCreated = food.DateCreated,
                    Calories = food.Calories,
                    PhotoUrl = food.PhotoUrl,
                    Ingredients = ingredients,
                    Categories = categories,
                    Reviews = reviews
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

        public async Task<ShoppingCart> GetShoppingCartByUserId(int userId)
        {
            return await _context.ShoppingCarts
            .Include(user => user.AppUser)
            .Include(cart => cart.CartItems)
            .ThenInclude(item => item.Food)
            .Where(cart => cart.Status == "Active")
            .FirstOrDefaultAsync(cart => cart.AppUserId == userId);
        }

        public async Task<ShoppingCartDTO> GetShoppingCartForUserAsync(int userId)
        {
            var shoppingCart = await _context.ShoppingCarts
            .Include(user => user.AppUser)
            .Include(cart => cart.CartItems)
            .ThenInclude(item => item.Food)
            .Where(cart => cart.Status == "Active")
            .FirstOrDefaultAsync(cart => cart.AppUserId == userId);

            if (shoppingCart == null)
            {
                return null;
            }

            var cartItemsDTO = shoppingCart.CartItems.Select(cartItem =>
            {
                var food = _context.Foods.Find(cartItem.Food.Id);
                var foodIngredients = _context.FoodIngredients.Include(fi => fi.Ingredient).Where(fi => fi.FoodId == cartItem.Food.Id).ToList();
                var ingredients = foodIngredients.Select(fi => fi.Ingredient).ToList();
                var categories = _context.Categories.Where(c => c.Foods.Any(f => f.Id == cartItem.Food.Id)).ToList();
                var reviews = _context.Reviews.Where(r => r.FoodId == cartItem.Food.Id).ToList();
                var foodDTO = new FoodDTO
                {
                    Id = food.Id,
                    Name = food.Name,
                    Description = food.Description,
                    Price = food.Price,
                    AverageRating = food.AverageRating,
                    ViewCount = food.ViewCount,
                    DateCreated = food.DateCreated,
                    Calories = food.Calories,
                    PhotoUrl = food.PhotoUrl,
                    Ingredients = ingredients,
                    Categories = categories,
                    Reviews = reviews
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

        public async Task PlaceOrder(int userId, PlaceOrderDTO placeOrderDTO)
        {
            var shoppingCart = _context.ShoppingCarts
            .Include(user => user.AppUser)
            .Include(cart => cart.CartItems)
                .ThenInclude(item => item.Food)
            .Where(cart => cart.Status == "Active")
            .FirstOrDefault(cart => cart.AppUserId == userId);

            if (shoppingCart == null)
            {
                return;
            }

            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            var tokenService = new TokenService();

            
            var customer = await customerService.CreateAsync(new CustomerCreateOptions
            {
                Email = shoppingCart.AppUser.EmailAddress,
                Source = placeOrderDTO.StripeToken
            });
            var charge = await chargeService.CreateAsync(new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(shoppingCart.TotalPrice) * 100,
                Description = "Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });        
        }

        public async Task ShoppingCartUpdates(int userId, PlaceOrderDTO placeOrderDTO){
            var shoppingCart = _context.ShoppingCarts
            .Include(user => user.AppUser)
            .Include(cart => cart.CartItems)
                .ThenInclude(item => item.Food)
            .Where(cart => cart.Status == "Active")
            .FirstOrDefault(cart => cart.AppUserId == userId);

            shoppingCart.Status = "Ordered";
            _context.ShoppingCarts.Update(shoppingCart);
            Create(shoppingCart.AppUser);
            await _context.SaveChangesAsync();

            var order = new Order
            {
                PhoneNumber = placeOrderDTO.PhoneNumber,
                Address = placeOrderDTO.Address,
                OrderStatus = "Created",
                DateCreated = DateTime.Now,
                AppUserId = shoppingCart.AppUserId,
                ShoppingCartId = shoppingCart.Id
            };

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();
        }
    }
}