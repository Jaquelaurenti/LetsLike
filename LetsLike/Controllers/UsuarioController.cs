using AutoMapper;
using LetsLike.Data;
using LetsLike.DTO;
using LetsLike.Interfaces;
using LetsLike.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsLike.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly LetsLikeContext _context;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper, LetsLikeContext context)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UsuarioDto> Post([FromQuery] UsuarioDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

        }
    }
}
