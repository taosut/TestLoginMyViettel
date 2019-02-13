using Newtonsoft.Json;

namespace TestLoginMyViettel.Models
{
    public class CaptchaModel : ViettelModelBase
    {
        /// <summary>
        ///     Data chứa captcha url và sid
        /// </summary>
        [JsonProperty("data")]
        public CaptchaViettelData CaptchaViettelData { get; set; }
    }

    public class CaptchaViettelData
    {
        /// <summary>
        ///     Captcha url
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        ///     Sid nạp thẻ
        /// </summary>
        [JsonProperty("sid")]
        public string Sid { get; set; }
    }
}