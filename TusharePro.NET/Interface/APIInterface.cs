using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TusharePro.Interface
{
    static class APIInterface
    {
        /// <summary>
        /// 表明这是一个数据字段
        /// </summary>
        public class DataFieldAttribute : Attribute
        {
            /// <summary>
            /// 数据字段在Json中的名称
            /// </summary>
            public string NameInJson { get; set; }
        }

        /// <summary>
        /// 获得返回数据集（转换为特定类型）
        /// </summary>
        /// <typeparam name="T">待转换的类型</typeparam>
        /// <param name="response">待转换的API回馈</param>
        /// <returns></returns>
        public static List<T> GetDataSet<T>(this APIResponse response) where T: class, new()
        {
            if (response.ResponseCode == ResponseCode.OK)
            {
                // Get fields in dataset
                Type type = typeof(T);
                Dictionary<int, string> availableColumn = new Dictionary<int, string>();
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public);
                foreach (PropertyInfo property in properties)
                {
                    DataFieldAttribute attribute = property.GetCustomAttribute<DataFieldAttribute>();
                    int indexOfData = response.DataSet.DataColumn.IndexOf(attribute.NameInJson);
                    if (indexOfData != -1)
                    {
                        availableColumn.Add(indexOfData, property.Name);
                    }
                }

                // Get data into dataset
                List<T> dataSet = new List<T>();
                foreach(List<string> row in response.DataSet.DataSet)
                {
                    T newInstance = new T();
                    foreach (KeyValuePair<int, string> col in availableColumn)
                    {
                        type.GetProperty(col.Value).SetValue(newInstance, row[col.Key]);
                    }

                    dataSet.Add(newInstance);
                }

                return dataSet;
            }
            else
            {
                throw new Exception("不可以获得错误返回的数据列表");
            }
        }
    }
}
