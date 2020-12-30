using GameFramework;
using GameFramework.Event;

public sealed class InputEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(InputEventArgs).GetHashCode();
    
    public override int Id => EventId;

    public float OffsetX
    {
        get;
        private set;
    }
    
    public float OffsetY
    {
        get;
        private set;
    }
    
    public static InputEventArgs Create(float offsetX, float offsetY)
    {
        var inputEventArgs = ReferencePool.Acquire<InputEventArgs>();
        inputEventArgs.OffsetX = offsetX;
        inputEventArgs.OffsetY = offsetY;

        return inputEventArgs;
    }

    /// <summary>
    /// 清理加载配置成功事件。
    /// </summary>
    public override void Clear()
    {
        OffsetX = 0f;
        OffsetY = 0f;
    }
}
