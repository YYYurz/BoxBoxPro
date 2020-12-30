using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BB
{
    public abstract class Entity : EntityLogic
    {
        [SerializeField]
        private EntityData entityData = null;

        public int Id => Entity.Id;
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            entityData = userData as EntityData;
            if (entityData == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }
            CachedTransform.localPosition = entityData.Position;
            CachedTransform.localRotation = entityData.Rotation;
            CachedTransform.localScale = Vector3.one;
        }

        // protected override void OnHide(bool isShutdown, object userData)
        // {
        //     base.OnHide(isShutdown, userData);
        // }
        //
        //
        // protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        // {
        //     base.OnAttached(childEntity, parentTransform, userData);
        // }
        //
        //
        // protected override void OnDetached(EntityLogic childEntity, object userData)
        // {
        //     base.OnDetached(childEntity, userData);
        // }
        //
        //
        // protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        // {
        //     base.OnAttachTo(parentEntity, parentTransform, userData);
        // }
        //
        //
        // protected override void OnDetachFrom(EntityLogic parentEntity, object userData)
        // {
        //     base.OnDetachFrom(parentEntity, userData);
        // }
        //
        // protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        // {
        //     base.OnUpdate(elapseSeconds, realElapseSeconds);
        // }
    }
}
