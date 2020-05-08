using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetClasses {
    [Serializable]
    public abstract class ExceptionBase : Exception {

        protected Dictionary<string, string> Tags { get; set; }

        protected ExceptionBase() { }

        protected ExceptionBase(string message) : base(message) { }

        protected ExceptionBase(string format, params object[] arguments) : base(string.Format(format, arguments)) { }

        protected ExceptionBase(Exception innerException, string message) : base(message, innerException) { }

        protected ExceptionBase(Exception innerException, string format, params object[] arguments) : base(string.Format(format, arguments), innerException) { }

        protected ExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);

            if (Tags == null) { return; }

            foreach (var tag in Tags) {
                info.AddValue(tag.Key, tag.Value);    
            }
        }
    }
}
