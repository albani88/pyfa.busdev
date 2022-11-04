using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CustomTokenAuthProvider;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Net.Http;
using pyfa.busdev.Controllers;
using pyfa.busdev.libs;


namespace pyfa.busdev.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("pyfabusdev/[controller]")]
    public class RencanaPemasaranController : Controller
    {
        private BaseController bc = new BaseController();
        private BaseTrxController bx = new BaseTrxController();
        private lConvert lc = new lConvert();
        private lMessage mc = new lMessage();
        private lServiceLogs lsl = new lServiceLogs();
        private lDbConn dbconn = new lDbConn();
        private lDataLayer ldl = new lDataLayer();
        private lPgsqlMapping lgsql = new lPgsqlMapping();
        private TokenController tc = new TokenController();

        [HttpPost("detail")]
        public JObject getDataRencanaPemasaran([FromBody] JObject json)
        {
            JObject jOut = new JObject();

            try
            {
                var retrunlist = ldl.getdatalistrencanapemasaran(json);
                jOut.Add("status", mc.GetMessage("api_output_ok"));
                jOut.Add("message", mc.GetMessage("process_success"));
                jOut.Add("data", retrunlist);

            }
            catch (Exception ex)
            {
                jOut = new JObject();
                jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                jOut.Add("message", ex.Message);
            }

            return jOut;
        }


        [HttpPost("insert")]
        public IActionResult insertupdatemarketreview([FromBody] JObject json)
        {
            int code = 200;
            JObject jOut = new JObject();
            try
            {

                var jaRetrune = ldl.checkdatarencanapemasaran(json);
                var checkdata = jaRetrune[0]["retrundata"].ToString();
                if (checkdata == "0")
                {
                    var res = bx.insertrencanapemasaran(json);
                    if (res == "success")
                    {
                        code = 200;
                        jOut = new JObject();
                        jOut.Add("status", mc.GetMessage("api_output_ok"));
                        jOut.Add("message", mc.GetMessage("save_success"));
                    }
                    else
                    {
                        jOut = new JObject();
                        jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                        jOut.Add("message", res);
                    }
                }
                else
                {
                    var res = bx.updaterencanapemasaran(json);
                    if (res == "success")
                    {
                        code = 200;
                        jOut = new JObject();
                        jOut.Add("status", mc.GetMessage("api_output_ok"));
                        jOut.Add("message", mc.GetMessage("save_success"));
                    }
                    else
                    {
                        jOut = new JObject();
                        jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                        jOut.Add("message", res);
                    }
                }

            }
            catch (Exception ex)
            {
                jOut = new JObject();
                code = 500;
                jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                jOut.Add("message", ex.Message);
            }

            return StatusCode(code, jOut);
        }
    }
}
