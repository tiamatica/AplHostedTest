using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetClasses {
    public class AplSystemErrorException : AplExceptionBase {
        public ErrorCodes ErrorCode { get; private set; }

        public enum ErrorCodes {
            WsFull = 1,
            SyntaxError = 2,
            IndexError = 3,
            RankError = 4,
            LengthError = 5,
            ValueError = 6,
            FormatError = 7,
            LimitError = 10,
            DomainError = 11,
            HoldError = 12,
            NonceError = 16
        }

        public AplSystemErrorException() { }

        public AplSystemErrorException(string message, ErrorCodes errorcode) : base(message) { ErrorCode = errorcode; }

        public AplSystemErrorException(string format, params object[] arguments) : base(string.Format(format, arguments)) { }

        public AplSystemErrorException(Exception innerException, string message) : base(message, innerException) { }

        public AplSystemErrorException(Exception innerException, string format, params object[] arguments) : base(string.Format(format, arguments), innerException) { }

        public AplSystemErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

    public class AplDomainErrorException : AplSystemErrorException {

        public AplDomainErrorException() { }

        public AplDomainErrorException(string message) : base(message, ErrorCodes.DomainError) { }

        public AplDomainErrorException(string format, params object[] arguments) : base(string.Format(format, arguments)) { }

        public AplDomainErrorException(Exception innerException, string message) : base(message, innerException) { }

        public AplDomainErrorException(Exception innerException, string format, params object[] arguments) : base(string.Format(format, arguments), innerException) { }

        public AplDomainErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

    public class AplIndexErrorException : AplSystemErrorException {

        public AplIndexErrorException() { }

        public AplIndexErrorException(string message) : base(message, ErrorCodes.DomainError) { }

        public AplIndexErrorException(string format, params object[] arguments) : base(string.Format(format, arguments)) { }

        public AplIndexErrorException(Exception innerException, string message) : base(message, innerException) { }

        public AplIndexErrorException(Exception innerException, string format, params object[] arguments) : base(string.Format(format, arguments), innerException) { }

        public AplIndexErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

    public class AplLengthErrorException : AplSystemErrorException {

        public AplLengthErrorException() { }

        public AplLengthErrorException(string message) : base(message, ErrorCodes.DomainError) { }

        public AplLengthErrorException(string format, params object[] arguments) : base(string.Format(format, arguments)) { }

        public AplLengthErrorException(Exception innerException, string message) : base(message, innerException) { }

        public AplLengthErrorException(Exception innerException, string format, params object[] arguments) : base(string.Format(format, arguments), innerException) { }

        public AplLengthErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
