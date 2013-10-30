namespace TCM.Model
{
    public interface ISyncable
    {
        ISyncable SyncTarget { get; set; }

        object Sync(string cmd, object attach);
    }
}
