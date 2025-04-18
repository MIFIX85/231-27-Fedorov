namespace StudentManager.Forms
{
	partial class MainForm
	{
		private DataGridView dataGridView;
		private TextBox txtSurname;
		private TextBox txtName;
		private TextBox txtPatronymic;
		private System.Windows.Forms.DateTimePicker BirthDate;
		private TextBox txtCourse;
		private TextBox txtGroup;
		private TextBox txtEmail;
		private TextBox txtPhone;

		private Button btnAdd;

		private Label labelSurname;
		private Label labelName;
		private Label labelPatronymic;
		private Label labelCourse;
		private Label labelGroup;
		private Label labelEmail;
		private Label labelPhone;
		private Label labelBirthDate;

		private System.ComponentModel.IContainer components = null;

		private void InitializeComponent()
		{
			dataGridView = new DataGridView();
			btnAdd = new Button();
			txtSurname = new TextBox();
			labelSurname = new Label();
			txtName = new TextBox();
			labelName = new Label();
			txtPatronymic = new TextBox();
			labelPatronymic = new Label();
			BirthDate = new DateTimePicker();
			labelBirthDate = new Label();
			txtCourse = new TextBox();
			labelCourse = new Label();
			txtGroup = new TextBox();
			labelGroup = new Label();
			txtEmail = new TextBox();
			labelEmail = new Label();
			txtPhone = new TextBox();
			labelPhone = new Label();
			((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
			SuspendLayout();
			// 
			// dataGridView
			// 
			dataGridView.ColumnHeadersHeight = 46;
			dataGridView.Location = new Point(12, 12);
			dataGridView.Name = "dataGridView";
			dataGridView.RowHeadersWidth = 82;
			dataGridView.Size = new Size(960, 300);
			dataGridView.TabIndex = 0;
			// 
			// btnAdd
			// 
			btnAdd.Location = new Point(650, 400);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(261, 36);
			btnAdd.TabIndex = 1;
			btnAdd.Text = "Добавить";
			btnAdd.Click += btnAdd_Click;
			// 
			// txtSurname
			// 
			txtSurname.Location = new Point(160, 350);
			txtSurname.Name = "txtSurname";
			txtSurname.Size = new Size(100, 39);
			txtSurname.TabIndex = 0;
			// 
			// labelSurname
			// 
			labelSurname.Location = new Point(85, 350);
			labelSurname.Name = "labelSurname";
			labelSurname.Size = new Size(100, 23);
			labelSurname.TabIndex = 1;
			labelSurname.Text = "Фамилия:";
			// 
			// txtName
			// 
			txtName.Location = new Point(160, 380);
			txtName.Name = "txtName";
			txtName.Size = new Size(100, 39);
			txtName.TabIndex = 1;
			// 
			// labelName
			// 
			labelName.Location = new Point(85, 380);
			labelName.Name = "labelName";
			labelName.Size = new Size(100, 23);
			labelName.TabIndex = 2;
			labelName.Text = "Имя:";
			// 
			// txtPatronymic
			// 
			txtPatronymic.Location = new Point(160, 410);
			txtPatronymic.Name = "txtPatronymic";
			txtPatronymic.Size = new Size(100, 39);
			txtPatronymic.TabIndex = 2;
			// 
			// labelPatronymic
			// 
			labelPatronymic.Location = new Point(85, 410);
			labelPatronymic.Name = "labelPatronymic";
			labelPatronymic.Size = new Size(100, 23);
			labelPatronymic.TabIndex = 3;
			labelPatronymic.Text = "Отчество:";
			// 
			// BirthDate
			// 
			BirthDate.Location = new Point(160, 440);
			BirthDate.Name = "BirthDate";
			BirthDate.Size = new Size(130, 39);
			BirthDate.TabIndex = 3;
			// 
			// labelBirthDate
			// 
			labelBirthDate.Location = new Point(60, 440);
			labelBirthDate.Name = "labelBirthDate";
			labelBirthDate.Size = new Size(100, 23);
			labelBirthDate.TabIndex = 4;
			labelBirthDate.Text = "День рождения:";
			// 
			// txtCourse
			// 
			txtCourse.Location = new Point(400, 350);
			txtCourse.Name = "txtCourse";
			txtCourse.Size = new Size(100, 39);
			txtCourse.TabIndex = 4;
			// 
			// labelCourse
			// 
			labelCourse.Location = new Point(350, 350);
			labelCourse.Name = "labelCourse";
			labelCourse.Size = new Size(100, 23);
			labelCourse.TabIndex = 5;
			labelCourse.Text = "Курс:";
			// 
			// txtGroup
			// 
			txtGroup.Location = new Point(400, 380);
			txtGroup.Name = "txtGroup";
			txtGroup.Size = new Size(100, 39);
			txtGroup.TabIndex = 5;
			// 
			// labelGroup
			// 
			labelGroup.Location = new Point(350, 380);
			labelGroup.Name = "labelGroup";
			labelGroup.Size = new Size(100, 23);
			labelGroup.TabIndex = 6;
			labelGroup.Text = "Группа:";
			// 
			// txtEmail
			// 
			txtEmail.Location = new Point(400, 410);
			txtEmail.Name = "txtEmail";
			txtEmail.Size = new Size(100, 39);
			txtEmail.TabIndex = 6;
			// 
			// labelEmail
			// 
			labelEmail.Location = new Point(350, 410);
			labelEmail.Name = "labelEmail";
			labelEmail.Size = new Size(100, 23);
			labelEmail.TabIndex = 7;
			labelEmail.Text = "Email:";
			// 
			// txtPhone
			// 
			txtPhone.Location = new Point(400, 440);
			txtPhone.Name = "txtPhone";
			txtPhone.Size = new Size(100, 39);
			txtPhone.TabIndex = 7;
			// 
			// labelPhone
			// 
			labelPhone.Location = new Point(350, 440);
			labelPhone.Name = "labelPhone";
			labelPhone.Size = new Size(100, 23);
			labelPhone.TabIndex = 8;
			labelPhone.Text = "Телефон:";
			// 
			// MainForm
			// 
			ClientSize = new Size(985, 529);
			Controls.Add(txtSurname);
			Controls.Add(labelSurname);
			Controls.Add(txtName);
			Controls.Add(labelName);
			Controls.Add(txtPatronymic);
			Controls.Add(labelPatronymic);
			Controls.Add(BirthDate);
			Controls.Add(labelBirthDate);
			Controls.Add(txtCourse);
			Controls.Add(labelCourse);
			Controls.Add(txtGroup);
			Controls.Add(labelGroup);
			Controls.Add(txtEmail);
			Controls.Add(labelEmail);
			Controls.Add(txtPhone);
			Controls.Add(labelPhone);
			Controls.Add(dataGridView);
			Controls.Add(btnAdd);
			Name = "MainForm";
			Text = "Student Manager";
			((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}