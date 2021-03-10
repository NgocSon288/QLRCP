using BTTH1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTH1.Common
{
    public static class Constants
    {
        public static readonly string SEPERATE_CHAR = "CS511";

        public static readonly string BASE_FILE_PATH = "../../DB/";
        public static readonly string MEMBER_FILE_PATH = BASE_FILE_PATH + "Member.txt";

        /// <summary>
        /// Color
        /// </summary>
        public static readonly Color BORDER_MENU_LEFT_COLOR = Color.FromArgb(249, 88, 155); // form
        public static readonly Color ACTIVE_COLOR = Color.FromArgb(104, 188, 169);      // login
        public static readonly Color LEAVE_COLOR = Color.Black;                         // login
        public static readonly Color ACTIVE_LABEL_COLOR = Color.FromArgb(247, 129, 68); // home
        public static readonly Color LEAVE_LABEL_COLOR = Color.FromArgb(68, 226, 255);  // home


        /// <summary>
        /// Permission
        /// </summary>
        public static readonly int[] PERMISSION_ADMIN       = new int[] { 1, 1, 1, 1, 1 };
        public static readonly int[] PERMISSION_STAFF       = new int[] { 1, 1, 1, 1, 0 };
        public static readonly int[] PERMISSION_CUSTOMER    = new int[] { 1, 1, 1, 0, 0 };
        public static readonly int[] PERMISSION_NULL        = new int[] { 1, 1, 0, 0, 0 };

        /// <summary>
        /// Email
        /// </summary>
        public static readonly string FROM_EMAIL_ADDRESS = "sondeptrai2288@gmail.com";
        public static readonly string FROM_EMAIL_DISPLAYNAME = "Huỳnh Ngọc Sơn";
        public static readonly string FROM_EMAIL_PASSWORD = "SOn01698182219";
        public static readonly string SMTP_HOST = "smtp.gmail.com";
        public static readonly int SMTP_PORT = 587;
        public static readonly bool ENABLED_SSL = true;



        public static Member CurrentMember = null;
    }
}
