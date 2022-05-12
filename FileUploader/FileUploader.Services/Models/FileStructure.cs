using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace FileUploader.Services.Models
{
    [IgnoreFirst]
    [IgnoreEmptyLines]
    [DelimitedRecord("|")]
    public class FileStructure
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
