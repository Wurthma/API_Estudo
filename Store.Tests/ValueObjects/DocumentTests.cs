using Store.Domain.StoreContext.ValueObjects;
using Xunit;

namespace BaltaStore.Tests
{
    public class DocumentTests
    {
        private Document validDocument;
        private Document invalidDocument;

        public DocumentTests()
        {
            validDocument = new Document("28659170377");
            invalidDocument = new Document("12345678910");
        }

        [Fact]
        public void ShouldReturnNotificationWhenDocumentIsNotValid()
        {
            Assert.False(invalidDocument.Valid);
            Assert.Equal(1, invalidDocument.Notifications.Count);
        }

        [Fact]
        public void ShouldReturnNotNotificationWhenDocumentIsValid()
        {
            Assert.True(validDocument.Valid);
            Assert.Equal(0, validDocument.Notifications.Count);
        }
    }
}