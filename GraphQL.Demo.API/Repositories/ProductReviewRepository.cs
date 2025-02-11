﻿using CarvedRock.Api.Data;
using CarvedRock.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarvedRock.Api.Repositories
{
    public class ProductReviewRepository
    {
        private readonly CarvedRockDbContext _dbContext;

        public ProductReviewRepository(CarvedRockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ILookup<int, ProductReview>> GetForProducts(IEnumerable<int> productIds)
        {
            var reviews = await _dbContext
                .ProductReviews
                .Where(pr => productIds.Contains(pr.ProductId))
                .ToListAsync();
            return reviews.ToLookup(r => r.ProductId);
        }

        public async Task<IEnumerable<ProductReview>> GetForProduct(int productId)
        {
            var reviews = await _dbContext.ProductReviews.Where(p => p.ProductId == productId).ToListAsync();
            return reviews;
        }

        public async Task<ProductReview> AddReview(ProductReview review)
        {
            _dbContext.ProductReviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
    }
}
