using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Services.DTO;
using PCbuild_ASP.MVC_.Services.Interfaces;

namespace PCbuild_ASP.MVC_.Services.Services
{
    public class PriceService: IPriceService
    {
        private IGenericRepository<Price> Prices { get; set; }
        private IUnitOfWork uow { get; set; }
        private IMapper mapper;

        public PriceService(IUnitOfWork unitOfWork, IGenericRepository<Price> repository)
        {
            uow = unitOfWork;
            Prices = repository;
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Price, PriceDTO>();
                cfg.CreateMap<PriceDTO, Price>();
            }));
        }

        public void DeletePrice(Guid guid)
        {
            var price = Prices.FindById(guid);
            Prices.Delete(price);
        }

        public void EditPrice(PriceDTO pricedto)
        {
            Price price = mapper.Map<PriceDTO, Price>(pricedto);
            Prices.Update(price);
        }

        public PriceDTO GetPriceByID(Guid guid)
        {
            Price price = Prices.FindById(guid);
            PriceDTO pricedto = mapper.Map<Price, PriceDTO>(price);
            return pricedto;
        }

        public IEnumerable<PriceDTO> GetPrices()
        {
            return mapper.Map<IEnumerable<Price>, IEnumerable<PriceDTO>>(Prices.Get());
        }

        public void SavePrice(PriceDTO pricedto)
        {
            Price price = mapper.Map<PriceDTO, Price>(pricedto);
            Prices.Create(price);
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    uow.Save();
                    uow.Dispose();
                }
                disposed = true;
            }
        }
    }
}
