using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Domain.Models
{
    public class Category : BaseEntity
    {
        public Category(string name)
        {
            Name = name;
        }
        public string Name { get; set; } = null!;
        public List<Job> Jobs { get; set; } = new();
    }
}
