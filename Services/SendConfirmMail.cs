using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace cotoiday_admin.Services
{
    public class SendConfirmMail
    {
        public static bool SendEmail(string confirmLink, string userEmail)
        {
            bool success = false;
            var fromAddress = new MailAddress("cntvnow@gmail.com", "Cotoiday.vn");
            var toAddress = new MailAddress(userEmail);
            const string fromPassword = "040687040687";
            const string subject = "Vui lòng xác nhận tài khoản";
            string body = string.Format("Nếu sau 24h mà không kích hoạt tài khoản, bạn sẽ phải tạo lại tài khoản.<br><a href=\"{0}\">Hãy click link này để kích hoạt tài khoản</a>",
                                                         confirmLink);
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            })
            {
                smtp.Send(message);
                success = true;
            }
            return success;
        }
    }
}