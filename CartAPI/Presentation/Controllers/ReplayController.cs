using CartAPI.Application.IService;
using CartAPI.Domain.Model;
using CartAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CartAPI.Presentation.Controllers
{
    public class ReplayController : CustomBaseController
    {
        private readonly ILogService _logService;
        private readonly RequestSender _requestSender;

        public ReplayController(ILogService logService, RequestSender requestSender)
        {
            _logService = logService;
            _requestSender = requestSender;
        }

        [HttpPost("/api/request/replay"), AllowAnonymous]
        public async ValueTask<IActionResult> ReplayRequest([FromQuery] Guid requestId, [FromHeader] string username, [FromHeader] string password)
        {
            if (string.IsNullOrEmpty(username))
                return Unauthorized("username or passwrd is incorrect");

            if (string.IsNullOrEmpty(password))
                return Unauthorized("username or passwrd is incorrect");

            if (requestId.Equals(Guid.Empty))
                return ValidationProblem("requestid must not be empty");

            var requestLogs = await _logService.GetLogAsync(requestId);
            try
            {
                var response = await SendRequest(requestLogs);
            }
            catch (Exception)
            {

                throw;
            }

            return Ok();
        }

        private async Task<(string, int)> SendRequest(RequestLog requestLogs)
        {
            var (type
                ,requestdomain
                ,requestip
                ,requestbody
                ,requestheader
                ,requesturl
                ,responsebody
                ,responseheader
                ,scheme
                ,formdata
                ,routedata
                ,requestquerystring
                ,method
                ,statuscode
                ,useragent
                ,timestamp) = requestLogs;
            var test = JsonSerializer.Deserialize<dynamic>(requestheader);
            return requestLogs.Method.ToLower() switch
            {
                "post" => ((string, int))await _requestSender.PostRequestAsync(
                                        requesturl,
                                        false,
                                        string.IsNullOrEmpty(requestbody) ? null : JsonSerializer.Deserialize<dynamic>(requestbody),
                                        JsonSerializer.Deserialize<Dictionary<string, object>>(requestheader)),
                "get" => await _requestSender.GetRequestAsync(
                                        requesturl,
                                        false,
                                        JsonSerializer.Deserialize<Dictionary<string, string>>(requestquerystring),
                                        JsonSerializer.Deserialize<Dictionary<string, object>>(requestheader)),
                "put" => ((string, int))await _requestSender.PutRequestAsync(
                                        requesturl,
                                        false,
                                        JsonSerializer.Deserialize<dynamic>(requestbody),
                                        JsonSerializer.Deserialize<Dictionary<string, object>>(requestbody)),
                _ => ("", 0),
            };
        }

        [HttpGet("/api/request"), AllowAnonymous]
        public async ValueTask<IActionResult> FetchRequest([FromQuery] Guid requestId, [FromHeader] string username, [FromHeader] string password)
        {
            if (string.IsNullOrEmpty(username))
                return Unauthorized("username or passwrd is incorrect");

            if (string.IsNullOrEmpty(password))
                return Unauthorized("username or passwrd is incorrect");

            if (!requestId.Equals(Guid.Empty))
            {
                return Ok(await _logService.GetLogAsync(requestId));
            }

            return Ok(await _logService.GetFilteredLogAsync());
        }

    }
}
