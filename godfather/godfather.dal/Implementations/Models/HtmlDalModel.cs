namespace godfather.dal.Implementations.Models
{
	public class HtmlDalModel
	{
		public int Id { get; set; }

		public string Html { get; set; }

		public string FileTimeUtc { get; set; }

		public int UrlId { get; set; }

		public virtual UrlDalModel Url { get; set; }
	}
}
