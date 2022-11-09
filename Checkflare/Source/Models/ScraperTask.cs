namespace Checkflare.Models;

public class ScraperTask
{

	public ScraperTask(string url)
	{
		Guid = Guid.NewGuid();
		Url = url;
	}
	
	public Guid Guid { get; set; }
	public string Url { get; set; }

	public int Status { get; set; }
	public DateTime CompletionTime { get; set; }
	public string? Html { get; set; }
}