using BTTH1.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BTTH1.Models
{
    public class RoomFilm
    {
        public Guid ID { get; set; }

        public Guid RoomID { get; set; }

        public Guid FilmID { get; set; }

        public DateTime DateShow { get; set; }

        public bool Status { get; set; }

        public RoomFilm()
        {
            ID = Guid.NewGuid();
            Status = true;
        }

        public RoomFilm(List<object> objValues)
        {
            try
            {
                var m = 0;
                foreach (PropertyInfo p in typeof(RoomFilm).GetProperties())
                {
                    string propertyName = p.Name;
                    var propertyType = p.PropertyType.Name;
                    var type = p.PropertyType;

                    var value = Converters<RoomFilm>.StringToValue(objValues[m++].ToString(), type);
                    var msgInfo = this.GetType().GetProperty(propertyName);

                    msgInfo.SetValue(this, value, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public RoomFilm(Guid roomID, Guid filmID, DateTime dateShow)
        {
            ID = Guid.NewGuid();
            RoomID = roomID;
            FilmID = filmID;
            DateShow = dateShow;
            Status = true;
        }
    }
}