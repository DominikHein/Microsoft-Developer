using System.Text.Json;
using System.Xml.Serialization;

namespace CustomSerialization
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Person person = new Person { Name = "John", Age = 30 };
			// Object to Binary
			using (FileStream fileStream = new FileStream("person.bin", FileMode.Create))
			{
				using (BinaryWriter writer = new BinaryWriter(fileStream))
				{
					writer.Write(person.Name);
					writer.Write(person.Age);
				}
			}
			Console.WriteLine("Binary serialization of the Person object is complete.");

			//Binary to Object
			Person deserializedPerson = new Person();
			using (FileStream fileStream = new FileStream("person.bin", FileMode.Open))
			{
				using (BinaryReader reader = new BinaryReader(fileStream))
				{
					person.Name = reader.ReadString();
					person.Age = reader.ReadInt32();
				}
			}
			Console.WriteLine("Binary deserialization of the Person object is complete.");
			Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");

			deserializedPerson.Init();

			// Object to XML
			XmlSerializer serializer = new XmlSerializer(typeof(Person));

			using (FileStream fileStream = new FileStream("person.xml", FileMode.Create))
			{
				serializer.Serialize(fileStream, person);
			}

			Console.WriteLine("XML serialization of the Person object is complete.");

			//XML to Object
			using (FileStream fileStream = new FileStream("person.xml", FileMode.Open))
			{
				deserializedPerson = (Person)serializer.Deserialize(fileStream);
			}

			Console.WriteLine("XML deserialization of the Person object is complete.");
			Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");

			deserializedPerson.Init();

			// Object to JSON
			string jsonString = JsonSerializer.Serialize(person);

			File.WriteAllText("person.json", jsonString);

			Console.WriteLine("JSON serialization of the Person object is complete.");

			//JSON to Object
			deserializedPerson = JsonSerializer.Deserialize<Person>(jsonString);
			Console.WriteLine("JSON deserialization of the Person object is complete.");
			Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
		}
	}

	public class Person
	{
		public string Name { get; set; }
		public int Age { get; set; }

		public void Init()
		{
			Name = "";
			Age = 0;
		}
	}
}
