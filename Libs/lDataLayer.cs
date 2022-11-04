
using pyfa.busdev.libs;
using pyfa.busdev.Controllers;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace pyfa.busdev.libs
{
    public class lDataLayer
    {
        private lDbConn dbconn = new lDbConn();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();

        public JArray ListMasterGroupTemplate()
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_listmastergrouptemplate";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public JArray ListMasterTamplatebyuser(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_listtamplatebyusr";
            string p1 = "@usr" + split + json.GetValue("user").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public JArray DetailMasterGroupTemplate(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_detailmastergrouptemplate";
            string p1 = "@code_group" + split + json.GetValue("code_group").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public JArray deletemastergrouptemplate(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_deletemastergrouptemplatebyid";
            string p1 = "@code_group" + split + json.GetValue("code_group").ToString() + split + "s";
            string p2 = "@usr" + split + json.GetValue("user").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1, p2);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public JArray getcodetemplate()
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getcodetemplate";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public JArray getuserall()
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_listusersall";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public JArray genetrategroupcategorycode()
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "gen_last_code_group_template";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;

        }

        #region advance


        public JArray getdatamasterformbytmpcode(string tempcode)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getdatamasterformbycodetmp";
            string p1 = "@code_tmp" + split + tempcode + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public List<dynamic> getdatamasterformdtlbyid(string id)
        {
          
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "getdatamasterformdtlbyid";
            string p1 = "@id" + split + Int32.Parse(id) + split + "i";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
           
            return retObject;

        }

        public List<dynamic> getdatamasterfieldtlbyfieldid(int id)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "getdatamasterfieldtlbyfieldid";
            string p1 = "@id" + split + id + split + "i";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
         
            return retObject;

        }

        public JArray getdynamicoptionbyscourecode(string codeddl)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "get_dropdown_list_advance";
            string p1 = "@codeddl" + split + codeddl + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }
        #endregion


        #region aspek legal 
        public JObject getlistaspeklegal(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistaspeklegal";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdataaspeklegal(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdataaspeklegal";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        #endregion

        #region market review

        public JObject getdatalistmarketreview(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistdatamarketreview";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdatamarketreview(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdatamarketreview";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }
        #endregion

        #region rencana pemasaran 
        public JObject getdatalistrencanapemasaran(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistdatarencanapemasaran";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdatarencanapemasaran(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdatarencanapemasaran";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        #endregion

        #region aspek pembelian raw material
        public JObject getdatalistaspekpembelianrm(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistdataaspekpembelianrm";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdataaspekpembelianrm(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdataaspekpembelianrm";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        #endregion

        #region aspek Pengembangan prodak
        public JObject getdatalistaspekpengembanganprodak(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistdataaspekpengembanganprodak";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdataaspekpengembanganprodak(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdataaspekpengembanganprodak";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        #endregion

        #region aspek registrasi
        public JObject getdatalistaspekregistrasi(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistdataaspekregistrasi";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdataaspekregistrasi(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdataaspekregistrasi";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }
        #endregion

        #region aspek produksi
        public JObject getdatalistaspekproduksi(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistdataaspekproduksi";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdataaspekproduksi(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdataaspekproduksi";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        #endregion

        #region Aspek Lab Uji
        public JObject getdatalistaspeklabuji(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistdataaspeklabuji";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdataaspeklabuji(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdataaspeklabuji";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        #endregion

        #region aspek pemastian 
        public JObject getdatalistaspekpemastian(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistdataaspekpemastian";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdataaspekpemastian(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdataaspekpemastian";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }
        #endregion

        #region aspek gudang
        public JObject getdatalistaspekgudang(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_getlistdataaspekgudang";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }
            return joReturn;

        }

        public JArray checkdataaspekgudang(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdataaspekgudang";
            string p1 = "@code" + split + json.GetValue("temp_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        #endregion

        #region master crud
        public JArray checkdatamastercrud(JObject json)
        {
            var jaReturn = new JArray();
            var joReturn = new JObject();
            List<dynamic> retObject = new List<dynamic>();
            var dbprv = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            var split = "||";

            string spname = "sp_checkdatamastercrud";
            string p1 = "@code" + split + json.GetValue("group_code").ToString() + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(dbprv, cstrname, split, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }
        #endregion
    }
}
