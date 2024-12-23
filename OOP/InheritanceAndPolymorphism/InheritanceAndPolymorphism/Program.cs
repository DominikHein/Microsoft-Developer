namespace InheritanceAndPolymorphism
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Dog dog = new Dog();
			Cat cat = new Cat();

			dog.Eat();
			cat.Eat();

			List<Animal> animals = new List<Animal>();
			animals.Add(dog);
			animals.Add(cat);

			for (int i = 0; i < animals.Count; i++)
			{
				animals[i].MakeSound();
			}

		}
	}

	public interface IAnimal
	{
		void Eat();
	}

	public class Animal() : IAnimal
	{
		public virtual void MakeSound()
		{
			Console.WriteLine("Animal makes a sound");
		}

		public void Eat()
		{
			Console.WriteLine("Animal eats");
		}
	}

	public class Dog : Animal
	{
		public override void MakeSound()
		{
			Console.WriteLine("barks");
		}
		public void Eat()
		{
			Console.WriteLine("Dog eats");
		}
	}

	public class Cat : Animal
	{
		public override void MakeSound()
		{
			Console.WriteLine("meows");
		}
		public void Eat()
		{
			Console.WriteLine("Cat eats");
		}
	}
}
