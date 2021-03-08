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
        public static readonly Color BORDER_MENU_LEFT_COLOR = Color.FromArgb(249, 88, 155);
        public static readonly Color ACTIVE_COLOR = Color.FromArgb(104, 188, 169);
        public static readonly Color LEAVE_COLOR = Color.Black;

        /// <summary>
        /// Permission
        /// </summary>
        public static readonly int[] PERMISSION_ADMIN       = new int[] { 1, 1, 1, 1, 1 };
        public static readonly int[] PERMISSION_STAFF       = new int[] { 1, 1, 1, 1, 0 };
        public static readonly int[] PERMISSION_CUSTOMER    = new int[] { 1, 1, 1, 0, 0 };
        public static readonly int[] PERMISSION_NULL        = new int[] { 1, 1, 0, 0, 0 };

        public static Member CurrentMember = null;
    }
}
