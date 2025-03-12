﻿using System.ComponentModel.DataAnnotations;

namespace Application.Common.Options;

public class EmailOptions
{
    [Required] public string MailSender { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
    [Required] public string SmtpHost { get; set; } = null!;
    [Required] public int SmtpPort { get; set; }
}