using BTTH1.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BTTH1.Models
{
    public class Member
    {
        public Guid ID { get; set; }

        public Guid CategoryMemberID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNunmber { get; set; }

        public DateTime BirthDay { get; set; }

        public int Salary { get; set; }

        public bool Status { get; set; }

        public Member()
        {
            ID = Guid.NewGuid();
        }

        public Member(Guid categoryMemberID = default, string username = null, string password = null, string name = null, string email = null, string address = null, string phoneNunmber = null, DateTime birthDay = default)
        {
            ID = Guid.NewGuid();
            CategoryMemberID = categoryMemberID;
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            Address = address;
            PhoneNunmber = phoneNunmber;
            BirthDay = birthDay;
            Salary = 0;
            Status = true;
        }

        /// <summary>
        /// Dùng đề đọc file, convert từ string trong file txt ra List<T>
        /// </summary>
        /// <param name="objValues"></param>
        public Member(List<object> objValues)
        {
            try
            {
                var m = 0;
                foreach (PropertyInfo p in typeof(Member).GetProperties())
                {
                    string propertyName = p.Name;
                    var propertyType = p.PropertyType.Name;
                    var type = p.PropertyType;

                    var value = Converters<Member>.StringToValue(objValues[m++].ToString(), type);
                    var msgInfo = this.GetType().GetProperty(propertyName);

                    msgInfo.SetValue(this, value, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Dùng để tạo instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="phoneNunmber"></param>
        /// <param name="birthDay"></param>
        /// <param name="salary"></param>
    }
}