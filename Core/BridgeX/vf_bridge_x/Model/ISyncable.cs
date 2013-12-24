namespace Vapula.Model
{
    //行为同步，用于关联对象的关联操作解耦
    public interface ISyncable
    {
        ISyncable SyncTarget { get; set; }

        object Sync(string cmd, object attach);
    }
}
