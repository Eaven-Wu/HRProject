using DAO;
using IBLL;
using IDAO;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace IocContainer
{
    public class IocCreate
    {
        #region 一级机构设置 (config_file_first_kind)
        public static config_file_first_kind_IDAO Create_config_file_first_kind_DAO()
        {
            UnityContainer ioc = new UnityContainer();
            ioc.RegisterType<config_file_first_kind_IDAO, config_file_first_kind_DAO>();
            return ioc.Resolve<config_file_first_kind_IDAO>();
        }

        public static config_file_first_kind_IBLL Create_config_file_first_kind_BLL()
        {
            UnityContainer ioc = new UnityContainer();
            //把Unity文件转换为文件对象
            ExeConfigurationFileMap ef = new ExeConfigurationFileMap();
            ef.ExeConfigFilename = @"D:\AProject\HR\HR\Unity.config";
            //把文件对象转换为配置对象
            Configuration cf = ConfigurationManager.OpenMappedExeConfiguration(ef, ConfigurationUserLevel.None);
            //读取Unity节点(业务逻辑层的块)
            UnityConfigurationSection cs = (UnityConfigurationSection)cf.GetSection("unity");
            //容器里加载业务逻辑层的配置块
            ioc.LoadConfiguration(cs, "containerOne");
            //根据配置文件的name来定位对象
            return ioc.Resolve<config_file_first_kind_IBLL>("config_file_first_kind_BLL");
        }
        #endregion
    }
}
