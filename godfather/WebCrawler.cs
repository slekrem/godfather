using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace godfather
{
	public class WebCrawler
	{
		public void Start()
		{
		}

		private async Task<string> AccessTheWebAsync(string requestUri)
		{
			if (string.IsNullOrWhiteSpace(requestUri))
				throw new ArgumentNullException("requestUri");

			var content = string.Empty;
			using (var httpClient = new HttpClient())
				content = await httpClient.GetStringAsync(requestUri);
			return content;
		}

		private static async Task RepeatActionEvery(Action action, TimeSpan interval, CancellationToken cancellationToken) 
		{
			while (true)
			{
				action();
				Task task = Task.Delay(interval, cancellationToken);

				try
				{
					await task;
				}

				catch (TaskCanceledException)
				{
					return;
				}
			}
		}

		public static WebCrawler Create() 
		{
			return new WebCrawler();
		}
	}
}
