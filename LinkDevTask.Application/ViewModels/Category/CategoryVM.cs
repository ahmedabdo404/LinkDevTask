using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.ViewModels.Category
{
    public class CategoryVM
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public int JobsInCategoryCount { get; set; }
    }
}
