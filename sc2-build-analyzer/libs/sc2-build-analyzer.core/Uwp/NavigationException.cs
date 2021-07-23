
using System;
using System.Runtime.Serialization;

namespace sc2_build_analyzer.core.Uwp
{
    [Serializable]
    public class NavigationException : Exception
    {
        private string message;
        private string name;

        public NavigationException()
        {
        }

        public NavigationException(string message) : base(message)
        {
        }

        public NavigationException(string message, string name)
        {
            this.message = message;
            this.name = name;
        }

        public NavigationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NavigationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}