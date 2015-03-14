using System;
using RfxCom.Messages.Handlers;
using Should;
using Xunit;

namespace RfxCom.UnitTests.Messages.Handlers
{
    public class ReceiveHandlerFactoryTests
    {
        [Fact]
        public void Create_ConstructsAChainOfHandlersCorrectly()
        {
            var factory = new ReceiveHandlerFactory();
            var handler = factory.Create();

            var order = new[]
            {
                typeof (TransceiverMessageHandler), 
                typeof (InterfaceResponseMessageHandler), 
                typeof (ChimeMessageHandler),
                typeof (UnhandledMessageHandler)
            };

            AssertHandleOrder(handler, order);

        }

        public void AssertHandleOrder(IReceiveHandler handler, params Type[] types)
        {
            var currentHandler = (IChainedReceiveHandler) handler;

            foreach (var type in types)
            {
                currentHandler.ShouldBeType(type);
                currentHandler = currentHandler.NextHandler;
            }
        }
    }
}