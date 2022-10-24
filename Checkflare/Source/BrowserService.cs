using Microsoft.Playwright;

namespace Checkflare;

public class BrowserService : IBrowserService
{
	private ILogger<BrowserService> logger;
	private IPlaywright playwright;
	private IBrowser browser;
	private IPage page;

	private List<ScraperTask> tasks = new List<ScraperTask>();

	public BrowserService(ILogger<BrowserService> logger)
	{
		this.logger = logger;
		logger.LogInformation("Browser service created");
		playwright = Playwright.CreateAsync().Result;
		browser = playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions() {Headless = false}).Result;
		page = browser.NewPageAsync().Result;
	}

	public string GotoPage(string url, int delay)
	{
		page.GotoAsync(url).Wait();
		Thread.Sleep(delay);
		return page.ContentAsync().Result;
	}

	public void AddTask(ScraperTask task)
	{
		tasks.Add(task);
		TaskListUpdateEvent();
	}

	private void TaskListUpdateEvent()
	{
		
	}

}