using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tastys.BLL.Interfaces;
using Tastys.Domain;
namespace Tastys.BLL.Services.Review
{
    public class ReviewCRUD: IReviewService
    {
        private readonly ITastysContext _Context;
        private readonly IMapper _Mapper;


        public ReviewCRUD(ITastysContext tastysContext, IMapper mapper)
        {
            _Context = tastysContext;
            _Mapper = mapper;
        }

       

        public async Task<List<ReviewDto>> GetAllReview()
        {
            try
            {
                return await _Context.Reviews.Where(review => review.IsDeleted != true)
               
                .Include(review => review.Usuario)
                .Select(review => _Mapper.Map<ReviewDto>(review))
                .ToListAsync();


            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
                
                
                 
                

        }

        public ReviewDto IsDeleteReview(int id)
        {
            try
            {
                Tastys.Domain.Review reviewExist = _Context.Reviews.FirstOrDefault(u => u.ReviewID == id);

                if (reviewExist != null)
                {
                    reviewExist.IsDeleted = true;
                    _Context.SaveChanges();

                    return _Mapper.Map<ReviewDto>(reviewExist);

                }
                else
                {
                    throw new HttpRequestException("La review no existe en la db", null, HttpStatusCode.BadRequest);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public ReviewDto PutReview(Domain.Review review)
        {
            try
            {
                Domain.Review reviewExist = _Context.Reviews.FirstOrDefault(u => u.ReviewID == review.ReviewID);

                if (reviewExist != null)
                {
                    reviewExist = review;
                    _Context.SaveChanges();

                    return _Mapper.Map<ReviewDto>(reviewExist);
                }
                else
                {
                    throw new HttpRequestException("El usuario no existe en la db", null, HttpStatusCode.BadRequest);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }





        public ReviewDto GetReviewById(int Id)
        {
            try
            {

                Tastys.Domain.Review ReviewExist = _Context.Reviews.FirstOrDefault(u => u.ReviewID == Id);

                if (ReviewExist != null)
                {
                    return _Mapper.Map<ReviewDto>(ReviewExist);
                }
                else
                {
                    throw new HttpRequestException("La Review no existe en la db", null, HttpStatusCode.BadRequest);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
