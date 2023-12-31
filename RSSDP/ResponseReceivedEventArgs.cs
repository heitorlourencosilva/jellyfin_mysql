using System;
using System.Net;
using System.Net.Http;

namespace Rssdp.Infrastructure
{
    /// <summary>
    /// Provides arguments for the <see cref="ISsdpCommunicationsServer.ResponseReceived"/> event.
    /// </summary>
    public sealed class ResponseReceivedEventArgs : EventArgs
    {
        public IPAddress LocalIPAddress { get; set; }

        private readonly HttpResponseMessage _Message;

        private readonly IPEndPoint _ReceivedFrom;

        /// <summary>
        /// Full constructor.
        /// </summary>
        public ResponseReceivedEventArgs(HttpResponseMessage message, IPEndPoint receivedFrom)
        {
            _Message = message;
            _ReceivedFrom = receivedFrom;
        }

        /// <summary>
        /// The <see cref="HttpResponseMessage"/> that was received.
        /// </summary>
        public HttpResponseMessage Message
        {
            get { return _Message; }
        }

        /// <summary>
        /// The <see cref="IPEndPoint"/> the response came from.
        /// </summary>
        public IPEndPoint ReceivedFrom
        {
            get { return _ReceivedFrom; }
        }
    }
}
