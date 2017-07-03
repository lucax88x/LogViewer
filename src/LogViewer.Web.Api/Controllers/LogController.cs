using System.Collections.Generic;
using System.IO;
using LogViewer.Web.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LogViewer.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ClientDataExtractor _clientDataExtractor;

        //public LogController(ClientDataExtractor clientDataExtractor)
        //{
        //    _clientDataExtractor = clientDataExtractor;
        //}

        public LogController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _clientDataExtractor = new ClientDataExtractor();
        }

        [HttpGet]
        public IEnumerable<ClientData> Get()
        {
            var logFile = Path.Combine(_hostingEnvironment.ContentRootPath, @"logs\IISLog.log");
            return _clientDataExtractor.Extract(logFile);
        }
    }
}
