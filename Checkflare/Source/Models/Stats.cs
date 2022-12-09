namespace Checkflare.Models;

public class Stats
{
	public Stats(List<ScraperTask> tasks)
	{
		Tasks = tasks;
		TaskCount = tasks.Count;
	}
	
	public int TaskCount { get; }
	public List<ScraperTask> Tasks { get; }
	
}