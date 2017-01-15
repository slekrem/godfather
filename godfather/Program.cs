namespace godfather
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Net.Http;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;

	class MainClass
	{
		public static void Main(string[] args)
		{
			using (var timer = new Timer(StartCrawling, null, 0, 60000)) 
			{
				Console.WriteLine("Press \'q\' to quit the sample.");
				while (Console.Read() != 'q') { }
			}
		}

		private static void StartCrawling(object state)
		{
			var urls = new[] 
			{
				"http://heise.de",
				"http://swp.de",
				"http://gibip.de"
			};

			foreach (var url in urls) 
			{
				var content = AccessTheWebAsync(url);
				content.Wait();
				if (string.IsNullOrWhiteSpace(content.Result))
					Console.WriteLine("ERROR: " + url);
				var newUrls = GetUrlsFromContent(content.Result, url);
				foreach (var newUrl in newUrls) 
				{
					Console.WriteLine(newUrl);
				}

			}
		}

		private static async Task<string> AccessTheWebAsync(string requestUri)
		{
			if (string.IsNullOrWhiteSpace(requestUri))
				throw new ArgumentNullException("requestUri");
			ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
			var content = string.Empty;
			using (var httpClient = new HttpClient() { })
				content = await httpClient.GetStringAsync(requestUri);
			return content;
		}

		private static IEnumerable<string> GetUrlsFromContent(string content, string startUrl) 
		{
			if (string.IsNullOrWhiteSpace(content))
				return new List<string>();
			
			string linkedUrl;
			var urlList = new List<string>();
			var regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");
			foreach (var match in regexLink.Matches(content))
			{
				if (!urlList.Contains(match.ToString()))
				{
					linkedUrl = GetLinkedUrl(match.ToString(), startUrl);
					urlList.Add(linkedUrl);
				}
			}
			return urlList;
		}

		private static string GetLinkedUrl(string url, string startUrl)
		{
			if (!url.Contains("http://"))
			{
				if (url.IndexOf('/', 0) != -1)
				{
					url = startUrl + url;
				}
				else
				{
					url = startUrl + "/" + url;
				}
			}
			return url;
		}
	}
}
