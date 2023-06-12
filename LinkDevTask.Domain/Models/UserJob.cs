using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Domain.Models
{
    public  class UserJob
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public string JobId { get; set; } = null!;
        public Job Job { get; set; } = null!;
    }
}
