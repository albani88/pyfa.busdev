using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pyfa.busdev.libs;
using System.Text;
using Npgsql;
using System.Data;
using System.Dynamic;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Data.SqlClient;
using System.Globalization;

namespace pyfa.busdev.Controllers
{
    public class BaseTrxController : Controller
    {
        private lDbConn dbconn = new lDbConn();
        private BaseController bc = new BaseController();

        public string Insertmastergrouptemplate(JObject json, string actiontype)
        {
            string strout = "";
            var retObject = new List<dynamic>();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            JObject jo = new JObject();
            var conn = dbconn.constringList(provider, cstrname);
            SqlTransaction trans;
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();

            try
            {

                SqlCommand cmd = new SqlCommand();
                if (actiontype == "update")
                {
                    cmd = new SqlCommand("sp_deletemastergrouptemplatebymfhid", connection, trans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codegroup", json.GetValue("code_group").ToString());
                    cmd.ExecuteNonQuery();
                }

                cmd = new SqlCommand("insert_master_group_template", connection, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codegroup", Convert.ToString(json.GetValue("code_group").ToString()));
                cmd.Parameters.AddWithValue("@desc", json.GetValue("desc").ToString());
                cmd.Parameters.AddWithValue("@assignto", json.GetValue("assignto").ToString());
                cmd.Parameters.AddWithValue("@usrbefore", json.GetValue("user").ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                retObject = bc.GetDataObjSqlsvr(dr);
                dr.Close();
                string mtg_id = Convert.ToString(retObject[0].mtg_id_header);

                JArray jaData = JArray.Parse(json["detail"].ToString());
                for (int i = 0; i < jaData.Count; i++)
                {
                    //insert form detail
                    var data = new JObject();
                    data = JObject.Parse(jaData[i].ToString());
                    cmd = new SqlCommand("insert_master_group_template_detail", connection, trans);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", Int32.Parse(mtg_id));
                    cmd.Parameters.AddWithValue("@tempcode", data.GetValue("temp_code").ToString());
                    cmd.Parameters.AddWithValue("@counter", Int32.Parse(data["counter"].ToString()));
                    cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            SqlConnection.ClearPool(connection);
            return strout;
        }

        public string Insertcrudbusdev(JObject json)
        {
            string strout = "";
            var retObject = new List<dynamic>();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            JObject jo = new JObject();
            var conn = dbconn.constringList(provider, cstrname);
            SqlTransaction trans;
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand();
                JArray jaData = JArray.Parse(json["detail"].ToString());
                for (int i = 0; i < jaData.Count; i++)
                {
                    //insert form detail
                    var data = new JObject();
                    data = JObject.Parse(jaData[i].ToString());
                    cmd = new SqlCommand("sp_insertcurdbusdev", connection, trans);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@groupcode", json.GetValue("group_code").ToString());
                    cmd.Parameters.AddWithValue("@tempcode", json.GetValue("temp_code").ToString());
                    cmd.Parameters.AddWithValue("@fieldid", data["fieldid"].ToString());
                    cmd.Parameters.AddWithValue("@fieldindex", data["fieldindex"].ToString());
                    cmd.Parameters.AddWithValue("@fieldname", data["fieldname"].ToString());
                    cmd.Parameters.AddWithValue("@fieldvalue", data["fieldvalue"].ToString());
                    cmd.Parameters.AddWithValue("@fieldtype", data["fieldtype"].ToString());
                    cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            SqlConnection.ClearPool(connection);
            return strout;
        }


        public string updatecrudbusdev(JObject json)
        {
            string strout = "";
            var retObject = new List<dynamic>();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            JObject jo = new JObject();
            var conn = dbconn.constringList(provider, cstrname);
            SqlTransaction trans;
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand();
                JArray jaData = JArray.Parse(json["detail"].ToString());
                for (int i = 0; i < jaData.Count; i++)
                {
                    //insert form detail
                    var data = new JObject();
                    data = JObject.Parse(jaData[i].ToString());
                    cmd = new SqlCommand("sp_updatecurdbusdev", connection, trans);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@groupcode", json.GetValue("group_code").ToString());
                    cmd.Parameters.AddWithValue("@tempcode", json.GetValue("temp_code").ToString());
                    cmd.Parameters.AddWithValue("@fieldid", data["fieldid"].ToString());
                    cmd.Parameters.AddWithValue("@fieldindex", data["fieldindex"].ToString());
                    cmd.Parameters.AddWithValue("@fieldname", data["fieldname"].ToString());
                    cmd.Parameters.AddWithValue("@fieldvalue", data["fieldvalue"].ToString());
                    cmd.Parameters.AddWithValue("@fieldtype", data["fieldtype"].ToString());
                    cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            SqlConnection.ClearPool(connection);
            return strout;
        }

        public string sumbitaplikasi(JObject json)
        {
            string strout = "";
            var retObject = new List<dynamic>();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("pyfatrack");
            JObject jo = new JObject();
            var conn = dbconn.constringList(provider, cstrname);
            SqlTransaction trans;
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();

            try
            {

                SqlCommand cmd = new SqlCommand();
                var data = new JObject();
                cmd = new SqlCommand("app_submitApplication", connection, trans);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@code", json.GetValue("code").ToString());
                cmd.Parameters.AddWithValue("@nextposisi", json.GetValue("nextposisi").ToString());
                cmd.Parameters.AddWithValue("@nextuser", json.GetValue("nextuser").ToString());
                cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            SqlConnection.ClearPool(connection);
            return strout;
        }

        #region akan di delete
        //public string insertaspkelegal(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {
             
        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertaspeklegal", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@patenhaki", json.GetValue("patenhaki").ToString());
        //        cmd.Parameters.AddWithValue("@bpom", json.GetValue("bpom").ToString());
        //        cmd.Parameters.AddWithValue("@patenzataktif", json.GetValue("patenzataktif").ToString());
        //        cmd.Parameters.AddWithValue("@thnzataktif", json.GetValue("thnzataktif").ToString());
        //        cmd.Parameters.AddWithValue("@prosesformula", json.GetValue("prosesformula").ToString());
        //        cmd.Parameters.AddWithValue("@thnformula", json.GetValue("thnformula").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string updateaspkelegal(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_updateaspeklegal", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@patenhaki", json.GetValue("patenhaki").ToString());
        //        cmd.Parameters.AddWithValue("@bpom", json.GetValue("bpom").ToString());
        //        cmd.Parameters.AddWithValue("@patenzataktif", json.GetValue("patenzataktif").ToString());
        //        cmd.Parameters.AddWithValue("@thnzataktif", json.GetValue("thnzataktif").ToString());
        //        cmd.Parameters.AddWithValue("@prosesformula", json.GetValue("prosesformula").ToString());
        //        cmd.Parameters.AddWithValue("@thnformula", json.GetValue("thnformula").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string insertmarketreview(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertmarketreview", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@kebutuhanprodak", json.GetValue("kebutuhanprodak").ToString());
        //        cmd.Parameters.AddWithValue("@datapenjualan", json.GetValue("datapenjualan").ToString());
        //        cmd.Parameters.AddWithValue("@jmlkasusu", json.GetValue("jmlkasusu").ToString());
        //        cmd.Parameters.AddWithValue("@tranpengobatan", json.GetValue("tranpengobatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string updatemarketreview(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_updatemarketreview", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@kebutuhanprodak", json.GetValue("kebutuhanprodak").ToString());
        //        cmd.Parameters.AddWithValue("@datapenjualan", json.GetValue("datapenjualan").ToString());
        //        cmd.Parameters.AddWithValue("@jmlkasusu", json.GetValue("jmlkasusu").ToString());
        //        cmd.Parameters.AddWithValue("@tranpengobatan", json.GetValue("tranpengobatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string insertrencanapemasaran(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertrencanapemasaran", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@lineofbisnis", json.GetValue("lineofbisnis").ToString());
        //        cmd.Parameters.AddWithValue("@pemasaran", json.GetValue("pemasaran").ToString());
        //        cmd.Parameters.AddWithValue("@kriteria1", json.GetValue("kriteria1").ToString());
        //        cmd.Parameters.AddWithValue("@kriteria2", json.GetValue("kriteria2").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string updaterencanapemasaran(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_updaterencanapemasaran", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@lineofbisnis", json.GetValue("lineofbisnis").ToString());
        //        cmd.Parameters.AddWithValue("@pemasaran", json.GetValue("pemasaran").ToString());
        //        cmd.Parameters.AddWithValue("@kriteria1", json.GetValue("kriteria1").ToString());
        //        cmd.Parameters.AddWithValue("@kriteria2", json.GetValue("kriteria2").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string insertaspekpembelianrm(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertaspekpembelianrm", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@sourcebhnawal", json.GetValue("sourcebhnawal").ToString());
        //        cmd.Parameters.AddWithValue("@sourcebhnkemasan", json.GetValue("sourcebhnkemasan").ToString());
        //        cmd.Parameters.AddWithValue("@hargabhnawal", json.GetValue("hargabhnawal").ToString());
        //        cmd.Parameters.AddWithValue("@packingsize", json.GetValue("packingsize").ToString());
        //        cmd.Parameters.AddWithValue("@minimumorder", json.GetValue("minimumorder").ToString());
        //        cmd.Parameters.AddWithValue("@leadtime", json.GetValue("leadtime").ToString());
        //        cmd.Parameters.AddWithValue("@hrgbhnkemasan", json.GetValue("hrgbhnkemasan").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string udpateaspekpembelianrm(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_updateaspekpembelianrm", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@sourcebhnawal", json.GetValue("sourcebhnawal").ToString());
        //        cmd.Parameters.AddWithValue("@sourcebhnkemasan", json.GetValue("sourcebhnkemasan").ToString());
        //        cmd.Parameters.AddWithValue("@hargabhnawal", json.GetValue("hargabhnawal").ToString());
        //        cmd.Parameters.AddWithValue("@packingsize", json.GetValue("packingsize").ToString());
        //        cmd.Parameters.AddWithValue("@minimumorder", json.GetValue("minimumorder").ToString());
        //        cmd.Parameters.AddWithValue("@leadtime", json.GetValue("leadtime").ToString());
        //        cmd.Parameters.AddWithValue("@hrgbhnkemasan", json.GetValue("hrgbhnkemasan").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string insertaspekpengembanganprodak(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertaspekpengembanganprodak", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@pengembangan", json.GetValue("pengembangan").ToString());
        //        cmd.Parameters.AddWithValue("@pengembanganformula", json.GetValue("pengembanganformula").ToString());
        //        cmd.Parameters.AddWithValue("@methodanalisa", json.GetValue("methodanalisa").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string updatespekpengembanganprodak(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_udpateaspekpengembanganprodak", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@pengembangan", json.GetValue("pengembangan").ToString());
        //        cmd.Parameters.AddWithValue("@pengembanganformula", json.GetValue("pengembanganformula").ToString());
        //        cmd.Parameters.AddWithValue("@methodanalisa", json.GetValue("methodanalisa").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string insertaspekregistrasi(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertaspekregistrasi", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@kategori", json.GetValue("kategori").ToString());
        //        cmd.Parameters.AddWithValue("@golonganobat", json.GetValue("golonganobat").ToString());
        //        cmd.Parameters.AddWithValue("@ujibe", json.GetValue("ujibe").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string updateaspekregistrasi(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_updateaspekregistrasi", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@kategori", json.GetValue("kategori").ToString());
        //        cmd.Parameters.AddWithValue("@golonganobat", json.GetValue("golonganobat").ToString());
        //        cmd.Parameters.AddWithValue("@ujibe", json.GetValue("ujibe").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string insertaspekproduksi(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertaspekproduksi", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@sertifikatprd", json.GetValue("sertifikatprd").ToString());
        //        cmd.Parameters.AddWithValue("@mesin", json.GetValue("mesin").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasmesin", json.GetValue("kapasitasmesin").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitaskaryawan", json.GetValue("kapasitaskaryawan").ToString());
        //        cmd.Parameters.AddWithValue("@tollout", json.GetValue("tollout").ToString());
        //        cmd.Parameters.AddWithValue("@alternatiftollout1", json.GetValue("alternatiftollout1").ToString());
        //        cmd.Parameters.AddWithValue("@alternatiftollout2", json.GetValue("alternatiftollout2").ToString());
        //        cmd.Parameters.AddWithValue("@alternatiftollout3", json.GetValue("alternatiftollout3").ToString());
        //        cmd.Parameters.AddWithValue("@alternatiftollout4", json.GetValue("alternatiftollout4").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string updateaspekproduksi(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_updateaspekproduksi", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@sertifikatprd", json.GetValue("sertifikatprd").ToString());
        //        cmd.Parameters.AddWithValue("@mesin", json.GetValue("mesin").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasmesin", json.GetValue("kapasitasmesin").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitaskaryawan", json.GetValue("kapasitaskaryawan").ToString());
        //        cmd.Parameters.AddWithValue("@tollout", json.GetValue("tollout").ToString());
        //        cmd.Parameters.AddWithValue("@alternatiftollout1", json.GetValue("alternatiftollout1").ToString());
        //        cmd.Parameters.AddWithValue("@alternatiftollout2", json.GetValue("alternatiftollout2").ToString());
        //        cmd.Parameters.AddWithValue("@alternatiftollout3", json.GetValue("alternatiftollout3").ToString());
        //        cmd.Parameters.AddWithValue("@alternatiftollout4", json.GetValue("alternatiftollout4").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string insertaspeklabuji(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertaspeklabuji", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@peralatan", json.GetValue("peralatan").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasperalatan", json.GetValue("kapasitasperalatan").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasinstument", json.GetValue("kapasitasinstument").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasreagent", json.GetValue("kapasitasreagent").ToString());
        //        cmd.Parameters.AddWithValue("@wsrs", json.GetValue("wsrs").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasanalis", json.GetValue("kapasitasanalis").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string updateaspeklabuji(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertaspeklabuji", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@peralatan", json.GetValue("peralatan").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasperalatan", json.GetValue("kapasitasperalatan").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasinstument", json.GetValue("kapasitasinstument").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasreagent", json.GetValue("kapasitasreagent").ToString());
        //        cmd.Parameters.AddWithValue("@wsrs", json.GetValue("wsrs").ToString());
        //        cmd.Parameters.AddWithValue("@kapasitasanalis", json.GetValue("kapasitasanalis").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string insertaspekpemastian(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertaspekpemastian", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@fasilitasperjanjian", json.GetValue("fasilitasperjanjian").ToString());
        //        cmd.Parameters.AddWithValue("@kesesuaianpenyimpanan", json.GetValue("kesesuaianpenyimpanan").ToString());
        //        cmd.Parameters.AddWithValue("@persyaratanhalal", json.GetValue("persyaratanhalal").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string updateaspekpemastian(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_updateaspekpemastian", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@fasilitasperjanjian", json.GetValue("fasilitasperjanjian").ToString());
        //        cmd.Parameters.AddWithValue("@kesesuaianpenyimpanan", json.GetValue("kesesuaianpenyimpanan").ToString());
        //        cmd.Parameters.AddWithValue("@persyaratanhalal", json.GetValue("persyaratanhalal").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string insertaspekgudang(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_insertaspekgudang", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@gba", json.GetValue("kapasitas_gba").ToString());
        //        cmd.Parameters.AddWithValue("@gbk", json.GetValue("kapasitas_gbk").ToString());
        //        cmd.Parameters.AddWithValue("@goj", json.GetValue("kapasitas_goj").ToString());
        //        cmd.Parameters.AddWithValue("@karyawan", json.GetValue("kapastias_karyawan").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}

        //public string updateaspekgudang(JObject json)
        //{
        //    string strout = "";
        //    var retObject = new List<dynamic>();
        //    var provider = dbconn.sqlprovider();
        //    var cstrname = dbconn.constringName("pyfatrack");
        //    JObject jo = new JObject();
        //    var conn = dbconn.constringList(provider, cstrname);
        //    SqlTransaction trans;
        //    SqlConnection connection = new SqlConnection(conn);
        //    connection.Open();
        //    trans = connection.BeginTransaction();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        var data = new JObject();
        //        cmd = new SqlCommand("sp_updateaspekgudang", connection, trans);
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@code", json.GetValue("temp_code").ToString());
        //        cmd.Parameters.AddWithValue("@gba", json.GetValue("kapasitas_gba").ToString());
        //        cmd.Parameters.AddWithValue("@gbk", json.GetValue("kapasitas_gbk").ToString());
        //        cmd.Parameters.AddWithValue("@goj", json.GetValue("kapasitas_goj").ToString());
        //        cmd.Parameters.AddWithValue("@karyawan", json.GetValue("kapastias_karyawan").ToString());
        //        cmd.Parameters.AddWithValue("@catatan", json.GetValue("catatan").ToString());
        //        cmd.Parameters.AddWithValue("@usr", json.GetValue("user").ToString());

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        trans.Commit();
        //        strout = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        strout = ex.Message;
        //    }
        //    connection.Close();
        //    SqlConnection.ClearPool(connection);
        //    return strout;
        //}


       
        #endregion
    }
}
