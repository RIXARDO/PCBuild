using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuild_ASP.MVC_.Services.DTO;

namespace PCbuild_ASP.MVC_.Services.Interfaces
{
    public interface IPriceService
    {
        IEnumerable<PriceDTO> GetPrices();
        PriceDTO GetPriceByID(Guid guid);
        void SavePrice(PriceDTO price);
        void EditPrice(PriceDTO price);
        void DeletePrice(Guid guid);
    }
}
