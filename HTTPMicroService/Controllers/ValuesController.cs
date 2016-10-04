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
        public CCResponseModel Get(CCRequestModel request)
        {
            return new Models.CCResponseModel(5, "Testing", 'y', true);
        }

        ////// GET api/values/5
        //public CCResponseModel Get(CCRequestModel request)
        //{
        //    return new CCResponseModel(5,"Testing", 'y', true);
        //}


        // POST api/values
        /// <summary>
        /// Post a request to the service and generate a response
        /// </summary>
        /// <param name="request">A string containing the request</param>
        /// <returns></returns>
        public CCResponseModel Post([FromBody]CCRequestModel request)
        {
            var response = new CCResponseModel();
            try
            {
                response.text = request.request;
                response.number = request.request.Length;
                response.boolresult = ((request.request.Length % 2) == 1);
                response.letter = request.request[0];
            }
            catch (Exception ex)
            {
                response.text = $"Error on response: {ex.Message}";
            }
            return response;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
