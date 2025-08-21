using AutoMapper;
using ITB.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using PropertySearch.Business.Filter;
using PropertySearch.Business.Models.DTOs;
using PropertySearch.Business.Models.DTOs.PageSort;
using PropertySearch.Business.Models.RMs;
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
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PropertyService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResultDTO<PropertyDTO>> ListAsync(PagedRM<PropertyFilter> request)
        {
            var qry = _unitOfWork.Repository<Property>().Query()
                            .Include(x => x.Spaces)
                            .Select(x => x);

            if (request.RequestData != null)
            {
                if (!string.IsNullOrEmpty(request.RequestData.Type))
                    qry = qry.Where(c => c.Type.ToLower().Contains(request.RequestData.Type.ToLower()));

                if (request.RequestData.MinPrice>0)
                    qry = qry.Where(c => c.Price >= request.RequestData.MinPrice);

                if (request.RequestData.MaxPrice>0)
                    qry = qry.Where(c => c.Price <= request.RequestData.MaxPrice);
            }

            var res = await qry.ToPagedResultAsync(request, _mapper.Map<List<PropertyDTO>>);

            return res;
        }


        public async Task<PropertyDTO> GetByIdAsync(int id)
        {
            var model = await _unitOfWork.Repository<Property>().Query()
                    .Include(x => x.Spaces)
                        .FirstOrDefaultAsync(x => x.Id == id);

            var propertyDTO = _mapper.Map<PropertyDTO>(model);

            return propertyDTO;
        }
        public async Task<PropertyDTO> CreateAsync(CreatePropertyRequest request)
        {
            #region validation
            if (string.IsNullOrWhiteSpace(request.Address))
                throw new ArgumentException("Address is required.");

            if (string.IsNullOrWhiteSpace(request.Type))
                throw new ArgumentException("Property type is required.");

            if (request.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            if (request.Spaces != null && request.Spaces.Any(s => s.Size <= 0))
                throw new ArgumentException("Each space must have a valid size.");
            #endregion
            var property = new Property
            {
                Address = request.Address,
                Type = request.Type,
                Price = request.Price,
                Description = request.Description,
                Spaces = request.Spaces?.Select(s => new Space
                {
                    Type = s.Type,
                    Size = s.Size,
                    Description = s.Description
                }).ToList()
            };

            await _unitOfWork.Repository<Property>().AddAsync(property);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<PropertyDTO>(property);
        }
        public async Task<int> GetTotalPropertyAsync()
        {
            var qry = await _unitOfWork.Repository<Property>().Query().CountAsync();
            return qry;
        }
        public async Task<decimal> GetAvgropertyAsync()
        {
            var qry = await _unitOfWork.Repository<Property>().Query().AverageAsync(x => x.Price);
            return qry;
        }
    }
}
