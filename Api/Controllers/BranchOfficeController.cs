using Core.Exceptions;
using Core.Models;
using Core.Request;
using Core.Static;
using Data.DTOs;
using Data.Interfaces;
using Entities;
using Geolocation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    [Route("v{version:apiVersion}/BranchOffice")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BranchOfficeController : Controller
    {
        private readonly IBranchOfficeRepository branchOfficeRepository;

        public BranchOfficeController(IBranchOfficeRepository branchOfficeRepository)
        {
            this.branchOfficeRepository = branchOfficeRepository;
        }


        [HttpGet("getBranchOfficeById")]
        public BranchOfficeDTO GetBranchOfficeById([FromQuery] GetBranchOfficeByIdRequest request)
        {
            if (UseFull.IsInvalidId(request.Id))
                throw new BusinessException("Id no puede ser 0.", BusinessExceptionCode.RequireId);

            var Entity = branchOfficeRepository.GetBranchOfficeById(request.Id);

            if (Entity == null)
                throw new BusinessException("Sucursal no existe.", BusinessExceptionCode.BranchOfficeNotExist);

            return UseFull.BecomeEntityIntoDTO(Entity);
        }

        [HttpGet("getBranchOfficeByLatAndLog")]
        public BranchOfficeDTO GetBranchOfficeByLatAndLog([FromQuery] GetBranchOfficeByLatAndLogRequest request)
        {
            if (UseFull.IsInvalidLongOrLat(request.Longitud, request.Latitud))
                throw new BusinessException("Longitud o latitud invalidos.", BusinessExceptionCode.LongOrLatInvalid);

            var branchOffices = branchOfficeRepository.GetAllBranchOffice();

            var nearestBranchOffice = GetNearestBranchOffice(branchOffices, request);

            return UseFull.BecomeEntityIntoDTO(nearestBranchOffice);
        }


        [HttpPost("addBranchOffice")]
        public IActionResult AddBranchOffice([FromBody] AddBranchOfficeRequest request)
        {
            if (string.IsNullOrEmpty(request.Direccion))
                throw new BusinessException("Direccion requerida.", BusinessExceptionCode.AddressRequired);

            if (UseFull.IsInvalidLongOrLat(request.Longitud, request.Latitud))
                throw new BusinessException("Longitud o latitud invalidos.", BusinessExceptionCode.LongOrLatInvalid);

            try
            {
                var Entity = UseFull.BecomeRequestIntoEntity(request);

                branchOfficeRepository.Add(Entity);
                return Ok();
            }
            catch (System.Exception)
            {
                throw new BusinessException("Error al crear sucursal.", BusinessExceptionCode.BranchOfficeCreationError);
            }

        }


        private BranchOffice GetNearestBranchOffice(List<BranchOffice> branchOfficeList, GetBranchOfficeByLatAndLogRequest destination)
        {
            var BranchOfficeDistanceList = GetBranchOfficeDistanceList(branchOfficeList, destination);

            var branchOfficeDistance = BranchOfficeDistanceList.OrderByDescending(x => x.Distance).First();

            var branchOffice = branchOfficeList.Where(x => x.Id == branchOfficeDistance.Id).FirstOrDefault();

            return branchOffice;
        }

        private List<BranchOfficeDistance> GetBranchOfficeDistanceList(List<BranchOffice> branchOfficeList, GetBranchOfficeByLatAndLogRequest destination)
        {
            var BranchOfficeDistanceList = new List<BranchOfficeDistance>();

            foreach (var origin in branchOfficeList)
            {
                var distance = GeoCalculator.GetDistance(origin.Latitud, origin.Longitud, destination.Latitud, destination.Longitud, 1);
                var branchOfficeDistance = new BranchOfficeDistance
                {
                    Id = origin.Id,
                    Distance = distance
                };

                BranchOfficeDistanceList.Add(branchOfficeDistance);
            }

            return BranchOfficeDistanceList;
        }
    }
}
