using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetClasses {
    public class AplFieldErrorException : AplExitSpecificationException {

        public AplFieldErrorException() { }

        public AplFieldErrorException(string message) : base(message) { }

        public AplFieldErrorException(string format, params object[] arguments) : base(string.Format(format, arguments)) { }

        public AplFieldErrorException(Exception innerException, string message) : base(message, innerException) { }

        public AplFieldErrorException(Exception innerException, string format, params object[] arguments) : base(string.Format(format, arguments), innerException) { }

        public AplFieldErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
