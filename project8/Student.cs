using System;

namespace StudentManager.Models
{
	public class Student
	{
		public Guid Id { get; } = Guid.NewGuid();
		public string Surname { get; set; }
		public string Name { get; set; }
		public string Patronymic { get; set; }
		public int Course { get; set; }
		public string Group { get; set; }
		public DateTime BirthDate { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
	}
}