using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebApplication1.service;

namespace WebApplication1
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler
    {
        cangku cangku = new cangku();
        经营管理系统 经营管理系统 = new 经营管理系统();

      



        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("aa");
          
            


            string sql = context.Request["sql"];
            string str = context.Request["info"];
            string param = context.Request["info"];
            string type = context.Request["type"];
            string username = context.Request["username"];
            string name = context.Request["name"];
            string password = context.Request["password"];

            if (type.Equals("getcangkuinfo"))
            {
                cangku.getcangkuinfo(str, context);
            }
            else if (type.Equals("cahrushuju"))
            {
                cangku.cahrushuju(str, name, context);
            }
            else if (type.Equals("cahchushuju"))
            {
                cangku.cahchushuju(str, name, context);
            }
            else if (type.Equals("login"))
            {
                cangku.login(username, password, context);
            }
            else if (type.Equals("testip"))
            {
                cangku.testip(context);
            }
            else if (type.Equals("android"))
            {
                string sql1 = context.Request["sql"];
                string db = context.Request["db"];
                
                if (param.Equals("getdata"))
                {
                    经营管理系统.ExecuteScalar(sql1, db, context);
                }
                else
                {
                    经营管理系统.返回数据json(sql1, db, context);
                }

            }
            else if (type.Equals("desktop"))
            {

              



                string sqlbefore = context.Request["sql"];
                string version = context.Request["version"];
                string sql1 = sqlbefore;
                string db = context.Request["db"];
                Stream stream = context.Request.InputStream;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                if (version == null)
                {

                }
                else
                {
                    if (version.Equals("1"))
                    {
                        sql1 = doit(UnBase64String(sqlbefore), "12345678876543211234567887654abc");
                    }
                    else
                    {

                    }
                }
                if (param.Equals("shoujitupian"))
                {
                    经营管理系统.jixiujiedan(db,context);
                }
                if (param.Equals("mujujiedan"))
                {
                    经营管理系统.mujujiedan(db, context);
                }
                if (param.Equals("shoujitupiandingw"))
                {
                    经营管理系统.jixiujiedandingw(db, context);
                }
                if (param.Equals("duqu"))
                {
                    经营管理系统.duqu(sql1, db, context);
                }
                else if (param.Equals("getdata"))
                {
                    经营管理系统.返回数据(sql1, db, context);
                }
                else if (param.Equals("ExecuteScalar"))
                {
                    经营管理系统.ExecuteScalar(sql1, db, context);
                }
                else if (param.Equals("ExecuteNonquerytuzhi"))
                {
                    经营管理系统.ExecuteNonquerytuzhi(sql1, db, buffer, context);
                }
                else if (param.Equals("innn"))
                {
                    经营管理系统.innn(sql1, db, context);
                }
                else if (param.Equals("ExecuteNonquery"))
                {
                    经营管理系统.ExecuteNonquery(sql1, db, buffer, context);
                }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public static string doit(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        public static string UnBase64String(string value)
        {
            if (value == null || value == "")
            {
                return "";
            }
            byte[] bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}