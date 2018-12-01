using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bnaas.Controllers
{
    [Route("/")]
    public class BandNamesController : Controller
    {
        private static readonly Random Random = new Random();

        private readonly List<string> _bandNames;

        private const string _htmlFormat = @"
        <html>
          <body>
            <div style=""width: 300px; margin: 0 auto; text-align: center; font-family: 'Lucida Sans Unicode', 'Lucida Grande', sans-serif"">
              <p><i>You should call your band...</i></p>
              <h1>{0}</h1>
            </div>
          </body>
        </html>";

        public BandNamesController()
        {
            _bandNames = new BandNameGetter().Get();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bandName = new BandNameResult(_bandNames[Random.Next(_bandNames.Count)].Trim());

            if (Request.Headers.ContainsKey("Accept") && Request.Headers["Accept"].FirstOrDefault().Contains("text/html"))
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = string.Format(_htmlFormat, bandName.BandName)
                };
            }

            return Ok(JsonConvert.SerializeObject(bandName));
        }
    }
}
