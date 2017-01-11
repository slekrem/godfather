namespace godfather
{
	using System;
	using System.IO;
	using System.Net;
	using System.Timers;

	class MainClass
	{
		public static void Main(string[] args)
		{
			var timer = new Timer();
			timer.Elapsed += DownloadSourceCode;
			timer.Interval = 900000;
			timer.Enabled = true;

			Console.WriteLine("Press \'q\' to quit the sample.");
			while (Console.Read() != 'q')
			{
			}
		}

		private static void DownloadSourceCode(object source, ElapsedEventArgs e)
		{
			using (var client = new WebClient() 
			{
				Proxy = new WebProxy("192.168.178.37:8118")
			})
			{
				var dateTime = DateTime.UtcNow;

				client.DownloadFile("http://gibip.de", "./gibip.de/" + dateTime.ToFileTimeUtc() + ".html");
				var ipAddress = File.ReadAllLines("./gibip.de/" + dateTime.ToFileTimeUtc() + ".html");
				Console.WriteLine(dateTime.ToLongTimeString() + ": " + ipAddress[0]);

				client.DownloadFile("http://heise.de", "./heise.de/" + dateTime.ToFileTimeUtc() + ".html");
				//var srcCode = File.ReadAllText("./heise.de/" + dateTime.ToFileTimeUtc() + ".html");
				//Console.WriteLine(srcCode);
			}
		}
	}
}
