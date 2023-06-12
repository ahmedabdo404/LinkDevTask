﻿using LinkDevTask.Application.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.ViewModels.Job
{
    public class PagedJobVM
    {
        public int RecordsFiltered { get; set; }
        public object Data { get; set; } = null!;
    }
}
