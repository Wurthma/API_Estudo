using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Enums;
using Store.Domain.StoreContext.ValueObjects;
using Xunit;

namespace Store.Tests
{
    public class OrderTests
    {
        private Product _mouse;
        private Product _keyboard;
        private Product _chair;
        private Product _monitor;
        private Customer _customer;
        private Order _order;

        public OrderTests()
        {
            // Recupera os produtos do banco
            var name = new Name("George", "Wurthmann");
            var document = new Document("46718115533");
            var email = new Email("hello@email.com");
            _customer = new Customer(name, document, email, "551999876542");
            _order = new Order(_customer);

            _mouse = new Product("Mouse Gamer", "Mouse Gamer", "mouse.jpg", 100M, 10);
            _keyboard = new Product("Teclado Gamer", "Teclado Gamer", "Teclado.jpg", 100M, 10);
            _chair = new Product("Cadeira Gamer", "Cadeira Gamer", "Cadeira.jpg", 100M, 10);
            _monitor = new Product("Monitor Gamer", "Monitor Gamer", "Monitor.jpg", 100M, 10);
        }

        // Consigo criar um novo pedido
        [Fact]
        public void ShouldCreateOrderWhenValid()
        {
            Assert.True(_order.Valid);
        }

        // Ao criar o pedido, o status deve ser created
        [Fact]
        public void StatusShouldBeCreatedWhenOrderCreated()
        {
            Assert.Equal(EOrderStatus.Created, _order.Status);
        }

        // Ao adicionar um novo item, a quantidade de itens deve mudar
        [Fact]
        public void ShouldReturnTwoWhenAddedTwoValidItems()
        {
            _order.AddItem(_monitor, 5);
            _order.AddItem(_mouse, 5);
            Assert.Equal(2, _order.Items.Count);
        }

        // Ao adicionar um novo item, deve subtrair a quantidade do produto
        [Fact]
        public void ShouldReturnFiveWhenAddedPurchasedFiveItem()
        {
            _order.AddItem(_mouse, 5);
            Assert.Equal(5, _mouse.QuantityOnHand);
        }

        // Ao confirmar pedido, deve gerar um número
        [Fact]
        public void ShouldReturnANumberWhenOrderPlaced()
        {
            _order.Place();
            Assert.NotEqual("", _order.Number);
        }

        // Ao pagar um pedido, o status deve ser PAGO
        [Fact]
        public void ShouldReturnPaidWhenOrderPaid()
        {
            _order.Pay();
            Assert.Equal(EOrderStatus.Paid, _order.Status);
        }

        // Dados mais 10 produtos, devem haver duas entregas
        [Fact]
        public void ShouldTwoShippingsWhenPurchasedTenProducts()
        {
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.Ship();

            Assert.Equal(2, _order.Deliveries.Count);
        }

        // Ao cancelar o pedido, o status deve ser cancelado
        [Fact]
        public void StatusShouldBeCanceledWhenOrderCanceled()
        {
            _order.Cancel();
            Assert.Equal(EOrderStatus.Canceled, _order.Status);
        }

        // Ao cancelar o pedido, deve cancelar as entregas
        [Fact]
        public void ShouldCancelShippingsWhenOrderCanceled()
        {
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.Ship();

            _order.Cancel();
            foreach (var x in _order.Deliveries)
            {
                Assert.Equal(EDeliveryStatus.Canceled, x.Status);
            }
        }
    }
}