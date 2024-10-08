﻿using AutoMapper;
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
    public class ReviewCRUD : IReviewService
    {
        private readonly ITastysContext _Context;
        private readonly IMapper _Mapper;


        public ReviewCRUD(ITastysContext tastysContext, IMapper mapper)
        {
            _Context = tastysContext;
            _Mapper = mapper;
        }

        public async Task<ReviewDto> AddReview(CreateReviewDTO review, string auth0Id)
        {
            Console.WriteLine("RecetaID: " + review.RecetaID);
            try
            {
                // Asegúrate de que el auth0Id existe y corresponde a un usuario
                Usuario usuario = _Context.Usuarios.FirstOrDefault(u => u.Auth0Id == auth0Id);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                // Busca si ya existe una review para esa receta y usuario
                Tastys.Domain.Review reviewExist = _Context.Reviews
                    .FirstOrDefault(r => r.RecetaID == review.RecetaID && r.UsuarioID == usuario.UsuarioID);

                if (reviewExist == null)
                {
                    // Crea una nueva review si no existe
                    Domain.Review newReview = new Domain.Review
                    {
                        Calificacion = review.Calificacion,
                        Comentario = review.Comentario,
                        RecetaID = review.RecetaID,
                        UsuarioID = usuario.UsuarioID, // Cambiado de reviewExist.UsuarioID
                    };

                    _Context.Reviews.Add(newReview);
                    _Context.SaveChanges();

                    // Verifica la review recién creada
                    var reviewCheck = await _Context.Reviews
                        .Include(r => r.Usuario)
                        .Include(r => r.Receta)
                        .FirstOrDefaultAsync(r => r.ReviewID == newReview.ReviewID);

                    // Devuelve el DTO de la review
                    return new ReviewDto
                    {
                        ReviewID = reviewCheck.ReviewID,
                        IsDeleted = reviewCheck.IsDeleted,
                        Comentario = reviewCheck.Comentario,
                        Calificacion = reviewCheck.Calificacion,
                        create_at = reviewCheck.create_at,
                        Usuario = new UsuarioPublicDto
                        {
                            UsuarioID = reviewCheck.Usuario.UsuarioID,
                            Nombre = reviewCheck.Usuario.Nombre
                        },
                        Receta = reviewCheck.Receta
                    };
                }
                else
                {
                    throw new Exception("El usuario ya hizo una review de la receta");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception($"Error al agregar la review: {ex.Message}", ex);
            }
        }


        public async Task<List<ReviewDto>> GetAllReview()
        {
            try
            {
                return await _Context.Reviews.Where(review => review.IsDeleted != true)

                .Include(review => review.Usuario)
                .Include(review => review.Receta)
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
                Tastys.Domain.Review reviewExist = _Context.Reviews.Include(review => review.Usuario)
                .Include(review => review.Receta)
                .First(u => u.ReviewID == id);



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

        public async Task<List<ReviewDto>> GetReviewFromRecetaId(int RecetaID)
        {
            try
            {
                List<Tastys.Domain.Review> reviews = await _Context.Reviews.Include(review => review.Usuario)
                .Include(review => review.Receta)
                .Where(u => u.RecetaID == RecetaID).ToListAsync();
                if (reviews != null)
                {
                    List<ReviewDto> recetaDtos = reviews.Select(r => _Mapper.Map<ReviewDto>(r)).ToList();
                    return recetaDtos;
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
