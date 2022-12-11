using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class FileUploadWithExtension
    {
        public string FileName { get; set; }
        public Guid Id { get; set; }
        public string Extension { get; set; }
    }
}
