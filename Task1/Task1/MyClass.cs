using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Task1;

public class MyClass
{
	public static Func<T, U> CreateGetter<T, U>(string path)
	{
		var properties = path.Split('.');
		var parameter = Expression.Parameter(typeof(T));
		Expression body = parameter;
		
		foreach (var property in properties)
		{
			var match = Regex.Match(property, @"(\w+)(?:\[(\d+)\])?");
			var propertyName = match.Groups[1].Value;
			var index = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : -1;
			
			body = Expression.PropertyOrField(body, propertyName);
			body = Expression.ArrayIndex(body, Expression.Constant(index));

		}

		var lambda = Expression.Lambda<Func<T, U>>(body, parameter);
		return lambda.Compile();
	}
}