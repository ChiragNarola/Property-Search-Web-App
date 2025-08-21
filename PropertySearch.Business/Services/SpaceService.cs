using AutoMapper;
using ITB.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using PropertySearch.Business.Filter;
using PropertySearch.Business.Models.DTOs;
using PropertySearch.Business.Models.DTOs.PageSort;
using PropertySearch.Business.Models.RMs.PageSort;
using PropertySearch.Business.Services.Interfaces;
using PropertySearch.Data;
using PropertySearch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySearch.Business.Services
{
    public class SpaceService: ISpaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SpaceService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResultDTO<SpaceDTO>> ListAsync(PagedRM<SpaceFilter> request)
        {
            var qry = _unitOfWork.Repository<Space>().Query()
            .Include(x => x.Property)
            .Select(x => x);

            if (request.RequestData != null)
            {
                if (request.RequestData.PropertyId > 0)
                    qry = qry.Where(c => c.PropertyId == request.RequestData.PropertyId);

                if (!string.IsNullOrEmpty(request.RequestData.Type))
                    qry = qry.Where(c => c.Type.ToLower().Contains(request.RequestData.Type.ToLower()));

                if (request.RequestData.MinSize > 0)
                    qry = qry.Where(c => c.Size >= request.RequestData.MinSize);
            }

            var res = await qry.ToPagedResultAsync(request, _mapper.Map<List<SpaceDTO>>);

            return res;
        }
        public async Task<double> GetAvgSpaceSize()
        {
            var qry = await _unitOfWork.Repository<Space>().Query().AverageAsync(x=>x.Size);
            return qry;
        }
        public async Task<int> GetTotalSpace()
        {
            var qry = await _unitOfWork.Repository<Space>().Query().CountAsync();
            return qry;
        }

    }
}
