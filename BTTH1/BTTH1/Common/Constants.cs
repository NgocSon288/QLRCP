using BTTH1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTTH1.Common
{
    public static class Constants
    {
        public static readonly string SEPERATE_CHAR = "CS511";

        public static readonly string BASE_FILE_PATH = "../../DB/";
        public static readonly string MEMBER_FILE_PATH = BASE_FILE_PATH + "Member.txt";

        public static readonly int OTP_LENGTH = 8;


        /// <summary>
        /// Component params
        /// </summary>
        public static readonly int WIDTH_PAGINATION = 70;
        public static readonly int HEIGHT_PAGINATION = 70;
        public static readonly int WIDTH_CATEGORY_BUTTON = 200;
        public static readonly int HEIGHT_CATEGORY_BUTTON = 70;
        public static readonly int FONTSIZE_CATEGORY_BUTTON = 10;
        public static readonly int HEIGHT_SEAT = 120;
        public static readonly int WIDTH_SEAT = 120;


        /// <summary>
        /// Color
        /// </summary>
        public static readonly Color BORDER_MENU_LEFT_COLOR = Color.FromArgb(249, 88, 155); // form
        public static readonly Color ACTIVE_COLOR = Color.FromArgb(104, 188, 169);      // login
        public static readonly Color LEAVE_COLOR = Color.Black;                         // login
        public static readonly Color ACTIVE_LABEL_COLOR = Color.FromArgb(247, 129, 68); // home
        public static readonly Color LEAVE_LABEL_COLOR = Color.FromArgb(68, 226, 255);  // home
        public static readonly Color ACTIVE_PAGINATION_BG_COLOR = Color.FromArgb(58, 216, 245);  // Film
        public static readonly Color LEAVE_PAGINATION_BG_COLOR = Color.FromArgb(40, 40, 40);  // Film
        public static readonly Color ACTIVE_CATEGORY_BUTTON_BG_COLOR = Color.FromArgb(58, 216, 245);  // Film
        public static readonly Color LEAVE_CATEGORY_BUTTON_BG_COLOR = Color.FromArgb(40, 40, 40);  // Film
        public static readonly Color ACTIVE_TEXTBOX_MEMBER_DETAIL = Color.FromArgb(68, 226, 255);  // Film 
        public static readonly Color LEAVE_TEXTBOX_MEMBER_DETAIL = Color.FromArgb(102, 139, 172);  // Film
        public static readonly Color ACTIVE_PANEL_MEMBER_DETAIL = Color.FromArgb(106, 252, 255);  // Film 
        public static readonly Color LEAVE_PANEL_MEMBER_DETAIL = Color.FromArgb(0, 0, 0);  // Film
        public static readonly Color BUTTON_SEAT_BG_SELECTED = Color.FromArgb(145, 20, 205);  // Seat
        public static readonly Color BUTTON_SEAT_BG_BOUGHT = Color.FromArgb(223, 249, 251);  // Seat
        public static readonly Color BUTTON_SEAT_BG_EMPTY = Color.FromArgb(149, 175, 192);  // Seat
        public static readonly Color BUTTON_SEAT_FORE_SELECTED = Color.FromArgb(240, 147, 43);  // Seat
        public static readonly Color BUTTON_SEAT_FORE_BOUGHT = Color.FromArgb(223, 249, 251);  // Seat
        public static readonly Color BUTTON_SEAT_FORE_EMPTY = Color.FromArgb(40, 40, 40);  // Seat

        public static readonly Color LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL = Color.FromArgb(255, 0, 0);  // Film
        public static readonly Color LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL = Color.FromArgb(255, 0, 0);  // Film





        /// <summary>
        /// Permission
        /// </summary>
        public static readonly int[] PERMISSION_ADMIN = new int[] { 1, 1, 1, 1, 1 };
        public static readonly int[] PERMISSION_STAFF = new int[] { 1, 1, 1, 1, 0 };
        public static readonly int[] PERMISSION_CUSTOMER = new int[] { 1, 1, 1, 0, 0 };
        public static readonly int[] PERMISSION_NULL = new int[] { 1, 1, 0, 0, 0 };

        /// <summary>
        /// Email
        /// </summary>
        public static readonly string FROM_EMAIL_ADDRESS = "18521694gm.uit.edu.vn@gmail.com";
        public static readonly string FROM_EMAIL_DISPLAYNAME = "Huỳnh Ngọc Sơn";
        public static readonly string FROM_EMAIL_PASSWORD = "SOn01698182219";
        public static readonly string SMTP_HOST = "smtp.gmail.com";
        public static readonly int SMTP_PORT = 587;
        public static readonly bool ENABLED_SSL = true;

        public static Member CurrentMember = null;
        public static fMain MainForm = null;

        public static Control Root = null;
    }

    public enum PREVIOUS_FROM
    {
        DETAIL,
        HOME,
        FILM
    }
}
