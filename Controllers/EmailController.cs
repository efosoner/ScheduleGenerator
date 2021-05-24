using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using ScheduleApp.Data;
using ScheduleApp.Models;
using ScheduleApp.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScheduleApp.Controllers
{
    public class EmailController : Controller
    { 
        DatabaseService dbService;
        public EmailController(DatabaseService dbService)
        {
            this.dbService = dbService;
        }

        // GET: EmailController
        [GoogleScopedAuthorize(GmailService.ScopeConstants.GmailSend, GmailService.ScopeConstants.GmailReadonly)]
        public async Task<ActionResult> Index()
        {
            List<Supervisor> supervisors = await dbService.GetSupervisors();
            ViewBag.Supervisors = supervisors;
            return View();
        }

        // POST: EmailController ==> Email/Send
        [HttpPost]
        public async Task<IActionResult> Send([FromServices] IGoogleAuthProvider auth, EmailModel emailModel)
        {
            /*if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }*/
            string emailAddress = (await dbService.GetSupervisorByID(emailModel.SupervisorID)).Email;
            string emailReceiver = (await dbService.GetSupervisorByID(emailModel.SupervisorID)).ToString();
            string emailTitle = emailModel.Title;
            string emailText = emailModel.MailText;

            var builder = new BodyBuilder();
            if (emailModel.Files != null)
            {
                foreach (var attachment in emailModel.Files)
                {
                    using (var ms = new MemoryStream())
                    {
                        await attachment.CopyToAsync(ms);
                        var array = ms.ToArray();
                        builder.Attachments.Add(Path.GetFileName(attachment.FileName), array);
                    }
                }
            }
            
            builder.TextBody = emailText;
            
            GoogleCredential cred = await auth.GetCredentialAsync();
            var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = cred
            });

            var profile = await service.Users.GetProfile("me").ExecuteAsync();
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(profile.EmailAddress, profile.EmailAddress));
            mailMessage.To.Add(new MailboxAddress(emailReceiver, emailAddress));
            mailMessage.Subject = emailTitle;

            /*var body = new TextPart("plain")
            {
                Text = emailText
            };*/

            mailMessage.Body = builder.ToMessageBody();

            Message gmailMessage = new Message();

            using (var memory = new MemoryStream())
            {
                mailMessage.WriteTo(memory);
                var blob = memory.ToArray();
                gmailMessage.Raw = System.Convert.ToBase64String(blob);
            }

            await service.Users.Messages.Send(gmailMessage, "me").ExecuteAsync();

            return RedirectToAction("Index");
        }
    }
}
