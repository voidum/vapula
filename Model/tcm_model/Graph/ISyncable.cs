namespace TCM.Model
{
    public interface ISyncable
    {
        ISyncable SyncTarget { get; set; }

        void Sync(string cmd, object attach);
    }
}
