using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NaniTrader.Fyers
{
    public class CreateFyersCredentialsDto
    {
        public string AppId { get; set; }
        public string SecretId { get; set; }
    }
}
