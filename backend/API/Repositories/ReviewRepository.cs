using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces.Repositories;

namespace API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetReviewsForUser(int userId)
        {
            return _context.Reviews.Where(r => r.AppUserId == userId).ToList();
        }
        
    }
}