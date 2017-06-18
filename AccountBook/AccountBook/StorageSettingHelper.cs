using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AccountBook
{
    public class StorageSettingHelper
    {
        private static ApplicationDataContainer localSettings = null;
        private static ApplicationDataContainer dataContainer = null;
        // 容器的名称
        private  const string containerName = "DataContainer";


        public static ApplicationDataContainer GetDataContainer()
        {
            if (localSettings==null)
            {
                // 获取根容器
                localSettings = ApplicationData.Current.LocalSettings;
            }
            if (dataContainer==null)
            {
                // 如果容器不存在则新建
                dataContainer = localSettings.CreateContainer(containerName,
                    ApplicationDataCreateDisposition.Always);
            }
            return dataContainer;
        }
       

        
        public static bool Exist(string key)
        {
            return GetDataContainer().Values.Keys.Contains(key);
        }
       
        public static object Load(string key)
        {
            if (Exist(key))
            {
                return GetDataContainer().Values[key];
            }
            return null;
        }
        
        public static void Save(string key, object obj)
        {
            GetDataContainer().Values[key] = obj; 
        }

    }
}
