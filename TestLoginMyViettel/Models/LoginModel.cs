using Mcsoft.Core.Json;
using Newtonsoft.Json;

namespace TestLoginMyViettel.Models
{
    public class LoginModel : ViettelModelBase
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public LoginModelData Data { get; set; }
    }

    public class LoginModelData
    {
        [JsonProperty("errorCode")]
        public long ErrorCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public LoginDataData LoginData { get; set; }
    }

    public class LoginDataData
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("keyRefresh")]
        public string KeyRefresh { get; set; }

        [JsonProperty("isChargePasswordSet")]
        public long IsChargePasswordSet { get; set; }

        [JsonProperty("telType")]
        public object TelType { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("is_security")]
        public long IsSecurity { get; set; }

        [JsonProperty("isLockApp")]
        public long IsLockApp { get; set; }

        [JsonProperty("survey")]
        public long Survey { get; set; }

        [JsonProperty("user_type", NullValueHandling = NullValueHandling.Ignore)]
        public UserType UserType { get; set; }

        [JsonProperty("serviceType")]
        public string ServiceType { get; set; }

        [JsonProperty("contract_id")]
        public string ContractId { get; set; }

        [JsonProperty("sub_id")]
        public string SubId { get; set; }

        [JsonProperty("cusId")]
        public string CusId { get; set; }

        [JsonProperty("contractPhone")]
        public string ContractPhone { get; set; }

        [JsonProperty("productCode")]
        public string ProductCode { get; set; }

        [JsonProperty("user_type_name")]
        public string UserTypeName { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("cmnd_date")]
        public string CmndDate { get; set; }

        [JsonProperty("cmnd_place")]
        public string CmndPlace { get; set; }

        [JsonProperty("subscriber_classId")]
        public string SubscriberClassId { get; set; }

        [JsonProperty("subscriber_className")]
        public string SubscriberClassName { get; set; }

        [JsonProperty("subscriber_isdn")]
        public string SubscriberIsdn { get; set; }

        [JsonProperty("pointRate_pri")]
        public object PointRatePri { get; set; }

        [JsonProperty("pointExchange_pri")]
        public object PointExchangePri { get; set; }

        [JsonProperty("subName_pri")]
        public object SubNamePri { get; set; }

        [JsonProperty("birthDay_pri")]
        public object BirthDayPri { get; set; }

        [JsonProperty("startDate_pri")]
        public object StartDatePri { get; set; }

        [JsonProperty("endDate_pri")]
        public object EndDatePri { get; set; }

        [JsonProperty("is_privilege")]
        public long IsPrivilege { get; set; }

        [JsonProperty("is_member")]
        public long IsMember { get; set; }

        [JsonProperty("adminPrivilege")]
        public string AdminPrivilege { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("is_viettel_user")]
        public long IsViettelUser { get; set; }

        [JsonProperty("contactNo")]
        public string ContactNo { get; set; }

        [JsonProperty("need_confirm_device")]
        public bool NeedConfirmDevice { get; set; }
    }

    public class UserType
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("type_name")]
        public string TypeName { get; set; }

        [JsonProperty("user_type")]
        public string UserTypeUserType { get; set; }

        [JsonProperty("user_type_name")]
        public string UserTypeName { get; set; }
    }
}