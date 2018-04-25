using GCS.Models.Dtos;
using System.Linq;

namespace GCS.Models.Extensions {
    public static class Extensions {
        public static IQueryable<T> FilterByDate<T>(this IQueryable<T> query, int year, int month) where T : SearchEntry {
            return query.Where(
                 q => year > -1 && month > -1 ? q.Date.Year == year && q.Date.Month == month
                 : year > -1 ? q.Date.Year == year
                 : month > -1 ? q.Date.Month == month
                 : true
                );
        }
    }
}