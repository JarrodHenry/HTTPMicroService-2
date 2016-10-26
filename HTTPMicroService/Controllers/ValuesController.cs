using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTPMicroService.Models;
using System.Web;
using Newtonsoft.Json;
using System.Text;

namespace HTTPMicroService.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        bool calibratedAngle = false;
        int startRotation = -1;
        enum Commands
        {
            Idle,
            MoveForward,
            RotateLeft,
            RotateRight,
            MoveBackwards,
            FireShell
        };

        // GET api/values
        public object Get(CCRequestModel request)
        {
            var name = new NameClass();
            name.name = "Bob";
            return name;
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
        public object Post([FromBody]CCRequestModel request)
        {
            string response = "";

            try
            {
                //response.text = request.request;
                if (request.MsgId == 1)
                {
                    var name = new NameClass();
                    name.name = "Bob";

                    return name;

                }
                if (request.MsgId == 2)
                {
                    var radarResult = JsonConvert.DeserializeObject<dynamic>(request.MsgData);

                    var myLocation = radarResult.yourLocation;
                    var enemyLocation = radarResult.enemies[0].location;
       
                                 
                    var commandList = new List<CommandClass>();
                    var command = new CommandClass();

                    if ( Math.Abs(FindRotationalToEnemy(radarResult)) > 15) 
                    {
                        BuildCommandList(ref commandList, (int)Commands.RotateLeft, 100);
                    }
                    else
                    {
                        BuildCommandList(ref commandList, (int)Commands.FireShell, 200);
                        BuildCommandList(ref commandList, (int)Commands.FireShell, 200);
                        BuildCommandList(ref commandList, (int)Commands.FireShell, 200);
                        //   BuildCommandList(ref commandList, (int)Commands.RotateRight, 200);
                        if (FindDistance(radarResult) > 20)
                        {
                            BuildCommandList(ref commandList, (int)Commands.MoveForward, 500);
                        }
                        else
                        {
                            BuildCommandList(ref commandList, (int)Commands.MoveBackwards, 600);
                        }
                    }
                    return commandList;

                }
            }
            catch (Exception ex)
            {
                return ex;
            }
            return response;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        private void BuildCommandList(ref List<CommandClass> cmdList, int command, int duration)
        {
            var newcommand = new CommandClass();
            newcommand.commandType = command;
            newcommand.durationInMillis = duration;
            cmdList.Add(newcommand);
        }

        private double FindRotationalToEnemy(dynamic radarResult)
        {
            var myLocation = radarResult.yourLocation;
            var enemyLocation = radarResult.enemies[0].location;
            var myRotation = radarResult.yourRotation;
            var deltaX = Convert.ToDouble(myLocation.x) - Convert.ToDouble(enemyLocation.x);
            var deltaY = Convert.ToDouble(myLocation.y) - Convert.ToDouble(enemyLocation.y);

            var angleInDegrees = (Math.Atan2(deltaY, deltaX) * (180 / Math.PI));
            myRotation = (Convert.ToDouble(myRotation) + 180) % 360;
            angleInDegrees = (angleInDegrees + 360) % 360;
            return myRotation-angleInDegrees;
        }

        private double FindDistance(dynamic radarResult)
        {
            var myLocation = radarResult.yourLocation;
            var enemyLocation = radarResult.enemies[0].location;
            var myRotation = radarResult.yourRotation;
            var deltaX = Convert.ToDouble(myLocation.x) - Convert.ToDouble(enemyLocation.x);
            var deltaY = Convert.ToDouble(myLocation.y) - Convert.ToDouble(enemyLocation.y);

            var distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            return distance;
        }
    }
}
