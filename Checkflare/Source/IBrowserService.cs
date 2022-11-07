namespace Checkflare;

public interface IBrowserService
{
	public string GotoPage(string url, int delay);
	void AddTask(ScraperTask task);
	ScraperTask? GetTask(Guid guid);
}