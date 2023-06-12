using LinkDevTask.Application.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.ViewModels.Job
{
    public class PageVM
    {
        public int start { get; set; } = 0;
        public int length { get; set; } = General.PageSize;
    }
}
