// using System;
// using System.Collections.Generic;
// using GameFramework;
// using GameFramework.Event;
// using GameFramework.Resource;
// using Hr.Net;
// using UnityEngine.U2D;
// using UnityGameFramework.Runtime;
// using XLua;
// /// <summary>
// /// Lua调用的常用方法 ( 需要Wrap!)
// /// </summary>
//
// namespace BB
// {
//     public class LuaCallStatic
//     {
//         #region UIForm
//         /// <summary>
//         /// 关闭UIFrom
//         /// </summary>
//         public static void LuaCloseForm(int seriaId)
//         {
//             if (GameEntry.UI.HasUIForm(seriaId))
//             {
//                 GameEntry.UI.CloseUIForm(seriaId);
//             }
//         }
//
//         /// <summary>
//         /// 关闭UIFrom
//         /// </summary>
//         public static void LuaCloseForm(int seriaId, object obj = null)
//         {
//             GameEntry.UI.CloseUIForm(seriaId, obj);
//         }
//
//         /// <summary>
//         /// 打开UIFrom
//         /// </summary>
//         /// <param name="uiFormId">UI ID</param>
//         /// <param name="userData">用户自定义数据</param>
//         /// <returns></returns>
//         public static int? LuaOpenForm(int uiFormId, object userData = null)
//         {
//             return GameEntry.UI.OpenUIForm(uiFormId, userData);
//         }
//
//         /// <summary>
//         /// 打开UITipsFrom
//         /// </summary>
//         /// <param name="strTips">提示文字</param>
//         /// <returns></returns>
//         public static int? LuaShowUITips(string strTips = null)
//         {
//             return GameEntry.UI.ShowUITips(strTips);
//         }
//
//         /// <summary>
//         /// 增加弹框UI到序列
//         /// </summary>
//         /// <param name="nFormID"></param>
//         /// <param name="data"></param>
//         public static void AddPopupUIToSequence(int nFormID, object data)
//         {
//             GameEntry.GameLogic.SeqLogic.UIFormPopSequence.AddPopupUI(nFormID, data);
//         }
//         #endregion
//
//
//         #region GameLogic
//         /// <summary>
//         /// 暂停游戏
//         /// </summary>
//         public static void PauseGame(int nPauseType)
//         {
//             GameEntry.GameLogic.PauseGameLogic(0.0f, nPauseType);
//         }
//
//         /// <summary>
//         /// 恢复游戏
//         /// </summary>
//         public static void ResumeGame(int nPauseType)
//         {
//             GameEntry.GameLogic.ResumeGameLogic(nPauseType);
//         }
//         #endregion
//
//
//         #region GameData
//         public static PlayerDataModel GetPlayerData()
//         {
//             return GameEntry.GameData.PlayerData;
//         }
//
//         public static AccountSetting GetAccountSettingData()
//         {
//             return GameEntry.GameData.AccountSettingData;
//         }
//
//         public static SystemSetting GetSystemSettingData()
//         {
//             return GameEntry.GameData.SystemSettingData;
//         }
//
//         public static ServerDataModel GetServerData()
//         {
//             return GameEntry.GameData.ServerData;
//         }
//
//         public static GameServerDataCache GetServerCacheData()
//         {
//             return GameEntry.GameData.ServerCacheData;
//         }
//
//         public static TollgateDataModel GetTollgateData()
//         {
//             return GameEntry.GameData.TollgateData;
//         }
//
//         public static DeviceDataModel GetDeviceData()
//         {
//             return GameEntry.GameData.DeviceData;
//         }
//
//         public static SettingComponent GetGameSetting()
//         {
//             return GameEntry.Setting;
//         }
//
//         #endregion
//
//         #region EVENT
//         public static void AddEvent(int eventId, EventHandler<GameEventArgs> onEventHandler)
//         {
//             GameEntry.Event.Subscribe(eventId, onEventHandler);
//         }
//
//         public static void RemoveEvent(int eventId, EventHandler<GameEventArgs> onEventHandler)
//         {
//             GameEntry.Event.Unsubscribe(eventId, onEventHandler);
//         }
//
//         /// <summary>
//         /// 发送C#层事件 参数非装箱
//         /// </summary>
//         /// <param name="eventId"></param>
//         /// <param name="sender"></param>
//         /// <param name="nParam1"></param>
//         /// <param name="nParam2"></param>
//         /// <param name="nParam3"></param>
//         public static void FireEvent(int eventId, string sender, int nParam1 = 0, int nParam2 = 0, int nParam3 = 0)
//         {
//             GameEntry.Event.Fire(sender, ReferencePool.Acquire<LuaCSharpEventArgs>().Fill(eventId, sender, nParam1, nParam2, nParam3));
//         }
//
//         /// <summary>
//         /// 发送C#层事件 参数装箱
//         /// </summary>
//         /// <param name="eventId"></param>
//         /// <param name="sender"></param>
//         /// <param name="nParam1"></param>
//         /// <param name="nParam2"></param>
//         /// <param name="nParam3"></param>
//         public static void FireEventBox(int eventId, string sender, object[] param = null)
//         {
//             GameEntry.Event.Fire(sender, ReferencePool.Acquire<LuaCSharpEventArgs>().Fill(eventId, sender, param));
//         }
//         #endregion
//
//         #region 资源加载
//         /// <summary>
//         /// todo 加载Atlas
//         /// </summary>
//         public static void LoadSpriteAtlas(string assetName, Action<Object> assetAction)
//         {
//             string assetPath = Utility.Text.Format(Constant.AssetPath.SpriteAtlasPath, assetName);
//             LoadAsset(assetPath, assetAction);
//         }
//
//         /// <summary>
//         /// todo 加载Prefab
//         /// </summary>
//         public static void LoadPrefab(string assetName, Action<Object> assetAction)
//         {
//             string assetPath = Utility.Text.Format(Constant.AssetPath.PrefabPath, assetName);
//             LoadAsset(assetPath, assetAction);
//         }
//         /// <summary>
//         /// todo 通用的加载 
//         /// </summary>
//         /// <param name="assetPath"></param>
//         /// <param name="assetAction"></param>
//         public static void LoadAsset(string assetPath, Action<Object> assetAction)
//         {
//             LoadAssetCallbacks mAtlasCallBack = new LoadAssetCallbacks(LuaLoadAssetCallback.LoadSuccessCallback, LuaLoadAssetCallback.LoadFailureCallback);
//             LuaLoadAssetCallback userData = ReferencePool.Acquire<LuaLoadAssetCallback>();
//             userData.Fill(assetAction);
//             GameEntry.Resource.LoadAsset(assetPath, mAtlasCallBack, userData);
//         }
//
//         public static void UnloadAsset(object asset)
//         {
//             GameEntry.Resource.UnloadAsset(asset);
//         }
//
//         public static string GetDictionary(string strKey)
//         {
//             return GameEntry.Localization.GetString(strKey).Replace("\\n", "\n");
//         }
//         #endregion
//
//         #region 新手引导
//         public static bool IsDoNoviceGuide(int nStepIndex)
//         {
//             GameEnum.NOVICE_GUIDE_STEPS step = (GameEnum.NOVICE_GUIDE_STEPS)nStepIndex;
//             return NoviceGuideManager.Instance.IsDoNoviceGuide(step);
//         }
//
//         public static void SetDoNoviceGuide(int nStepIndex)
//         {
//             GameEnum.NOVICE_GUIDE_STEPS step = (GameEnum.NOVICE_GUIDE_STEPS)nStepIndex;
//             NoviceGuideManager.Instance.SetDoNoviceGuide(step);
//         }
//
//         public static void SendGetNoviceGuideReward(int nStepIndex, bool bIsInRoom)
//         {
//             NetSenderHelper.SendGetNewGuideRewardReq(nStepIndex, bIsInRoom);
//         }
//
//         /// <summary>
//         /// 开始boss引导假关卡
//         /// </summary>
//         public static void LuaStartBossNoviceGuide()
//         {
//             NoviceGuideManager.Instance.StartBossNoviceGuide();
//         }
//
//         #endregion
//
//         #region 支付
//         /// <summary>
//         /// 支付
//         /// </summary>
//         public static void DoRequestPay(string payTppe, string productType, string productID, string price, string produceName)
//         {
//             DoPaySingleItemInfo singlePayInfo = new DoPaySingleItemInfo();
//             singlePayInfo.PayChannelType = payTppe;
//             singlePayInfo.ProductType = productType;
//             singlePayInfo.ProductID = productID;
//             singlePayInfo.Price = price;
//             singlePayInfo.ProductName = produceName;
//             GameEntry.Recharge.DoRecharge(singlePayInfo);
//         }
//         #endregion
//
//         #region 网络
//         public static void LuaSendPacket(uint nCommand, byte[] bytes, int nHttpFlag = 0)
//         {
//             NetworkChannelHelper.Instance.LuaSendPacket(nCommand, bytes, nHttpFlag);
//         }
//
//         #endregion
//
//         #region MASK
//
//         /// <summary>
//         /// 展示遮罩
//         /// </summary>
//         public static void ShowMask(string key)
//         {
//             UIMaskController.Instance.ShowMask(key);
//         }
//
//         /// <summary>
//         /// 隐藏遮罩
//         /// </summary>
//         public static void HideMask(string key)
//         {
//             UIMaskController.Instance.HideMask(key);
//         }
//
//         /// <summary>
//         /// 遮罩的点击事件
//         /// </summary>
//         public static void OnClickMask()
//         {
//             UIMaskController.Instance.OnClickMask();
//         }
//         #endregion
//
//
//         #region 动画
//         /// <summary>
//         /// 按钮公用动画
//         /// </summary>
//         public static void AddButtonTween(UnityEngine.GameObject go)
//         {
//             CommonButtonTween.AddTween(go);
//         }
//         #endregion
//
//
//         #region Sound
//
//         /// <summary>
//         /// 播放音效
//         /// </summary>
//         public static void LuaPlaySound(int soundId, object userData = null, Entity bindingEntity = null)
//         {
//             GameEntry.Sound.PlaySound(soundId, userData, bindingEntity);
//         }
//
//         /// <summary>
//         /// 静音/开启音乐group
//         /// </summary>
//         public static void LuaMute(string soundGroupName, bool mute)
//         {
//             GameEntry.Sound.Mute(soundGroupName, mute);
//         }
//         #endregion
//
//         #region app地址
//         /// <summary>
//         /// app地址
//         /// </summary>
//         public static string LuaAppPath()
//         {
//             return Hr.AssetUtility.AppPath;
//         }
//
//         #endregion
//
//         #region Config
//
//         /// <summary>
//         /// 从指定全局配置项中读取字符串值。
//         /// </summary>
//         /// <param name="configName">要获取全局配置项的名称。</param>
//         /// <param name="defaultValue">当指定的全局配置项不存在时，返回此默认值。</param>
//         /// <returns>读取的字符串值。</returns>
//         public static string LuaGetConfigString(string configName, string defaultValue)
//         {
//             return GameEntry.Config.GetString(configName, defaultValue);
//         }
//
//         /// <summary>
//         /// 从指定全局配置项中读取浮点数值。
//         /// </summary>
//         /// <param name="configName">要获取全局配置项的名称。</param>
//         /// <param name="defaultValue">当指定的全局配置项不存在时，返回此默认值。</param>
//         /// <returns>读取的浮点数值。</returns>
//         public static float LuaGetConfigFloat(string configName, float defaultValue)
//         {
//             return GameEntry.Config.GetFloat(configName, defaultValue);
//         }
//
//         /// <summary>
//         /// 从指定全局配置项中读取整数值。
//         /// </summary>
//         /// <param name="configName">要获取全局配置项的名称。</param>
//         /// <param name="defaultValue">当指定的全局配置项不存在时，返回此默认值。</param>
//         /// <returns>读取的整数值。</returns>
//         public static int LuaGetConfigInt(string configName, int defaultValue)
//         {
//             return GameEntry.Config.GetInt(configName, defaultValue);
//         }
//
//         /// <summary>
//         /// 从指定全局配置项中读取布尔值。
//         /// </summary>
//         /// <param name="configName">要获取全局配置项的名称。</param>
//         /// <param name="defaultValue">当指定的全局配置项不存在时，返回此默认值。</param>
//         /// <returns>读取的布尔值。</returns>
//         public static bool LuaGetConfigBool(string configName, bool defaultValue)
//         {
//             return GameEntry.Config.GetBool(configName, defaultValue);
//         }
//         #endregion
//
//
//         #region Setting
//         /// <summary>
//         /// 从指定游戏配置项中读取字符串值。
//         /// </summary>
//         /// <param name="settingName">要获取游戏配置项的名称。</param>
//         /// <returns>读取的字符串值。</returns>
//         public static string LuaGetSettingString(string settingName)
//         {
//             return GameEntry.Setting.GetString(settingName);
//         }
//
//         /// <summary>
//         /// 向指定游戏配置项写入字符串值。
//         /// </summary>
//         /// <param name="settingName">要写入游戏配置项的名称。</param>
//         /// <param name="value">要写入的字符串值。</param>
//         public static void LuaSetSettingString(string settingName, string value)
//         {
//             GameEntry.Setting.SetString(settingName, value);
//         }
//
//         /// <summary>
//         /// 从指定游戏配置项中读取布尔值。
//         /// </summary>
//         /// <param name="settingName">要获取游戏配置项的名称。</param>
//         /// <returns>读取的布尔值。</returns>
//         public static bool LuaGetSettingBool(string settingName)
//         {
//             return GameEntry.Setting.GetBool(settingName);
//         }
//
//         /// <summary>
//         /// 向指定游戏配置项写入布尔值。
//         /// </summary>
//         /// <param name="settingName">要写入游戏配置项的名称。</param>
//         /// <param name="value">要写入的布尔值。</param>
//         public static void LuaSetSettingBool(string settingName, bool value)
//         {
//             GameEntry.Setting.SetBool(settingName, value);
//         }
//
//         /// <summary>
//         /// 从指定游戏配置项中读取整数值。
//         /// </summary>
//         /// <param name="settingName">要获取游戏配置项的名称。</param>
//         /// <returns>读取的整数值。</returns>
//         public static int LuaGetSettingInt(string settingName)
//         {
//             return GameEntry.Setting.GetInt(settingName);
//         }
//
//
//         /// <summary>
//         /// 向指定游戏配置项写入整数值。
//         /// </summary>
//         /// <param name="settingName">要写入游戏配置项的名称。</param>
//         /// <param name="value">要写入的整数值。</param>
//         public static void LuaSetSettingInt(string settingName, int value)
//         {
//             GameEntry.Setting.SetInt(settingName, value);
//         }
//
//         #endregion
//
//
//         #region DeviceDataModel
//         public static string GetTDString()
//         {
//             return GameEntry.GameData.DeviceData.TDString;
//         }
//         #endregion
//     }
//
// #if UNITY_EDITOR
//     public static class LuaCallStaticExporter
//     {
//         [LuaCallCSharp]
//         public static List<Type> LuaCallCSharp = new List<Type>() {
//             typeof (LuaCallStatic)
//         };
//     }
// #endif
// }