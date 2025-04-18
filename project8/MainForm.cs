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

			// ���������� �������
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Surname",
				HeaderText = "�������"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Name",
				HeaderText = "���"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Patronymic",
				HeaderText = "��������"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Course",
				HeaderText = "����"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Group",
				HeaderText = "������"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "BirthDate",
				HeaderText = "���� ��������"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Email",
				HeaderText = "����������� �����"
			});
			dataGridView.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Phone",
				HeaderText = "�������"
			});
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtSurname.Text))
				{
					MessageBox.Show("������� �����������!", "������",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtSurname.Focus();
					return;
				}

				if (!int.TryParse(txtCourse.Text, out int course))
				{
					MessageBox.Show("������������ ����! ������� �����.", "������",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (course < 1 || course > 4)
				{
					MessageBox.Show("���� ������ ���� �� 1 �� 4!", "������",
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

				Validators.ValidateStudent(student); // ���������
				students.Add(student); // ���������� � ������
				hasUnsavedChanges = true; // ��������� �� ���������

				// ������� ����� (�����������)
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
				MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				var result = MessageBox.Show("��������� ���������?", "��������",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Yes) btnSave_Click(null, null);
				else if (result == DialogResult.Cancel) e.Cancel = true;
			}
			base.OnFormClosing(e);
		}
	}
}