using BUYERDBENTITY.Entity;
using BUYERDBENTITY.Models;
using BUYERDBENTITY.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyerServiceTesting
{

    [TestFixture]
    public class BuyerRepositoryTesting
    {
        IBuyerRepository buyerRepository;
        DbContextOptionsBuilder<BuyerDBContext> _builder;
        BuyerDBContext db;
        [SetUp]
        public void SetUp()
        {
            _builder = new DbContextOptionsBuilder<BuyerDBContext>().EnableSensitiveDataLogging().UseInMemoryDatabase(Guid.NewGuid().ToString());

            BuyerDBContext db = new BuyerDBContext(_builder.Options);
             db.Add(new Buyer { Buyerid = 134, Username = "ell", Password = "ell@", Email = "ell@gmail.com", Mobileno = "9182731927" });

            buyerRepository = new BuyerRepository(db);
        }

        [TearDown]
        public void TearDown()
        {
            buyerRepository = null;
        }
        /// <summary>
        /// Testing buyer profile
        /// </summary>
        [Test]
        [TestCase(4526)]
        [TestCase(451)]
        [Description("testing buyer Profile")]
        public async Task BuyerProfile_Successfull(int buyerId)
        {
            try
            {
                var result = await buyerRepository.GetBuyerProfile(buyerId);
                Assert.NotNull(result.userName);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// Testing buyer profile
        /// </summary>
        [Test]
        [TestCase(458)]
        [TestCase(322)]
        [Description("testing buyer Profile failure")]
        public async Task BuyerProfile_UnSuccessfull(int buyerId)
        {
            try
            {
                var result = await buyerRepository.GetBuyerProfile(buyerId);
                var mock = new Mock<IBuyerRepository>();
                mock.Setup(x => x.GetBuyerProfile(buyerId));
                Assert.IsNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// Testing buyer profile
        /// </summary>
        [Test]
        [Description("testing buyer edit Profile")]
        public async Task EditBuyerProfile_Successfull()
        {
            try
            {
                BuyerData buyer = new BuyerData() { buyerId = 134, userName = "divya", password = "12345678", emailId = "anvi@gmail.com", mobileNo = "9873452567", dateTime = System.DateTime.Now };
                var result = await buyerRepository.EditBuyerProfile(buyer);
                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// Testing buyer profile
        /// </summary>
        [Test]
        [Description("testing buyer edit Profile")]
        public async Task EditBuyerProfile_UnSuccessfull()
        {
            try
            {
                BuyerData buyer = new BuyerData() { buyerId = 674, userName = "anvi", password = "abcdefg@", emailId = "anvi@gmail.com", mobileNo = "9873452567", dateTime = System.DateTime.Now };
                var mock = new Mock<IBuyerRepository>();
                mock.Setup(x => x.EditBuyerProfile(buyer)).ReturnsAsync(false);
                var result = await buyerRepository.EditBuyerProfile(buyer);
                Assert.AreEqual(false, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}