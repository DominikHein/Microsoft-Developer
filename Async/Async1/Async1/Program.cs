namespace Async1
{
	internal class Program
	{
		public static async Task Main(string[] args)
		{
			await DownloadDataAsync();
			Console.WriteLine("Main method completed.");
		}

		public static async Task DownloadDataAsync()
		{

			try
			{
				Console.WriteLine("Download started...");
				throw new InvalidOperationException("Simulated download error.");
				await Task.Delay(3000);
				Console.WriteLine("Download completed.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occurred: " + ex.Message);
			}

		}
	}
}
