namespace HomeCon.RestServer
{
    public interface ILogger
    {
        void Write(string message);
        void WriteLine(string message);
    }
}