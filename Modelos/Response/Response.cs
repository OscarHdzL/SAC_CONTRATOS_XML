using System;
using System.Runtime.Serialization;

namespace Modelos.Response
{
    [DataContract]
    public class Response
    {
        public Response()
        {
            this.Status = ResponseStatus.Success;
        }

        public Response(Exception currentException)
        {
            this.CurrentException = currentException.ToString();
            this.Status = ResponseStatus.Failed;
        }

        public Response(string currentException)
        {
            this.CurrentException = currentException;
            this.Status = ResponseStatus.Failed;
        }

        public Response(string format, params object[] args)
        {
            this.CurrentException = string.Format(format, args);
            this.Status = ResponseStatus.Failed;
        }

        [DataMember] public ResponseStatus Status { get; set; }
        [DataMember] public string CurrentException { get; set; }
    }
}
