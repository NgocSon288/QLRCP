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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class fModifiedFilm : UserControl
    {
        private readonly ICategoryFilmService _categoryFilmService;
        private readonly IFilmService _filmService;


        private Film film;
        private Control previous;
        private string pathNewImage;
        private bool isChanged = false;
        private bool isImageChanged = false;
        private string fileNameRoot = "";

        private fFilmManagement parent;

        public fModifiedFilm(fFilmManagement parent, Control previous, Film film = null)
        {
            InitializeComponent();

            this._categoryFilmService = new CategoryFilmService();
            this._filmService = new FilmService();

            this.parent = parent;
            this.film = film;
            this.previous = previous;

            imgMain.AllowDrop = true;

            Load();
        }


        #region Events

        private void imgMain_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;)|*.jpg; *.jpeg; *.gif; *.bmp;";

            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileNameRoot = open.FileName;
                    imgMain.BackgroundImage = new Bitmap(open.FileName);

                    pathNewImage = Path.GetFileName(fileNameRoot);
                    ptbCamera.Visible = false;
                    isChanged = true;
                    isImageChanged = true;
                    pnlImage.BackColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ảnh chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void imgMain_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                foreach (string pic in ((string[])e.Data.GetData(DataFormats.FileDrop)))
                {
                    var img = Image.FromFile(pic);
                    imgMain.BackgroundImage = img;
                    fileNameRoot = pic;

                    pathNewImage = Path.GetFileName(pic);
                    isChanged = true;
                    isImageChanged = true;
                    pnlImage.BackColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ảnh được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void imgMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            ptbCamera.Visible = false;
        }

        private void imgMain_DragLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pathNewImage) && film == null)
            {
                ptbCamera.Visible = true;
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if(CreateOrUpdate())
            {
                // Cập nhật UI
                UpdateUI();

                if (film == null)
                {
                    parent.Reload();
                    MessageBox.Show("Thêm mới phim thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    parent.Reload(film);
                    MessageBox.Show("Cập nhật phim thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            isChanged = true;
        }
        #endregion


        #region Methods

        new private void Load()
        {
            pathNewImage = film == null ? "" : film.Image;

            LoadDetail();

            cbbCategory.SelectedIndexChanged += (s, e) => isChanged = true;
        }

        private void LoadDetail()
        {
            btnCreate.Text = film == null ? "THÊM MỚI" : "CẬP NHẬT";

            LoadImage();

            LoadText();

            LoadCategory();
        }

        private void LoadImage()
        {
            ptbCamera.Visible = film == null;

            if (film != null)
            {
                imgMain.BackgroundImage = new Bitmap("../../Resources/" + film.Image);
            }
        }

        private void LoadText()
        {
            if (film != null)
            {
                lblTitle.Text = film.Name.Split('-')[0].ToUpper();
                txtName.Text = film.Name;
                txtLanguage.Text = film.Language;
                txtCreatedYear.Text = film.CreatedYear.ToString();
                txtActor.Text = film.Actor;
                txtDirector.Text = film.Director;
                txtNational.Text = film.National;
                txtTimeLong.Text = film.TimeLong.ToString();
                txtPrice.Text = film.Price.ToString("#,##");
                txtDescription.Text = film.Description;
            }
            else
            {
                lblTitle.Text = "THÊM PHIM MỚI";
                txtName.Text = "";
                txtLanguage.Text = "";
                txtCreatedYear.Text = "";
                txtActor.Text = "";
                txtDirector.Text = "";
                txtNational.Text = "";
                txtTimeLong.Text = "";
                txtPrice.Text = "";
                txtDescription.Text = "";
            }
        }

        private void LoadCategory()
        {
            var categories = _categoryFilmService.GetAll();

            cbbCategory.DataSource = categories;
            cbbCategory.DisplayMember = "Name";
            // không ID
            if (film != null)
            {
                cbbCategory.SelectedItem = categories.FirstOrDefault(c => c.ID == film.CategoryFilmID);
            }
        }

        private bool CreateOrUpdate()
        {
            if (CheckValidateValue())
            {
                var newFilm = GetFilmByUI();

                // if isImageChaned save image into resources
                if (isImageChanged)
                {
                    if (!File.Exists(Path.Combine("../../Resources/", pathNewImage)))
                    {
                        File.Copy(fileNameRoot, Path.Combine("../../Resources/", pathNewImage));  // save file   
                    }
                }

                if (film == null)
                {
                    _filmService.Insert(newFilm);
                }
                else
                {
                    _filmService.Update(newFilm);
                }

                film = newFilm;
                isChanged = false;
                isImageChanged = false;

                return true;
            }

            return false;
        }

        private void UpdateUI()
        {
            lblTitle.Text = film.Name.Split('-')[0].ToUpper();
            btnCreate.Text = "CẬP NHẬT";
        }

        private Film GetFilmByUI()
        {
            var f = new Film();
            f.ID = film == null ? f.ID : film.ID;
            f.CategoryFilmID = (cbbCategory.SelectedItem as CategoryFilm).ID;
            f.Name = txtName.Text;
            f.Image = pathNewImage;
            f.Price = Decimal.Parse(txtPrice.Text);
            f.Description = txtDescription.Text;
            f.Director = txtDirector.Text;
            f.National = txtNational.Text;
            f.Language = txtLanguage.Text;
            f.OrderCount = film != null ? film.OrderCount : 0;
            f.TimeLong = Int32.Parse(txtTimeLong.Text);
            f.Rating = film != null ? film.Rating : 0;
            f.Actor = txtActor.Text;
            f.CreatedYear = Int32.Parse(txtCreatedYear.Text);

            return f;
        }

        private bool CheckValidateValue()
        {
            var check = true;

            if (!isImageChanged && film == null)
            {
                pnlImage.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;
                check = false;
            }

            check = !CheckValidate(txtName, pnlName) ? false : check;
            check = !CheckValidate(txtLanguage, pnlLanguage) ? false : check;
            check = !CheckValidate(txtCreatedYear, pnlCreatedYear, true) ? false : check;
            check = !CheckValidate(txtActor, pnlActor) ? false : check;
            check = !CheckValidate(txtDirector, pnlDirector) ? false : check;
            check = !CheckValidate(txtNational, pnlNational) ? false : check;
            check = !CheckValidate(txtTimeLong, pnlTimeLong, true) ? false : check;
            check = !CheckValidate(txtPrice, pnlPrice, true) ? false : check;
            check = !CheckValidate(txtDescription, pnlDescription) ? false : check;

            return check;
        }

        private bool CheckValidate(TextBox txt, Panel pnl, bool isNumber = false)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnl.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;

                return false;
            }
            else if (isNumber && !decimal.TryParse(txt.Text, out decimal res))
            {
                txt.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnl.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;

                return false;
            }
            else
            {
                txt.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
                pnl.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;

                return true;
            }
        }

        #endregion

        #region UI

        private void btnBack_MouseEnter(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(34, 34, 34);
            btnBack.IconColor = Color.FromArgb(255, 34, 101);
            btnBack.ForeColor = Color.FromArgb(255, 34, 101);
        }

        private void btnBack_MouseLeave(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(40, 40, 40);
            btnBack.IconColor = Color.FromArgb(68, 226, 255);
            btnBack.ForeColor = Color.FromArgb(68, 226, 255);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (isChanged && film != null)
            {
                if (MessageBox.Show("Bạn có muốn hủy các thay đổi!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UIHelper.ShowCombackControl(previous);
                }
            }
            else if (isChanged && film == null)
            {
                if (MessageBox.Show("Bạn có muốn hủy các dữ liệu!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UIHelper.ShowCombackControl(previous);
                }
            }
            else
            {
                UIHelper.ShowCombackControl(previous);
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            txtName.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlName.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            CheckValidate(txtName, pnlName);
        }

        private void txtLanguage_Enter(object sender, EventArgs e)
        {
            txtLanguage.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlLanguage.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtLanguage_Leave(object sender, EventArgs e)
        {
            CheckValidate(txtLanguage, pnlLanguage);
        }

        private void txtCreatedYear_Enter(object sender, EventArgs e)
        {
            txtCreatedYear.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlCreatedYear.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtCreatedYear_Leave(object sender, EventArgs e)
        {

            CheckValidate(txtCreatedYear, pnlCreatedYear, true);
        }

        private void txtDirector_Enter(object sender, EventArgs e)
        {
            txtDirector.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlDirector.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtDirector_Leave(object sender, EventArgs e)
        {

            CheckValidate(txtDirector, pnlDirector);
        }

        private void txtNational_Enter(object sender, EventArgs e)
        {
            txtNational.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlNational.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtNational_Leave(object sender, EventArgs e)
        {

            CheckValidate(txtNational, pnlNational);
        }

        private void txtTimeLong_Enter(object sender, EventArgs e)
        {
            txtTimeLong.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlTimeLong.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtTimeLong_Leave(object sender, EventArgs e)
        {
            CheckValidate(txtTimeLong, pnlTimeLong, true);
        }

        private void txtPrice_Enter(object sender, EventArgs e)
        {
            txtPrice.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlPrice.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            CheckValidate(txtPrice, pnlPrice, true);
        }

        private void txtActor_Enter(object sender, EventArgs e)
        {
            txtActor.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlActor.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtActor_Leave(object sender, EventArgs e)
        {

            CheckValidate(txtActor, pnlActor);
        }

        private void txtDescription_Enter(object sender, EventArgs e)
        {
            txtDescription.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlDescription.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {

            CheckValidate(txtDescription, pnlDescription);
        }

        #endregion
    }
}
