﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request.company
{
    public class CompanyCreateRequestModel
    {
        //public List<CompanyPropertyAddRequestModel> CompanyPropertyAddRequestModel { get; set; }
        //public string Logo { get; set; }
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyTypeId { get; set; }
        public int IlPlaka { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }

    }


}
