using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Linq;
using StudentManager.Models;
using StudentManager.Utilities;

namespace StudentManager.Forms
{
	public partial class MainForm : Form
	{
		private BindingList<Student> students = new BindingList<Student>();
		private bool hasUnsavedChanges = false;

		public MainForm()
		{
			InitializeComponent();
			ConfigureDataGridView();
		}

		private void ConfigureDataGridView()
		{
			dataGridView.AutoGenerateColumns = false;
			dataGridView.DataSource = students;

			// Добавление колонок
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Surname",
				HeaderText = "Фамилия"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Name",
				HeaderText = "Имя"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Patronymic",
				HeaderText = "Отчество"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Course",
				HeaderText = "Курс"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Group",
				HeaderText = "Группа"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "BirthDate",
				HeaderText = "Дата рождкния"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Email",
				HeaderText = "Электронная почта"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Phone",
				HeaderText = "Телефон"
			});
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtSurname.Text))
				{
					MessageBox.Show("Фамилия обязательна!", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtSurname.Focus();
					return;
				}

				if (!int.TryParse(txtCourse.Text, out int course))
				{
					MessageBox.Show("Некорректный курс! Введите число.", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (course < 1 || course > 4)
				{
					MessageBox.Show("Курс должен быть от 1 до 4!", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				var student = new Student
				{
					Surname = txtSurname.Text.Trim(),
					Name = txtName.Text.Trim(),
					Patronymic = txtPatronymic.Text.Trim(),
					Course = course,
					Group = txtGroup.Text.Trim(),
					BirthDate = BirthDate.Value,
					Email = txtEmail.Text.Trim(),
					Phone = txtPhone.Text.Trim()
				};

				Validators.ValidateStudent(student); // Валидация
				students.Add(student); // Добавление в список
				hasUnsavedChanges = true; // Изменения не сохранены

				// Очистка полей (опционально)
				txtSurname.Clear();
				txtName.Clear();
				txtPatronymic.Clear();
				txtCourse.Clear();
				txtGroup.Clear();
				txtEmail.Clear();
				txtPhone.Clear();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			FileManager.SaveToJson("students.json", new List<Student>(students));
			hasUnsavedChanges = false;
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (hasUnsavedChanges)
			{
				var result = MessageBox.Show("Сохранить изменения?", "Внимание",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Yes) btnSave_Click(null, null);
				else if (result == DialogResult.Cancel) e.Cancel = true;
			}
			base.OnFormClosing(e);
		}
	}
}