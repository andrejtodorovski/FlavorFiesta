using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviewsForUser(int userId);

    }
}