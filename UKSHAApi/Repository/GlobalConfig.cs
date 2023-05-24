using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UKSHAApi.Repository
{
    public class GlobalConfig
    {      
    
        public static string ConStr_UKSHA = ConfigurationManager.ConnectionStrings["ConStr_UKSHA"].ToString();
        public static string ConStr_ExHrd = ConfigurationManager.ConnectionStrings["ConStr_ExHrd"].ToString();
		public static string ConStr_LISByItDose = ConfigurationManager.ConnectionStrings["ConStr_LISByItDose"].ToString();
	}
}