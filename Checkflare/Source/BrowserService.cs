using Microsoft.Playwright;

namespace Checkflare;

public class BrowserService : IBrowserService
{
	private ILogger<BrowserService> logger;
	private IPlaywright playwright;
	private IBrowser browser;
	private IPage page;

	private Dictionary<Guid, ScraperTask> tasks = new Dictionary<Guid, ScraperTask>();

	public BrowserService(ILogger<BrowserService> logger)
	{
		this.logger = logger;
		logger.LogInformation("Browser service created");
		playwright = Playwright.CreateAsync().Result;
		browser = playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions() {Headless = false}).Result;
		page = browser.NewPageAsync().Result;

		Task.Run(async () =>
		{
			while (true)
			{
				await Loop();
				Thread.Sleep(100);
			}
		});
	}

	public string GotoPage(string url, int delay)
	{
		page.GotoAsync(url).Wait();
		Thread.Sleep(delay);
		return page.ContentAsync().Result;
	}

	public void AddTask(ScraperTask task)
	{
		tasks.Add(task.Guid, task);
	}

	public ScraperTask? GetTask(Guid guid)
	{
		tasks.TryGetValue(guid, out ScraperTask? task);
		return task;
	}

	private async Task Loop()
	{
		// If all tasks are not status 0, then they are all completed (200 for ok, 404 for not found, etc)
		if (tasks.All(t => t.Value.Status != 0))
			return;

		ScraperTask task = tasks.First(t => t.Value.Status == 0).Value;
		
		IResponse response = (await page.GotoAsync(task.Url))!;
		Thread.Sleep(2500);

		task.Status = response.Status;
		task.Html = await page.ContentAsync();
		task.CompletionTime = DateTime.Now;
	}

}