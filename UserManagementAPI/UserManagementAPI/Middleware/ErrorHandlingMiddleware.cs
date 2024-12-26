namespace UserManagementAPI.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context); // Call the next middleware
			}
			catch (Exception ex)
			{
				context.Response.ContentType = "application/json"; // Set content type to JSON
				context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Internal server error
				await context.Response.WriteAsync(new { error = "Internal server error" }.ToString()); // Return error message
				Console.WriteLine($"Exception: {ex.Message}"); // Log the exception
			}
		}
	}
}
