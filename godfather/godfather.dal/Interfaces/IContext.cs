namespace godfather.dal
{
	using System.Data.Entity;
	using Implementations.Models;

	public interface IContext
	{
		DbSet<UrlDalModel> Urls { get; set; }

		DbSet<HtmlDalModel> Htmls { get; set; }
	}
}
