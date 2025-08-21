using PropertySearch.Business.Filter;
using PropertySearch.Business.Models.DTOs;
using PropertySearch.Business.Models.DTOs.PageSort;
using PropertySearch.Business.Models.RMs.PageSort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySearch.Business.Services.Interfaces
{
    public interface ISpaceService
    {
        Task<PagedResultDTO<SpaceDTO>> ListAsync(PagedRM<SpaceFilter> request);
        Task<double> GetAvgSpaceSize();
    }
}
