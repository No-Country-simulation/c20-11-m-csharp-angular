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
        Task<ReviewDto> AddReview(CreateReviewDTO review,string auth0Id);
        Task<List<ReviewDto>> GetAllReview();
        Task<List<ReviewDto>> GetReviewFromRecetaId(int RecetaID);
        public ReviewDto IsDeleteReview(int id);
        public ReviewDto PutReview(Review review);
    }
}

       

