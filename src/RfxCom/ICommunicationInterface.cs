using System;
using System.Threading.Tasks;

namespace RfxCom
{

    public interface ICommunicationInterface
    {
        Task WriteAsync(Byte[] buffer);
        Task<Byte[]> ReadAsync();
        Task FlushAsync();
    }


}