using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetClasses { 
    [Serializable]
    public abstract class AplExceptionBase : ExceptionBase {
        protected AplExceptionBase() { }

        protected AplExceptionBase(string message) : base(message) { }

        protected AplExceptionBase(string format, params object[] arguments) : base(string.Format(format, arguments)) { }

        protected AplExceptionBase(Exception innerException, string message) : base(message, innerException) { }

        protected AplExceptionBase(Exception innerException, string format, params object[] arguments) : base(string.Format(format, arguments), innerException) { }

        protected AplExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
