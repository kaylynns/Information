using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
namespace IocContainer
{
  public  class IocCreate
    {
        public static T CreateAll<T>(string jieDian, string jieDian2)
        {
            UnityContainer ioc = CreatIoc(jieDian);
            T ib = ioc.Resolve<T>(jieDian2);
            return ib;
        }
        private static UnityContainer CreatIoc(string name)
        {
            UnityContainer ioc = new UnityContainer();
            ExeConfigurationFileMap exf = new ExeConfigurationFileMap();
            exf.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "Unity.config";
            exf.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory+"Unity.config";
            Configuration  cf = ConfigurationManager.OpenMappedExeConfiguration(exf, ConfigurationUserLevel.None);
            UnityConfigurationSection cfs = (UnityConfigurationSection)cf.GetSection("unity");
            ioc.LoadConfiguration(cfs, name);
            return ioc;
        }
     
    }


}
