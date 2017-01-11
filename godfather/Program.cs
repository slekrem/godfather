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
			timer.Interval = 5000;//900000;
			timer.Enabled = true;

			Console.WriteLine("Press \'q\' to quit the sample.");
			while (Console.Read() != 'q')
			{
			}
		}

		private static void DownloadSourceCode(object source, ElapsedEventArgs e)
		{
			DownloadHttp("gibip.de");
			DownloadHttp("heise.de");
		}

		private static void DownloadHttp(string host) 
		{
			using (var client = new WebClient()
			{
				Proxy = new WebProxy("192.168.178.37:8118")
			})
			{
				var dateTime = DateTime.UtcNow;
				client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
				client.DownloadFile("http://" + host, "./" + host + "/" + dateTime.ToFileTimeUtc() + ".html");
				var content = File.ReadAllText("./" + host + "/" + dateTime.ToFileTimeUtc() + ".html");
				if (string.IsNullOrWhiteSpace(content))
					Console.WriteLine("ERROR: " + host);
				else
					Console.WriteLine("Success: " + host);
			}
		}
	}
}
