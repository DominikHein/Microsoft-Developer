namespace PracticalAsync
{
	public class Program
	{
		public async Task<string> DownloadFileAsync(string fileName)
		{
			Console.WriteLine($"Starting download of {fileName}...");
			await Task.Delay(3000);
			Console.WriteLine($"Completed download of {fileName}.");
			return $"{fileName} content";
		}

		public async Task DownloadFilesAsync()
		{
			var downloadTask1 = DownloadFileAsync("File1.txt");
			var downloadTask2 = DownloadFileAsync("File2.txt");


			await Task.WhenAll(downloadTask1, downloadTask2);
			Console.WriteLine("All downloads completed.");
		}

		public static async Task Main(string[] args)
		{
			Program program = new Program();
			await program.DownloadFilesAsync();
		}
	}
}
