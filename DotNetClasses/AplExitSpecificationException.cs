using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetClasses {
    public class AplExitSpecificationException : AplExceptionBase {
        public ExitSpecification ExitSpecification { get; private set; }

        public AplExitSpecificationException() { }

        public AplExitSpecificationException(ExitSpecification xs) : base(xs.Message) {
            ExitSpecification = xs;
        }

        public AplExitSpecificationException(string message) : base(message) { }

        public AplExitSpecificationException(string format, params object[] arguments) : base(string.Format(format, arguments)) { }

        public AplExitSpecificationException(Exception innerException, string message) : base(message, innerException) { }

        public AplExitSpecificationException(Exception innerException, string format, params object[] arguments) : base(string.Format(format, arguments), innerException) { }

        public AplExitSpecificationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
