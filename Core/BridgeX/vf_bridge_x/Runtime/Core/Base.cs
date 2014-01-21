namespace Vapula.Runtime
{
    /// <summary>
    /// 运行期状态
    /// </summary>
    public enum State
    {
        Idle = 0,
        BusyBack = 1,
        BusyFront = 2,
        Pause = 3
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
        Cancel = 2,
        Terminate = 3,
        NullTask = 4
    }
}
