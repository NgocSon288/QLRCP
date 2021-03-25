using BTTH1.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BTTH1.Models
{
    public class Member
    {
        public Guid ID { get; set; }

        public Guid CategoryMemberID { get; set; }// 3

        public string Username { get; set; }// 2

        public string Password { get; set; }

        public string Name { get; set; }// 1

        public string Email { get; set; }// 4

        public string Address { get; set; }// 6

        public string PhoneNunmber { get; set; }// 5

        public DateTime BirthDay { get; set; }// 7

        public int Salary { get; set; }// 1 0

        public bool Status { get; set; }

        public string Avatar { get; set; }

        public Member()
        {
            ID = Guid.NewGuid();
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

        public Member(Guid categoryMemberID, string username, string password, string name, string email, string address, string phoneNunmber, DateTime birthDay, string avatar, int salary = 0, bool status = true)
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
            Salary = salary;
            Status = status;
            Avatar = avatar;
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