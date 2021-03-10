using BTTH1.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BTTH1.Models
{
    public class Room
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public int SeatMax { get; set; }

        public int SeatCount { get; set; }

        public Room()
        {
            ID = Guid.NewGuid();
            SeatCount = 0;
        }

        public Room(List<object> objValues)
        {
            try
            {
                var m = 0;
                foreach (PropertyInfo p in typeof(Room).GetProperties())
                {
                    string propertyName = p.Name;
                    var propertyType = p.PropertyType.Name;
                    var type = p.PropertyType;

                    var value = Converters<Room>.StringToValue(objValues[m++].ToString(), type);
                    var msgInfo = this.GetType().GetProperty(propertyName);

                    msgInfo.SetValue(this, value, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public Room(string name, int seatMax)
        {
            ID = Guid.NewGuid();
            Name = name;
            SeatMax = seatMax;
            SeatCount = 0;
        }
    }
}
