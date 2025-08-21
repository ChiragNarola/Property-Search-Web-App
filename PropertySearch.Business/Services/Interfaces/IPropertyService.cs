using PropertySearch.Business.Filter;
using PropertySearch.Business.Models.DTOs;
using PropertySearch.Business.Models.DTOs.PageSort;
using PropertySearch.Business.Models.RMs;
using PropertySearch.Business.Models.RMs.PageSort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySearch.Business.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<PagedResultDTO<PropertyDTO>> ListAsync(PagedRM<PropertyFilter> request);
        Task<PropertyDTO> GetByIdAsync(int id);
        Task<PropertyDTO> CreateAsync(CreatePropertyRequest request);
    }
}
