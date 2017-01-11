namespace godfather.dal
{
	using System.Data.Entity;
	using Implementations.Models;

	public class Context : DbContext, IContext
	{
		public DbSet<HtmlDalModel> Htmls { get; set; }

		public DbSet<UrlDalModel> Urls { get; set; }
	}
}