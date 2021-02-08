using Store.Domain.StoreContext.CustomerCommands.Inputs;
using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Handlers;
using Store.Domain.StoreContext.ValueObjects;
using Xunit;

namespace Store.Tests
{
    public class CustomerHandlerTests
    {
        [Fact]
        public void ShouldRegisterCustomerWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "George";
            command.LastName = "Wurthmann";
            command.Document = "28659170377";
            command.Email = "wurthmann@gmail.com";
            command.Phone = "11999999997";

            var handler = new CustomerHandler(new FakeCustomerRepository(), new FakeEmailService());
            var result = handler.Handle(command);

            Assert.NotNull(result);
            Assert.True(handler.Valid);
        }
    }
}