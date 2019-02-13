using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mcsoft.Core.Extensions;
using Mcsoft.Core.HttpClient;
using TestLoginMyViettel.Common;
using TestLoginMyViettel.Heper;
using TestLoginMyViettel.HttpClientServices;
using TestLoginMyViettel.Models;

namespace TestLoginMyViettel.Services
{
    public class MyViettelServices : IMyViettelServices
    {
        #region Private members

        private readonly IHttpClientFactory _clientFactory;

        #endregion

        #region Constructor

        public MyViettelServices(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        #endregion

        #region Implementation of IMyViettelServices

        /// <summary>
        ///     Đăng nhập hệ thống MyViettel
        /// </summary>
        /// <param name="user">Số điện thoại/Account</param>
        /// <param name="pwd">Mật khẩu</param>
        /// <param name="type">mob/adsl</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<LoginModel> LoginAsync(string user, string pwd, string type = "mob", CancellationToken ct = default)
        {
            var error = "";
            BEGIN:
            try
            {
                var retry = 0;
                POST:
                retry++;
                if (retry == 5)
                {
                    Log4NetManager.LogToFile($"Server Viettel tạm đóng hoặc đang quá tải\r\nAccount đăng nhập: {user}");
                    return new LoginModel
                    {
                        ErrorCode = 504,
                        Message   = "Server Viettel tạm đóng hoặc đang quá tải"
                    };
                }

                const string url = "https://apivtp.vietteltelecom.vn:6768/myviettel.php/loginV2";

                // Tạo random user-agent
                var headers = new Dictionary<string, object>
                {
                    {"User-Agent", AppConstant.UserAgentMyViettel}
                };

                // Post data: content-type form-url-encoded
                var postData = new Dictionary<string, object>
                {
                    {"username", user},
                    {"password", pwd},
                    {"actionForm", type},
                    {"build_code", "2019.1.25.1"},                      // Thời gian code được đóng gói.
                    {"device_id", Guid.NewGuid().ToString().ToUpper()}, // IMEI máy
                    {"device_name", AppConstant.DeviceName},            // Tên thiết bị 
                    {"os_type", "ios"},                                 // Hệ điều hành
                    {"version_app", "3.13.2"},                          // Phiên bản app
                    {"appID", "id1014838705"}                           // ID app
                };

                var response = await _clientFactory.CreateClient.SendAsync(url, postData.AsFormUrlEncodedContent(), headers, ct).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.BadGateway || response.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    Log4NetManager.LogToFile("Hệ thống MyViettel đang bận do quá tải hoặc tạm ngưng phục vụ");
                    goto POST;
                }

                error = await response.ContentAsStringAsync().ConfigureAwait(false);
                var result = await response.ContentAsTypeAsync<LoginModel>().ConfigureAwait(false);
                return result;
            }
            catch (OperationCanceledException)
            {
                return new LoginModel {ErrorCode = 6768, Message = "(Server) Hủy yêu cầu đăng nhập MyViettel"};
            }
            catch (HttpRequestException e)
            {
                if (e.Message.Contains("An error occurred while sending the request"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Khong mo duoc ket noi, co gang dang nhap lai MyViettel");
                    goto BEGIN;
                }

                Log4NetManager.LogToFile($"Error: HttpRequestException. Lỗi đăng nhập MyViettel.\r\n{error}\r\n{e.Message} {e.InnerException?.Message}");
            }
            catch (Exception e)
            {
                Log4NetManager.LogToFile($"Error: .NET Exception. Lỗi đăng nhập MyViettel.\r\n{error}\r\n{e.Message} {e.InnerException?.Message}");
            }

            return new LoginModel {ErrorCode = 7777, Message = "(Server) Lỗi, đăng nhập MyViettel không thành công"};
        }

        public async Task<(string sid, string text)> GetCaptchaAsync(CancellationToken ct = default)
        {
            var retry = 0;
            BEGIN:
            retry++;
            try
            {
                var response       = await _clientFactory.CreateClient.SendAsync("/myviettel.php/getCaptcha", ct: ct).ConfigureAwait(false);
                var captchaViettel = await response.ContentAsTypeAsync<CaptchaModel>().ConfigureAwait(false);

                // Lấy được thông tin captcha url và sid
                if (captchaViettel.ErrorCode == 0 && captchaViettel.CaptchaViettelData != null && captchaViettel.CaptchaViettelData.Url.HasValue())
                {
                    // Gửi captcha url cho api của Tài xử lý.
                    var postData = new {url = captchaViettel.CaptchaViettelData.Url};
                    response = await _clientFactory.CreateClient.SendAsync("http://149.28.129.15:6886/gettextct", postData.AsJsonContent(), null, ct).ConfigureAwait(false);
                    var result = await response.ContentAsTypeAsync<BreakingCaptchaModel>().ConfigureAwait(false);
                    if (result != null)
                    {
                        if (result.Text == "Null")
                        {
                            retry = 0;
                            goto BEGIN;
                        }

                        return (captchaViettel.CaptchaViettelData.Sid, result.Text);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                // Ngoại lệ này xảy ra chủ yếu ở bước lấy url chứa captcha image.
                if (e.Message.Contains("An error occurred while sending the request"))
                {
                    if (retry > 20)
                        return (null, null);
                    Log4NetManager.LogToFile($"Lỗi xảy ra trong khi gửi yêu cầu lấy url captcha để xử lý mã nhận diện. Lần ghi log thứ {retry}");
                    goto BEGIN;
                }

                Log4NetManager.LogToFile($"Error: HttpRequestException. Lỗi xử lý mã captcha. {e.Message} {e.InnerException?.Message}");
            }
            catch (Exception e)
            {
                Log4NetManager.LogToFile($"Error: .NET Exception. Lỗi xử lý mã captcha. {e.Message} {e.InnerException?.Message}");
            }

            return (null, null);
        }

        /// <summary>
        ///     Nạp tiền thuê bao: cho chính mình hoặc thanh toán hộ cho thuê bao khác.
        ///     Sử dụng chung cho cả dịch vụ trả trước và trả sau Viettel.
        /// </summary>
        /// <param name="token">Token của thuê bao đã login MyViettel</param>
        /// <param name="sid">SID trong response json khi Get mã captcha Viettel</param>
        /// <param name="cardCode">Mã thẻ cào</param>
        /// <param name="phone">Thuê bao cần nạp</param>
        /// <param name="type">
        ///     Loại tài khoản gạch nợ
        ///     1. Di động, Dcom
        ///     3. PSTN
        ///     4. Dvu cố định
        /// </param>
        /// <param name="captcha">Mã captcha</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<PaymentResponseModel> PaymentOnlineAsync(string token, string sid, string captcha, string cardCode, string phone, int type,
                                                                   CancellationToken ct = default)
        {
            var error = "";
            try
            {
                var retry = 0;
                POST:
                retry++;
                if (retry == 10)
                    return new PaymentResponseModel
                    {
                        ErrorCode = 504,
                        Message   = "Hệ thống MyViettel đang bận do quá tải hoặc tạm ngưng phục vụ"
                    };

                const string url = "https://apivtp.vietteltelecom.vn:6768/myviettel.php/paymentOnlineV2";
                var postData = new Dictionary<string, object>
                {
                    {"build_code", "2019.1.25.1"},
                    {"device_name", AppConstant.DeviceName},
                    {"os_type", "ios"},
                    {"version_app", "3.13.2"},
                    {"token", token},       // Login token
                    {"sid", sid},           // SessionId để biết ảnh captcha 
                    {"captcha", captcha},   // Mã captcha đã giải được
                    {"cardcode", cardCode}, // Mã thẻ nạp
                    {"phone", phone},       // Thuê bao cần được nạp
                    {"type", type}          // Loại thuê bao được nạp: 1-Di động, 4-Tbao cố định
                };
                var response = await _clientFactory.CreateClient.SendAsync(url, postData.AsFormUrlEncodedContent(), null, ct).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.BadGateway || response.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    Log4NetManager.LogToFile($"Hệ thống MyViettel đang bận do quá tải hoặc tạm ngưng phục vụ. Lần thử thứ {retry}");
                    goto POST;
                }

                error = await response.ContentAsStringAsync().ConfigureAwait(false);
                var result = await response.ContentAsTypeAsync<PaymentResponseModel>().ConfigureAwait(false);
                result.Account  = phone;
                result.CardCode = cardCode;
                return result;
            }
            catch (OperationCanceledException)
            {
                return new PaymentResponseModel
                {
                    ErrorCode = 6769,
                    Message   = "(Server) Đã hủy yêu cầu nạp thẻ",
                    Account   = phone,
                    CardCode  = cardCode
                };
            }
            catch (HttpRequestException e)
            {
                Log4NetManager.LogToFile($"Error: HttpRequestException. Lỗi nạp tiền cho thuê bao {phone}.\r\n{error}\r\n{e.Message} {e.InnerException?.Message}");
            }
            catch (Exception e)
            {
                Log4NetManager.LogToFile($"Error: .NET Exception. Lỗi nạp tiền cho thuê bao {phone}.\r\n{error}\r\n{e.Message} {e.InnerException?.Message}");
            }

            return new PaymentResponseModel
            {
                ErrorCode = 6768,
                Message   = $"(Server) Lỗi nạp tiền cho thuê bao {phone}",
                Account   = phone,
                CardCode  = cardCode
            };
        }

        #endregion
    }
}