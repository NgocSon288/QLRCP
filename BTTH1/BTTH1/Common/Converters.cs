﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BTTH1.Common
{
    public static class Converters<T>
    {
        public static string EntityToString(T entity)
        {
            Type t = entity.GetType();
            var props = t.GetProperties().ToList();

            return string.Join(Constants.SEPERATE_CHAR, props.Select(prop => prop.GetValue(entity).ToString()).ToList());
        }

        public static List<Object> StringToObjectList(string s)
        {
            return new List<object>(s.Split(new string[] { Constants.SEPERATE_CHAR }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static Object StringToValue(string value, Type type)
        {
            if (type.Name == "Guid")
            {
                return new Guid(value);
            }
            else if (type.Name == "DateTime")
            {
                return DateTime.Parse(value);
            }
            else if (type.Name == "Int32")
            {
                return Int32.Parse(value);
            }
            else if (type.Name == "Single")
            {
                return float.Parse(value);
            }
            else if (type.Name == "Double")
            {
                return double.Parse(value);
            }
            else if (type.Name == "Boolean")
            {
                return Boolean.Parse(value);
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Lấy ra ID của instance kiểu T
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Guid GetIDByGeneritType(T entity)
        {
            var prop = entity.GetType().GetProperty("ID");
            var value = prop.GetValue(entity);

            return new Guid(value.ToString());
        }
    }
}