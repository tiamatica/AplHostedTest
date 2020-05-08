using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DotNetClasses {

    public class ExitSpecification {
        public ReturnCodes ReturnCode { get; private set; }
        public string Message { get; private set; }
        public string Details { get; private set; }
        public enum ReturnCodes {
            Error = -1,
            FieldError = -2,
            ProgramError = -98,
            DatabaseError = -99,
            Ok = 0,
            Exit = 1,
            NoCallback = 2,
            Done = 4,
        }

        public ExitSpecification(object xs) {
            Message = GetErrorMessage(xs);
            Details = GetErrorDetails(xs);
            ReturnCode = (ReturnCodes)GetValue(xs);
        }

        /// <summary>
        /// Get corresponding exit specification exception based on xs.
        /// </summary>
        /// <param name="xs"></param>
        /// <returns></returns>
        public AplExitSpecificationException AsException() {
            switch (ReturnCode) {
                case ReturnCodes.Ok:
                case ReturnCodes.Exit:
                case ReturnCodes.NoCallback:
                case ReturnCodes.Done: {
                    throw new InvalidExitSpecification("Return and ok xs doesn't make sense here!");
                }
                case ReturnCodes.Error: {
                    return new AplExitSpecificationException(this);
                }
                //case -13: {
                //    return new APLDatabaseConnectionExitSpecification(xs);
                //}
                //case -98: {
                //    return new ProgramErrorExitSpecification(xs);
                //}
                //case -126: {
                //    return new ExceptionExitSpecification(xs);
                //}
                //case -2:
                //case -3:
                //case -4: {
                //    throw new InvalidExitSpecification("Field validation errors must be handled on the APL side.");
                //}
                //case 2: {
                //    throw new InvalidExitSpecification("xsNOCB not supported on the C# side.");
                //}
                default: {
                    throw new InvalidExitSpecification("This doesn't make sense here!");
                }
            }
        }

        public bool IsReturn() {
            switch (ReturnCode) {
                case ReturnCodes.Error: {
                    return true;
                }
                case ReturnCodes.Ok: {
                    return false;
                }
                case ReturnCodes.Exit: {
                    return true;
                }
                case ReturnCodes.Done: {
                    return true;
                }
                default: {
                    throw new UnknownReturnCode(ReturnCode.ToString());
                }
            }
        }

        public bool IsError() {
            return ReturnCode == ReturnCodes.Error;
        }

        private static string GetErrorMessage(object xs) {
            if (IsExitSpecificationNumber(xs)) {
                return null;
            }
            if ((xs is Array) && (((Array)xs).Rank == 1) && (((Array)xs).Length >= 3)) {
                object message = ((Array)xs).GetValue(2);
                if (message is string) {
                    return TypeConversion.ToNetStringsWithLineBreaks((string)message);
                }
                if (message is Array) {
                    var totalmessage = new StringBuilder();
                    string separator = "";
                    foreach (object messagepart in (Array)message) {
                        if (messagepart is string) {
                            totalmessage.Append(separator);
                            totalmessage.Append((string)messagepart);
                            separator = "\r\n";
                        } else if (messagepart is char) {
                            totalmessage.Append(separator);
                            totalmessage.Append((char)messagepart);
                            separator = "\r\n";
                        }
                    }
                    return TypeConversion.ToNetStringsWithLineBreaks(totalmessage.ToString());
                }
                throw new InvalidExitSpecification("value = " + GetArrayAsString((Array)xs));
            }
            throw new InvalidExitSpecification(xs.ToString());
        }

        private static bool IsExitSpecificationNumber(object value) {
            return (value is sbyte) || (value is int) || (value is short) || (value is bool) || (value is double);
        }

        private static string GetArrayAsString(IList array) {
            var result = new StringBuilder();
            result.Append("[");
            if (array != null) {
                for (int i = 0; i < (array).Count; i++) {
                    if (i > 0) {
                        result.Append(", ");
                    }
                    if (array[i] is IList) {
                        result.Append(GetArrayAsString((IList)array[i]));
                    } else {
                        result.Append(array[i].ToString());
                    }
                }
            }
            result.Append("]");
            return result.ToString();
        }

        private static string GetErrorDetails(object xs) {
            if (IsExitSpecificationNumber(xs)) {
                return null;
            }
            if ((xs is Array) && (((Array)xs).Rank == 1) && (((Array)xs).Length >= 4)) {
                object details = ((Array)xs).GetValue(3);
                if (details == null) {
                    return null;
                }
                if (details is string) {
                    return TypeConversion.ToNetStringsWithLineBreaks((string)details);
                }
                if (details is Array) {
                    var totaldetails = new StringBuilder();
                    string separator = "";
                    foreach (object detailspart in (Array)details) {
                        if (detailspart is string) {
                            totaldetails.Append(separator);
                            totaldetails.Append((string)detailspart);
                            separator = "\r\n";
                        } else if (detailspart is char) {
                            totaldetails.Append(separator);
                            totaldetails.Append((char)detailspart);
                            separator = "\r\n";
                        }
                    }
                    return TypeConversion.ToNetStringsWithLineBreaks(totaldetails.ToString());
                }
                throw new InvalidExitSpecification(GetArrayAsString((Array)xs));
            }
            throw new InvalidExitSpecification(xs.ToString());
        }

        //public void Check() {
        //    if (this is ErrorExitSpecification) {
        //        throw this;
        //    }
        //}

        private static int GetValue(object xs) {
            if (IsExitSpecificationNumber(xs)) {
                return Convert.ToInt32(xs);
            }
            if ((xs is Array) && (((Array)xs).Rank == 1)) {
                return Convert.ToInt32(((Array)xs).GetValue(0));
            }
            throw new InvalidExitSpecification(xs != null ? xs.ToString() : "(null)");
        }

        //public override string StackTrace {
        //    get {
        //        string stacktrace = "";
        //        if (!string.IsNullOrEmpty(Details)) {
        //            stacktrace = Details + "\r\n";
        //        }
        //        return stacktrace + base.StackTrace;
        //    }
        //}

        [Serializable]
        public class InvalidExitSpecification : ExceptionBase {
            public InvalidExitSpecification(string message) : base(message) { }
            protected InvalidExitSpecification(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        [Serializable]
        public class UnknownReturnCode : ExceptionBase {
            public UnknownReturnCode(string message) : base(message) { }
            protected UnknownReturnCode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
    }

}
