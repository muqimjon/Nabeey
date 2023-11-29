using Newtonsoft.Json;
using Nabeey.Domain.Commons;
using Nabeey.Service.Exceptions;
using Nabeey.Domain.Configurations;
using Nabeey.Service.Helpers;

namespace Nabeey.Service.Extensions;

public static class CollectionExtension
{
	public static IQueryable<T> ToPaginate<T>(this IQueryable<T> values, PaginationParams @params)
		=> values.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);

	public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, PaginationParams @params)
		where TEntity : Auditable
	{
		if (@params.PageSize == 0 && @params.PageIndex == 0)
		{
			@params = new PaginationParams()
			{
				PageSize = 10,
				PageIndex = 1
			};
		}
		var metaData = new PaginationMetaData(entities.Count(), @params);

		var json = JsonConvert.SerializeObject(metaData);

		if (HttpContextHelper.ResponseHeaders is not null)
		{
			if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
				HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

			HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
		}

		return entities.ToPaginate(@params);
	}

	public static IEnumerable<TEntity> OrderBy<TEntity>(this IEnumerable<TEntity> collect, Filter filter)
	{
		var prop = filter.OrderBy ?? "Id";

		var property = typeof(TEntity).GetProperties().FirstOrDefault(n
			=> n.Name.Equals(prop, StringComparison.OrdinalIgnoreCase))
			?? throw new CustomException(400, "Property that does not exist"); 

		if (property.Name is "Id" && !filter.IsDesc)
			return collect;

		if (filter.IsDesc)
			return collect.OrderByDescending(x => property.GetValue(x));

		return collect.OrderBy(x => property.GetValue(x));
	}
}