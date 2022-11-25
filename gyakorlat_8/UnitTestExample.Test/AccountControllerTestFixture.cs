using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UnitTestExample.Abstractions;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {

        [Test,
        TestCase("n1ierd", false),
        TestCase("n1@uni-corvinus", false),
        TestCase("n1.uni-corvinus.hu", false),
        TestCase("n1@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidateEmail(email);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

            public void TestRegisterHappyPath(string email, string password)
            {
                // Arrange
                var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
                accountServiceMock
                    .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                    .Returns<Account>(a => a);
                var accountController = new AccountController();
                accountController.AccountManager = accountServiceMock.Object;

                // Act
                var actualResult = accountController.Register(email, password);

                // Assert
                Assert.AreEqual(email, actualResult.Email);
                Assert.AreEqual(password, actualResult.Password);
                Assert.AreNotEqual(Guid.Empty, actualResult.ID);
                accountServiceMock.Verify(m => m.CreateAccount(actualResult), Times.Once);
            }
        }
    }
}
