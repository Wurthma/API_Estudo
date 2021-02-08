using Store.Domain.StoreContext.CustomerCommands.Inputs;
using Xunit;

namespace Store.Tests
{
    public class CreateCustomerCommandTests
    {
        [Fact]
        public void ShouldValidateWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "George";
            command.LastName = "Wurthmann";
            command.Document = "28659170377";
            command.Email = "gewurthmann@gmail.com";
            command.Phone = "11999999997";

            Assert.True(command.Validate());
        }
    }
}