using GameFramework;
using System;

namespace BB
{
    public class PreloadAssetInfo
    {
        /// <summary>
        /// 资源路径
        /// </summary>
        public string AssetPath { get; set; }

        public int SerialID { get; set; }

        public int UIFormID { get; set; }

        /// <summary>
        /// 资源加载类型
        /// </summary>
        public GameEnumType.GAMEASSET_TYPE AssetPreloadType { get; set; } = GameEnumType.GAMEASSET_TYPE.PAT_NORMAL;

        /// <summary>
        /// 当前资源状态
        /// </summary>
        public GameEnumType.PRELOADASSET_STATUS AssetPreloadStatus { get; set; } = GameEnumType.PRELOADASSET_STATUS.PS_UNSTART;

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type AssetType { get; set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        public object UserData { get; set; } = null;

        public PreloadAssetInfo(string strAssetPath, int nSerialID, GameEnumType.GAMEASSET_TYPE preloadType, Type assetType, object userData)
        {
            AssetPath = strAssetPath;
            SerialID = nSerialID;
            AssetPreloadType = preloadType;
            AssetPreloadStatus = GameEnumType.PRELOADASSET_STATUS.PS_UNSTART;
            AssetType = assetType;
            UserData = userData;
        }
    }
}