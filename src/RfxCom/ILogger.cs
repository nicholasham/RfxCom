namespace RfxCom
{
    public interface ILogger
    {
        void Info(string message, params object[] arguments);
        void Error(string message, params object[] arguments);
    }
}