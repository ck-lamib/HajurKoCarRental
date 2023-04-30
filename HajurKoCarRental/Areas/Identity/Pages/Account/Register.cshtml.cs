// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using HajurKoCarRental.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace HajurKoCarRental.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<HajurKoCarRentalUser> _signInManager;
        private readonly UserManager<HajurKoCarRentalUser> _userManager;
        private readonly IUserStore<HajurKoCarRentalUser> _userStore;
        private readonly IUserEmailStore<HajurKoCarRentalUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RegisterModel(
            UserManager<HajurKoCarRentalUser> userManager,
            IUserStore<HajurKoCarRentalUser> userStore,
            SignInManager<HajurKoCarRentalUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(250, ErrorMessage = "The {0} must be at least {2} and should not exceed {1} characters", MinimumLength = 3)]
            [Display(Name = "Name")]
            public string Name { get; set; }



            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


            [Required]
            [StringLength(250, ErrorMessage = "Enter your valid address", MinimumLength = 2)]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required]
            [StringLength(250, ErrorMessage = "Enter your valid Phone number", MinimumLength = 2)]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            //[StringLength(250, ErrorMessage = "The {0} must be at least {2} and should not exceed {1} characters", MinimumLength = 3)]
            //[Display(Name = "Document")]
            //public FormFile Document { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            Console.WriteLine("here1\n\n");

            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                var user = CreateUser();
                user.UserName = Input.Name;
                user.Address = Input.Address;
                user.PhoneNumber = Input.PhoneNumber;

                //if (File files != null && files.ContentLength > 0)
                //{
                //    // extract only the filename
                //    var fileName = Path.GetFileName(files.FileName);
                //    // store the file inside ~/App_Data/uploads folder
                //    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                //    files.SaveAs(path);
                //}

                // Get the uploaded file
                Console.WriteLine("here1\n\n");
                //IFormFile uploadedFile = Input.Document;
                //Console.WriteLine("here2");
                //// Check if file exists and has content
                //if (uploadedFile != null && uploadedFile.Length > 0)
                //{
                //    // Get the file extension
                //    string fileExtension = Path.GetExtension(uploadedFile.FileName).ToLower();

                //    // Check if file extension is allowed
                //    if (fileExtension == ".png" || fileExtension == ".pdf" || fileExtension == ".jpeg")
                //    {
                //        //// Get the file size in bytes
                //        //var fileSize = uploadedFile.Length;

                //        //// Check if file size is within limit
                //        //if (fileSize <= 1572864) // 1.5 MB in bytes
                //        //{
                //        //    var fileName = Path.GetFileName(uploadedFile.FileName);
                //        //    // store the file inside ~/App_Data/uploads folder
                //        //    Console.WriteLine(fileName);
                //        //    var webRootPath = _hostingEnvironment.WebRootPath;
                //        //    var path = Path.Combine(webRootPath, "uploads");
                //        //    //var path = Path.Combine(Serv.MapPath("~/App_Data/uploads"), fileName);
                //        //    uploadedFile;
                //        // File is valid, do something with it

                //        var fileName = Path.GetFileName(uploadedFile.FileName);

                //        // Get the web root path
                //        var webRootPath = _hostingEnvironment.WebRootPath;

                //        // Combine the web root path and the upload folder path
                //        var uploadPath = Path.Combine(webRootPath, "Uploads");

                //        // Create the directory if it doesn't exist
                //        if (!Directory.Exists(uploadPath))
                //        {
                //            Directory.CreateDirectory(uploadPath);
                //        }

                //        // Combine the upload folder path and the file name to get the full path
                //        var filePath = Path.Combine(uploadPath, fileName);

                //        // Copy the contents of the uploaded file to the new file
                //        using (var stream = new FileStream(filePath, FileMode.Create))
                //        {
                //            await uploadedFile.CopyToAsync(stream);
                //        }
                //    }
                //        else
                //        {
                //            // File size exceeded limit, return error message
                //            ModelState.AddModelError("file", "File size should not exceed 1.5MB.");
                //        }
                //    }
                //    else
                //    {
                //        // File type not allowed, return error message
                //        ModelState.AddModelError("file", "Only PNG, PDF, and JPEG file types are allowed.");
                //    }
                
                ////else
                ////{
                ////    // File does not exist or has no content, return error message
                ////    ModelState.AddModelError("file", "Please select a file to upload.");
                ////}


                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await SendEmailAsync(Input.Email, "Confirm your email for Hamro Car Rental", 
                        $"Your are invited to be a user of Hamro Car Rental. Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.\n Tou");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }



        private async Task<bool> SendEmailAsync(string email, string subject, string callbackLink)
        {
            try
            {
                MailMessage msg = new MailMessage();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                msg.From = new MailAddress("khattribimal06@gmail.com");
                msg.Subject = subject;
                msg.To.Add(email);
                msg.IsBodyHtml = true;
                msg.Body = callbackLink;

                client.Credentials = new NetworkCredential("khattribimal06@gmail.com", "bdrlbagnwtjfkyfh");
                client.EnableSsl = true;
                client.Send(msg);

                return true;

            }
            catch
            {
                return false;
            }
        }

        private HajurKoCarRentalUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<HajurKoCarRentalUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(HajurKoCarRentalUser)}'. " +
                    $"Ensure that '{nameof(HajurKoCarRentalUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<HajurKoCarRentalUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<HajurKoCarRentalUser>)_userStore;
        }
    }
}
