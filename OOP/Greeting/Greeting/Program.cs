namespace Greeting
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Person person = new Person();
			person.Name = "John";
			person.Age = 25;

			person.Greet();

			Person friend = new Person();
			friend.Name = "Jane";
			friend.Age = 30;

			friend.Greet();
		}
	}

	public class Person()
	{
		public string Name { get; set; }
		public int Age { get; set; }

		public void Greet()
		{
			Console.WriteLine($"Hello, my name is {Name} and I am {Age} years old.");
		}
	}
}