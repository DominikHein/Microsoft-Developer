namespace SimpleLibraryManagement
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] books = new string[5];
			bool[] isBookCheckedOut = new bool[5];
			int borrowedBooksCount = 0;
			const int borrowingLimit = 3;

			while (true)
			{
				Console.WriteLine("Choose an action: add, remove, display, borrow, return, exit");
				string action = Console.ReadLine().ToLower();

				if (action == "add")
				{
					AddBook(books);
				}
				else if (action == "remove")
				{
					RemoveBook(books, isBookCheckedOut);
				}
				else if (action == "display")
				{
					DisplayBooks(books, isBookCheckedOut);
				}
				else if (action == "borrow")
				{
					if (borrowedBooksCount < borrowingLimit)
					{
						BorrowBook(books, isBookCheckedOut, ref borrowedBooksCount);
					}
					else
					{
						Console.WriteLine("You have reached the borrowing limit of 3 books.");
					}
				}
				else if (action == "return")
				{
					ReturnBook(books, isBookCheckedOut, ref borrowedBooksCount);
				}
				else if (action == "exit")
				{
					break;
				}
				else
				{
					Console.WriteLine("Invalid request. Please try again.");
				}
			}
		}

		static void AddBook(string[] books)
		{
			Console.WriteLine("Enter the Title of the book which you want to add:");
			string newBook = Console.ReadLine();

			for (int i = 0; i < books.Length; i++)
			{
				if (string.IsNullOrEmpty(books[i]))
				{
					books[i] = newBook;
					Console.WriteLine($"Book '{newBook}' was added.");
					return;
				}
			}

			Console.WriteLine("No more room for new books.");
		}

		static void RemoveBook(string[] books, bool[] isBookCheckedOut)
		{
			Console.WriteLine("Enter the Title of the book which you want to remove:");
			string bookToRemove = Console.ReadLine();

			for (int i = 0; i < books.Length; i++)
			{
				if (books[i] == bookToRemove)
				{
					books[i] = null;
					isBookCheckedOut[i] = false;
					Console.WriteLine($"Book '{bookToRemove}' was removed.");
					return;
				}
			}

			Console.WriteLine("Book not found.");
		}

		static void DisplayBooks(string[] books, bool[] isBookCheckedOut)
		{
			Console.WriteLine("Books in this library:");
			bool hasBooks = false;

			for (int i = 0; i < books.Length; i++)
			{
				if (!string.IsNullOrEmpty(books[i]))
				{
					string status = isBookCheckedOut[i] ? " (Checked Out)" : "";
					Console.WriteLine($"- {books[i]}{status}");
					hasBooks = true;
				}
			}

			if (!hasBooks)
			{
				Console.WriteLine("No Books in this Library");
			}
		}

		static void BorrowBook(string[] books, bool[] isBookCheckedOut, ref int borrowedBooksCount)
		{
			Console.WriteLine("Enter the Title of the book which you want to borrow:");
			string bookToBorrow = Console.ReadLine();

			for (int i = 0; i < books.Length; i++)
			{
				if (books[i] == bookToBorrow && !isBookCheckedOut[i])
				{
					isBookCheckedOut[i] = true;
					borrowedBooksCount++;
					Console.WriteLine($"Book '{bookToBorrow}' was borrowed.");
					return;
				}
			}

			Console.WriteLine("Book not found or already checked out.");
		}

		static void ReturnBook(string[] books, bool[] isBookCheckedOut, ref int borrowedBooksCount)
		{
			Console.WriteLine("Enter the Title of the book which you want to return:");
			string bookToReturn = Console.ReadLine();

			for (int i = 0; i < books.Length; i++)
			{
				if (books[i] == bookToReturn && isBookCheckedOut[i])
				{
					isBookCheckedOut[i] = false;
					borrowedBooksCount--;
					Console.WriteLine($"Book '{bookToReturn}' was returned.");
					return;
				}
			}

			Console.WriteLine("Book not found or not checked out.");
		}
	}

}