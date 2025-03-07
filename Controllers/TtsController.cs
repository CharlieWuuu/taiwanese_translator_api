using Microsoft.AspNetCore.Mvc;
using taiwanese_translator_api.Models;

namespace taiwanese_translator_api.Controllers
{
	[ApiController]
	[Route("tts")] // API 路徑變成 "/tts"
	public class TtsController : ControllerBase
	{
		private readonly HttpClient _httpClient;

		public TtsController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		[HttpPost]
		public async Task<IActionResult> GetTtsResult([FromBody] TtsRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Text))
			{
				return BadRequest("Text cannot be empty.");
			}

			// 編碼 URL，確保特殊字元不會破壞請求
			string apiUrl = $"http://tts001.iptcloud.net:8804/html_taigi_zh_tw?text0={Uri.EscapeDataString(request.Text)}";

			try
			{
				var response = await _httpClient.GetStringAsync(apiUrl);
				return Ok(new { result = response }); // 回傳 API 結果
			}
			catch (HttpRequestException ex)
			{
				return StatusCode(500, $"Error calling external API: {ex.Message}");
			}
		}
	}
}
