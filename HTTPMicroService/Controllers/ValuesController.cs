using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTPMicroService.Models;

namespace HTTPMicroService.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public CCResponseModel Get()
        {
            return new Models.CCResponseModel();
        }

        //// GET api/values/5
        //public CCResponseModel Get(CCRequestModel request)
        //{
        //    return new CCResponseModel();
        //}


        // POST api/values
        public void Post([FromBody]CCRequestModel request)
        {
            
            var x = 1;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
