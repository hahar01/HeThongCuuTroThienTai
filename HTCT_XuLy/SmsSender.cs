using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class SmsSender
    {
        public void SendOtp(string toPhoneNumber, string otp)
        {
            // Mô phỏng gửi OTP 
            Console.WriteLine($"[SMS Sent] To: {toPhoneNumber}, OTP: {otp}");
        }
    }
}
