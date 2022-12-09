using Checkflare.Models;

namespace Checkflare;

public interface IBrowserService
{
	public string GotoPage(string url, int delay);
	void AddTask(ScraperTask task);
	Stats GetStats();
	ScraperTask? GetTask(Guid guid);
}