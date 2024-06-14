using CommonUtils.Wrappers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CommonUtils.Helpers
{
    public class ExceptionHelper : IExceptionHelper
    {
        public string AppropriateMessage(string shortMessage, Exception exception, ILogger logger, bool enableSwagger)
        {
            if (exception is CustomException)
            {
                shortMessage = exception.Message;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(shortMessage))
                    shortMessage = "An error has occurred";
            }

            string message = ExceptionCompleteMessage(exception);
            RegisterError(logger, message, exception);

            if (!enableSwagger)
                message = shortMessage;

            return message;
        }

        public static string ExceptionCompleteMessage(Exception exception)
        {
            string msg = string.Empty;
            string stk = string.Empty;
            Exception tmpEx = exception;

            while (null != tmpEx)
            {
                msg = string.Concat(msg, tmpEx.Message, Environment.NewLine);
                stk = string.Concat(stk, tmpEx.StackTrace, Environment.NewLine);
                tmpEx = tmpEx.InnerException;
            }

            return $"{msg}{Environment.NewLine}{stk}";
        }

        public void RegisterError(ILogger logger, string message, params Object[] objCollection)
        {
            logger.LogError(PrepareStringToRegister(message, objCollection));
        }
        public void RegisterTrace(ILogger logger, string message, params Object[] objCollection)
        {
            logger.LogTrace(PrepareStringToRegister(message, objCollection));
        }

        private static string PrepareStringToRegister(string message, Object[] objCollection)
        {
            string tmpSerializedObj = string.Empty;
            string serializedObjs = string.Empty;
            if (null != objCollection)
            {
                foreach (object obj in objCollection)
                {
                    tmpSerializedObj = string.Empty;
                    try
                    {
                        tmpSerializedObj = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { MaxDepth = 1, });
                        serializedObjs = string.Concat(serializedObjs, Environment.NewLine, tmpSerializedObj);
                    }
                    catch
                    {
                        tmpSerializedObj = string.Empty;
                    }
                }

                message = string.Concat(message, serializedObjs);
            }

            return message;
        }
    }
}
