using System;
using System.IO;
using System.IO.Ports;
using System.Threading.Tasks;
using RfxCom;

namespace RfxComSandpit
{
    public class UsbInterface : CommunicationInterface
    {
        public UsbInterface(SerialPort streamPort) : base(streamPort.BaseStream)
        {
        }
    }
}