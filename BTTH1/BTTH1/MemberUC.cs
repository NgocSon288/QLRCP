using BTTH1.Common;
using BTTH1.Models;
using BTTH1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class MemberUC : UserControl
    {
        private readonly IMemberService _memberService;
        private readonly ICategoryMemberService _categoryMemberService;
        private readonly IOrderService _orderService;

        private Member member;
        private bool isView;

        private string fileNameRoot = "";
        private string pathNew = "";
        private string pathOld = "";
        private bool isChangeImage = false;
        private string avatarTemp = "";


        public MemberUC()
        {
            InitializeComponent();

            this._memberService = new MemberService();
            this._categoryMemberService = new CategoryMemberService();
            this._orderService = new OrderService();

            member = Constants.CurrentMember;
            pathOld = "../../Assets/Images/" + member.Avatar;

            isView = false;

            ptbAvatar.AllowDrop = true;

            Load();
        }

        #region Events

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var result = DialogResult.No;

            if (!isView)
            {
                if (!IsValidValueUpdate())
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    txtName.Focus();

                    MessageBox.Show("Tên không được bỏ trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                result = MessageBox.Show("Bạn có muốn cập nhật thông tin?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.No)
                {
                    // cập nhật DB
                    if (UpdateMember())
                    {
                        // Cập nhật header
                        Constants.MainForm.LoadHeader();

                        MessageBox.Show("Cập nhật thành công");
                        ChangeVisible();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại");
                    }
                }
            }
            else
            {
                ChangeVisible();
            }
        }

        private void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            ChangeVisible(false);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (isView)
                return;

            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;)|*.jpg; *.jpeg; *.gif; *.bmp;";

            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileNameRoot = open.FileName;
                    var fileName = member.Username + Path.GetFileName(fileNameRoot);
                    pathNew = Path.Combine("../../Assets/Images/", fileName);

                    avatarTemp = fileName;

                    ptbAvatar.BackgroundImage = new Bitmap(open.FileName);

                    isChangeImage = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ảnh chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ptbAvatar_DragEnter(object sender, DragEventArgs e)
        {
            if (isView)
                return;

            isChangeImage = true;

            e.Effect = DragDropEffects.Copy;
        }

        private void ptbAvatar_DragDrop(object sender, DragEventArgs e)
        {
            if (isView)
                return;

            try
            {
                foreach (string pic in ((string[])e.Data.GetData(DataFormats.FileDrop)))
                {
                    var img = Image.FromFile(pic);
                    ptbAvatar.BackgroundImage = img;

                    fileNameRoot = pic;
                    var fileName = member.Username + Path.GetFileName(fileNameRoot);
                    pathNew = Path.Combine("../../Assets/Images/", fileName);

                    avatarTemp = fileName;
                }

                isChangeImage = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Ảnh được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditPassword_Click(object sender, EventArgs e)
        {
            UIHelper.ShowControl(new fUpdatePassword(panelContent), panelContent);
        }

        #endregion


        #region Methods

        private bool IsValidValueUpdate()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Focus();

                MessageBox.Show("Tên không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Length != 10)
            {
                txtPhone.Focus();

                MessageBox.Show("Số điện thoại không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                txtAddress.Focus();

                MessageBox.Show("Địa chỉ không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private bool UpdateMember()
        {
            member.Name = txtName.Text.Trim();
            member.PhoneNunmber = txtPhone.Text.Trim();
            member.Address = txtAddress.Text.Trim();
            member.BirthDay = dtpBirthday.Value;
            member.Avatar = avatarTemp;

            if (isChangeImage)
            {
                //if (File.Exists(pathOld))
                //{
                //    var image = Image.FromFile(pathOld);

                //    image.Dispose(); // this removes all resources

                //    //later...

                //    File.Delete(pathOld);
                //}

                if (!File.Exists(pathNew))
                {
                    File.Copy(fileNameRoot, pathNew);  // save file 
                }

                fileNameRoot = "";
                pathOld = pathNew;
                pathNew = "";
                isChangeImage = false;
            }

            return _memberService.Update(member);
        }

        new private void Load()
        {
            dtpBirthday.MinDate = new DateTime(1990, 1, 1);
            dtpBirthday.MaxDate = DateTime.Now;

            if (File.Exists("../../Assets/Images/" + member.Avatar))
            {
                ptbAvatar.BackgroundImage = new Bitmap("../../Assets/Images/" + member.Avatar);
            }

            ChangeVisible();

            LoadOrders();
        }

        private void LoadOrders()
        {
            lvOrders.Items.Clear();

            var id = 1;
            var orders = _orderService.GetAllByMemberID(member.ID);

            if (orders.Count <= 0)
            {
                pnlEmptyOrders.Visible = true;
                lvOrders.Visible = false;

                return;
            }

            pnlEmptyOrders.Visible = false;
            lvOrders.Visible = true;

            lvOrders.Columns.Add("ID", 50, HorizontalAlignment.Left);
            lvOrders.Columns.Add("Tên phim", 510, HorizontalAlignment.Left);
            lvOrders.Columns.Add("Thể loại phim", 200, HorizontalAlignment.Left);
            lvOrders.Columns.Add("Số lượng", 70, HorizontalAlignment.Center);
            lvOrders.Columns.Add("Giá", 170, HorizontalAlignment.Center);
            lvOrders.Columns.Add("Ngày đặt", 310, HorizontalAlignment.Center);
            lvOrders.Columns.Add("Ngày chiếu", 310, HorizontalAlignment.Center);
            lvOrders.Columns.Add("Tổng cộng", 170, HorizontalAlignment.Center);

            foreach (var item in orders)
            {
                var total = (item.Count * item.Price);
                var itemListView = new ListViewItem();
                itemListView.Text = (id++).ToString();

                itemListView.SubItems.Add(item.FilmName);
                itemListView.SubItems.Add(item.CategoryFilmName);
                itemListView.SubItems.Add(item.Count.ToString());
                itemListView.SubItems.Add(item.Price.ToString("#,##") + " đ");
                itemListView.SubItems.Add(item.CreatedDate.ToString());
                itemListView.SubItems.Add(item.DateShow.ToString());
                itemListView.SubItems.Add(total.ToString("#,##") + " đ");

                lvOrders.Items.Add(itemListView);
            }
        }

        private void LoadMemberView()
        {
            // Detail
            lblName.Text = member.Name;
            lblUsername.Text = member.Username;
            lblPassword.Text = "".PadLeft(member.Password.Length, '*');
            lblCategoryMember.Text = _categoryMemberService.GetByID(member.CategoryMemberID).Name;
            lblEmail.Text = member.Email;
            lblPhone.Text = member.PhoneNunmber;
            lblAddress.Text = member.Address;
            lblBirthday.Text = member.BirthDay.ToShortDateString();

            btnEditPassword.Location = new Point(lblPassword.Location.X + lblPassword.Width, lblPassword.Location.Y - 20);
        }

        private void LoadMemberEdit()
        {
            // Edit
            txtName.Text = member.Name;
            txtUsername.Text = member.Username;
            txtPassword.Text = member.Password;
            txtCategoryMember.Text = _categoryMemberService.GetByID(member.CategoryMemberID).Name;
            txtEmail.Text = member.Email;
            txtPhone.Text = member.PhoneNunmber;
            txtAddress.Text = member.Address;
            dtpBirthday.Text = member.BirthDay.ToShortDateString();
        }

        private void ChangeVisible(bool isChaned = true)
        {
            isView = !isView;
            btnUpdate.Text = btnUpdate.Text == "CẬP NHẬT" ? "CHỈNH SỬA" : "CẬP NHẬT";

            if (isView)
            {
                pnlView.Visible = true;
                pnlEdit.Visible = false;

                if (isChaned)
                    LoadMemberView();
            }
            else
            {
                pnlEdit.Visible = true;
                pnlView.Visible = false;

                if (isChaned)
                    LoadMemberEdit();
            }
        }

        #endregion

        #region UI

        private void txtName_Enter(object sender, EventArgs e)
        {
            txtName.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlName.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnlName.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;
            }
            else
            {
                txtName.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
                pnlName.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;
            }
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            txtUsername.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlUsername.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            txtUsername.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
            pnlUsername.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlPassword.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
            pnlPassword.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;
        }

        private void txtCategoryMember_Enter(object sender, EventArgs e)
        {
            txtCategoryMember.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlCategoryMember.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtCategoryMember_Leave(object sender, EventArgs e)
        {
            txtCategoryMember.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
            pnlCategoryMember.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            txtEmail.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlEmail.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            txtEmail.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
            pnlEmail.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            txtPhone.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlPhone.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Length != 10)
            {
                txtPhone.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnlPhone.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;
            }
            else
            {
                txtPhone.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
                pnlPhone.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;
            }
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                txtPhone.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnlPhone.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;
            }
            else
            {
                txtAddress.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
                pnlAddress.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
            }
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                txtAddress.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnlAddress.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;
            }
            else
            {
                txtAddress.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
                pnlAddress.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;
            }
        }

        #endregion
    }
}
