using System;
using Newtonsoft.Json;

namespace TestLoginMyViettel.Models
{
    /// <summary>
    ///     Kết quả nạp cước Viettel trả về
    /// </summary>
    public class PaymentResponseModel : ViettelModelBase
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("cardCode")]
        public string CardCode { get; set; }

        [JsonProperty("cardTrueValue")]
        public int? CardTrueValue { get; set; }

        [JsonProperty("transactionTime")]
        public string TransactionTime => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");

        /*/// <summary>
        ///     Dữ liệu trả về khi nạp thẻ. Thường thấy Viettel response về mảng null.
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }*/
    }
}