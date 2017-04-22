using FuncPlot.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FuncPlot.Controllers
{
    public class GraphController : ApiController
    { public GraphCreator Create;
        public IHttpActionResult Post(string function)
        {
            Create = new GraphCreator();
            List<Point> xycoord = new List<Point>();
            xycoord = Create.GetCoordinates(function);
            return Ok(xycoord);
        }
    }
}
