﻿using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetEntity(IQueryable<TEntity> inputquery,
            ISpecification<TEntity> spec)
        {
            var query = inputquery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); //p => p.ProductTypeId == Id;
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;

        }
    }
}