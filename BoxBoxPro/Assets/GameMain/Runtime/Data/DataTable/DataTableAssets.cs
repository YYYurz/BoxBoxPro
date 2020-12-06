using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BB
{
    public class DataTableAssets
    {
        private Dictionary<Type, ITableReader> mDicDataTableReaders = new Dictionary<Type, ITableReader>();

        public Dictionary<Type, ITableReader> DicDataTableReaders
		{
			get
			{
				return mDicDataTableReaders;
			}
		}

        public void ParseDataTable(object asset, ParseConfigDataInfo parseConfigDataInfo)
        {
            TextAsset textAsset = asset as TextAsset;
            ITableReader tableReader = parseConfigDataInfo.UserData as ITableReader;
            tableReader.LoadDataFile(textAsset.bytes);
            mDicDataTableReaders[parseConfigDataInfo.ConfigClassType] = tableReader;
            Log.Debug("DataTableAssets: ParseData Success! Type[{0}]", parseConfigDataInfo.ConfigClassType.Name);
        }

        public T GetDataTableReader<T>()
        {
            ITableReader tableReader;
            if (mDicDataTableReaders.TryGetValue(typeof(T), out tableReader))
            {
                return (T)tableReader;
            }

            return default;
        }

        public bool IsDataTableLoaded<T>()
        {
            if (mDicDataTableReaders.ContainsKey(typeof(T)))
            {
                return true;
            }

            return false;
        }
    }
}
