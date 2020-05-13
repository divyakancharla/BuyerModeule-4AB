using BUYERDBENTITY.Entity;
using BUYERDBENTITY.Models;
using BUYERDBENTITY.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace UserServiceTesting
{
    [TestFixture]
    public class TestUserRepository
    {
        IUserRepository userRepository;
        DbContextOptionsBuilder<BuyerDBContext> _builder;
        BuyerDBContext db;
        [SetUp]
        public void SetUp()
        {
            _builder = new DbContextOptionsBuilder<BuyerDBContext>().EnableSensitiveDataLogging().UseInMemoryDatabase(Guid.NewGuid().ToString());

            BuyerDBContext db = new BuyerDBContext(_builder.Options);
            userRepository = new UserRepository(db);
        }

        [TearDown]
        public void TearDown()
        {
            userRepository = null;
        }
        /// <summary>
        /// Testing register buyer
        /// </summary>
        [Test]
        [TestCase(121, "divya", "12345678","9365756295","divya@gmail.com")]
        [TestCase(606, "steve", "abcdefg2", "9462753495", "steve@gmail.com")]
        [Description("testing buyer Register")]
        public async Task RegisterBuyer_Successfull(int buyerId, string userName, string password, string mobileNo, string email)
        {
            try
            {
                DateTime datetime = System.DateTime.Now;
                var buyer = new BuyerRegister { buyerId = buyerId, userName = userName, password = password, mobileNo = mobileNo, emailId=email,dateTime = datetime };
                var mock = new Mock<IUserRepository>();
                mock.Setup(x => x.BuyerRegister(buyer)).ReturnsAsync(true);
                var result=await userRepository.BuyerRegister(buyer);
                Assert.NotNull(result);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// Buyer Login success
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Test]
        [TestCase("divya","12345678")]
        [Description("testing buyer login")]
        public async Task BuyerLogin_Successfull(string userName,string password)
        {
            try
            {
                var result = await userRepository.BuyerLogin(userName,password);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [TestCase("chandin", "abcdefg@")]
        [Description("Test buyer login failure case")]
        public async Task BuyerLogin_UnSuccessfull(string userName,string password)
        {
            try
            {
                var result = await userRepository.BuyerLogin(userName, password);
                Assert.IsNull(result,"Invalid");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }

}