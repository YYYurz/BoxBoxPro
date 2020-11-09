//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BB
{
    [DisallowMultipleComponent]
    public class GameDataComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 数据表
        /// </summary>
        public DataTableAssets DataTableInfo { get; private set; } = new DataTableAssets();
    }
}