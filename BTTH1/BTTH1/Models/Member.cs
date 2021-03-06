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

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNunmber { get; set; }

        public DateTime BirthDay { get; set; }

        public int Salary { get; set; }

        public bool Status { get; set; }

        public Member()
        {
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
        public Member(Guid categoryMemberID, string name, string email, string address, string phoneNunmber, DateTime birthDay, int salary, bool status)
        {
            CategoryMemberID = categoryMemberID;
            Name = name;
            Email = email;
            Address = address;
            PhoneNunmber = phoneNunmber;
            BirthDay = birthDay;
            Salary = salary;
            Status = status;
        }
    }
}