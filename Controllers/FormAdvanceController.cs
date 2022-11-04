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
    public class FormAdvanceController : Controller
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

        [HttpPost("list")]
        public JObject ListMastertamplateAdvance([FromBody] JObject json)
        {
            JObject jOut = new JObject();

            try
            {
                var retrunlist = ldl.ListMasterTamplatebyuser(json);
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


        [HttpPost("DynamicAdvance")]
        public IActionResult DynamicAdvance([FromBody] JObject json)
        {
            int code = 200;
            var jOut = new JObject();
            var jaResMaster = new JArray();
            var jaMasterFrm = new JArray();
            var jaForm = new JArray();
            var jData = new JObject();
            var jaContent = new JArray();

            var retHeadById = new List<dynamic>();
            var retObject = new List<dynamic>();
            var jaRowForm = new List<dynamic>();
            var retFormDet = new List<dynamic>();

            var arrDataDtr = new JArray();
            var arrItems = new JArray();
            var jaFormDet = new JArray();
            var jaDrp = new JArray();

            var objDataDtr = new JObject();
            var joHead = new JObject();
            var jFormDetData = new JObject();
            var jFormCont = new JObject();
            var jDrpDet = new JObject();

            string  hid = "0";
            int mid = 0;

            try
            {

                jaResMaster = ldl.DetailMasterGroupTemplate(json);
                if (jaResMaster.Count > 0)
                {
                    int groupid = Int32.Parse(jaResMaster[0]["mtg_id"].ToString());
                    for (int i = 0; i < jaResMaster.Count; i++)
                    {
                        joHead = new JObject();
                        var tempcode = jaResMaster[i]["mgd_temp_code"].ToString();
                        jaMasterFrm = ldl.getdatamasterformbytmpcode(tempcode);

                        if (jaMasterFrm.Count > 0)
                        {
                            string typ = jaMasterFrm[0]["mfh_content_type"].ToString().ToLower();
                            hid = jaMasterFrm[0]["mfh_id"].ToString();
                            joHead.Add("counter", jaResMaster[i]["mgd_counter"].ToString());
                            joHead.Add("temp_id", jaMasterFrm[0]["mfh_id"].ToString());
                            joHead.Add("temp_code", jaMasterFrm[0]["mfh_tamp_code"].ToString());
                            joHead.Add("temp_headername", jaMasterFrm[0]["mfh_tamp_name"].ToString());
                            joHead.Add("temp_contenttype", jaMasterFrm[0]["mfh_content_type"].ToString());

                            if (typ == "form")
                            {
                                jaForm = new JArray();
                                jaRowForm = new List<dynamic>();
                                jaRowForm = ldl.getdatamasterformdtlbyid(hid);
                                for (int index = 0; index < jaRowForm.Count; index++)
                                {
                                    jaFormDet = new JArray();
                                    mid = jaRowForm[index].mfd_field_id;
                                    retFormDet = ldl.getdatamasterfieldtlbyfieldid(mid);
                                    for (int x = 0; x < retFormDet.Count; x++)
                                    {
                                        jFormDetData = new JObject();
                                        jFormDetData.Add("field_id", retFormDet[x].mfd_id);
                                        jFormDetData.Add("field_type", retFormDet[x].mfd_type_element);
                                        jFormDetData.Add("label", retFormDet[x].mfd_label_name);
                                        jFormDetData.Add("needs", retFormDet[x].mfd_required);
                                        jFormDetData.Add("placeholder", retFormDet[x].mfd_placeholder);
                                        jFormDetData.Add("format", retFormDet[x].mfd_format);
                                        jFormDetData.Add("length", retFormDet[x].mfd_length);
                                        jFormDetData.Add("source", retFormDet[x].mfd_source);

                                        string codeddl = retFormDet[x].mfd_source;
                                        string typefield = retFormDet[x].mfd_type_element;
                                        jaDrp = new JArray();
                                        if ((typefield.ToLower() == "dropdownlist") || (typefield.ToLower() == "dropdownlistReadonly") || (typefield.ToLower() == "radio"))
                                        {
                                            var jDrp = ldl.getdynamicoptionbyscourecode(codeddl);
                                            for (int z = 0; z < jDrp.Count; z++)
                                            {
                                                jDrpDet = new JObject();
                                                jDrpDet.Add("urut", jDrp[z]["ddp_counter"].ToString() ?? "");
                                                jDrpDet.Add("val", jDrp[z]["ddp_value"].ToString() ?? "");
                                                jDrpDet.Add("text", jDrp[z]["ddp_name"].ToString() ?? "");
                                                jaDrp.Add(jDrpDet);
                                            }
                                        }

                                        jFormDetData.Add("connected", jaDrp);
                                        jaFormDet.Add(jFormDetData);

                                    }
                                    jFormCont = new JObject();
                                    jFormCont.Add("row", jaRowForm[index].mfd_counter);
                                    jFormCont.Add("object", jaFormDet);
                                    jaForm.Add(jFormCont);
                                }



                                joHead.Add("contents", jaForm);
                            }
                            else if (typ == "datarow")
                            {
                                jaForm = new JArray();
                                jaRowForm = new List<dynamic>();
                                jaRowForm = ldl.getdatamasterformdtlbyid(hid);
                                for (int index = 0; index < jaRowForm.Count; index++)
                                {
                                    jaFormDet = new JArray();
                                    mid = jaRowForm[index].mfd_field_id;
                                    retFormDet = ldl.getdatamasterfieldtlbyfieldid(mid);
                                    for (int x = 0; x < retFormDet.Count; x++)
                                    {
                                        jFormDetData = new JObject();
                                        jFormDetData.Add("field_id", retFormDet[x].mfd_id);
                                        jFormDetData.Add("field_type", retFormDet[x].mfd_type_element);
                                        jFormDetData.Add("label", retFormDet[x].mfd_label_name);
                                        jFormDetData.Add("needs", retFormDet[x].mfd_required);
                                        jFormDetData.Add("placeholder", retFormDet[x].mfd_placeholder);
                                        jFormDetData.Add("format", retFormDet[x].mfd_format);
                                        jFormDetData.Add("length", retFormDet[x].mfd_length);
                                        jFormDetData.Add("source", retFormDet[x].mfd_source);

                                        string codeddl = retFormDet[x].mfd_source;
                                        string typefield = retFormDet[x].mfd_type_element;
                                        jaDrp = new JArray();
                                        if ((typefield.ToLower() == "dropdownlist") || (typefield.ToLower() == "dropdownlistReadonly") || (typefield.ToLower() == "radio"))
                                        {
                                            var jDrp = ldl.getdynamicoptionbyscourecode(codeddl);
                                            for (int z = 0; z < jDrp.Count; z++)
                                            {
                                                jDrpDet = new JObject();
                                                jDrpDet.Add("urut", jDrp[z]["ddp_counter"].ToString() ?? "");
                                                jDrpDet.Add("val", jDrp[z]["ddp_value"].ToString() ?? "");
                                                jDrpDet.Add("text", jDrp[z]["ddp_name"].ToString() ?? "");
                                                jaDrp.Add(jDrpDet);
                                            }
                                        }

                                        jFormDetData.Add("connected", jaDrp);
                                        jaFormDet.Add(jFormDetData);

                                    }
                                    jFormCont = new JObject();
                                    jFormCont.Add("row", jaRowForm[index].mfd_counter);
                                    jFormCont.Add("object", jaFormDet);
                                    jaForm.Add(jFormCont);
                                }
                                joHead.Add("contents", jaForm);
                            }
                            else
                            {
                               
                                    joHead.Add("contents", new JArray());
                            }

                        }
                        jaContent.Add(joHead);
                    }

                    jData.Add("group_id", jaResMaster[0]["mtg_id"].ToString());
                    jData.Add("group_code", jaResMaster[0]["mtg_code_group"].ToString());
                    jData.Add("group_assignto", jaResMaster[0]["mtg_is_user"].ToString());
                    jData.Add("group_desc", jaResMaster[0]["mtg_desc"].ToString());
                    jData.Add("status", jaResMaster[0]["mtg_status"].ToString());
                    jData.Add("form_content", jaContent);

                    jOut = new JObject();
                    jOut.Add("status", mc.GetMessage("api_output_ok"));
                    jOut.Add("message", mc.GetMessage("process_success"));
                    jOut.Add("data", new JArray(jData));

                }
                else
                {
                    jOut = new JObject();
                    jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                    jOut.Add("message", "Form Not Found");
                }

            }
            catch (Exception ex)
            {
                code = 500;
                jOut = new JObject();
                jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                jOut.Add("message", ex.Message);
            }

            return StatusCode(code, jOut);

        }

        [HttpPost("submit")]
        public JObject SubmitApplication([FromBody] JObject json)
        {
            JObject jOut = new JObject();

            try
            {
                    var res = bx.sumbitaplikasi(json);
                    if (res == "success")
                    {
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
