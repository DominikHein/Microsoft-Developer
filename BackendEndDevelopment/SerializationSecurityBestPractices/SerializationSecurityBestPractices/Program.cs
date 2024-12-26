using System.Text;
using System.Text.Json;

namespace SerializationSecurityBestPractices
{
	internal class Program
	{
		static void Main(string[] args)
		{
			User user = new User
			{
				Name = "John Doe",
				Email = "jd@test.com",
				Password = "password123" //Risk Password is in plain text
			};

			User deserializedUser = new User();

			Console.WriteLine(user.SerializeUserData(user));

			deserializedUser = user.DeserializeUserData(user.SerializeUserData(user), true);

			if (deserializedUser != null)
				Console.WriteLine(deserializedUser.Name);
		}

		/*
			Declare a User class with properties Name, Email, and Password.

			Create a SerializeUserData method that converts a User object to a JSON string.

			In the Main method, create a User object and pass it to SerializeUserData.

			Print the serialized JSON data to observe what information is exposed.

			Document any potential risks (e.g., if sensitive data like Password is exposed in plain text).
		 */

		public class User
		{
			public string Name { get; set; }
			public string Email { get; set; }
			public string Password { get; set; }

			public string SerializeUserData(User user)
			{
				/*Now, you’ll add input validation to secure the serialization process and prevent deserialization attacks.

				Steps:

				Modify the User class to include validation attributes for each property, ensuring data follows the expected format.

				In the SerializeUserData method, add code to validate the User object before serialization. Use data annotations or simple conditional checks.

				If any property fails validation, output a message indicating invalid data and avoid serializing the object.

				Run the application, testing both valid and invalid user input.*/

				if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) //Checking for empty data
				{
					Console.WriteLine("Invalid Data \n Serialization aborted");
					return string.Empty;
				}
				else
				{
					/*
					 Steps:

					Update the User class by adding an EncryptData method that encrypts the Password property.

					Modify the SerializeUserData method to call EncryptData on the User object before serialization.

					Print the serialized JSON data, ensuring that the Password field is encrypted.

					Document the security improvement with encrypted sensitive data.
					 */


					//Improvment of Security, Lowering the risk of data exposure through encryption
					user.Password = EncryptData(user.Password); //Encrypting Password

					return JsonSerializer.Serialize(user); //Password is still in plain text, no encryption
				}
			}

			private string EncryptData(string data)
			{
				return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
			}

			/*
			 Steps:

			Create a DeserializeUserData method that takes a JSON string and converts it back into a User object.

			Add a check to verify the source of the data before deserialization. Simulate this by only allowing deserialization from trusted sources.

			Attempt to deserialize data from both trusted and untrusted sources, observing the program’s response.

			Document the importance of avoiding deserialization of untrusted data.
			 */
			public User DeserializeUserData(string json, bool trustedSource)
			{
				//Deserializing untrusted data risks code injection, tampering, DoS attacks, privilege escalation, and data leaks.
				if (trustedSource)
				{
					//If invalid printing exception
					try
					{
						var deserializedObject = JsonSerializer.Deserialize<User>(json); //Deserializing JSON data
						return deserializedObject;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						return null;
					}

				}
				else
				{
					Console.WriteLine("Untrusted source. Deserialization aborted");
					return null;
				}
			}
		}
	}
}
