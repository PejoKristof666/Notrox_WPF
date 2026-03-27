using Notrox.Model;
using Notrox.Services;
using System.Buffers.Text;

namespace Notrox_WPF_Test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void OrderTotalPriceNull()
        {

            OrdersClass order = new OrdersClass();

            order.Items = new();

            Assert.AreEqual(0, order.TotalPrice);
        }

        [TestMethod]
        public void OrderTotalPriceOneItem()
        {
            OrdersClass order = new OrdersClass();

            List<OrderItemClass> e = new List<OrderItemClass>();

            e.Add(new OrderItemClass { ProductId = 1, Quantity = 1, PriceAtPurchase = 200 });

            order.Items = e;

            Assert.AreEqual(200, order.TotalPrice);

        }

        [TestMethod]
        public void OrderTotalPriceManyItems()
        {
            OrdersClass order = new OrdersClass();

            List<OrderItemClass> e = new List<OrderItemClass>();

            for (int i = 0; i < 5; i++)
            {
                e.Add(new OrderItemClass { ProductId = i, Quantity = 1, PriceAtPurchase = 200 });
            }

            order.Items = e;

            Assert.AreEqual(1000, order.TotalPrice);
        }

        [TestMethod]
        public void OrderTotalPrice_WithQuantities()
        {
            OrdersClass order = new OrdersClass();

            order.Items = new List<OrderItemClass>
            {
                new OrderItemClass { ProductId = 1, Quantity = 2, PriceAtPurchase = 100 },
                new OrderItemClass { ProductId = 2, Quantity = 3, PriceAtPurchase = 50 }
            };

            Assert.AreEqual(350, order.TotalPrice);
        }

        [TestMethod]
        public void OrderTotalPrice_ZeroQuantity()
        {
            OrdersClass order = new OrdersClass();

            order.Items = new List<OrderItemClass>
            {
                new OrderItemClass { ProductId = 1, Quantity = 0, PriceAtPurchase = 999 }
            };

            Assert.AreEqual(0, order.TotalPrice);
        }

        [TestMethod]
        public void OrderTotalPrice_NegativePrice()
        {
            OrdersClass order = new OrdersClass();

            order.Items = new List<OrderItemClass>
            {
                new OrderItemClass { ProductId = 1, Quantity = 1, PriceAtPurchase = -100 }
            };

            Assert.AreEqual(-100, order.TotalPrice);
        }

        [TestMethod]
        public void OrderTotalPrice_LargeValues()
        {
            OrdersClass order = new OrdersClass();

            order.Items = new List<OrderItemClass>
            {
                new OrderItemClass { ProductId = 1, Quantity = int.MaxValue, PriceAtPurchase = 1 }
            };

            Assert.AreEqual(int.MaxValue, order.TotalPrice);
        }

        [TestMethod]
        public async Task LoginGoodHttps()
        {
            string randomst = "https://Asd";
            string username = "username";
            string password = "password";

            ServerConnection connection = new ServerConnection(randomst);

            bool worked = await connection.Login(username, password);


            Assert.IsFalse(worked);
        }

        [TestMethod]
        public async Task Login_EmptyCredentials()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            bool result = await connection.Login("", "");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Login_NullCredentials()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            bool result = await connection.Login(null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task TestConnectionUrl()
        {
            Assert.ThrowsException<ArgumentException>(() => new ServerConnection("asd"));
        }

        [TestMethod]
        public void Constructor_Http_ShouldThrow()
        {
            Assert.ThrowsException<ArgumentException>(() => new ServerConnection("http://example.com"));
        }

        [TestMethod]
        public async Task GetCurrentUser_Failure_ReturnsNull()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            var user = await connection.GetCurrentUser();

            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task ListUsers_Failure_ReturnsEmpty()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            var users = await connection.ListUsers();

            Assert.IsNotNull(users);
            Assert.AreEqual(0, users.Count);
        }

        [TestMethod]
        public async Task DeleteUser_Failure_ReturnsFalse()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            bool result = await connection.DeleteUser(1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AddProduct_Failure_ReturnsFalse()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            bool result = await connection.AddProduct("Test", "Desc", 100, 1, 1, "img");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task DeleteProduct_Failure_ReturnsFalse()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            bool result = await connection.DeleteProduct(1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task EditProduct_Failure_ReturnsFalse()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            bool result = await connection.EditProduct(1, "Name", "Desc", 100, 1, "img");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task EditOrder_Failure_ReturnsFalse()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            bool result = await connection.EditOrder(1, "Shipped");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task EditAddress_Failure_ReturnsFalse()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            bool result = await connection.EditAddress("City", 1234, "Street");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task EditBillingAddress_Failure_ReturnsFalse()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            bool result = await connection.EditBillingAddress("City", 1234, "Street");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task MultipleCalls_ShouldNotThrow()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            for (int i = 0; i < 10; i++)
            {
                await connection.ListUsers();
                await connection.ListProducts();
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Logout_MultipleCalls()
        {
            ServerConnection connection = new ServerConnection("https://test");

            connection.Logout();
            connection.Logout();
            connection.Logout();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task Methods_ShouldNeverThrow()
        {
            ServerConnection connection = new ServerConnection("https://invalid.localhost");

            await connection.Login("a", "b");
            await connection.ListUsers();
            await connection.ListProducts();
            await connection.GetCurrentUser();

            Assert.IsTrue(true);
        }
    }
}
