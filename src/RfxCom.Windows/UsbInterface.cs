using System.IO.Ports;

namespace RfxCom.Windows
{
    
    public class UsbInterface : CommunicationInterface
    {
        SerialPort SerialPort { get; set; }

        public UsbInterface(string portName) : this(CreateSerialPort(portName))
        {
        }

        protected UsbInterface(SerialPort serialPort) : base(serialPort.BaseStream)
        {
            SerialPort = serialPort;
        }

        private static SerialPort CreateSerialPort(string portName)
        {
            var serialPort = new SerialPort
            {
                PortName = portName,
                BaudRate = 38400,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None
            };

            serialPort.Open();

            return serialPort;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SerialPort.Dispose();
            }
        }
    }
}