using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.ViewModels.Job
{
    public class JobVM
    {
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Responsibilities { get; set; }
        public string? Skills { get; set; }
        public string? CategoryName { get; set; }
        public string CategoryId { get; set; } = null!;
        public DateTime ValidFrom { get; set; } = DateTime.Now;
        public DateTime ValidTo { get; set; } = DateTime.Now.AddDays(3);
        public short? MaximumApplications { get; set; }
        public byte[]? Picture { get; set; }

    }
}
