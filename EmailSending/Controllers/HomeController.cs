using EmailSending.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;

using MailKit.Net.Smtp; //package namespace

namespace EmailSending.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Contact contact)
        {
            if (!ModelState.IsValid) //model state valid illena return view contact la povum
            {
                return View(contact);
            }

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(contact.From));
            email.To.Add(MailboxAddress.Parse(contact.To));
            email.Subject=$"Subject:{contact.Subject}";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = $"From:{contact.From}" + "\n" + $"To:{contact.To}" + "\n" + $"Subject:{contact.Subject}" + "\n" + $"Body:{contact.Body}"
            };

            var smtp = new SmtpClient(); //mail send pana variable create pandrom
            smtp.Connect("smtp.gmail.com",587,MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("asha.kashini@gmail.com", "lrqbuqdfurmhycfi");
            smtp.Send(email);
            smtp.Disconnect(true);

            ViewBag.Email = "A mail has been send successfully";
            return View("Privacy");

           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
