// TusharePro.NET
// Copyright(C) 2021 Aperture Electronic
// API Copyright(C) Tushare which is following BSD-3-Clause License.

//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or any later version.

//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TusharePro.Interface
{
    public static class APIInterface
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

            /// <summary>
            /// 枚举字段的别名映射表
            /// </summary>
            public string EnumNameMap { get; set; }

            /// <summary>
            /// 该字段要求字符串到日期时间转换
            /// </summary>
            public bool DateTimeConvert { get; set; }
        }

        /// <summary>
        /// 获得该类所有数据字段的名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static List<string> GetAllDataFieldName(Type T)
        {
            if (T.IsClass)
            {
                List<string> fields = new List<string>();

                PropertyInfo[] properties = T.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    DataFieldAttribute attribute = property.GetCustomAttribute<DataFieldAttribute>();
                    if (attribute is null)
                    {
                        break;
                    }

                    fields.Add(attribute.NameInJson);
                }

                return fields;
            }
            else
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 数据字段
        /// </summary>
        private struct DataField
        {
            /// <summary>
            /// 数据字段名称
            /// </summary>
            public string DataFieldName;
            /// <summary>
            /// 数据字段特性
            /// </summary>
            public DataFieldAttribute Attriubte;
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
                Dictionary<int, DataField> availableColumn = new Dictionary<int, DataField>();
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    DataFieldAttribute attribute = property.GetCustomAttribute<DataFieldAttribute>();
                    if (attribute is null)
                    {
                        break;
                    }

                    int indexOfData = response.DataSet.DataColumn.IndexOf(attribute.NameInJson);
                    if (indexOfData != -1)
                    {
                        availableColumn.Add(indexOfData, new DataField() { DataFieldName = property.Name, Attriubte = attribute});
                    }
                }

                // Get data into dataset
                List<T> dataSet = new List<T>();
                foreach(List<string> row in response.DataSet.DataSet)
                {
                    T newInstance = new T();
                    foreach (KeyValuePair<int, DataField> col in availableColumn)
                    {
                        string str = row[col.Key];
                        DataFieldAttribute attribute = col.Value.Attriubte;
                        if (attribute.DateTimeConvert)
                        {
                            // Field type is datetime, it need convert form string
                            if (str is null)
                            {
                                type.GetProperty(col.Value.DataFieldName).SetValue(newInstance, null);
                            }
                            else
                            {
                                type.GetProperty(col.Value.DataFieldName).SetValue(newInstance,
                                DateTime.ParseExact(row[col.Key], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture));
                            }
                        }
                        else if (!(attribute.EnumNameMap is null))
                        {
                            // Field type is enum with name, convert string to the enum
                            int index = new List<string>(attribute.EnumNameMap.Split(',')).IndexOf(str??"");
                            if (index >= 0)
                            {
                                type.GetProperty(col.Value.DataFieldName).SetValue(newInstance, index);
                            }
                            else
                            {
                                throw new Exception("在枚举字段中获得了预期以外的值");
                            }
                        }
                        else
                        {
                            type.GetProperty(col.Value.DataFieldName).SetValue(newInstance, str);
                        }
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
