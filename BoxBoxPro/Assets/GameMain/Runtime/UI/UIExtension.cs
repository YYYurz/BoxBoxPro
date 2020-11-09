//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using GameConfig;
using GameFramework.DataTable;
using GameFramework.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace BB
{
    public static class UIExtension
    {
        public static bool HasUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            return uiComponent.HasUIForm(uiFormId, uiGroupName);
        }

        public static UGuiForm GetUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            return uiComponent.GetUIForm(uiFormId, uiGroupName);
        }

        
        public static void CloseUIForm(this UIComponent uiComponent, UGuiForm uiForm)
        {
            uiComponent.CloseUIForm(uiForm.UIForm);
        }

        public static int? OpenUIForm(this UIComponent uiComponent, int uiFormId, object userData = null)
        {
            DTUIFormData? uiFormDataTable = GameEntry.GameData.DataTableInfo.GetDataTableReader<DTUIFormDataTableReader>().GetInfo((uint)uiFormId);
            if (uiFormDataTable == null)
            {
                Log.Error("LuaForm Open Error! invalid UIDataTable!!! FormID : {0}", uiFormId);
                return default;
            }
            string strAssetPath = AssetUtility.GetUIFormAsset(uiFormDataTable.Value.AssetPath);
            if (uiFormDataTable.Value.AllowMultiInstance == 0)
            {
                if (uiComponent.IsLoadingUIForm(strAssetPath))
                {
                    return null;
                }
            
                if (uiComponent.HasUIForm(strAssetPath))
                {
                    return null;
                }
            }
            
            UIFormOpenDataInfo uiFormOpenDataInfo = UIFormOpenDataInfo.Create(uiFormId, uiFormDataTable.Value.LuaFile, userData);
            return uiComponent.OpenUIForm(strAssetPath, uiFormDataTable.Value.UIGroupName, Constant.AssetPriority.UIFormAsset, (uiFormDataTable.Value.PauseCoveredUIForm == 1), uiFormOpenDataInfo);
        }

        // public static int? ShowUITips(this UIComponent uiComponent, string strTips)
        // {
        //
        //     DTUIFormData? uiFormDataTable = GameEntry.GameData.DataTableInfo.GetDataTableReader<DTUIFormDataTableReader>().GetInfo((uint)Constant.UIFormID.UITipsForm);
        //     if (uiFormDataTable == null)
        //     {
        //         Log.Error("LuaForm Open Error! invalid UIDataTable!!! FormID : {0}", Constant.UIFormID.UITipsForm);
        //         return default;
        //     }
        //     string strAssetPath = AssetUtility.GetUIFormAsset(uiFormDataTable.Value.AssetPath);
        //
        //     if (uiComponent.IsLoadingUIForm(strAssetPath))
        //     {
        //         return null;
        //     }
        //
        //     if (uiComponent.HasUIForm(strAssetPath))
        //     {
        //         UIFormOpenDataInfo uiFormOpenDataInfoTips = UIFormOpenDataInfo.Create(Constant.UIFormID.UITipsForm, uiFormDataTable.Value.LuaFile, strTips);
        //         uiComponent.GetUIForm(strAssetPath).OnOpen(uiFormOpenDataInfoTips);
        //         GameFramework.ReferencePool.Release(uiFormOpenDataInfoTips);
        //         return null;
        //     }
        //
        //     UIFormOpenDataInfo uiFormOpenDataInfo = UIFormOpenDataInfo.Create(Constant.UIFormID.UITipsForm, uiFormDataTable.Value.LuaFile, strTips);
        //     return uiComponent.OpenUIForm(strAssetPath, uiFormDataTable.Value.UIGroupName, Constant.AssetPriority.UIFormAsset, (uiFormDataTable.Value.PauseCoveredUIForm == 1), uiFormOpenDataInfo);
        // }
        //
        // public static void CloseAllUIForms(this UIComponent uiComponent)
        // {
        //     UIMaskController.Instance.ForceCloseMask();
        //     uiComponent.CloseAllLoadedUIForms();
        //     uiComponent.CloseAllLoadingUIForms();
        // }
    }
}
