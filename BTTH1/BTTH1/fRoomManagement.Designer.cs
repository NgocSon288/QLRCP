
namespace BTTH1
{
    partial class fRoomManagement
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpRoom = new System.Windows.Forms.FlowLayoutPanel();
            this.lvFilm = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteRoom = new System.Windows.Forms.Button();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.lblRoomName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpDateShow = new System.Windows.Forms.DateTimePicker();
            this.cbbFilms = new System.Windows.Forms.ComboBox();
            this.pnlSeatCount = new System.Windows.Forms.Panel();
            this.txtSeatCount = new System.Windows.Forms.TextBox();
            this.pnlSeatMax = new System.Windows.Forms.Panel();
            this.txtSeatMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDeleteRoomFilm = new System.Windows.Forms.Button();
            this.btnUpdateRoomFilm = new System.Windows.Forms.Button();
            this.btnCreateRoomFilm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpRoom
            // 
            this.flpRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpRoom.Location = new System.Drawing.Point(20, 20);
            this.flpRoom.Name = "flpRoom";
            this.flpRoom.Size = new System.Drawing.Size(938, 1144);
            this.flpRoom.TabIndex = 0;
            // 
            // lvFilm
            // 
            this.lvFilm.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvFilm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lvFilm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvFilm.Font = new System.Drawing.Font("Consolas", 19F);
            this.lvFilm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.lvFilm.FullRowSelect = true;
            this.lvFilm.GridLines = true;
            this.lvFilm.HideSelection = false;
            this.lvFilm.HotTracking = true;
            this.lvFilm.HoverSelection = true;
            this.lvFilm.Location = new System.Drawing.Point(964, 628);
            this.lvFilm.MultiSelect = false;
            this.lvFilm.Name = "lvFilm";
            this.lvFilm.Size = new System.Drawing.Size(947, 652);
            this.lvFilm.TabIndex = 1;
            this.lvFilm.TabStop = false;
            this.lvFilm.UseCompatibleStateImageBehavior = false;
            this.lvFilm.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeleteRoom);
            this.panel1.Controls.Add(this.btnCreateRoom);
            this.panel1.Location = new System.Drawing.Point(20, 1184);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(938, 96);
            this.panel1.TabIndex = 2;
            // 
            // btnDeleteRoom
            // 
            this.btnDeleteRoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteRoom.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnDeleteRoom.FlatAppearance.BorderSize = 2;
            this.btnDeleteRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteRoom.Font = new System.Drawing.Font("Consolas", 16.125F, System.Drawing.FontStyle.Bold);
            this.btnDeleteRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnDeleteRoom.Location = new System.Drawing.Point(15, 14);
            this.btnDeleteRoom.Name = "btnDeleteRoom";
            this.btnDeleteRoom.Size = new System.Drawing.Size(343, 67);
            this.btnDeleteRoom.TabIndex = 33;
            this.btnDeleteRoom.TabStop = false;
            this.btnDeleteRoom.Text = "XÓA";
            this.btnDeleteRoom.UseVisualStyleBackColor = true;
            this.btnDeleteRoom.Click += new System.EventHandler(this.btnDeleteRoom_Click);
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateRoom.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnCreateRoom.FlatAppearance.BorderSize = 2;
            this.btnCreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRoom.Font = new System.Drawing.Font("Consolas", 16.125F, System.Drawing.FontStyle.Bold);
            this.btnCreateRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnCreateRoom.Location = new System.Drawing.Point(580, 14);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(343, 67);
            this.btnCreateRoom.TabIndex = 31;
            this.btnCreateRoom.TabStop = false;
            this.btnCreateRoom.Text = "THÊM";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // lblRoomName
            // 
            this.lblRoomName.AutoSize = true;
            this.lblRoomName.Font = new System.Drawing.Font("Consolas", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(226)))), ((int)(((byte)(255)))));
            this.lblRoomName.Location = new System.Drawing.Point(24, 8);
            this.lblRoomName.Name = "lblRoomName";
            this.lblRoomName.Size = new System.Drawing.Size(262, 51);
            this.lblRoomName.TabIndex = 3;
            this.lblRoomName.Text = "Phòng: R01";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.label1.Location = new System.Drawing.Point(27, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ngày chiếu:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.label3.Location = new System.Drawing.Point(27, 319);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 32);
            this.label3.TabIndex = 13;
            this.label3.Text = "Số ghế:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.label5.Location = new System.Drawing.Point(27, 432);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(255, 32);
            this.label5.TabIndex = 15;
            this.label5.Text = "Số ghế hiện tại:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblRoomName);
            this.panel2.Controls.Add(this.dtpDateShow);
            this.panel2.Controls.Add(this.cbbFilms);
            this.panel2.Controls.Add(this.pnlSeatCount);
            this.panel2.Controls.Add(this.txtSeatCount);
            this.panel2.Controls.Add(this.pnlSeatMax);
            this.panel2.Controls.Add(this.txtSeatMax);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(964, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(947, 500);
            this.panel2.TabIndex = 17;
            // 
            // dtpDateShow
            // 
            this.dtpDateShow.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(139)))), ((int)(((byte)(172)))));
            this.dtpDateShow.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.dtpDateShow.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.dtpDateShow.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(139)))), ((int)(((byte)(172)))));
            this.dtpDateShow.Font = new System.Drawing.Font("Consolas", 21F);
            this.dtpDateShow.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateShow.Location = new System.Drawing.Point(375, 206);
            this.dtpDateShow.Name = "dtpDateShow";
            this.dtpDateShow.Size = new System.Drawing.Size(552, 73);
            this.dtpDateShow.TabIndex = 1;
            this.dtpDateShow.ValueChanged += new System.EventHandler(this.dtpDateShow_ValueChanged);
            // 
            // cbbFilms
            // 
            this.cbbFilms.BackColor = System.Drawing.Color.White;
            this.cbbFilms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFilms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbFilms.Font = new System.Drawing.Font("Consolas", 21F);
            this.cbbFilms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(139)))), ((int)(((byte)(172)))));
            this.cbbFilms.FormattingEnabled = true;
            this.cbbFilms.Location = new System.Drawing.Point(367, 89);
            this.cbbFilms.Name = "cbbFilms";
            this.cbbFilms.Size = new System.Drawing.Size(560, 74);
            this.cbbFilms.TabIndex = 0;
            // 
            // pnlSeatCount
            // 
            this.pnlSeatCount.BackColor = System.Drawing.Color.Silver;
            this.pnlSeatCount.ForeColor = System.Drawing.Color.Red;
            this.pnlSeatCount.Location = new System.Drawing.Point(367, 456);
            this.pnlSeatCount.Name = "pnlSeatCount";
            this.pnlSeatCount.Size = new System.Drawing.Size(566, 3);
            this.pnlSeatCount.TabIndex = 29;
            // 
            // txtSeatCount
            // 
            this.txtSeatCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtSeatCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSeatCount.Font = new System.Drawing.Font("Consolas", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeatCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(139)))), ((int)(((byte)(172)))));
            this.txtSeatCount.Location = new System.Drawing.Point(379, 423);
            this.txtSeatCount.Name = "txtSeatCount";
            this.txtSeatCount.ReadOnly = true;
            this.txtSeatCount.Size = new System.Drawing.Size(551, 66);
            this.txtSeatCount.TabIndex = 3;
            this.txtSeatCount.Text = "tên";
            this.txtSeatCount.Enter += new System.EventHandler(this.txtSeatCount_Enter);
            this.txtSeatCount.Leave += new System.EventHandler(this.txtSeatCount_Leave);
            // 
            // pnlSeatMax
            // 
            this.pnlSeatMax.BackColor = System.Drawing.Color.Silver;
            this.pnlSeatMax.ForeColor = System.Drawing.Color.Red;
            this.pnlSeatMax.Location = new System.Drawing.Point(364, 345);
            this.pnlSeatMax.Name = "pnlSeatMax";
            this.pnlSeatMax.Size = new System.Drawing.Size(566, 3);
            this.pnlSeatMax.TabIndex = 29;
            // 
            // txtSeatMax
            // 
            this.txtSeatMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtSeatMax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSeatMax.Font = new System.Drawing.Font("Consolas", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeatMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(139)))), ((int)(((byte)(172)))));
            this.txtSeatMax.Location = new System.Drawing.Point(376, 312);
            this.txtSeatMax.Name = "txtSeatMax";
            this.txtSeatMax.ReadOnly = true;
            this.txtSeatMax.Size = new System.Drawing.Size(551, 66);
            this.txtSeatMax.TabIndex = 2;
            this.txtSeatMax.Text = "tên";
            this.txtSeatMax.Enter += new System.EventHandler(this.txtSeatMax_Enter);
            this.txtSeatMax.Leave += new System.EventHandler(this.txtSeatMax_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.label6.Location = new System.Drawing.Point(27, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 32);
            this.label6.TabIndex = 17;
            this.label6.Text = "Tên phim:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDeleteRoomFilm);
            this.panel3.Controls.Add(this.btnUpdateRoomFilm);
            this.panel3.Controls.Add(this.btnCreateRoomFilm);
            this.panel3.Location = new System.Drawing.Point(964, 526);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(947, 96);
            this.panel3.TabIndex = 18;
            // 
            // btnDeleteRoomFilm
            // 
            this.btnDeleteRoomFilm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteRoomFilm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnDeleteRoomFilm.FlatAppearance.BorderSize = 2;
            this.btnDeleteRoomFilm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteRoomFilm.Font = new System.Drawing.Font("Consolas", 16.125F, System.Drawing.FontStyle.Bold);
            this.btnDeleteRoomFilm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnDeleteRoomFilm.Location = new System.Drawing.Point(15, 14);
            this.btnDeleteRoomFilm.Name = "btnDeleteRoomFilm";
            this.btnDeleteRoomFilm.Size = new System.Drawing.Size(192, 67);
            this.btnDeleteRoomFilm.TabIndex = 33;
            this.btnDeleteRoomFilm.TabStop = false;
            this.btnDeleteRoomFilm.Text = "XÓA";
            this.btnDeleteRoomFilm.UseVisualStyleBackColor = true;
            this.btnDeleteRoomFilm.Click += new System.EventHandler(this.btnDeleteRoomFilm_Click);
            // 
            // btnUpdateRoomFilm
            // 
            this.btnUpdateRoomFilm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateRoomFilm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnUpdateRoomFilm.FlatAppearance.BorderSize = 2;
            this.btnUpdateRoomFilm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateRoomFilm.Font = new System.Drawing.Font("Consolas", 16.125F, System.Drawing.FontStyle.Bold);
            this.btnUpdateRoomFilm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnUpdateRoomFilm.Location = new System.Drawing.Point(375, 14);
            this.btnUpdateRoomFilm.Name = "btnUpdateRoomFilm";
            this.btnUpdateRoomFilm.Size = new System.Drawing.Size(192, 67);
            this.btnUpdateRoomFilm.TabIndex = 32;
            this.btnUpdateRoomFilm.TabStop = false;
            this.btnUpdateRoomFilm.Text = "SỬA";
            this.btnUpdateRoomFilm.UseVisualStyleBackColor = true;
            this.btnUpdateRoomFilm.Click += new System.EventHandler(this.btnUpdateRoomFilm_Click);
            // 
            // btnCreateRoomFilm
            // 
            this.btnCreateRoomFilm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateRoomFilm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnCreateRoomFilm.FlatAppearance.BorderSize = 2;
            this.btnCreateRoomFilm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRoomFilm.Font = new System.Drawing.Font("Consolas", 16.125F, System.Drawing.FontStyle.Bold);
            this.btnCreateRoomFilm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(101)))));
            this.btnCreateRoomFilm.Location = new System.Drawing.Point(735, 14);
            this.btnCreateRoomFilm.Name = "btnCreateRoomFilm";
            this.btnCreateRoomFilm.Size = new System.Drawing.Size(192, 67);
            this.btnCreateRoomFilm.TabIndex = 31;
            this.btnCreateRoomFilm.TabStop = false;
            this.btnCreateRoomFilm.Text = "THÊM";
            this.btnCreateRoomFilm.UseVisualStyleBackColor = true;
            this.btnCreateRoomFilm.Click += new System.EventHandler(this.btnCreateRoomFilm_Click);
            // 
            // fRoomManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lvFilm);
            this.Controls.Add(this.flpRoom);
            this.Name = "fRoomManagement";
            this.Size = new System.Drawing.Size(1928, 1300);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpRoom;
        private System.Windows.Forms.ListView lvFilm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeleteRoom;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Label lblRoomName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDeleteRoomFilm;
        private System.Windows.Forms.Button btnUpdateRoomFilm;
        private System.Windows.Forms.Button btnCreateRoomFilm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlSeatCount;
        private System.Windows.Forms.TextBox txtSeatCount;
        private System.Windows.Forms.Panel pnlSeatMax;
        private System.Windows.Forms.TextBox txtSeatMax;
        private System.Windows.Forms.ComboBox cbbFilms;
        private System.Windows.Forms.DateTimePicker dtpDateShow;
    }
}
