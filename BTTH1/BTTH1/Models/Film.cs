using BTTH1.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BTTH1.Models
{
    public class Film
    {
        public Guid ID { get; set; }

        public Guid CategoryFilmID { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public string National { get; set; }

        public string Language { get; set; }

        public int OrderCount { get; set; }

        public int TimeLong { get; set; }

        public int Rating { get; set; }

        public string Actor { get; set; }

        public int CreatedYear { get; set; }

        public Film()
        {
            ID = Guid.NewGuid();
        }

        public Film(List<object> objValues)
        {
            try
            {
                var m = 0;
                foreach (PropertyInfo p in typeof(Film).GetProperties())
                {
                    string propertyName = p.Name;
                    var propertyType = p.PropertyType.Name;
                    var type = p.PropertyType;

                    var value = Converters<Film>.StringToValue(objValues[m++].ToString(), type);
                    var msgInfo = this.GetType().GetProperty(propertyName);

                    msgInfo.SetValue(this, value, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public Film(Guid categoryFilmID, string name, string image, decimal price, string description, string director, string national, string language, int timeLong, string actor, int createdYear)
        {
            ID = Guid.NewGuid();
            CategoryFilmID = categoryFilmID;
            Name = name;
            Image = image;
            Price = price;
            Description = description;
            Director = director;
            National = national;
            Language = language;
            OrderCount = 0;
            TimeLong = timeLong;
            Rating = 0;
            Actor = actor;
            CreatedYear = createdYear;
        }
    }
}