using System;

namespace Notino.Charts.Helm
{

    [Serializable]
    public class HelmException : Exception
    {
        public HelmException() { }
        public HelmException(string message) : base(message) { }
        public HelmException(string message, Exception inner) : base(message, inner) { }
        protected HelmException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
