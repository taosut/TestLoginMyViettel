using Newtonsoft.Json;

namespace TestLoginMyViettel.Models
{
    /// <summary>
    ///     Model giải mã Captcha app MyViettel. Được parse từ JSON response của Tài dở.
    /// </summary>
    public class BreakingCaptchaModel
    {
        /// <summary>
        ///     Khả năng đoán chính xác mã captcha
        /// </summary>
        [JsonProperty("Probability")]
        public string Probability { get; set; }

        /// <summary>
        ///     Text detect được từ image
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}