using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReview(Review review)
        {
            var r = _context.Reviews.Add(review).Entity;
            await _context.SaveChangesAsync();
            return r;
        }

        public IEnumerable<Review> GetReviewsForFood(int foodId)
        {
            return _context.Reviews.Include(f=>f.Food).Include(u=>u.AppUser).Where(r => r.FoodId == foodId).ToList();
        }

        public IEnumerable<Review> GetReviewsForUser(int userId)
        {
            return _context.Reviews.Include(f=>f.Food).Where(r => r.AppUserId == userId).ToList();
        }
        
    }
}