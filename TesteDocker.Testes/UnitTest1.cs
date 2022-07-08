using Domain.DomainUser;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework.Internal;
using WebApplication1.Controlllers;

namespace TesteDocker.Testes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var repositoryMock = new Mock<IDomainUser>();
            repositoryMock.Setup(x => x.RetornoTeste()).Returns("ok");
            var controller = new TesteController(repositoryMock.Object);
            
            var user = new User();
            user.Nome = "ok";

            var result = controller.Get();

            Assert.IsInstanceOfType(result,typeof(OkResult));
        }
    }
}