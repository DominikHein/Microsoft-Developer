using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserManagementController : ControllerBase
	{
		private static List<User> users = new List<User>
		{
			new User(1, "John"),
			new User(2, "Jane"),
			new User(3, "Doe")
		};

		// GET /GetUser Endpoint - Get all users
		[HttpGet]
		[Route("GetUser")]
		public ActionResult<List<User>> GetUser()
		{
			try
			{
				return Ok(users); //Get all users
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}"); //Return internal server error if users if request is invalid or exception occurs
			}
		}
		// GET /GetUserById/{id} Endpoint - Get user by ID
		[HttpGet]
		[Route("GetUserById/{id}")]
		public ActionResult<User> GetUserById(int id)
		{
			try
			{
				var user = users.FirstOrDefault(x => x.Id == id); //Find user by ID
				if (user == null)
				{
					return NotFound("User not found"); //Return not found if user doesnt exist
				}
				return Ok(user); //Return user if exists
			}
			catch (Exception ex) //If exception occurs
			{
				return StatusCode(500, $"Internal server error: {ex.Message}"); //Return internal server error
			}
		}
		// POST /AddUser Endpoint - Add a new user
		[HttpPost]
		[Route("AddUser")]
		public IActionResult AddUser([FromBody] User newUser)
		{
			try
			{
				if (newUser == null || string.IsNullOrEmpty(newUser.Name)) //Check for valid user data
				{
					return BadRequest("Invalid user data."); //If not valid return bad request
				}

				if (users.Any(x => x.Id == newUser.Id)) //Search user by ID
				{
					return BadRequest("User already exists"); //Return Bad Request if user exists
				}

				users.Add(newUser); //If user doesnt exist, add user	
				return Ok("User added");
			}
			catch (Exception ex) //If exception 
			{
				return StatusCode(500, $"Internal server error: {ex.Message}"); //Return internal server error
			}
		}
		// PUT /UpdateUser Endpoint - Update an existing user
		[HttpPost]
		[Route("UpdateUser")]
		public IActionResult UpdateUser(int userId, [FromBody] User updatedUser)
		{
			try
			{
				if (updatedUser == null || string.IsNullOrEmpty(updatedUser.Name)) //Check if user data is valid
				{
					return BadRequest("Invalid user data."); //If not valid return bad request
				}

				var user = users.FirstOrDefault(x => x.Id == userId); //Find User by ID
				if (user == null)
				{
					return NotFound("User not found"); //Return not found if user doesnt exist
				}

				user.Name = updatedUser.Name; //Change Username
				return Ok("User updated");
			}
			catch (Exception ex) //If exception 
			{
				return StatusCode(500, $"Internal server error: {ex.Message}"); //Return internal server error
			}
		}
		// DELETE /DeleteUser Endpoint - Delete a user
		[HttpDelete]
		[Route("DeleteUser")]
		public IActionResult DeleteUser(int userId)
		{
			try
			{
				var user = users.FirstOrDefault(x => x.Id == userId); // Find user by ID
				if (user == null) // If user not found
				{
					return NotFound("User not found"); // Return not found
				}

				users.Remove(user); // Remove user from list
				return Ok("User deleted"); 
			}
			catch (Exception ex) //If exception occurs
			{
				return StatusCode(500, $"Internal server error: {ex.Message}"); //Return internal server error
			}
		}
	}
}
