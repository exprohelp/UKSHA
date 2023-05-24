using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UKSHAApi.App_Start;
using UKSHAApi.Models;

namespace UKSHAApi.Repository.Unit
{
    public class Lab
    {
        UKSHAApi.LISProxy.SaleService ItdoseProxy = new LISProxy.SaleService();
        public dataSet getMemberInfo(string pmrssm_id)
        {
            ipMemberKey obj = new ipMemberKey();
            obj.AuthKey = "XBKJGFPPUHBC178HJKLP984LKJGDCNMLK9087640";
            obj.pmrssm_id = pmrssm_id;
            dataSet dsResult = APIProxy.CallWebApiMethod("SHA/GetMemberInformationJSON", obj);
            return dsResult;           
        }
        public dataSet Unit_VerificationQueries(ipUnit objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_UKSHA))
            {
                using (SqlCommand cmd = new SqlCommand("pSHA_Unit_VerificationQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@LabCode", SqlDbType.VarChar, 10).Value = objBO.LabCode;
                    cmd.Parameters.Add("@CentreId", SqlDbType.VarChar, 10).Value = objBO.CentreId;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 100).Value = objBO.VisitNo;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime, 30).Value = Convert.ToDateTime(objBO.from);
                    cmd.Parameters.Add("@to", SqlDbType.DateTime, 30).Value = Convert.ToDateTime(objBO.to);
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
        public dataSet LabQueries(ipUnit objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_UKSHA))
            {
                using (SqlCommand cmd = new SqlCommand("pSHA_LabQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 20).Value = objBO.VisitNo;
                    cmd.Parameters.Add("@SHAId", SqlDbType.VarChar, 20).Value = objBO.SHAId;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = objBO.to;
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
        public string Register_Patient(PatientInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_UKSHA))
            {
                using (SqlCommand cmd = new SqlCommand("pSHA_Register_Patient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@pmrssm_id", SqlDbType.VarChar, 20).Value = objBO.pmrssm_id;
                    cmd.Parameters.Add("@trea_code", SqlDbType.Int).Value = objBO.trea_code;
                    cmd.Parameters.Add("@ddo_code", SqlDbType.Int).Value = objBO.ddo_code;
                    cmd.Parameters.Add("@ddo_name", SqlDbType.VarChar, 255).Value = objBO.ddo_name;
                    cmd.Parameters.Add("@family_id", SqlDbType.VarChar, 50).Value = objBO.family_id;
                    cmd.Parameters.Add("@emp_pen_code", SqlDbType.VarChar,50).Value = objBO.emp_pen_code;
                    cmd.Parameters.Add("@member_id", SqlDbType.VarChar, 50).Value = objBO.member_id;
                    cmd.Parameters.Add("@ABHA_Id", SqlDbType.VarChar, 50).Value = objBO.ABHA_Id;
                    cmd.Parameters.Add("@member_name_eng", SqlDbType.VarChar, 100).Value = objBO.member_name_eng;
                    cmd.Parameters.Add("@dob", SqlDbType.Date).Value = objBO.dob;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar, 10).Value = objBO.gender;
                    cmd.Parameters.Add("@relation", SqlDbType.VarChar, 50).Value = objBO.relation;
                    cmd.Parameters.Add("@care_of_type_dec", SqlDbType.VarChar, 50).Value = objBO.care_of_type_dec;
                    cmd.Parameters.Add("@rural_urban_ben", SqlDbType.VarChar, 20).Value = objBO.rural_urban_ben;
                    cmd.Parameters.Add("@state_name_ben", SqlDbType.VarChar, 70).Value = objBO.state_name_ben;
                    cmd.Parameters.Add("@district_name_ben", SqlDbType.VarChar, 70).Value = objBO.district_name_ben;
                    cmd.Parameters.Add("@member_type", SqlDbType.NVarChar, 20).Value = objBO.member_type;
                    cmd.Parameters.Add("@mobile_member", SqlDbType.VarChar, 10).Value = objBO.mobile_member;
                    cmd.Parameters.Add("@CentreId", SqlDbType.VarChar, 10).Value = objBO.CentreId;
                    cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 20).Value = objBO.doctorId;
                    cmd.Parameters.Add("@doctorName", SqlDbType.VarChar, 100).Value = objBO.doctorName;
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 200).Value = objBO.remark;
                    cmd.Parameters.Add("@TestCodes", SqlDbType.VarChar, 500).Value = objBO.TestCodes;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public string Insert_ScanedDocument(PatientDetail objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_UKSHA))
            {
                using (SqlCommand cmd = new SqlCommand("pSHA_Insert_ScanedDocument", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@centreId", SqlDbType.VarChar, 5).Value = objBO.CentreId;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 25).Value = objBO.VisitNo;             
                    cmd.Parameters.Add("@doc_name", SqlDbType.VarChar, 30).Value = objBO.doc_name;
                    cmd.Parameters.Add("@doc_location", SqlDbType.VarChar, 200).Value = objBO.doc_location;
                    cmd.Parameters.Add("@ServerUrl", SqlDbType.VarChar, 100).Value = objBO.ServerUrl;
                    cmd.Parameters.Add("@f_size", SqlDbType.BigInt).Value = objBO.fSize;
                    cmd.Parameters.Add("@emp_code", SqlDbType.VarChar, 20).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public string Unit_InsertUpdateVerification(List<PatientDetails> objBO)
        {

            string VisitNo = string.Empty;
            string PatientName = string.Empty;
            string Gender = string.Empty;
            string Prm1 = string.Empty;
            string Logic = string.Empty;
            int Age = 0;
            string AgeType = string.Empty;
            string DoctorId = string.Empty;
            string processInfo = string.Empty;
            string login_id = string.Empty;
            string hosp_id = string.Empty;
            if (objBO.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("VisitNo", typeof(string));
                dt.Columns.Add("TestCode", typeof(string));
                dt.Columns.Add("Amount", typeof(decimal));
                dt.Columns.Add("IsCancelled", typeof(int));
                dt.Columns.Add("CancelRemark", typeof(string));
                foreach (PatientDetails obj in objBO)
                {
                    VisitNo = obj.VisitNo;
                    PatientName = obj.PatientName;
                    Prm1 = obj.Prm1;
                    Gender = obj.Gender;
                    Logic = obj.Logic;
                    Age = obj.Age;
                    AgeType = obj.AgeType;
                    DoctorId = obj.DoctorId;
                    hosp_id = obj.hosp_id;
                    login_id = obj.login_id;
                    if (obj.IsCancelled > -1)
                    {
                        DataRow dr = dt.NewRow();
                        dr["VisitNo"] = obj.VisitNo;
                        dr["TestCode"] = obj.TestCode;
                        dr["Amount"] = obj.Amount;
                        dr["IsCancelled"] = obj.IsCancelled;
                        dr["CancelRemark"] = obj.CancelRemark;
                        dt.Rows.Add(dr);
                    }
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_UKSHA))
                {
                    using (SqlCommand cmd = new SqlCommand("pSHA_Unit_VerifyAndCancel", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 50).Value = VisitNo;
                        cmd.Parameters.Add("@PatientName", SqlDbType.VarChar, 100).Value = PatientName;
                        cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = Gender;
                        cmd.Parameters.Add("@Age", SqlDbType.Int, 3).Value = Age;
                        cmd.Parameters.Add("@AgeType", SqlDbType.VarChar, 10).Value = AgeType;
                        cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 10).Value = Prm1;
                        cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = DoctorId;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = Logic;
                        cmd.Parameters.AddWithValue("udt_TestInfo", dt);
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                            con.Close();

                            if (Logic == "UpdateOrCancel")
                            {
                                // Cancelling Test in ITDOSE 
                                ipUnit obj1 = new ipUnit();
                                obj1.VisitNo = VisitNo;
                                obj1.from =Convert.ToDateTime("1900/01/01");
                                obj1.to = Convert.ToDateTime("1900/01/01");
                                obj1.Logic = "CancelTestInfo";
                                dataSet ds = Unit_VerificationQueries(obj1);
                                if (ds.ResultSet.Tables.Count > 0 && ds.ResultSet.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
                                    {
                                        try
                                        {
                                            ItdoseProxy.UPHealth_CancelTest(dr["VisitNo"].ToString(), dr["testCode"].ToString());
                                        }
                                        catch (Exception ex) { string test = ex.Message; }
                                    }
                                }
                            }


                        }
                        catch (SqlException sqlEx)
                        {
                            processInfo = "Error Found   : " + sqlEx.Message;
                        }
                        finally { con.Close(); }
                    }
                }
            }
            return processInfo;
        }
        public string MarkTestApproved(TestApproveInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_UKSHA))
            {
                using (SqlCommand cmd = new SqlCommand("pMarkTestApproved", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 20).Value = objBO.VisitNo;
                    cmd.Parameters.Add("@testCode", SqlDbType.VarChar, 20).Value = objBO.testCode;
                    cmd.Parameters.Add("@ItDoseTestId", SqlDbType.VarChar, 20).Value = objBO.ItDoseTestId;
                    cmd.Parameters.Add("@ApprovedDate", SqlDbType.DateTime).Value = objBO.ApprovedDate;
                    cmd.Parameters.Add("@ApprovedBy", SqlDbType.VarChar, 200).Value = objBO.ApprovedBy;
                    cmd.Parameters.Add("@ApprovedBylab", SqlDbType.VarChar, 20).Value = objBO.ApprovedBylab;
                    cmd.Parameters.Add("@ItemID_Interface", SqlDbType.VarChar, 20).Value = objBO.ItemID_Interface;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
    }
}