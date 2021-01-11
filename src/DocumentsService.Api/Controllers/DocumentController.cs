using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services = DocumentsService.Api.Services;

namespace DocumentsService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly Services.DocumentsService _service;

        public DocumentController(ILogger<DocumentController> logger, Services.DocumentsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public void Get() 
        {

        }

        [HttpPut]
        public void Put()
        {

        }

        [HttpPost]
        [Route("search")]
        public async Task<ActionResult> Search(string term)
        {
            var response = await _service.Search(term);

            return new OkObjectResult(response);
        }
    }
}