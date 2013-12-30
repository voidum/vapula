namespace Vapula.Designer
{
    public interface IWindow
    {
        string Id { get; }
        WindowHub.State State { get; set; }
    }
}
