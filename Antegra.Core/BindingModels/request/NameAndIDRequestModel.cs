﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request
{
    public class NameAndIdRequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
