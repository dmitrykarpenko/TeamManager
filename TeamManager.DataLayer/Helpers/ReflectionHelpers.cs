﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.DataLayer.Helpers
{
    public static class ReflectionHelpers
    {
        public static IEnumerable<IEnumerable<T>> GetCollections<T>(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var type = obj.GetType();
            var res = new List<IEnumerable<T>>();
            foreach (var prop in type.GetProperties())
            {
                // is IEnumerable<T>?
                if (typeof(IEnumerable<T>).IsAssignableFrom(prop.PropertyType))
                {
                    var get = prop.GetGetMethod();
                    if (!get.IsStatic && get.GetParameters().Length == 0) // skip indexed & static
                    {
                        var collection = (IEnumerable<T>)get.Invoke(obj, null);
                        if (collection != null)
                            res.Add(collection);
                    }
                }
            }
            return res;
        }

        public static Dictionary<string, int> EnumToDictionary<TEnum>() where TEnum : struct, IConvertible
        {
            var resultDict = new Dictionary<string, int>();
            var enums = Enum.GetValues(typeof(TEnum));

            foreach (var e in enums)
            {
                resultDict.Add(e.ToString(), Convert.ToInt32(e));
            }

            return resultDict;
        }
    }
}
