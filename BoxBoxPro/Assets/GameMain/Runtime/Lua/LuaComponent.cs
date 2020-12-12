//------------------------------------------------------------
// Copyright © 2017-2020 Chen Hua. All rights reserved.
// Author: 一条猪儿虫
// Email: 1184923569@qq.com
//------------------------------------------------------------

using GameFramework;
using GameFramework.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using XLua;

/// <summary>
/// Lua组件game
/// </summary>
namespace BB
{
    public class LuaComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 全局唯一Lua虚拟环境
        /// </summary>
        private static LuaEnv _luaEnv;
        private ResourceComponent _resourceComponent;
        private EventComponent _eventComponent;
        // private LuaUpdater _luaUpdater = null;

        // public CSharpLuaDataHelper LuaDataHelper { get; private set; } = new CSharpLuaDataHelper();

        /// <summary>
        /// Lua文件列表信息
        /// </summary>
        public List<LuaFileInfo> LuaFileInfos { get; private set; }

        private Dictionary<string, string> CacheLuaDict { get; set; }
        
        private void Start()
        {
            _resourceComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<ResourceComponent>();
            _eventComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
            if (_resourceComponent == null)
            {
                Log.Error(" m_ResourceComponent is null.");
                
                return;
            }

            CacheLuaDict = new Dictionary<string, string>();
            _luaEnv = new LuaEnv();
            // LuaArrAccessAPI.RegisterPinFunc(_luaEnv.L);
            LuaFileInfos = new List<LuaFileInfo>();
        }

        private void Update()
        {
            if (_luaEnv == null)
            {
                return;
            }
            _luaEnv.Tick();
        }

        private void OnDestroy()
        {
            CacheLuaDict.Clear();
            // if (_luaUpdater != null)
            // {
            //     _luaUpdater.OnDispose();
            // }

            if (_luaEnv == null)
            {
                return;
            }
            _luaEnv.Dispose();
            _luaEnv = null;
        }

        /// <summary>
        /// 加载lua文件列表
        /// </summary>
        public void LoadLuaFilesConfig()
        {
            var callBacks = new LoadAssetCallbacks(OnLoadLuaFilesConfigSuccess, OnLoadLuaFilesConfigFailure);
            Debug.Log("Lua");
            var assetName = AssetUtility.GetLuaFileConfig();
            _resourceComponent.LoadAsset(assetName, callBacks);
        }

        /// <summary>
        /// 解析Lua文件配置列表
        /// </summary>
        /// <param name="content">配置列表内容</param>
        private void ParseLuaFilesConfig(string content)
        {
            if (LuaFileInfos == null)
            {
                Log.Error("ParseLuaFilesConfig Error!!!!!! LuaFileInfos is null!");
            }
            LuaFileInfos.Clear();
            var contentLines = content.Split('\n');
            var len = contentLines.Length;
            for (var i = 0; i < len; i++)
            {
                if (string.IsNullOrEmpty(contentLines[i]))
                {
                    return;
                }
                var info = GameUtils.DeserializeObject<LuaFileInfo>(contentLines[i]);
                if (info == null)
                {
                    Log.Error("invalid pase result!");
                }
                else
                {
                    LuaFileInfos.Add(info);
                }
            }
        }

        /// <summary>
        /// 初始化 Lua 环境第三方库接口
        /// </summary>
        public void InitLuaEnvExternalInterface()
        {
            if (_luaEnv != null)
            {
                _luaEnv.AddLoader(CustomLoader);
                // _luaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
                // _luaEnv.AddBuildin("lpeg", XLua.LuaDLL.Lua.LoadLpeg);
                // _luaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadPb);
                // _luaEnv.AddBuildin("protobuf.c", XLua.LuaDLL.Lua.LoadProtobufC);
                Log.Debug("InitLuaEnvExternalInterface success!!!");
            }
            else
            {
                Log.Error("InitLuaEnv error! invalid luaenv");
            }
        }
        
        public void InitLuaCommonScript()
        {
            if (_luaEnv == null)
            {
                return;
            }
            // RequireLuaFile(Constant.Lua.LuaCommonMainScript);
            // _luaUpdater = gameObject.GetComponent<LuaUpdater>();
            // if (_luaUpdater == null)
            // {
            //     _luaUpdater = gameObject.AddComponent<LuaUpdater>();
            // }
            // _luaUpdater.OnInit(_luaEnv);
        }
        
        public void StartRunLuaLogic()
        {
            if (_luaEnv == null)
            {
                return;
            }
            RequireLuaFile(Constant.Lua.LuaGameMainScript);
            SafeDoString("GameMain.Start()");
        }

        /// <summary>
        /// 加载lua文件
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="luaName"></param>
        public void LoadLuaFile(string luaName, string assetName)
        {
            var callBacks = new LoadAssetCallbacks(OnLoadLuaAssetSuccess, OnLoadLuaAssetFailure);
            
            if (CacheLuaDict.TryGetValue(luaName, out var strLuaValue))
            {
                _eventComponent.Fire(this, ReferencePool.Acquire<LoadLuaSuccessEventArgs>().Fill(assetName, luaName, strLuaValue, 0));
                return;
            }
            
            _resourceComponent.LoadAsset(assetName, callBacks, luaName);
        }

        /// <summary>
        /// 执行lua文件
        /// </summary>
        /// <param name="luaName"></param>
        public void DoLuaFile(string luaName)
        {
            string strValue;
            if (CacheLuaDict.TryGetValue(luaName, out strValue))
            {
                SafeDoString(strValue);
            }
            else
            {
                Log.Error("Lua file '{0}' is not load,please execute LoadLuaFile() first.", luaName);
            }
        }

