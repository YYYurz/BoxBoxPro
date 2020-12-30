using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Event;

namespace BB
{
    public class PlayerLogic : TargetableObject
    {
        [SerializeField]
        protected PlayerData myPlayerData;

        private CharacterController characterController;
        private readonly Vector3 gravityDirection = new Vector3(0f, -9.8f, 0f);
        private Vector3 moveDirection = new Vector3(0f, 0f, 0f);

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            myPlayerData = userData as PlayerData;
            if (myPlayerData == null)
            {
                Log.Error("My aircraft data is invalid.");
                return;
            }
            characterController = GetComponent<CharacterController>();
            GetComponent<CameraLogic>().OnStartFollowing();
            
            GameEntry.Event.Subscribe(InputEventArgs.EventId, OnInputEvent);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            
            GameEntry.Event.Unsubscribe(InputEventArgs.EventId, OnInputEvent);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        
            characterController.Move(gravityDirection * Time.deltaTime);
        }

        private void OnInputEvent(object sender, GameEventArgs args)
        {
            Debug.Log("OnInputEvent");
            var e = (InputEventArgs) args;
            if (e == null)
            {
                Log.Error("InputEventArgs is Null or Invalid");
                return;
            }
            moveDirection.Set(e.OffsetX, 0, e.OffsetY);
            moveDirection *= myPlayerData.MoveSpeed;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }
}