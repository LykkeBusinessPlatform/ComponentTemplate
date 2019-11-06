using AutoMapper;
using Lykke.Service.LykkeService.Client;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.LykkeService.Controllers
{
    [Route("api/LykkeService")] // TODO fix route
    public class LykkeServiceController : Controller, ILykkeServiceApi
    {
        private readonly IMapper _mapper;

        public LykkeServiceController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
