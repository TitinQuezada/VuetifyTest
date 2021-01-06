using Core.Entities;
using Core.Interfaces;
using Core.Managers;
using Core.ViewModels;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Test.Managers
{
    public sealed class DashBoardManagerTest
    {
        [Test]
        public async Task GetDashBoarDataWhenRegisteredUsersCountFailShouldReturnOperationResultFail()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            systemUserRepository.CountAsync().Throws(new Exception());

            DashBoardManager dashBoardManager = new DashBoardManager(systemUserRepository);

            IOperationResult<DashBoardViewModel> actual = await dashBoardManager.GetDashBoarData();

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error al cargar los datos del tablero", actual.Message);
        }

        [Test]
        public async Task GetDashBoarDataWhenActiveUsersQuantityFailShouldReturnOperationResultFail()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            systemUserRepository.CountAsync(Arg.Any<Expression<Func<SystemUser, bool>>>()).Throws(new Exception());

            DashBoardManager dashBoardManager = new DashBoardManager(systemUserRepository);

            IOperationResult<DashBoardViewModel> actual = await dashBoardManager.GetDashBoarData();

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error al cargar los datos del tablero", actual.Message);
        }

        [Test]
        public async Task GetDashBoarDataWhenAllSuccessShouldReturnOperationResultSuccess()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();

            systemUserRepository.CountAsync().Returns(1);
            systemUserRepository.CountAsync(Arg.Any<Expression<Func<SystemUser, bool>>>()).Returns(2);

            DashBoardManager dashBoardManager = new DashBoardManager(systemUserRepository);

            IOperationResult<DashBoardViewModel> actual = await dashBoardManager.GetDashBoarData();

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Entity.RegisteredUsersQuantity, 1);
            Assert.AreEqual(actual.Entity.ActiveUsersQuantity, 2);
        }
    }
}
