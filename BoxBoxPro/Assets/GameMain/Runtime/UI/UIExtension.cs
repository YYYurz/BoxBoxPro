using GameConfig;
using UnityGameFramework.Runtime;

namespace BB
{
    public static class UIExtension
    {
        public static bool IsUIOpen(this UIComponent uiComponent, int uiFormId)
        {
            var assetName = GameEntry.TableData.DataTableInfo.GetDataTableReader<DTUIWindowTableReader>().GetInfo((uint)uiFormId).AssetPath;
            return uiComponent.HasUIForm(AssetUtility.GetUIFormAsset(assetName));
        }

        public static void CloseUI(this UIComponent uiComponent, int uiFormId)
        {
            var assetName = GameEntry.TableData.DataTableInfo.GetDataTableReader<DTUIWindowTableReader>().GetInfo((uint)uiFormId).AssetPath;
            var uiForm = uiComponent.GetUIForm(AssetUtility.GetUIFormAsset(assetName));
            var uis = uiComponent.GetAllLoadedUIForms();
            if (uiForm == null)
            {
                Log.Error("UIExtension : Try to close UI but failed UI name : " + assetName);
                return;
            }
            uiComponent.CloseUIForm(uiForm);
        }

        public static int? OpenUI(this UIComponent uiComponent, int uiFormId, object userData = null)
        {
            DTUIWindow? uiFormDataTable = GameEntry.TableData.DataTableInfo.GetDataTableReader<DTUIWindowTableReader>().GetInfo((uint)uiFormId);
            var strAssetPath = AssetUtility.GetUIFormAsset(uiFormDataTable.Value.AssetPath);
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
            var uiFormOpenDataInfo = UIFormOpenDataInfo.Create(uiFormId, uiFormDataTable.Value.LuaFile, userData);
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
        
        public static void CloseAllUIForms(this UIComponent uiComponent)
        {
            uiComponent.CloseAllLoadedUIForms();
            uiComponent.CloseAllLoadingUIForms();
        }
    }
}
