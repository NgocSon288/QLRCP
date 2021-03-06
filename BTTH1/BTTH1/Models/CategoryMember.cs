using BTTH1.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BTTH1.Models
{
    public class CategoryMember
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public CategoryMember()
        {

        }

        public CategoryMember(List<object> objValues)
        {
            try
            {
                var m = 0;
                foreach (PropertyInfo p in typeof(CategoryMember).GetProperties())
                {
                    string propertyName = p.Name;
                    var propertyType = p.PropertyType.Name;
                    var type = p.PropertyType;

                    var value = Converters<CategoryMember>.StringToValue(objValues[m++].ToString(), type);
                    var msgInfo = this.GetType().GetProperty(propertyName);

                    msgInfo.SetValue(this, value, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public CategoryMember(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
        }
    }
}
