using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using StudentManager.Models;

namespace StudentManager.Utilities
{
	public static class FileManager
	{
		public static void SaveToJson(string path, List<Student> students)
		{
			string json = JsonConvert.SerializeObject(students, Newtonsoft.Json.Formatting.Indented);
			File.WriteAllText(path, json);
		}

		public static List<Student> LoadFromJson(string path)
		{
			string json = File.ReadAllText(path);
			return JsonConvert.DeserializeObject<List<Student>>(json);
		}

		public static List<Student> ImportFromCsv(string path)
		{
			var students = new List<Student>();
			// Реализация импорта CSV
			return students;
		}
	}
}