using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwitchgearInfo.Models;
using SwitchgearInfo.Models.XML;

namespace SwitchgearInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointDataQController : ControllerBase
    {
        // GET: api/PointDataQ
        [HttpGet]
        public IEnumerable<SGSPointData> Get()
        {
            return SGSPointData.GetPointDataQ();
        }

        // GET: api/PointDataQ/5
        [HttpGet("{id}", Name = "GetQ")]
        public IEnumerable<SGSPointData> GetQ(int id)
        {
            var opd = new OnePointData { Id = id };
            return SGSPointData.GetPointData(opd);
        }

        [HttpPost("{id}", Name = "PostQ")]
        public IEnumerable<SGSPointData> PostQ([FromBody] OnePointData onePointData, long id)
        {
            onePointData.Id = id;
            return SGSPointData.GetPointData(onePointData);
        }

        [HttpPost]
        public IEnumerable<SGSPointData> Post([FromBody] ManyPointsData manyPointsData)
        {

            return SGSPointData.GetPointData(manyPointsData);
        }

        // PUT: api/PointDataQ   
        [HttpPut]
        public List<Messadge> Put([FromBody]IEnumerable<SGSPointData> SGSPointDatas)
        {
            return Messadge.Put(SGSPointDatas);
        }
    }
}
