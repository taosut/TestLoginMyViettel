using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mcsoft.Core.Helper;
using Ninject;
using TestLoginMyViettel.Heper;
using TestLoginMyViettel.IoC;
using TestLoginMyViettel.Models;

namespace TestLoginMyViettel
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IoCContainer.Kernel = new StandardKernel(new ConfigModules());

            Console.WriteLine("--- TEST LOGIN MYVIETTEL ---");
            
            var accountLst = new List<AccountModel>();
            var items      = await FileEx.ReadAllLinesAsync("./check_tkmyviettel.txt").ConfigureAwait(false);
            foreach (var item in items)
            {
                var split = item.Split(new[] {"\t"}, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 3)
                    accountLst.Add(new AccountModel
                    {
                        Username    = split[0],
                        Password    = split[1],
                        AccountType = split[2]
                    });
            }

            var lstFailed = new List<AccountModel>();
            while (true)
            {
                BEGIN:
                try
                {
                    var success = 0;
                    foreach (var account in accountLst)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($">> mincadev:// account {account.Username} dang login vao he thong MyViettel...");
                        var login = await IoCContainer.MyViettelServices.LoginAsync(account.Username, account.Password, account.AccountType).ConfigureAwait(false);
                        if (login.ErrorCode == 0)
                        {
                            success++;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($":: account {account.Username} - {login.Data.LoginData.FullName} login thanh cong, lan thu {success:N0}");

                            //var sw = Stopwatch.StartNew();
                            //var (sid, text) = await IoCContainer.MyViettelServices.GetCaptchaAsync().ConfigureAwait(false);
                            //sw.Stop();
                            //if (sid != null && text != null) Console.WriteLine($":: Giai ma catpcha thanh cong, ma {text}\tTime {sw.ElapsedMilliseconds / 1000:N3} seconds");
                        }
                        else
                        {
                            success                 = 0;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($":: account {account.Username} login that bai");
                            //Log4NetManager.LogToFile($"Lỗi đăng nhập, account {account.Username}. {login.Message}");
                            lstFailed.Add(account);
                        }

                        await Task.Delay(3000).ConfigureAwait(false);
                    }

                    Console.WriteLine("Vui long cho de xuat file Excel...");
                    ExcelHelper.ExportToExcelFromCollection("dstk_failed", "DSTK MYVIETTEL KHONG DANG NHAP DUOC HE THONG", lstFailed, false);
                }
                catch (Exception e)
                {
                    Log4NetManager.LogToFile($"Ngoại lệ xảy ra khi login. {e.Message} {e.InnerException?.Message}");
                    goto BEGIN;
                }
            }
        }
    }
}