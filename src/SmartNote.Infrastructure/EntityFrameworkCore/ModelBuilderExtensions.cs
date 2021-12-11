using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace SmartNote.Infrastructure.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// From: https://davecallan.com/entity-framework-core-query-filters-multiple-entities/
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="expression"></param>
        /// <typeparam name="TInterface"></typeparam>
        public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder,
            Expression<Func<TInterface, bool>> expression)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetInterface(typeof(TInterface).Name) != null)
                {
                    var newParams = Expression.Parameter(entityType.ClrType);
                    var newBody = ReplacingExpressionVisitor.Replace(
                        expression.Parameters.Single(), newParams, expression.Body);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(newBody, newParams));
                }
            }
        }
    }
}