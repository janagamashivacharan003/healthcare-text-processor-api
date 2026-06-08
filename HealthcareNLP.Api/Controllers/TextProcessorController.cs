using HealthcareNLP.Api.Models;
using HealthcareNLP.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareNLP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TextProcessorController : ControllerBase
{
    private readonly ITextExtractionService _service;
    private static readonly List<ExtractionResult> _history = new();

    public TextProcessorController(ITextExtractionService service)
    {
        _service = service;
    }

    [HttpPost("extract")]
    public IActionResult Extract([FromBody] MedicalTextRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            return BadRequest("Text cannot be empty.");

        var result = _service.Extract(request);
        _history.Add(result);
        return Ok(result);
    }

    [HttpPost("summarize")]
    public IActionResult Summarize([FromBody] MedicalTextRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            return BadRequest("Text cannot be empty.");

        return Ok(new { Summary = _service.Summarize(request.Text) });
    }

    [HttpGet("history")]
    public IActionResult GetHistory()
    {
        return Ok(_history);
    }
}