using System.Text.Json;
using Checkflare.Models;
using Microsoft.AspNetCore.Mvc;

namespace Checkflare.Controllers;

[ApiController]
[Route("[controller]")]
public class ScraperController : ControllerBase
{
	private readonly IBrowserService browserService;
	private readonly ILogger<ScraperController> logger;

	public ScraperController(IBrowserService browserService, ILogger<ScraperController> logger)
	{
		this.browserService = browserService;
		this.logger = logger;
	}

	/// <summary>
	/// Hello world
	/// </summary>
	/// <param name="url">URL to read HTML from</param>
	/// <param name="delay">Time to wait after elements are loaded before returning the HTML, in milliseconds</param>
	[HttpGet]
	[Obsolete("Use SubmitTask and GetResult instead, as they support concurrent usage")]
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
			logger.LogInformation("Something went wrong!");
			return BadRequest("Nope!");
		}
	}

	[HttpGet]
	[Route("[action]")]
	public IActionResult Ping()
	{
		logger.LogInformation("Ping endpoint hit!");
		return Ok("Pong!");
	}

	[HttpGet]
	[Route("[action]")]
	public IActionResult GetStats()
	{
		return Ok(browserService.GetStats());
	}

	/// <summary>
	/// Submits a new task to scrape the requested webpage
	/// </summary>
	/// <param name="url">The URL of the webpage to extract HTML data from</param>
	/// <returns>A token</returns>
	[HttpPost]
	[HttpGet]
	[Route("[action]")]
	public IActionResult SubmitTask(string url)
	{
		ScraperTask t = new ScraperTask(url);
		browserService.AddTask(t);
		
		logger.LogInformation("Added new task: {}", JsonSerializer.Serialize(t));

		return Accepted(new ResponseToken() {token = t.Guid});
	}

	[HttpGet]
	[Route("[action]/{guid:guid}")]
	public IActionResult GetResult(Guid guid)
	{
		ScraperTask? task = browserService.GetTask(guid);
		
		logger.LogInformation("Getting result for {}: {}", guid, JsonSerializer.Serialize(task));
		
		if (task is null)
			return NotFound();

		if (task.Status == 0)
			return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
		
		return Ok(task);
	}
}