        public void RequireLuaFile(string scriptName)
        {
            SafeDoString(string.Format("require('{0}')", scriptName));
        }

        public void SafeDoString(string scriptContent)
        {
            if (_luaEnv != null)
            {
                try
                {
                    Log.Debug(scriptContent + "SafeDoString");
                    _luaEnv.DoString(scriptContent);
                }
                catch (System.Exception ex)
                {
                    string msg = string.Format("xLua exception : {0}\n {1} scriptContent:{2}", ex.Message, ex.StackTrace, scriptContent);
                    Log.Error(msg, 0);
                }
            }
        }

        /// <summary>
        /// 获取全局类的LuaTable
        /// </summary>
        /// <param name="luaName"></param>
        /// <param name="className"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public LuaTable GetClassLuaTable(string className)
        {
            LuaTable classLuaTable = _luaEnv.Global.Get<LuaTable>(className);
            return classLuaTable;
        }

        /// <summary>
        /// 获取LuaTable
        /// </summary>
        /// <param name="luaName"></param>
        /// <param name="className"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public LuaTable GetLuaTable(string luaName, string className, string tableName)
        {
            if (CacheLuaDict.ContainsKey(luaName))
            {
                LuaTable classLuaTable = _luaEnv.Global.Get<LuaTable>(className);
                LuaTable luaTable = classLuaTable.Get<LuaTable>(tableName);
                classLuaTable.Dispose();
                classLuaTable = null;
                return luaTable;
            }
            else
            {
                Log.Error("Lua file '{0}' is not load,please execute LoadLuaFile() first.", luaName);
                return null;
            }
        }

        /// <summary>
        /// 调用Lua方法
        /// </summary>
        /// <param name="funcName"></param>
        public void CallLuaFunction(LuaTable luaTable, string funcName, params object[] param)
        {
            if (luaTable != null)
            {
                try
                {
                    LuaFunction luaFunction = luaTable.Get<LuaFunction>(funcName);
                    luaFunction.Call(param);
                    luaFunction.Dispose();
                    luaFunction = null;
                }
                catch (Exception exception)
                {
                    Log.Error(exception.Message);
                }
            }
            else
            {
                Log.Error("LuaTable is invalid.");
            }
        }

        /// <summary>
        /// 调用Lua方法
        /// </summary>
        /// <param name="luaName"></param>
        /// <param name="className"></param>
        /// <param name="funcName"></param>
        /// <param name="parms"></param>
        public void CallLuaFunction(string luaName, string className, string funcName, params object[] parms)
        {
            if (CacheLuaDict.ContainsKey(luaName))
            {
                try
                {
                    LuaTable classLuaTable = _luaEnv.Global.Get<LuaTable>(className);
                    LuaFunction luaFunc = classLuaTable.Get<LuaFunction>(funcName);
                    luaFunc.Call(parms);
                    classLuaTable.Dispose();
                    luaFunc.Dispose();
                    classLuaTable = null;
                    luaFunc = null;
                }
                catch (Exception exception)
                {
                    Log.Error(exception.Message);
                }
            }
            else
            {
                Log.Error("Lua file '{0}' is not load,please execute LoadLuaFile() first.", luaName);
            }
        }

        private void OnLoadLuaFilesConfigFailure(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            Log.Info("Load LuaFilesConfig: '{0}' failure.", assetName);
            // _eventComponent.Fire(this, ReferencePool.Acquire<LoadLuaFilesConfigFailureEventArgs>().Fill(assetName, errorMessage));
        }

        private void OnLoadLuaFilesConfigSuccess(string assetName, object asset, float duration, object userData)
        {
            var textAsset = (TextAsset)asset;
            Log.Info("Load LuaFilesConfig: '{0}' success.", assetName);

            string content = textAsset.text;
            //开始解析Lua配置文件列表
            ParseLuaFilesConfig(content);
            _eventComponent.Fire(this, ReferencePool.Acquire<LoadLuaFilesConfigSuccessEventArgs>().Fill(assetName, content));

            _resourceComponent.UnloadAsset(asset);
        }

        private void OnLoadLuaAssetSuccess(string assetName, object asset, float duration, object userData)
        {
            string luaName = (string)userData;

            if (CacheLuaDict.ContainsKey(luaName))
            {
                Log.Warning("CacheLuaDict has exist lua file '{0}'.", luaName);
                return;
            }

            TextAsset textAsset = (TextAsset)asset;
            CacheLuaDict.Add(luaName, textAsset.text);

            // Debug.Log($"Load lua '{luaName}' success. duration:{duration}");
            _eventComponent.Fire(this, ReferencePool.Acquire<LoadLuaSuccessEventArgs>().Fill(assetName, luaName, textAsset.text, duration));
        }

        private void OnLoadLuaAssetFailure(string assetName, string dependencyAssetName, int loadedCount, int totalCount, object userData)
        {
            var luaName = (string)userData;
            var errorMessage = string.Format("Load lua file failed. The file is {0}. ", assetName);
            _eventComponent.Fire(this, ReferencePool.Acquire<LoadLuaFailureEventArgs>().Fill(assetName, luaName, errorMessage));
        }

        private static byte[] CustomLoader(ref string filepath)
        {
             var scriptPath = string.Empty;
             var strLuaName = filepath.Replace(".", "/");
            
             string strLuaContent;
             if (GameEntry.Lua.CacheLuaDict.TryGetValue(strLuaName, out strLuaContent))
             {
                 return Utility.Converter.GetBytes(strLuaContent);
             }
             return null;
        }
    }
}
