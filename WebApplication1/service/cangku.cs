using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1.service
{
    public class cangku
    {
        public void getcangkuinfo1(string str, HttpContext context)
        {

        }
        public void testip(HttpContext context)
        {
            string sendinfo = "success";
            context.Response.ContentType = "text/plain";
            context.Response.Write(sendinfo);
        }
        public void getcangkuinfo(string str, HttpContext context)
        {
            //string sql6 = "insert into tb_test(用户,时间) values('" + str + "','1')";
            //int i = SQLhelp.innn(sql6, "db_xiangmuguanli", CommandType.Text);

            //string sql4 = "insert into tb_test(用户,时间) values('" + sql + "','4')";
            //int i4 = SQLhelp.innn(sql4, "db_xiangmuguanli", CommandType.Text);
            int length = str.Split('|').Length;
           

            string id = str.Split('|')[0];

            //string sql = "insert into tb_test(用户,时间) values('" + id + "','1')";
            //int i = SQLhelp.innn(sql, "db_xiangmuguanli", CommandType.Text);

            string sql2 = " select 工作令号,项目名称,项目工令号,设备名称,编码,型号,名称,单位,实际采购数量,单位,实际到货数量,库存数量 from tb_caigouliaodan where id='" + id + "'";

           
            DataTable dt = SQLhelp.GetDataTable1(sql2, "db_xiangmuguanli", CommandType.Text);
          


            string sendinfo = "";
            if (dt.Rows.Count == 0)
            {
                sendinfo = "查询不到此数据";
            }
            else
            {
                String txtgongzuolinghao = dt.Rows[0]["工作令号"].ToString();
                String txtxiangmumingcheng = dt.Rows[0]["项目名称"].ToString();
                String txtshebeimingcheng = dt.Rows[0]["设备名称"].ToString();
                String txtbianma = dt.Rows[0]["编码"].ToString();
                String txtxinghao = dt.Rows[0]["型号"].ToString();
                String txtmingcheng = dt.Rows[0]["名称"].ToString();
                String danwei = dt.Rows[0]["单位"].ToString();
                String txtshijicaigoushuliang = dt.Rows[0]["实际采购数量"].ToString();
                String daohuoshuliang = dt.Rows[0]["实际到货数量"].ToString();
                String kucunshuliang = dt.Rows[0]["库存数量"].ToString();
                String xinghao = dt.Rows[0]["型号"].ToString();
                if (kucunshuliang == "")
                {
                    kucunshuliang = "0";
                }
                if (daohuoshuliang == "")
                {
                    daohuoshuliang = "0";
                }
                sendinfo = id + "|工作令号:" + txtgongzuolinghao + "|项目名称:" + txtxiangmumingcheng + "|型号:" + xinghao + "|设备名称:" + txtshebeimingcheng + "|单位:" + danwei +
                    "|实际采购数量:" + txtshijicaigoushuliang + "|实际到货数量:" + daohuoshuliang + "|" + kucunshuliang;
              


            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(sendinfo);

        }
        public void login(string username, string password, HttpContext context)
        {

            string result = "";
            string sql2 = " select * from tb_operator where 用户名='" + username + "'";
            if (Convert.ToString(SQLhelp.ExecuteScalar(sql2, "db_office", CommandType.Text)) == "")
            {

                result = "登录用户不正确";
            }
            else
            {
                DataTable dt = SQLhelp.GetDataTable1(sql2, "db_office", CommandType.Text);

                DataRow dr = dt.Rows[0];
                Console.WriteLine("aa");
                string pass = dt.Rows[0]["密码"].ToString(); //用户名
                string bumen = dt.Rows[0]["部门"].ToString(); //用户名
                string jibie = dt.Rows[0]["级别"].ToString(); //用户名
                if (password != pass)  //若密码不相同
                {
                    result = "{\"rescode\":\"密码错误\"}";
                }
                else
                {
                    result = "{\"rescode\":\"登录成功\",\"部门\":\""+ bumen+"\",\"级别\":\""+jibie+"\"}";
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(result);

        }
        public void cahrushuju(string str, string name, HttpContext context)
        {
            int length = str.Split('|').Length;
            string id = str.Split('|')[0];
            string kuweihao = str.Split('|')[length - 1];
            string number = str.Split('|')[length - 2];
            string sql2 = " select 工作令号,项目名称,设备名称,编码,型号,名称,制造类型,实际采购数量,采购单价,单位,库存数量,合同号,实际到货数量,供方名称 from tb_caigouliaodan where id='" + id + "'";
            DataTable dt = SQLhelp.GetDataTable1(sql2, "tb_caigouliaodan", CommandType.Text);
            String txtgongzuolinghao = dt.Rows[0]["工作令号"].ToString();
            String txtxiangmumingcheng = dt.Rows[0]["项目名称"].ToString();
            String txtshebeimingcheng = dt.Rows[0]["设备名称"].ToString();
            String txtbianma = dt.Rows[0]["编码"].ToString();
            String shijidaohuo = dt.Rows[0]["实际到货数量"].ToString();
            String txtxinghao = dt.Rows[0]["型号"].ToString();
            String txtmingcheng = dt.Rows[0]["名称"].ToString();
            String txtzhizaoleixing = dt.Rows[0]["制造类型"].ToString();
            String hetonghao = dt.Rows[0]["合同号"].ToString();
            String txtshijicaigoushuliang = dt.Rows[0]["实际采购数量"].ToString();
            double danjia = Convert.ToDouble(dt.Rows[0]["采购单价"]);
            double zongjia = Convert.ToDouble(danjia) * Convert.ToDouble(dt.Rows[0]["实际采购数量"]);
            String kucun_shuliang = dt.Rows[0]["库存数量"].ToString();
            String gongfangmingchen = dt.Rows[0]["供方名称"].ToString();
            if (kucun_shuliang == "")
            {
                kucun_shuliang = "0";
            }
            if (shijidaohuo == "")
            {
                shijidaohuo = "0";
            }
            kucun_shuliang = Convert.ToDouble(kucun_shuliang) + Convert.ToDouble(number) + "";
            String shijidaohushuliang = Convert.ToDouble(number) + Convert.ToDouble(shijidaohuo) + "";
            if (Convert.ToDouble(txtshijicaigoushuliang) < Convert.ToDouble(shijidaohushuliang))
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("数量过大,请重新输入" + "|||实际到货数量" + shijidaohushuliang + "|||实际采购数量" + txtshijicaigoushuliang);
            }
            else
            {
                string sql1 = "";
                if (txtshijicaigoushuliang == shijidaohushuliang)
                {
                    sql1 = "update tb_caigouliaodan set 实际到货数量='" + shijidaohushuliang + "',库位号='" + kuweihao + "',库存数量='" + kucun_shuliang + "',仓库确认=1,仓库确认时间='" + DateTime.Now + "',当前状态='9已到货'  where id='" + id + "'";
                }
                else
                {
                    sql1 = "update tb_caigouliaodan set 实际到货数量='" + shijidaohushuliang + "',库位号='" + kuweihao + "',库存数量='" + kucun_shuliang + "' where id='" + id + "'";
                }

                //string sql = "select 权限管理 from tb_operator where 用户名='" + str + "'";
                SQLhelp.ExecuteScalar(sql1, "db_xiangmuguanli", CommandType.Text);



                string sql13 = "insert into tb_ruku (工作令号,项目名称,设备名称,编码,型号,名称,制造类型,实际采购数量,入库数量,入库时间,入库人,定位,单位,库位号,合同号,采购单价,总价,供方名称) values ('" + txtgongzuolinghao + "','" + txtxiangmumingcheng + "','" + txtshebeimingcheng + "','" + txtbianma + "','" + txtxinghao + "','" + txtmingcheng + "','" + txtzhizaoleixing + "','" + txtshijicaigoushuliang + "','" + number + "','" + DateTime.Now + "','" + name + "','" + id + "','" + dt.Rows[0]["单位"].ToString() + "','" + kuweihao + "','" + hetonghao + "','" + danjia + "','" + zongjia + "','" + gongfangmingchen + "')";
                SQLhelp.ExecuteScalar(sql13, "db_xiangmuguanli", CommandType.Text);

                context.Response.ContentType = "text/plain";
                context.Response.Write("操作成功");
            }

        }
        public void cahchushuju(string str, string name, HttpContext context)
        {
            int length = str.Split('|').Length;
            string id = str.Split('|')[0];
            string kuweihao = str.Split('|')[length - 1];
            string number = str.Split('|')[length - 2];
            string sqlquory = " select 工作令号,项目名称,项目工令号,设备名称,编码,型号,名称,制造类型,实际采购数量,采购单价,合同号,单位,库存数量,出库数量,供方名称 from tb_caigouliaodan where id='" + id + "'";
            DataTable dt = SQLhelp.GetDataTable1(sqlquory, "tb_caigouliaodan", CommandType.Text);
            String txtgongzuolinghao = dt.Rows[0]["工作令号"].ToString();
            String txtxiangmumingcheng = dt.Rows[0]["项目名称"].ToString();
            String txtshebeimingcheng = dt.Rows[0]["设备名称"].ToString();
            String txtbianma = dt.Rows[0]["编码"].ToString();
            String txtxinghao = dt.Rows[0]["型号"].ToString();
            String txtmingcheng = dt.Rows[0]["名称"].ToString();
            String txtzhizaoleixing = dt.Rows[0]["制造类型"].ToString();
            String txtshijicaigoushuliang = dt.Rows[0]["实际采购数量"].ToString();
            String kuncunshuliangbefore = dt.Rows[0]["库存数量"].ToString();
            String hetonghao = dt.Rows[0]["合同号"].ToString();
            double danjia = Convert.ToDouble(dt.Rows[0]["采购单价"]);
            double zongjia = Convert.ToDouble(danjia) * Convert.ToDouble(dt.Rows[0]["实际采购数量"]);
            String kuncunshuliang = Convert.ToDouble(kuncunshuliangbefore) - Convert.ToDouble(number) + "";
            String gongfangmingchen = dt.Rows[0]["供方名称"].ToString();
            String chukushuliang = dt.Rows[0]["出库数量"].ToString();
            if (chukushuliang == "")
            {
                chukushuliang = "0";
            }
            chukushuliang = Convert.ToDouble(chukushuliang) + Convert.ToDouble(number) + "";
            string sql1 = "";
            //if (kuncunshuliang == "0")
            if (chukushuliang == txtshijicaigoushuliang)
            {
                sql1 = "update tb_caigouliaodan set 出库数量='" + chukushuliang + "',库存数量='" + kuncunshuliang + "',出库确认=1,出库时间='" + DateTime.Now + "',当前状态='10已出库'  where id='" + id + "'";
            }
            else
            {
                sql1 = "update tb_caigouliaodan set 出库数量='" + chukushuliang + "',库存数量='" + kuncunshuliang + "',出库时间 = '" + DateTime.Now + "'  where id='" + id + "'";

            }

            SQLhelp.ExecuteScalar(sql1, "db_xiangmuguanli", CommandType.Text);



            string sql13 = "insert into tb_chuku (工作令号,项目名称,设备名称,编码,型号,名称,制造类型,实际采购数量,出库数量,出库时间,出库人,定位,单位,合同号,采购单价,总价,供方名称) values ('" + txtgongzuolinghao + "','" + txtxiangmumingcheng + "','" + txtshebeimingcheng + "','" + txtbianma + "','" + txtxinghao + "','" + txtmingcheng + "','" + txtzhizaoleixing + "','" + txtshijicaigoushuliang + "','" + number + "','" + DateTime.Now + "','" + name + "','" + id + "','" + dt.Rows[0]["单位"].ToString() + "','" + hetonghao + "','" + danjia + "','" + zongjia + "','" + gongfangmingchen + "')";
            SQLhelp.ExecuteScalar(sql13, "db_xiangmuguanli", CommandType.Text);

            context.Response.ContentType = "text/plain";
            context.Response.Write("出库成功");
        }
        public void test(string sql, HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            context.Response.Write(sql);
        }
    }
}