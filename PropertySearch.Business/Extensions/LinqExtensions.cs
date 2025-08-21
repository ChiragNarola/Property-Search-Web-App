using PropertySearch.Business.Models.DTOs.PageSort;
using PropertySearch.Business.Models.RMs.PageSort;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace ITB.Business.Extensions
{
    public static class LinqExtensions
    {
        #region Queryable
        public static IQueryable<TEntity> ToPagedQueryable<TEntity>(this IQueryable<TEntity> source, int pageSize, int pageNo)
        {
            return source
              .Skip(pageSize * (pageNo - 1))
              .Take(pageSize);
        }
        public static IOrderedQueryable<T> ToOrderedQueryable<T>(this IQueryable<T> source, IEnumerable<SortRM>? sorts)
        {
            if (sorts == null || !sorts.Where(x => !string.IsNullOrWhiteSpace(x.Property)).Any())
                return source.OrderBy(_ => _);

            IOrderedQueryable<T>? res = null;
            foreach (var sort in sorts.Where(x => !string.IsNullOrWhiteSpace(x.Property)))
                res = ApplyOrder(res ?? source, sort.Property, sort.Type.ToString());

            return res ?? throw new Exception("Unexpected error occurred on ToOrderedQueryable<T>");
        }
        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            if (property.Contains('.') || property.Contains('['))
                throw new NotImplementedException("IOrderedMongoQueryable-ApplyOrder method is not implemented for nested property or array.");

            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                if (string.IsNullOrEmpty(prop)) continue;
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo? pi = type.GetProperty(prop);
                if (pi != null)
                {
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object? result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, [source, lambda]);

            return result == null ? throw new Exception("Unexpected error occurred on IOrderedMongoQueryable-ApplyOrder") : (IOrderedQueryable<T>)result;
        }
        public static List<AggregationResultDTO>? ApplyAggregations<TSource>(this IQueryable<TSource> source, IEnumerable<AggregationRM>? aggregations)
        {
            if (source == null)
                throw new Exception("source cannont be null for ApplyAggregations<TSource> function");

            if (aggregations == null || !aggregations.Where(x => !string.IsNullOrWhiteSpace(x.Property)).Any())
                return null;

            var res = new List<AggregationResultDTO>();
            foreach (var a in aggregations)
            {
                if (string.IsNullOrEmpty(a.Property))
                    throw new Exception("Property cannont be null for the ApplyAggregations<TSource> function.");

                // Properties
                string[] props = a.Property.Split('.');
                Type type = typeof(TSource);
                ParameterExpression arg = Expression.Parameter(type, "x");
                Expression expr = arg;
                foreach (string prop in props)
                {
                    if (string.IsNullOrEmpty(prop)) continue;
                    // use reflection (not ComponentModel) to mirror LINQ
                    PropertyInfo? pi = type.GetProperty(prop);
                    if (pi != null)
                    {
                        expr = Expression.Property(expr, pi);
                        type = pi.PropertyType;
                    }
                }
                Type delegateType = typeof(Func<,>).MakeGenericType(typeof(TSource), type);
                LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

                switch (a.Type)
                {
                    case AggregateType.Sum:
                        {
                            var sum = (typeof(Queryable).GetMethods().First(
                                m => m.Name == nameof(Queryable.Sum) &&
                                m.ReturnType == type &&
                                m.IsGenericMethodDefinition &&
                                m.GetGenericArguments().Length == 1 &&
                                m.GetParameters().Length == 2))
                                .MakeGenericMethod(typeof(TSource))
                                .Invoke(null, [source, lambda]);

                            res.Add(new(a.Property, a.Type, sum));
                            break;
                        }

                    case AggregateType.Average:
                        {
                            var average = (typeof(Queryable).GetMethods().First(
                                m => m.Name == nameof(Queryable.Average) &&
                                m.IsGenericMethodDefinition &&
                                m.GetGenericArguments().Length == 1 &&
                                m.GetParameters().Length == 2 &&
                                m.GetParameters()[1].ParameterType.GetGenericArguments()[0].GetGenericArguments()[1] == type))
                                .MakeGenericMethod(typeof(TSource))
                                .Invoke(null, [source, lambda]);
                            res.Add(new(a.Property, a.Type, average));
                            break;
                        }

                    case AggregateType.Min:
                        {
                            var min = (typeof(Queryable).GetMethods().First(
                                m => m.Name == nameof(Queryable.Min) &&
                                m.IsGenericMethodDefinition &&
                                m.GetGenericArguments().Length == 2 &&
                                m.GetParameters().Length == 2))
                                .MakeGenericMethod(typeof(TSource), type)
                                .Invoke(null, [source, lambda]);
                            res.Add(new(a.Property, a.Type, min));
                            break;
                        }

                    case AggregateType.Max:
                        {
                            var max = (typeof(Queryable).GetMethods().First(
                                m => m.Name == nameof(Queryable.Max) &&
                                m.IsGenericMethodDefinition &&
                                m.GetGenericArguments().Length == 2 &&
                                m.GetParameters().Length == 2))
                                .MakeGenericMethod(typeof(TSource), type)
                                .Invoke(null, [source, lambda]);
                            res.Add(new(a.Property, a.Type, max));
                            break;
                        }

                    default:
                        throw new NotImplementedException($"AggregateType {a.Type} not implemented.");
                }
            }

            return res;
        }
        public static async Task<PagedResultDTO<TEntity>> ToPagedResultAsync<TEntity>(this IQueryable<TEntity> source, IPageSortAggregation psa)
        {
            IQueryable<TEntity> qry;
            if (psa.Sorts != null && psa.Sorts.Any())
            {
                qry = source
                    .ToOrderedQueryable(psa.Sorts)
                    .ToPagedQueryable(psa.PageSize, psa.PageNo);
            }
            else
            {
                qry = source
                    .ToPagedQueryable(psa.PageSize, psa.PageNo);
            }
#if DEBUG
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(qry.ToString());
            Console.ResetColor();
            Console.WriteLine();
#endif

            return new PagedResultDTO<TEntity>()
            {
                PageNo = psa.PageNo,
                PageSize = psa.PageSize,
                Items = await qry.ToListAsync(),
                TotalCount = await source.LongCountAsync(),
                AggregationResults = source.ApplyAggregations(psa.Aggregations)
            };
        }
        public static async Task<PagedResultDTO<TDTO>> ToPagedResultAsync<TEntity, TDTO>(this IQueryable<TEntity> source, IPageSortAggregation psa, Func<List<TEntity>, List<TDTO>> map)
        {
            IQueryable<TEntity> qry;
            if (psa.Sorts != null && psa.Sorts.Any())
            {
                qry = source
                    .ToOrderedQueryable(psa.Sorts)
                    .ToPagedQueryable(psa.PageSize, psa.PageNo);
            }
            else
            {
                qry = source
                    .ToPagedQueryable(psa.PageSize, psa.PageNo);
            }
#if DEBUG
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(qry.ToString());
            Console.ResetColor();
            Console.WriteLine();
#endif
            return new PagedResultDTO<TDTO>()
            {
                PageNo = psa.PageNo,
                PageSize = psa.PageSize,
                Items = map(await qry.ToListAsync()),
                TotalCount = await source.LongCountAsync(),
                AggregationResults = source.ApplyAggregations(psa.Aggregations)
            };
        }
        #endregion

        #region Enumerable
        public static IOrderedEnumerable<T> ToOrderedEnumerable<T>(this IEnumerable<T> source, IEnumerable<SortRM>? sorts)
        {
            IOrderedEnumerable<T> res = source.OrderBy(x => 0);
            if (sorts == null || !sorts.Any()) return res;
            foreach (var sort in sorts.Where(x => !string.IsNullOrWhiteSpace(x.Property)))
                res = ApplyOrder(res, sort.Property, sort.Type);

            return res;
        }

        private static IOrderedEnumerable<T> ApplyOrder<T>(IEnumerable<T> source, string property, SortType type)
        {
            if (!source.Any()) return source.OrderBy(x => 0);

            var propertyInfo = source.First()?.GetType().GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                return source.OrderBy(x => 0);

            return type switch
            {
                SortType.OrderBy => source.OrderBy(e => propertyInfo.GetValue(e, null)),
                SortType.OrderByDescending => source.OrderByDescending(e => propertyInfo.GetValue(e, null)),
                SortType.ThenBy => ((IOrderedEnumerable<T>)source).ThenBy(e => propertyInfo.GetValue(e, null)),
                SortType.ThenByDescending => ((IOrderedEnumerable<T>)source).ThenByDescending(e => propertyInfo.GetValue(e, null)),
                _ => throw new NotImplementedException("IEnumerable - SortType.Undefined is not implemented."),
            };
        }

        public static IEnumerable<TEntity> ToPagedEnumerable<TEntity>(this IEnumerable<TEntity> source, int pageSize, int pageNo)
        {
            return source
              .Skip(pageSize * (pageNo - 1))
              .Take(pageSize);
        }

        public static PagedResultDTO<T> ToPagedResult<T>(this IEnumerable<T> source, IPageSortAggregation psa) where T : class
        {
            var totalCount = source.LongCount();
            var items = source.ToOrderedEnumerable(psa.Sorts).ToPagedEnumerable(psa.PageSize, psa.PageNo).ToList();

            return new PagedResultDTO<T>()
            {
                PageNo = psa.PageNo,
                PageSize = psa.PageSize,
                Items = items,
                TotalCount = totalCount,
            };
        }
        #endregion
    }
}
