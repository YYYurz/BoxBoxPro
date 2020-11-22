using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BB
{
    public class GameEnumType
    {
        /// <summary>
        /// DataComponent数据类型
        /// </summary>
        public enum GAMEASSET_TYPE
        {
            PAT_NORMAL,
            PAT_LUAFILE,
            PAT_SCRIPTABLE,
            PAT_DATATABLE,
            PAT_CONFIGTXT,
            PAT_LOCALIZATION,
            PAT_UIFORM,
            PAT_PREFAB
        }    
        
        public enum PRELOADASSET_STATUS
        {
            PS_UNSTART,
            PS_LOADING,
            PS_LOADED,
        }
    }
}

