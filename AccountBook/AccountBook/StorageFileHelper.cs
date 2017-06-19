IStorageFolder applicationFolder = await GetDataFolder();
                // 根据文件名获取文件夹里面的文件
                IStorageFile storageFile = await applicationFolder.GetFileAsync(fileName);
                // 打开文件获取文件的数据流
                IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
                // 使用StreamReader读取文件的内容，需要将IRandomAccessStream对象转化为Stream对象来初始化StreamReader对象
                using (StreamReader streamReader = new StreamReader(accessStream.AsStreamForRead((int)accessStream.Size)))
                {
                    text = streamReader.ReadToEnd();
                }﻿using System;
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
                IStorageFolder applicationFolder = await GetDataFolder();
                // 根据文件名获取文件夹里面的文件
                IStorageFile storageFile = await applicationFolder.GetFileAsync(fileName);
                // 打开文件获取文件的数据流
                IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
                // 使用StreamReader读取文件的内容，需要将IRandomAccessStream对象转化为Stream对象来初始化StreamReader对象
                using (StreamReader streamReader = new StreamReader(accessStream.AsStreamForRead((int)accessStream.Size)))
                {
                    text = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                text = "文件读取错误：" + e.Message;
            }
            return text;
        }

        public static async Task WriteFileAsync(string fileName, string content)
        {
           // 获取存储数据的文件夹
            IStorageFolder applicationFolder = await GetDataFolder();
            // 在文件夹里面创建文件，如果文件存在则替换掉
            IStorageFile storageFile = await applicationFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            // 使用FileIO类把字符串信息写入文件
            await FileIO.WriteTextAsync(storageFile, content);
        }
        public static async Task WriteAsync<T>(T data, string filename)
        {
            // 获取存储数据的文件夹
           IStorageFolder applicationFolder = await GetDataFolder();

            StorageFile file = await applicationFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            await file.DeleteAsync();
            file = await applicationFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            // 获取文件的数据流来进行操作
            using (IRandomAccessStream raStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
                {
                    // 创建序列化对象写入数据
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(outStream.AsStreamForWrite(), data);
                    await outStream.FlushAsync();
                }
            }

        }
        public static async Task<T> ReadAsync<T>(string filename)
        {
            // 获取实体类类型实例化一个对象
            T sessionState_ = default(T);
            // 获取存储数据的文件夹
            IStorageFolder applicationFolder = await GetDataFolder();
            StorageFile file = await applicationFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            if (file == null)
                return sessionState_;
            try
            {
                using (IInputStream inStream = await file.OpenSequentialReadAsync())
                {
                    // 反序列化XML数据
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    sessionState_ = (T)serializer.ReadObject(inStream.AsStreamForRead());
                }
            }
            catch(Exception)
            {

            }

            return sessionState_;
        }
    }
}
