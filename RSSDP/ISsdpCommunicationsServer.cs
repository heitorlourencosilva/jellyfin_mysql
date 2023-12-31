using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Rssdp.Infrastructure
{
    /// <summary>
    /// Interface for a component that manages network communication (sending and receiving HTTPU messages) for the SSDP protocol.
    /// </summary>
    public interface ISsdpCommunicationsServer : IDisposable
    {
        /// <summary>
        /// Raised when a HTTPU request message is received by a socket (unicast or multicast).
        /// </summary>
        event EventHandler<RequestReceivedEventArgs> RequestReceived;

        /// <summary>
        /// Raised when an HTTPU response message is received by a socket (unicast or multicast).
        /// </summary>
        event EventHandler<ResponseReceivedEventArgs> ResponseReceived;

        /// <summary>
        /// Causes the server to begin listening for multicast messages, being SSDP search requests and notifications.
        /// </summary>
        void BeginListeningForMulticast();

        /// <summary>
        /// Causes the server to stop listening for multicast messages, being SSDP search requests and notifications.
        /// </summary>
        void StopListeningForMulticast();

        /// <summary>
        /// Sends a message to a particular address (uni or multicast) and port.
        /// </summary>
        Task SendMessage(byte[] messageData, IPEndPoint destination, IPAddress fromLocalIPAddress, CancellationToken cancellationToken);

        /// <summary>
        /// Sends a message to the SSDP multicast address and port.
        /// </summary>
        Task SendMulticastMessage(string message, IPAddress fromLocalIPAddress, CancellationToken cancellationToken);
        Task SendMulticastMessage(string message, int sendCount, IPAddress fromLocalIPAddress, CancellationToken cancellationToken);

        /// <summary>
        /// Gets or sets a boolean value indicating whether or not this instance is shared amongst multiple <see cref="SsdpDeviceLocator"/> and/or <see cref="ISsdpDevicePublisher"/> instances.
        /// </summary>
        /// <remarks>
        /// <para>If true, disposing an instance of a <see cref="SsdpDeviceLocator"/>or a <see cref="ISsdpDevicePublisher"/> will not dispose this comms server instance. The calling code is responsible for managing the lifetime of the server.</para>
        /// </remarks>
        bool IsShared { get; set; }
    }
}
