using BTTH1.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BTTH1.Models
{
    public class CategoryFilm
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public CategoryFilm()
        {
            ID = Guid.NewGuid();
        }

        public CategoryFilm(List<object> objValues)
        {
            try
            {
                var m = 0;
                foreach (PropertyInfo p in typeof(CategoryFilm).GetProperties())
                {
                    string propertyName = p.Name;
                    var propertyType = p.PropertyType.Name;
                    var type = p.PropertyType;

                    var value = Converters<CategoryFilm>.StringToValue(objValues[m++].ToString(), type);
                    var msgInfo = this.GetType().GetProperty(propertyName);

                    msgInfo.SetValue(this, value, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public CategoryFilm(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
        }
    }
}