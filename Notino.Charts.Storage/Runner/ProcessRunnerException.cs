using System;

namespace Notino.Charts.Runner
{

    [Serializable]
    public class ProcessRunnerException : Exception
    {
        public ProcessRunnerException() { }
        public ProcessRunnerException(string message) : base(message) { }
        public ProcessRunnerException(string message, Exception inner) : base(message, inner) { }
        protected ProcessRunnerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
