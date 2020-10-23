namespace BLL.Service
{
    public interface ILogService
    {
        string Get();
    }
    
    public class LogService : ILogService
    {
        public string Get()
        {
            return "42";
        }
    }
}