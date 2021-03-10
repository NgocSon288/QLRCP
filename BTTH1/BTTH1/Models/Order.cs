using BTTH1.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BTTH1.Models
{
    public class Order
    {
        public Guid ID { get; set; }

        public Guid MemberID { get; set; }

        public Guid RoomID { get; set; }

        public Guid FilmID { get; set; }

        public string FilmName { get; set; }

        public string CategoryFilmName { get; set; }

        public int TimeLong { get; set; }

        public DateTime DateShow { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }


        public DateTime CreatedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime DeletedDate { get; set; }

        public Guid DeletedBy { get; set; }

        public bool Status { get; set; }

        public Order()
        {
            ID = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            DeletedDate = DateTime.Now;
            DeletedBy = new Guid();
            Status = true;
        }

        public Order(List<object> objValues)
        {
            try
            {
                var m = 0;
                foreach (PropertyInfo p in typeof(Order).GetProperties())
                {
                    string propertyName = p.Name;
                    var propertyType = p.PropertyType.Name;
                    var type = p.PropertyType;

                    var value = Converters<Order>.StringToValue(objValues[m++].ToString(), type);
                    var msgInfo = this.GetType().GetProperty(propertyName);

                    msgInfo.SetValue(this, value, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public Order(Guid roomID, Guid filmID, string filmName, string categoryFilmName, int timeLong, DateTime dateShow, int count, decimal price)
        {
            ID = Guid.NewGuid();
            MemberID = Constants.CurrentMember.ID;
            RoomID = roomID;
            FilmID = filmID;
            FilmName = filmName;
            CategoryFilmName = categoryFilmName;
            TimeLong = timeLong;
            DateShow = dateShow;
            Count = count;
            Price = price;
            CreatedDate = DateTime.Now;
            CreatedBy = Constants.CurrentMember.ID;
            DeletedDate = DateTime.Now;
            DeletedBy = new Guid();
            Status = true;
        }
    }
}
