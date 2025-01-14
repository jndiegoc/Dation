﻿using System.Runtime.Serialization;

namespace CommonUtils.Wrappers
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException()
        {
        }

        public CustomException(string message)
        : base(message) { }

        public CustomException(string message, Exception inner)
            : base(message, inner) { }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
