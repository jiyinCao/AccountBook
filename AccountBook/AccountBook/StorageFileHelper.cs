using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace AccountBook
{
    public class StorageFileHelper
    {
        // 存储记账数据的文件夹名称
        private const string FolderName="Data";
        // 存储记账数据的文件夹对象
        private static IStorageFolder DataFolder = null;
        // 获取存储记账数据的文件夹对象
        private static async Task<IStorageFolder> GetDataFolder()
        {
            // 获取存储数据的文件夹
            if (DataFolder == null)
            {
                DataFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(FolderName, CreationCollisionOption.OpenIfExists);
            }
            return DataFolder;
        }
        
        public static async Task<string> ReadFileAsync(string fileName)
        {
            string text;
            try
            {
                 // 获取存储数据的文件夹
                
            }
            catch (Exception e)
            {
                text = "文件读取错误：" + e.Message;
            }
            return text;
        }

        public static async Task WriteFileAsync(string fileName, string content)
        {
           
        }
        public static async Task WriteAsync<T>(T data, string filename)
        {
            // 获取存储数据的文件夹
           

        }
        public static async Task<T> ReadAsync<T>(string filename)
        {
            // 获取实体类类型实例化一个对象
            

            return sessionState_;
        }
    }
}
