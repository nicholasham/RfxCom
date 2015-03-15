using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom
{

    

    public interface ICommunicationInterface : IDisposable
    {
        /// <summary>
        /// Writes a bock of bytes to the interface
        /// </summary>
        /// <param name="buffer">a block of data to be written</param>
        /// <returns>Awaitable task</returns>
        Task WriteAsync(Byte[] buffer);

        /// <summary>
        /// Reads a full data from the interface 
        /// </summary>
        /// <returns>Awaitable task that returns blocks of bytes</returns>
        Task<Byte[]> ReadAsync(CancellationToken cancellationToken);
        
        Task FlushAsync();
    }
}