using Microsoft.AspNetCore.Mvc;
using taiwanese_translator_api.Models;

namespace taiwanese_translator_api.Controllers
{
	[ApiController]
	public class TtsController : ControllerBase
	{
		private readonly HttpClient _httpClient;

		public TtsController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}


		[Route("TranslateText")] // API 路徑變成 "/TranslateText"
		[HttpPost]
		public async Task<IActionResult> TranslateText([FromBody] TtsRequest request)
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

		[Route("GetPinyin")] // API 路徑變成 "/GetPinyin"
		[HttpPost]
		public async Task<IActionResult> GetPinyin([FromBody] TtsRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Text))
			{
				return BadRequest("Text cannot be empty.");
			}

			// 編碼 URL，確保特殊字元不會破壞請求
			string apiUrl = $"http://tts001.iptcloud.net:8804/html_taigi_tw_py?text0={Uri.EscapeDataString(request.Text)}";

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

		[Route("GetAudio")] // API 路徑變成 "/GetAudio"
		[HttpPost]
		public async Task<IActionResult> GetAudio([FromBody] TtsRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Text))
			{
				return BadRequest("Text cannot be empty.");
			}

			// 確保特殊字元編碼正確
			string apiUrl = $"http://tts001.iptcloud.net:8804/synthesize_TLPA?text1={Uri.EscapeDataString(request.Text)}&gender=%E5%A5%B3%E8%81%B2&accent=%E5%BC%B7%E5%8B%A2%E8%85%94%EF%BC%88%E9%AB%98%E9%9B%84%E8%85%94%EF%BC%89";

			try
			{
				// 取得音檔 Stream
				var responseStream = await _httpClient.GetStreamAsync(apiUrl);

				// 設定檔名（可選）
				var fileName = "audio.mp3"; // 如果是 WAV 檔，可以改成 "audio.wav"

				// 回傳 audio 檔案給前端
				return File(responseStream, "audio/mpeg", fileName);
			}
			catch (HttpRequestException ex)
			{
				return StatusCode(500, $"Error calling external API: {ex.Message}");
			}
		}

	}
}
