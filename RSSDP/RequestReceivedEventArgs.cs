using System;
using System.Net;
using System.Net.Http;

namespace Rssdp.Infrastructure
{
    /// <summary>
    /// Provides arguments for the <see cref="ISsdpCommunicationsServer.RequestReceived"/> event.
    /// </summary>
    public sealed class RequestReceivedEventArgs : EventArgs
    {
        private readonly HttpRequestMessage _Message;

        private readonly IPEndPoint _ReceivedFrom;

        public IPAddress LocalIPAddress { get; private set; }

        /// <summary>
        /// Full constructor.
        /// </summary>
        public RequestReceivedEventArgs(HttpRequestMessage message, IPEndPoint receivedFrom, IPAddress localIPAddress)
        {
            _Message = message;
            _ReceivedFrom = receivedFrom;
            LocalIPAddress = localIPAddress;
        }

        /// <summary>
        /// The <see cref="HttpRequestMessage"/> that was received.
        /// </summary>
        public HttpRequestMessage Message
        {
            get { return _Message; }
        }

        /// <summary>
        /// The <see cref="IPEndPoint"/> the request came from.
        /// </summary>
        public IPEndPoint ReceivedFrom
        {
            get { return _ReceivedFrom; }
        }
    }
}
