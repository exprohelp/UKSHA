using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UKSHAApi.Models;

namespace UKSHAApi.Repository.Report
{
    public class Report
    {
        public dataSet MIS_ReportQueries(ipReport objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_UKSHA))
            {
                using (SqlCommand cmd = new SqlCommand("pSHA_MIS_ReportQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@DistrictName", SqlDbType.VarChar, 100).Value = objBO.DistrictName;
                    cmd.Parameters.Add("@CentreType", SqlDbType.VarChar, 100).Value = objBO.CentreType;
                    cmd.Parameters.Add("@CentreId", SqlDbType.VarChar, 10).Value = objBO.CentreId;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 100).Value = objBO.VisitNo;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
                    cmd.Parameters.Add("@from", SqlDbType.Date, 20).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date, 20).Value = objBO.to;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                        dsObj.Msg = "Success";
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
        public dataSet ChandanMIS_ReportQueries(ipReport objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_UKSHA))
            {
                using (SqlCommand cmd = new SqlCommand("pSHA_ChandanMIS_ReportQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@DistrictName", SqlDbType.VarChar, 100).Value = objBO.DistrictName;
                    cmd.Parameters.Add("@CentreType", SqlDbType.VarChar, 100).Value = objBO.CentreType;
                    cmd.Parameters.Add("@CentreId", SqlDbType.VarChar, 10).Value = objBO.CentreId;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 100).Value = objBO.VisitNo;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
                    cmd.Parameters.Add("@from", SqlDbType.Date, 20).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date, 20).Value = objBO.to;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                        dsObj.Msg = "Success";
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
    }
}