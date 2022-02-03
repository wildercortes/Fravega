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
    [Route("v{version:apiVersion}/Sucursales")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BranchOfficeController : Controller
    {
        private readonly IBranchOfficeRepository branchOfficeRepository;

        public BranchOfficeController(IBranchOfficeRepository branchOfficeRepository)
        {
            this.branchOfficeRepository = branchOfficeRepository;
        }

        [HttpGet("ObtenerTodasLasSucursales")]
        public List<BranchOfficeDTO> GetAllBranchOffice()
        {
            var branchOffices = branchOfficeRepository.GetAllBranchOffice().Select(x => new BranchOfficeDTO(x)).ToList();

            return branchOffices;
        }


        [HttpGet("ObtenerSucursalPorId")]
        public BranchOfficeDTO GetBranchOfficeById([FromQuery] GetBranchOfficeByIdRequest request)
        {
            if (UseFull.IsInvalidId(request.Id))
                throw new BusinessException("Id no puede ser 0.", BusinessExceptionCode.RequireId);

            var Entity = branchOfficeRepository.GetBranchOfficeById(request.Id);

            if (Entity == null)
                throw new BusinessException("Sucursal no existe.", BusinessExceptionCode.BranchOfficeNotExist);

            return UseFull.BecomeEntityIntoDTO(Entity);
        }

        [HttpGet("ObtenerSucursalMasCercana")]
        public BranchOfficeDTO GetBranchOfficeByLatAndLog([FromQuery] GetBranchOfficeByLatAndLogRequest request)
        {
            if (UseFull.ValidateLatitude(request.Latitud))
                throw new BusinessException("La latitud debe ser un numero mayor que -90 o menor que 90.", BusinessExceptionCode.LatInvalid);

            if (UseFull.ValidateLongitude(request.Longitud))
                throw new BusinessException("La longitud debe ser un numero mayor que -180 o menor que 180", BusinessExceptionCode.LongInvalid);

            var branchOffices = branchOfficeRepository.GetAllBranchOffice();

            var nearestBranchOffice = GetNearestBranchOffice(branchOffices, request);

            return UseFull.BecomeEntityIntoDTO(nearestBranchOffice);
        }


        [HttpPost("CrearSucursal")]
        public IActionResult AddBranchOffice([FromBody] AddBranchOfficeRequest request)
        {
            if (string.IsNullOrEmpty(request.Direccion))
                throw new BusinessException("Direccion requerida.", BusinessExceptionCode.AddressRequired);

            if (UseFull.ValidateLatitude(request.Latitud))
                throw new BusinessException("La latitud debe ser un numero mayor que -90 o menor que 90.", BusinessExceptionCode.LatInvalid);


            if (UseFull.ValidateLongitude(request.Longitud))
                throw new BusinessException("La longitud debe ser un numero mayor que -180 o menor que 180", BusinessExceptionCode.LongInvalid);

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

            var branchOfficeDistance = BranchOfficeDistanceList.OrderBy(x => x.Distance).First();

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
