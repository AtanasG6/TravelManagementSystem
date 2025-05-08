using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystem.Application.Parameters.Base;
using TravelManagementSystem.Application.Parameters.Destinations;
using TravelManagementSystem.Application.Parameters.Trips;
using TravelManagementSystem.Application.Parameters.Users;
using TravelManagementSystem.Domain.Common;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeNavigationProperties<T>(this IQueryable<T> query) where T : BaseEntity
        {
            if (typeof(T).IsAssignableTo(typeof(AuditableEntity)))
            {
                query = query
                    .Include(nameof(AuditableEntity.CreatedBy))
                    .Include(nameof(AuditableEntity.UpdatedBy));
            }

            if (typeof(T) == typeof(Trip))
            {
                query = query.Include(nameof(Trip.Destination));
            }

            return query;
        }

        //public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, QueryParameters parameters)
        //{
        //    return query
        //        .Skip((parameters.Page - 1) * parameters.PageSize)
        //        .Take(parameters.PageSize);
        //}

        //public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, QueryParameters parameters)
        //{
        //    if (string.IsNullOrWhiteSpace(parameters.OrderBy))
        //        return query;

        //    string orderBy = parameters.OrderBy.ToLower();
        //    bool isDescending = parameters.IsDescending;

        //    if (typeof(T) == typeof(Trip))
        //    {
        //        var tripQuery = query.Cast<Trip>();

        //        if (orderBy == "title")
        //            tripQuery = isDescending ? tripQuery.OrderByDescending(t => t.Title) : tripQuery.OrderBy(t => t.Title);
        //        else if (orderBy == "price")
        //            tripQuery = isDescending ? tripQuery.OrderByDescending(t => t.Price) : tripQuery.OrderBy(t => t.Price);

        //        return (IQueryable<T>)tripQuery;
        //    }
        //    else if (typeof(T) == typeof(User))
        //    {
        //        var userQuery = query.Cast<User>();

        //        if (orderBy == "username")
        //            userQuery = isDescending ? userQuery.OrderByDescending(u => u.Username) : userQuery.OrderBy(u => u.Username);
        //        else if (orderBy == "email")
        //            userQuery = isDescending ? userQuery.OrderByDescending(u => u.Email) : userQuery.OrderBy(u => u.Email);

        //        return (IQueryable<T>)userQuery;
        //    }
        //    else if (typeof(T) == typeof(Destination))
        //    {
        //        var destinationQuery = query.Cast<Destination>();

        //        if (orderBy == "country")
        //            destinationQuery = isDescending ? destinationQuery.OrderByDescending(d => d.Country) : destinationQuery.OrderBy(d => d.Country);
        //        else if (orderBy == "city")
        //            destinationQuery = isDescending ? destinationQuery.OrderByDescending(d => d.City) : destinationQuery.OrderBy(d => d.City);

        //        return (IQueryable<T>)destinationQuery;
        //    }

        //    return query;
        //} 

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, QueryParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.OrderBy))
                return query;

            var direction = parameters.IsDescending ? "descending" : "ascending";
            var orderString = $"{parameters.OrderBy} {direction}";

            try
            {
                return query.OrderBy(orderString);
            }
            catch (ParseException)
            {
                return query;
            }
        }

        //public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, QueryParameters parameters)
        //{
        //    if (typeof(T) == typeof(User) && parameters is UserQueryParameters userParams)
        //    {
        //        var userQuery = query.Cast<User>();

        //        if (!string.IsNullOrWhiteSpace(userParams.Username))
        //            userQuery = userQuery.Where(u => u.Username.Contains(userParams.Username));

        //        if (!string.IsNullOrWhiteSpace(userParams.Email))
        //            userQuery = userQuery.Where(u => u.Email.Contains(userParams.Email));

        //        return (IQueryable<T>)userQuery;
        //    }

        //    if (typeof(T) == typeof(Trip) && parameters is TripQueryParameters tripParams)
        //    {
        //        var tripQuery = query.Cast<Trip>();

        //        if (!string.IsNullOrWhiteSpace(tripParams.Title))
        //            tripQuery = tripQuery.Where(t => t.Title.Contains(tripParams.Title));

        //        if (tripParams.PriceMin.HasValue)
        //            tripQuery = tripQuery.Where(t => t.Price >= tripParams.PriceMin.Value);

        //        if (tripParams.PriceMax.HasValue)
        //            tripQuery = tripQuery.Where(t => t.Price <= tripParams.PriceMax.Value);

        //        return (IQueryable<T>)tripQuery;
        //    }

        //    if (typeof(T) == typeof(Destination) && parameters is DestinationQueryParameters destParams)
        //    {
        //        var destQuery = query.Cast<Destination>();

        //        if (!string.IsNullOrWhiteSpace(destParams.Country))
        //            destQuery = destQuery.Where(d => d.Country.Contains(destParams.Country));

        //        if (!string.IsNullOrWhiteSpace(destParams.City))
        //            destQuery = destQuery.Where(d => d.City.Contains(destParams.City));

        //        return (IQueryable<T>)destQuery;
        //    }

        //    return query;
        //}

        public static IQueryable<T> ApplyDynamicFiltering<T>(this IQueryable<T> query, Dictionary<string, object?> filters)
        {
            foreach (var filter in filters)
            {
                if (filter.Value is null) continue;

                var key = filter.Key.Trim();
                var value = filter.Value;

                // >= оператор
                if (key.EndsWith(">="))
                {
                    var prop = key.Replace(">=", "").Trim();
                    query = query.Where($"{prop} >= @0", value);
                }
                // <= оператор
                else if (key.EndsWith("<="))
                {
                    var prop = key.Replace("<=", "").Trim();
                    query = query.Where($"{prop} <= @0", value);
                }
                // string.Contains
                else if (typeof(T).GetProperty(key)?.PropertyType == typeof(string))
                {
                    query = query.Where($"{key}.Contains(@0)", value);
                }
                // equals за други типове
                else
                {
                    query = query.Where($"{key} == @0", value);
                }
            }

            return query;
        }

        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, QueryParameters parameters)
        {
            if (parameters is UserQueryParameters userParams)
                return query.ApplyDynamicFiltering(userParams.ToFilterDictionary());

            if (parameters is TripQueryParameters tripParams)
                return query.ApplyDynamicFiltering(tripParams.ToFilterDictionary());

            if (parameters is DestinationQueryParameters destParams)
                return query.ApplyDynamicFiltering(destParams.ToFilterDictionary());

            return query;
        }

    }
}

