using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.ViewModels.Account
{
    public class LogInVM
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
