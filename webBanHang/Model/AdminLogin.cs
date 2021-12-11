using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Model.Model;

namespace Model
{
    public class AdminLogin
    {
        private WebBanHangNongSan context = null;
        public AdminLogin()
        {
            context = new WebBanHangNongSan();
        }
        public bool Login(string userName, string passWord)
        {
            object[] paras =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@PassWord", passWord),
            };
            var res = context.Database.SqlQuery<bool>("LoginAdmin @UserName , @PassWord ", paras).SingleOrDefault();
            return res;
        }
    }
}