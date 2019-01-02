using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mappers
{
    public static class Converter
    {
        public static IList ConvertList(Type type, IList source)
        {
            var listType = typeof(List<>);
            Type[] typeArgs = { type };
            var genericListType = listType.MakeGenericType(typeArgs);
            var typedList = (IList)Activator.CreateInstance(genericListType);
            foreach (var item in source)
            {
                typedList.Add(type.Cast(item));
            }
            return typedList;
        }

        public static object Cast(this Type Type, object data)
        {
            ParameterExpression DataParameter = Expression.Parameter(typeof(object), "data");
            BlockExpression Body = Expression.Block(Expression.Convert(Expression.Convert(DataParameter, data.GetType()), Type));

            Delegate Run = Expression.Lambda(Body, DataParameter).Compile();
            object ret = Run.DynamicInvoke(data);
            return ret;
        }
    }
}
