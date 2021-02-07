using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.ValueObjects;
using Xunit;

namespace BaltaStore.Tests
{
    public class NameTests
    {
        [Fact]
        public void ShouldReturnNotificationWhenNameIsNotValid()
        {
            var name = new Name("", "Baltieri");
            Assert.False(name.Valid);
            Assert.Equal(1, name.Notifications.Count);
        }
    }
}