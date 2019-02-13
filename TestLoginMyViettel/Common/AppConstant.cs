using System;
using System.Collections.Generic;

namespace TestLoginMyViettel.Common
{
    public class AppConstant
    {
        /// <summary>
        ///     API cổng ứng dụng MyViettel
        /// </summary>
        public const string ApiMyViettel = "https://apivtp.vietteltelecom.vn:6768";

        /// <summary>
        ///     Url: https://viettel.vn
        /// </summary>
        public const string ViettelUrl = "https://viettel.vn";

        public static string UserAgentMyViettel
        {
            get
            {
                var lstUa = new List<string>
                {
                    "My Viettel/3.13.2 (iPhone; iOS 12.1.4; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 12.1.3; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 12.1.2; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 12.1.1; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 12.1.1; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 12.1; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 12.0; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 11.4; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 11.3; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 11.2; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 11.1; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 11.0; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 10.3; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 10.2; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 10.1; Scale/3.00)",
                    "My Viettel/3.13.2 (iPhone; iOS 10.0; Scale/3.00)",
                };
                var rnd = new Random().Next(lstUa.Count);
                return lstUa[rnd];
            }
        }

        public static string DeviceName
        {
            get
            {
                var lstDevice = new List<string>
                {
                    "iPhone11,8", "iPhone11,6", "iPhone11,2",
                    "iPhone10,6", "iPhone10,4", "iPhone10,2",
                    "iPhone9,3", "iPhone9,2",
                    "iPhone8,4", "iPhone8,2", "iPhone8,1",
                    "iPhone7,2", "iPhone7,1"
                };
                var rnd = new Random().Next(lstDevice.Count);
                return lstDevice[rnd];
            }
        }
    }
}