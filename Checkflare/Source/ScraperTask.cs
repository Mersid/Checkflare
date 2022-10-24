namespace Checkflare;

public class ScraperTask
{

	public ScraperTask(string url)
	{
		Guid = Guid.NewGuid();
		Url = url;
	}
	
	public Guid Guid;
	public string Url;

	public int Status;
	public DateTime CompletionTime;
	public string? Html;
}