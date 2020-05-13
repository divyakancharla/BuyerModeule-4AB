using BUYERDBENTITY.Entity;
using BUYERDBENTITY.Models;
using BUYERDBENTITY.Repositories;
using ItemsService.Manager;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ItemServiceTesting
{
    [TestFixture]
    public class ItemManagerTesting
    {
        IItemManager iitemManager;

        [SetUp]
        public void SetUp()
        {
            iitemManager = new ItemManager(new ItemRepository(new BuyerdataContext()));
        }
        [TearDown]
        public void TearDown()
        {
            iitemManager = null;
        }
        /// <summary>
        /// Add to cart
        /// </summary>
        /// <returns></returns>
        [Test]
        [TestCase(123, 856, 401, 1235, 662, 50, "atta", "flour", "342", "good", "atta.img")]
        [Description("Add to cart testing")]
        public async Task AddToCart_Successfull(int cartId, int buyerId, int itemid, int price, string itemName, string description, int stockno, string remarks, string imageName)
        {
            try
            {
                var cart = new AddCart { cartId = cartId, buyerId = buyerId, itemId = itemid, price = price, itemName = itemName, description = description, stockno = stockno, remarks = remarks, imageName = imageName };
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.AddToCart(cart)).ReturnsAsync(true);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.AddToCart(cart);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// buy item
        /// </summary>
        /// <returns></returns>
        [Test]
        [TestCase(2341, 1234, 662, "debit", 2, "good quality", "paid")]
        [Description("Buy item sucessfull")]
        public async Task BuyItem_Sucessfull(int purchaseId, int buyerId, int itemId, string transactionType, int noofitems, string remarks, string transactionStatus)
        {
            try
            {
                DateTime dateTime = System.DateTime.Now;
                var purchaseHistory = new PurchaseHistory { purchaseId = purchaseId, buyerId = buyerId, itemId = itemId, transactionType = transactionType, noOfItems = noofitems, remarks = remarks, transactionStatus = transactionStatus, dateTime = dateTime };
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.BuyItem(purchaseHistory)).ReturnsAsync(true);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.BuyItem(purchaseHistory);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// check cart item
        /// </summary>
        /// <param name="buyerid"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        [Test]
        [TestCase(1235, 662)]
        [Description("check cart")]
        public async Task CheckCartItem_Sucessfull(int buyerid, int itemid)
        {
            try
            {
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.CheckCartItem(buyerid, itemid)).ReturnsAsync(true);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.CheckCartItem(buyerid, itemid);
                Assert.True(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [TestCase(1232, 532)]
        [Description("Check cart by cart buyerid")]
        public async Task CheckCartItem_UnSucessfull(int buyerid, int itemid)
        {
            try
            {
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.CheckCartItem(buyerid, itemid)).ReturnsAsync(false);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.CheckCartItem(buyerid, itemid);
                Assert.False(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// delete cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [Test]
        [TestCase(123)]
        [Description("Delete cart Successful")]
        public async Task DeleteCart_Sucessfull(int cartId)
        {
            try
            {
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.DeleteCart(cartId)).ReturnsAsync(true);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.DeleteCart(cartId);
                Assert.True(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [TestCase(12)]
        [Description("Delete cart Unsucessful")]
        public async Task DeleteCart_UnSucessfull(int cartId)
        {
            try
            {
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.DeleteCart(cartId)).ReturnsAsync(false);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.DeleteCart(cartId);
                Assert.False(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// get cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [Test]
        [TestCase(123)]
        [Description("testing cart item")]
        public async Task GetCart_Successfull(int cartId)
        {
            try
            {
                AddCart cart = new AddCart();
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.GetCartItem(cartId)).ReturnsAsync(cart);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.GetCartItem(cartId);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [TestCase(124)]
        [Description("testing cart item")]
        public async Task GetCart_UnSuccessfull(int cartId)
        {
            try
            {
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.GetCartItem(cartId));
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.GetCartItem(cartId);
                Assert.IsNull(result, "Not found");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// get buyer cart
        /// </summary>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        [Test]
        [TestCase(1235)]
        [Description("testing buyer cart item")]
        public async Task GetBuyerCart_Successfull(int buyerId)
        {
            try
            {
                List<AddCart> cart = new List<AddCart>();
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.GetCarts(buyerId)).ReturnsAsync(cart);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.GetCarts(buyerId);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [TestCase(1234)]
        [Description("testing buyer cart item")]
        public async Task GetBuyerCart_UnSuccessfull(int buyerId)
        {
            try
            {
                List<AddCart> cart = new List<AddCart>();
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.GetCarts(buyerId)).ReturnsAsync(cart);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.GetCarts(buyerId);
                Assert.IsEmpty(result, "No Items");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// get buyer cart count
        /// </summary>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        [Test]
        [TestCase(1235)]
        [Description("testing buyer cart item")]
        public async Task GetCartCount_Successfull(int buyerId)
        {
            try
            {
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.GetCount(buyerId)).ReturnsAsync(1);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.GetCount(buyerId);
                Assert.NotZero(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [TestCase(1234)]
        [Description("testing buyer cart item")]
        public async Task GetCartCount_UnSuccessfull(int buyerId)
        {
            try
            {
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.GetCount(buyerId));
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.GetCount(buyerId);
                Assert.Zero(result, "No cart items");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// sort items by price
        /// </summary>
        /// <param name="price"></param>
        /// <param name="price1"></param>
        /// <returns></returns>
        [Test]
        [TestCase(30, 100)]
        [Description("testing items in range ")]
        public async Task GetItems_Successfull(int price, int price1)
        {
            try
            {
                List<Product> products = new List<Product>();
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.Items(price, price1)).ReturnsAsync(products);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.Items(price, price1);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// purchase history
        /// </summary>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        [Test]
        [TestCase(1235)]
        [Description("testing purchase history")]
        public async Task PurchaseHistory_Successfull(int buyerId)
        {
            try
            {
                PurchaseHistory purchase = new PurchaseHistory { buyerId = buyerId };
                List<PurchaseHistory> products = new List<PurchaseHistory>();
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.Purchase(buyerId)).ReturnsAsync(products);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.Purchase(buyerId);
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [TestCase(1234)]
        [Description("testing purchasehistory")]
        public async Task PurchaseHistory_UnSuccessfull(int buyerId)
        {
            try
            {
                PurchaseHistory purchase = new PurchaseHistory { buyerId = buyerId };
                List<PurchaseHistory> products = new List<PurchaseHistory>();
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.Purchase(buyerId)).ReturnsAsync(products);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.Purchase(buyerId);
                Assert.IsEmpty(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// search items
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        [Test]
        [TestCase("milk")]
        [Description("testing search items")]
        public async Task SearchItems_Successfull(string itemName)
        {
            try
            {
                Product product = new Product { productName = itemName };
                List<Product> products = new List<Product>();
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.Search(itemName)).ReturnsAsync(products);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.Search(itemName);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [TestCase("choco")]
        [Description("testing search items")]
        public async Task SearchItems_UnSuccessfull(string itemName)
        {
            try
            {
                Product product = new Product { productName = itemName };
                List<Product> products = new List<Product>();
                var mock = new Mock<IItemRepository>();
                mock.Setup(x => x.Search(itemName)).ReturnsAsync(products);
                ItemManager itemManager = new ItemManager(mock.Object);
                var result = await itemManager.Search(itemName);
                Assert.IsEmpty(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
