using API.DTOs;
using API.Interfaces.Repositories;
using API.Interfaces.Services;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IOrderService _orderService;
        private readonly IFoodRepository _foodRepository;
        public UserService(IUserRepository repository, IReviewRepository reviewRepository, IOrderService orderService, IFoodRepository foodRepository)
        {
            _repository = repository;
            _reviewRepository = reviewRepository;
            _orderService = orderService;
            _foodRepository = foodRepository;
        }

        public Task<IEnumerable<ReviewInfo>> GetReviewsForUser(string username)
        {
            var user = _repository.GetUserByUsername(username);
            var reviews = _reviewRepository.GetReviewsForUser(user.Id);
            var reviewInfos = new List<ReviewInfo>();
            foreach (var review in reviews)
            {
                var reviewInfo = new ReviewInfo
                {
                    Id = review.Id,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    FoodName = review.Food.Name,
                    FoodPhotoUrl = review.Food.PhotoUrl,
                    FoodAverageRating = review.Food.AverageRating,
                    AppUserName = user.UserName,
                    AppUserPhotoUrl = user.PhotoUrl,
                };
                reviewInfos.Add(reviewInfo);
            }
            return Task.FromResult(reviewInfos.AsEnumerable());
        }

        public async Task<UserInfoDTO> GetUserInfo(string username)
        {
            var user = _repository.GetUserByUsername(username);
            var reviews = _reviewRepository.GetReviewsForUser(user.Id);
            var orders = await _orderService.GetAllOrdersForUser(username);

            var dictionary = orders.SelectMany(order => order.ShoppingCart.CartItems)
            .GroupBy(cartItem => cartItem.Food.Id)
            .ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));

            var mostOrderedFoods = dictionary.Select(item => new FoodStatsDTO
            {
                Food = _foodRepository.ConvertFoodToDTO(item.Key),
                NumberOfOrders = item.Value
            }).OrderByDescending(item => item.NumberOfOrders).Take(5).ToList();


            return await Task.FromResult(new UserInfoDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Reviews = reviews,
                NumberOfReviews = reviews.Count(),
                Orders = orders,
                NumberOfOrders = orders.Count(),
                MostOrderedFoods = mostOrderedFoods
            });
        }

        public Task<IEnumerable<UserInfoDTO>> GetUsersInfo()
        {
            throw new NotImplementedException();
        }
    }
}