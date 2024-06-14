using Microsoft.Extensions.Logging;

namespace CommonUtils.Helpers
{
    public interface IExceptionHelper
    {
        string AppropriateMessage(string shortMessage, Exception exception, ILogger logger, bool enableSwagger);
        void RegisterError(ILogger logger, string message, params Object[] objCollection);
        void RegisterTrace(ILogger logger, string message, params Object[] objCollection);
    }
}
