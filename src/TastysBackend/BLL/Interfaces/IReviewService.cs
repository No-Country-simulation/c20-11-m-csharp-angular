using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tastys.Domain;

namespace Tastys.BLL.Interfaces
{
    internal interface IReviewService
    {
        Task<List<ReviewDto>> GetAllReview();
        public ReviewDto GetReviewById(int id);
        public ReviewDto IsDeleteReview(int id);
        public ReviewDto PutReview(Review review);
    }
}

       

