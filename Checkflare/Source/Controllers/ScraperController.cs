using Microsoft.AspNetCore.Mvc;

namespace Checkflare.Controllers;

[ApiController]
[Route("[controller]")]
public class ScraperController : ControllerBase
{
	private readonly IBrowserService browserService;

	public ScraperController(IBrowserService browserService)
	{
		this.browserService = browserService;
	}

	/// <summary>
	/// Hello world
	/// </summary>
	/// <param name="url">URL to read HTML from</param>
	/// <param name="delay">Time to wait after elements are loaded before returning the HTML, in milliseconds</param>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Get(string url, int delay)
	{
		try
		{
			string s = browserService.GotoPage(url, delay);
			
			return Ok(new ScrapeResult
			{
				html = s
			});
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return BadRequest("Nope!");
		}
	}

	/// <summary>
	/// Submits a new task to scrape the requested webpage
	/// </summary>
	/// <param name="url">The URL of the webpage to extract HTML data from</param>
	/// <returns>A token</returns>
	[HttpPost]
	[Route("[action]")]
	public IActionResult SubmitTask(string url)
	{
		
		return Ok(Guid.NewGuid());
	}
}