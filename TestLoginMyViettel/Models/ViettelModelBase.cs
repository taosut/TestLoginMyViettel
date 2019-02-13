using Mcsoft.Core.Json;
using Newtonsoft.Json;

namespace TestLoginMyViettel.Models
{
    public class ViettelModelBase
    {
        /// <summary>
        ///     Mã lỗi
        /// </summary>
        [JsonProperty("errorCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ErrorCode { get; set; }

        /// <summary>
        ///     Mô tả mã lỗi
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}