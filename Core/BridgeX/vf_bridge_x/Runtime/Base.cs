namespace Vapula.Runtime
{
    /// <summary>
    /// 运行期状态
    /// </summary>
    public enum State
    {
        Idle = 0,
        Busy = 1,
        Pause = 2,
        UI = 3
    }

    //控制码
    public enum CtrlCode
    {
        Null = 0,
        Pause = 1,
        Resume = 2,
        Cancel = 3,
        Restart = 4
    }

    /// <summary>
    /// 返回值
    /// </summary>
    public enum ReturnCode
    {
        Error = 0,
        Normal = 1,
        CancelByMsg = 2,
        CancelByForce = 3,
        NullEntry = 4,
        NullTask = 5
    }
}
