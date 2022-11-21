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
    public class MasterGroupTemplateController : Controller
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

        [HttpGet("list")]
        public JObject ListMasterGroupTemplate()
        {
            JObject jOut = new JObject();

            try
            {
                var retrunlist = ldl.ListMasterGroupTemplate();
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

        [HttpPost("detail")]
        public JObject detailmastergrouptemplate([FromBody] JObject json)
        {
            JObject jOut = new JObject();
            JArray jdata = new JArray();
            try
            {
                var jaData = ldl.DetailMasterGroupTemplate(json);
                jOut.Add("status", mc.GetMessage("api_output_ok"));
                jOut.Add("message", mc.GetMessage("process_success"));
                jOut.Add("mtg_id", jaData[0]["mtg_id"].ToString());
                jOut.Add("mtg_code_group", jaData[0]["mtg_code_group"].ToString());
                jOut.Add("mtg_desc", jaData[0]["mtg_desc"].ToString());
                for (int i = 0; i < jaData.Count; i++)
                {

                    var jsonData = new JObject();
                    jsonData.Add("mgd_temp_code", jaData[i]["mgd_temp_code"].ToString());
                    jsonData.Add("mgd_counter", jaData[i]["mgd_counter"].ToString());
                    jdata.Add(jsonData);

                }

                jOut.Add("data", jdata);

        

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
        public IActionResult CreateMasterForm([FromBody] JObject json)
        {
            int code = 200;
            JObject jOut = new JObject();

            try
            {
                List<dynamic> retObject = new List<dynamic>();
                var jaData = ldl.genetrategroupcategorycode();
                string tmpcode = jaData[0]["gmc_code"].ToString();
                if (tmpcode != "")
                {
                    json["code_group"] = tmpcode;
                }
                else
                {
                    json["code_group"] = "";
                }


                var res = bx.Insertmastergrouptemplate(json, "insert");
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
                    jOut.Add("message", "Create Failed");
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

        [HttpPost("update")]
        public IActionResult UpdateMasterForm([FromBody] JObject json)
        {
            int code = 200;
            JObject jOut = new JObject();

            try
            {
                List<dynamic> retObject = new List<dynamic>();

                var res = bx.Insertmastergrouptemplate(json, "update");
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
                    jOut.Add("message", "Update Failed");
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

        [HttpPost("delete")]
        public JObject deletemastergrouptemplate([FromBody] JObject json)
        {
            JObject jOut = new JObject();

            try
            {
                var retrun = ldl.deletemastergrouptemplate(json);
                jOut.Add("status", mc.GetMessage("api_output_ok"));
                jOut.Add("message", mc.GetMessage("process_success"));

            }
            catch (Exception ex)
            {
                jOut = new JObject();
                jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                jOut.Add("message", ex.Message);
            }

            return jOut;
        }

        [HttpGet("getcodetemp")]
        public JObject getcodetemplate()
        {
            JObject jOut = new JObject();

            try
            {
                var retrun = ldl.getcodetemplate();
                jOut.Add("status", mc.GetMessage("api_output_ok"));
                jOut.Add("message", mc.GetMessage("process_success"));
                jOut.Add("data", retrun);

            }
            catch (Exception ex)
            {
                jOut = new JObject();
                jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                jOut.Add("message", ex.Message);
            }

            return jOut;
        }

        [HttpGet("getuserall")]
        public JObject getuserall()
        {
            JObject jOut = new JObject();

            try
            {
                var retrun = ldl.getuserall();
                jOut.Add("status", mc.GetMessage("api_output_ok"));
                jOut.Add("message", mc.GetMessage("process_success"));
                jOut.Add("data", retrun);

            }
            catch (Exception ex)
            {
                jOut = new JObject();
                jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                jOut.Add("message", ex.Message);
            }

            return jOut;
        }
    }
}
