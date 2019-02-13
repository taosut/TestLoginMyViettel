using System.Threading;
using System.Threading.Tasks;
using TestLoginMyViettel.Models;

namespace TestLoginMyViettel.Services
{
    public interface IMyViettelServices
    {
        /// <summary>
        ///     Đăng nhập hệ thống MyViettel
        /// </summary>
        /// <param name="user">Số điện thoại/Account</param>
        /// <param name="pwd">Mật khẩu MyViettel</param>
        /// <param name="type">
        ///     Di động, Dcom: mob
        ///     Account dvu cố định: adsl
        /// </param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<LoginModel> LoginAsync(string user, string pwd, string type = "mob", CancellationToken ct = default);

        /// <summary>
        /// Lấy url chứa captcha image và giải mã
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<(string sid, string text)> GetCaptchaAsync(CancellationToken ct = default);

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
        Task<PaymentResponseModel> PaymentOnlineAsync(string token, string sid, string captcha, string cardCode, string phone, int type, CancellationToken ct = default);

    }
}