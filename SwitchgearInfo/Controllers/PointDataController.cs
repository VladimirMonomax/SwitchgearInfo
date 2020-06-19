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
    public class PointDataController : ControllerBase
    {
        // GET: api/PointData
        [HttpGet]
        public IEnumerable<SGSPointData> Get()
        {
            return SGSPointData.GetPointData();
        }

        // GET: api/PointData/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<SGSPointData> Get(long id)
        {
            return SGSPointData.GetPointData(id);
        }

       

        // POST: api/PointData/5
        [HttpPost("{id}", Name ="Post")]
        public IEnumerable<SGSPointData> Post([FromBody] OnePointData onePointData, long id)
        {
            return SGSPointData.GetPointData(id, onePointData.DateFrom, onePointData.DateTo);
        }

        [HttpPost]
        public IEnumerable<SGSPointData> Post([FromBody] ManyPointsData manyPointsData)
        {
            
            return SGSPointData.GetPointData(manyPointsData.PointsId, manyPointsData.DateFrom, manyPointsData.DateTo);
        }

        // POST: api/PointData/GetPoinstData
        [HttpPost("GetPoinstData", Name = "GetPoinstData")]
        public IEnumerable<SGSPointData> GetPoinstData([FromBody] IEnumerable<long> PointsId)
        {
            return SGSPointData.GetPointData(PointsId);
        }

        // PUT: api/PointData/-1    
        [HttpPut("{id}")]
        public List<Messadge> Put([FromBody]IEnumerable<SGSPointData> SGSPointDatas)
        {            
            return SGSPointData.Put(SGSPointDatas);
        }

      
    }

    

    
   
}
