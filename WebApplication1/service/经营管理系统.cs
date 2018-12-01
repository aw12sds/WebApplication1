using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.service
{
    public class 经营管理系统
    {
        public void 返回数据(string sql, string db, HttpContext context)
        {

            DataSet ds = SQLhelp.GetDataTable(sql,db, CommandType.Text);
            //string ret = DataTableToString(dt);

            StringWriter writer = new StringWriter();
            ds.WriteXml(writer, XmlWriteMode.WriteSchema);
            string xml = writer.ToString();


            context.Response.ContentType = "text/plain";
            context.Response.Write(xml);
        }

        public void 返回数据json(string sql, string db, HttpContext context)
        {
            DataSet ds = SQLhelp.GetDataTable(sql, db, CommandType.Text);
            ds.Tables[0].Columns[0].ColumnName = "title";
            ds.Tables[0].Columns[4].ColumnName = "subTitle";
            ds.Tables[0].Columns[12].ColumnName = "remark";
            string ret ="";
            if (ds.Tables[0].Rows.Count == 0)
            {
                ret = "无数据";
            }
            else
            {
                ret = DataSetToJson(ds);
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(ret);
        }
        public static string DataSetToJson(DataSet ds)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder("[");
            for (int o = 0; o < ds.Tables.Count; o++)
            {
                str.Append("{");
                str.Append(string.Format("\"{0}\":[", ds.Tables[o].TableName));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        str.Append(string.Format("\"{0}\":\"{1}\",", ds.Tables[0].Columns[j].ColumnName, ds.Tables[0].Rows[i][j].ToString()));
                    }
                    str.Remove(str.Length - 1, 1);
                    str.Append("},");
                }
                str.Remove(str.Length - 1, 1);
                str.Append("]},");
            }
            str.Remove(str.Length - 1, 1);
            str.Append("]");
            return str.ToString();
        }
        public void ExecuteScalar(string sql,string db, HttpContext context)
        {
            object object1= SQLhelp.ExecuteScalar(sql, db, CommandType.Text);

            context.Response.ContentType = "text/plain";
            context.Response.Write(object1);
        }
        public void ExecuteNonquerytuzhi(string sql, string db, byte[] files, HttpContext context)
        {
            int t = SQLhelp.ExecuteNonquerytuzhi(sql, db, files, CommandType.Text);
            context.Response.ContentType = "text/plain";
            context.Response.Write(t+"");
        }
        public void ExecuteNonquery(string sql, string db, byte[] files, HttpContext context)
        {
            int t = SQLhelp.ExecuteNonquery(sql, db, files, CommandType.Text);
            context.Response.ContentType = "text/plain";
            context.Response.Write(t + "");
        }
        public void innn(string sql, string db, HttpContext context)
        {
            int t = SQLhelp.innn(sql, db, CommandType.Text);
            context.Response.ContentType = "text/plain";
            context.Response.Write(t + "");
        }
       
        public static string DataTableToString(DataTable dt)
        {
            StringBuilder strData = new StringBuilder();
            StringWriter sw = new StringWriter();
            dt.TableName = "aa";
            dt.WriteXmlSchema(sw);
            strData.Append(sw.ToString());
            sw.Close();
            strData.Append("@&@");
            for (int i = 0; i < dt.Rows.Count; i++)      
            {
                DataRow row = dt.Rows[i];
                if (i > 0)                                   
                {
                    strData.Append("#$%");
                }
                for (int j = 0; j < dt.Columns.Count; j++)    
                {
                    if (j > 0)                               
                    {
                        strData.Append("^&*");
                    }
                    strData.Append(Convert.ToString(row[j])); 
                }
            }

            return strData.ToString();
        }
        public void duqu(string sql,string db, HttpContext context)
        {
            byte[] mypdffile = null;
            mypdffile = SQLhelp.duqu(sql,db, CommandType.Text);
            context.Response.ContentType = "text/plain";
            context.Response.BinaryWrite(mypdffile);
        }
        public void mujujiedan(string db, HttpContext context)
        {
            System.IO.Stream MyStream;
            var file = context.Request.Files["file"];
            string filename = context.Request["filename"];
            string 客户名称 = context.Request["客户名称"];
            string 客户单位名称 = context.Request["客户单位名称"];
            string 工作令号 = context.Request["工作令号"];
            string 模具部订单号申请号 = context.Request["模具部订单号申请号"];
            string 数量 = context.Request["数量"];
            string 联系人 = context.Request["联系人"];
            string 项目名称 = context.Request["项目名称"];
            string 模具部接单日期 = context.Request["模具部接单日期"];
            string 模具部交货日期 = context.Request["模具部交货日期"];
            string 产品类型 = context.Request["产品类型"];
            string 规格 = context.Request["规格"];
            string 名称 = context.Request["名称"];
            string 单位 = context.Request["单位"];
            string 模具部销售单价 = context.Request["模具部销售单价"];
            if (产品类型.Equals("零件"))
            {
                产品类型 = "模具部";
            }
            else if (产品类型.Equals("部件"))
            {
                产品类型 = "模具部部件";
            }
            string 备注 = context.Request["备注"];
            string yonghu = context.Request["yonghu"];
            int FileLen = file.ContentLength;
            string 附件名称 = file.FileName.Split('.')[0];
            string 附件类型 = file.FileName.Split('.')[1];
            byte[] bytes = new byte[FileLen];
            MyStream = file.InputStream;
            MyStream.Read(bytes, 0, FileLen);
            BinaryReader read = new BinaryReader(MyStream);
            read.Read(bytes, 0, Convert.ToInt32(FileLen));
            DateTime shijian1 = DateTime.Now;

            var sql = "insert into tb_caigouliaodan(附件,附件名称,附件类型,模具部订单号申请号,工作令号,模具部接单日期,模具部交货日期,模具部联系人,模具部客户,名称,项目名称,单位,型号,当前状态,实际采购数量,模具部数量,模具部申请人,备注,料单类型) " +
    "values(@pic,'" + 附件名称 + "','" + 附件类型 + "','" + 模具部订单号申请号 + "','" + 工作令号 + "','" + 模具部接单日期 + "','" + 模具部交货日期 + "','" + 客户名称 + "','" + 客户单位名称 + "','" + 名称 + "','" + 项目名称 + "','" + 单位 + "','" + 规格 + "','" + "1" + "','" + 数量 + "','" + 数量 + "','" + 联系人 + "','" + 备注 +"','" + 产品类型 + "')";

            int t = SQLhelp.ExecuteNonquery(sql, db, bytes, CommandType.Text);
            context.Response.ContentType = "text/plain";

            context.Response.Write("success");
        }
        public void jixiujiedan(string db, HttpContext context)
        {
            System.IO.Stream MyStream;
            var file = context.Request.Files["file"];
            string erp = context.Request["erp"];
            string filename = context.Request["filename"];
            string 客户名称 = context.Request["客户名称"];
            string 工件名称 = context.Request["工件名称"];
            string 加工内容 = context.Request["加工内容"];
            string 接单编号 = context.Request["接单编号"];
            string 数量 = context.Request["数量"];
            string 联系人 = context.Request["联系人"];
            string 生产方式 = context.Request["生产方式"];
            string 要求完成日期 = context.Request["要求完成日期"];
            string 产品类型 = "";
            string 产品类型得到 = context.Request["产品类型"];
            if (产品类型得到.Equals("零件"))
            {
                产品类型 = "机修件";
            }
            else if(产品类型得到.Equals("外协"))
            {
                产品类型 = "机修件部件";
            }
            string 责任人 = context.Request["责任人"];
            string 备注 = context.Request["备注"];
            string 部门 = context.Request["部门"];
            string 接单日期 = DateTime.Now.ToShortDateString().ToString();
            string 申请号 = context.Request["申请号"];


            string yonghu = context.Request["yonghu"];
            int FileLen = file.ContentLength;
            string 附件名称 = file.FileName.Split('.')[0] ;
            string 附件类型 = file.FileName.Split('.')[1];
            byte[] bytes = new byte[FileLen];
            MyStream = file.InputStream;
            MyStream.Read(bytes, 0, FileLen);
            BinaryReader read = new BinaryReader(MyStream);
            read.Read(bytes, 0, Convert.ToInt32(FileLen));
            DateTime shijian1 = DateTime.Now;
           
            var sql = "insert into tb_caigouliaodan(附件,附件名称,附件类型,机修件ERP,编码,接单编号,工作令号,接单日期,预交时间,客户名称,工件名称,名称,加工内容,计量单位,单位,完成,当前状态,机修件数量,实际采购数量,联系人,加工单位备注,责任人,备注,机修客户申请单号,部门,料单类型) " +
    "values(@pic,'" + 附件名称+ "','"+ 附件类型+ "','"+erp+ "','" + erp + "','" + 接单编号 + "','" + 接单编号 + "','" + 接单日期 + "','" + 要求完成日期 + "','" + 客户名称 + "','" + 工件名称 + "','" + 工件名称 +"','" + 加工内容 + "','个','个','机修待审批','待责任人审核','" + 数量 + "','" + 数量 + "','" + 联系人 + "','" + 生产方式 + "','" + 责任人 + "','" + 备注 + "','" + 申请号 + "','" + 部门 + "','" + 产品类型+"')";
            int t = SQLhelp.ExecuteNonquery(sql, db, bytes, CommandType.Text);
            context.Response.ContentType = "text/plain";
            context.Response.Write("success");
        }

        public void jixiujiedandingw(string db, HttpContext context)
        {
            var lon = context.Request["lon"];
            string lat = context.Request["lat"];
            string name = context.Request["name"];
            DateTime shijian1 = DateTime.Now;


            var sql = "insert into test(name,lon,lat,time) " +
    "values('" + name + "','" + lon + "','" + lat + "','" + shijian1+ "')";
            SQLhelp.ExecuteScalar(sql, db, CommandType.Text);
            context.Response.ContentType = "text/plain";
            context.Response.Write(sql);
        }
    }
}