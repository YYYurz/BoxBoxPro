using GameFramework;
using System;
using System.Collections.Generic;
using UnityGameFramework.Runtime;

namespace BB
{
    public abstract class PreloadAssetList
    {
        protected List<PreloadAssetInfo> mLisAssetInfo = new List<PreloadAssetInfo>();

        public List<PreloadAssetInfo> GetAssetInfoList()
        {
            return mLisAssetInfo;
        }

        public virtual void Clear()
        {
            mLisAssetInfo.Clear();
        }
    }

    public class PreloadLuaFileList : PreloadAssetList
    {
        public void SetLuaFileInfo(List<LuaFileInfo> lisLuaFiles)
        {
            foreach (var item in lisLuaFiles)
            {
                var preAssetInfo = new PreloadAssetInfo(item.AssetName, 0, GameEnumType.GAMEASSET_TYPE.PAT_LUAFILE, null, item.LuaName);
                mLisAssetInfo.Add(preAssetInfo);
            }
        }
    }

    // public class PreloadDataTableList : PreloadAssetList
    // {
    //     public void AddOneAssetInfo(Type type)
    //     {
    //         ITableReader tableReader = (ITableReader)Activator.CreateInstance(type);
    //         var preAssetInfo = new PreloadAssetInfo(tableReader.TablePath
    //             , 0
    //             , GameEnumType.GAMEASSET_TYPE.PAT_SCRIPTABLE
    //             , type
    //             , new ParseConfigDataInfo(type.Name, type, tableReader, GameEnumType.GAMEASSET_TYPE.PAT_DATATABLE));
    //
    //         mLisAssetInfo.Add(preAssetInfo);
    //     }
    // }

    public class PreloadUIFormList : PreloadAssetList
    {
        public void AddOneAssetInfo(int nFormID, object userData = null)
        {
            var preAssetInfo = new PreloadAssetInfo("UIForm" + nFormID
                , 0
                , GameEnumType.GAMEASSET_TYPE.PAT_UIFORM
                , null
                , userData);

            preAssetInfo.UIFormID = nFormID;
            mLisAssetInfo.Add(preAssetInfo);
        }
    }

    public class PreloadPrefabList : PreloadAssetList
    {
        public void AddOneAssetInfo(string assetPath, object userData = null)
        {
            var preAssetInfo = new PreloadAssetInfo(assetPath
                , 0
                , GameEnumType.GAMEASSET_TYPE.PAT_PREFAB
                , null
                , userData);
            mLisAssetInfo.Add(preAssetInfo);
        }

    }
}
