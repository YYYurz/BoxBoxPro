using GameConfig;
using GameFramework.DataTable;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BB
{
    public class ProcedureChangeScene : ProcedureBase
    {
        private const int MenuSceneId = 1;

        private bool _isChangeSceneComplete = false;
        private int _backgroundMusicId;
        private int _sceneId;

        private GameObject m_SceneObj = null;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Debug.Log("Enter ProcedureChangeScene  -- yzr");

            // UISplash = GameObject.Find("Splash");
            
            _isChangeSceneComplete = false;
            m_SceneObj = null;

            GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            GameEntry.Event.Subscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            GameEntry.Event.Subscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);

            // 停止所有声音
            GameEntry.Sound.StopAllLoadingSounds();
            GameEntry.Sound.StopAllLoadedSounds();

            // 隐藏所有实体
            GameEntry.Entity.HideAllLoadingEntities();
            GameEntry.Entity.HideAllLoadedEntities();

            // 卸载所有场景
            var loadedSceneAssetNames = GameEntry.Scene.GetLoadedSceneAssetNames();
            foreach (var loadedScene in loadedSceneAssetNames)
            {
                GameEntry.Scene.UnloadScene(loadedScene);
            }

            // 还原游戏速度
            // GameEntry.Base.ResetNormalGameSpeed();

            _sceneId = procedureOwner.GetData<VarInt>(Constant.ProcedureData.NextSceneId).Value;
            var sceneData = GameEntry.TableData.DataTableInfo.GetDataTableReader<DTSceneTableReader>().GetInfo((uint)_sceneId);
            GameEntry.Scene.LoadScene(AssetUtility.GetSceneAsset(sceneData.AssetName), Constant.AssetPriority.SceneAsset, this);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            GameEntry.Event.Unsubscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            GameEntry.Event.Unsubscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!_isChangeSceneComplete)
            {
                return;
            }
            
            switch (_sceneId)
            {
                case (int)GameEnum.SCENE_TYPE.MainLobby:
                    ChangeState<ProcedureMain>(procedureOwner);
                    break;
            }
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            var ne = (LoadSceneSuccessEventArgs)e;

            Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);

            if (_backgroundMusicId > 0)
            {
                // GameEntry.Sound.PlayMusic(m_BackgroundMusicId);
            }

            _isChangeSceneComplete = true;
        }


        private void OnLoadSceneFailure(object sender, GameEventArgs e)
        {
            LoadSceneFailureEventArgs ne = (LoadSceneFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.ErrorMessage);
        }

        private void OnLoadSceneUpdate(object sender, GameEventArgs e)
        {
            LoadSceneUpdateEventArgs ne = (LoadSceneUpdateEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' update, progress '{1}'.", ne.SceneAssetName, ne.Progress.ToString("P2"));
        }

        private void OnLoadSceneDependencyAsset(object sender, GameEventArgs e)
        {
            LoadSceneDependencyAssetEventArgs ne = (LoadSceneDependencyAssetEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' dependency asset '{1}', count '{2}/{3}'.", ne.SceneAssetName, ne.DependencyAssetName, ne.LoadedCount.ToString(), ne.TotalCount.ToString());
        }
    }
}
