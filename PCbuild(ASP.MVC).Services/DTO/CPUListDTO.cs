using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuild_ASP.MVC_.Services.DTO
{
    public class CPUListDTO
    {
        public IEnumerable<CPUdto> CPUList { get; set; }
        public PagingInfoDTO PagingInfo { get; set; }
    }
}
