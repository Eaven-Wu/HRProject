using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    public class config_file_first_kind
    {                                         
        //ffk_id : 主键，自动增长列 
        public int ffk_id { get; set; }

        //first_kind_id : 一级机构编号     
        public char first_kind_id { get; set; }

        //first_kind_name : 一级机构名称
        public string first_kind_name { get; set; }

        //first_kind_salary_id : 一级机构薪酬发放责任人编号
        public string first_kind_salary_id { get; set; }

        //first_kind_sale_id : 一级机构销售责任人编号
        public string first_kind_sale_id { get; set; }

    }
}
