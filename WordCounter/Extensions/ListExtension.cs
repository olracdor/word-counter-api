using System;
using System.Collections.Generic;

namespace WordCounter.Extensions
{
    public static class ListExtension
    {
        /// <summary>
        /// Checks if the value exists and automatically add and return the new object if it does not exist
        /// </summary>
        /// <param name="defaultValueExp">Expression that return the default value</param>
        /// <param name="findExpression">Expression that checks if the object exists in the list</param>
        public static T GetValueOrAddIfEmpty<T>(this List<T> list, Func<T> defaultValueExp, Func<T, bool> findExpression)
        {
            T obj = list.Find(word => findExpression(word));
            if (obj == null)
            {
                //Create new instance if we should
                obj = defaultValueExp();
                list.Add(obj);
                return obj;
            }
            return obj;
        }
    }
}
