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
    public sealed class AuthenticationManagerTest
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task LoginWhenUsernameIsNullOrEmptyShouldReturnFailOperationResult(string username)
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            IUserService userService = Substitute.For<IUserService>();
            IEncrypService encrypService = Substitute.For<IEncrypService>();

            AuthenticationManager authenticationManager = new AuthenticationManager(systemUserRepository, userService, encrypService);

            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                Username = username,
                Password = "password"
            };

            IOperationResult<AuthenticationViewModel> actual = await authenticationManager.Login(authenticationRequest);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("El nombre de usuario es requerido", actual.Message);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task LoginWhenPasswordIsNullOrEmptyShouldReturnFailOperationResult(string password)
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            IUserService userService = Substitute.For<IUserService>();
            IEncrypService encrypService = Substitute.For<IEncrypService>();

            AuthenticationManager authenticationManager = new AuthenticationManager(systemUserRepository, userService, encrypService);

            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                Username = "asd",
                Password = password
            };

            IOperationResult<AuthenticationViewModel> actual = await authenticationManager.Login(authenticationRequest);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("La contraseña del usuario es requerida", actual.Message);
        }

        [Test]
        public async Task LoginWhenModelIsNullShouldReturnFailOperationResult()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            IUserService userService = Substitute.For<IUserService>();
            IEncrypService encrypService = Substitute.For<IEncrypService>();

            AuthenticationManager authenticationManager = new AuthenticationManager(systemUserRepository, userService, encrypService);

            IOperationResult<AuthenticationViewModel> actual = await authenticationManager.Login(null);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("La autenticacion no puede estar nula", actual.Message);
        }

        [Test]
        public async Task LoginWhenUserNotFoundOrPasswordIsIncorrectShouldReturnFailOperationResult()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            IUserService userService = Substitute.For<IUserService>();
            IEncrypService encrypService = Substitute.For<IEncrypService>();

            systemUserRepository.FindAsync(Arg.Any<Expression<Func<SystemUser, bool>>>()).Returns(default(SystemUser));

            AuthenticationManager authenticationManager = new AuthenticationManager(systemUserRepository, userService, encrypService);

            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                Username = "prueba",
                Password = "password"
            };

            IOperationResult<AuthenticationViewModel> actual = await authenticationManager.Login(authenticationRequest);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Usuario o contraseña incorrecto", actual.Message);
        }

        [Test]
        public async Task LoginWhenUserIsInactiveShouldReturnFailOperationResult()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            IUserService userService = Substitute.For<IUserService>();
            IEncrypService encrypService = Substitute.For<IEncrypService>();
            SystemUser userResult = new SystemUser { Active = false };

            systemUserRepository.FindAsync(Arg.Any<Expression<Func<SystemUser, bool>>>()).Returns(userResult);

            AuthenticationManager authenticationManager = new AuthenticationManager(systemUserRepository, userService, encrypService);

            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                Username = "prueba",
                Password = "password"
            };

            IOperationResult<AuthenticationViewModel> actual = await authenticationManager.Login(authenticationRequest);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Este usuario no se encuentra activo", actual.Message);
        }

        [Test]
        public async Task LoginWhenSystemUserRepositoryThrowShouldReturnFailOperationResult()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            IUserService userService = Substitute.For<IUserService>();
            IEncrypService encrypService = Substitute.For<IEncrypService>();

            systemUserRepository.FindAsync(Arg.Any<Expression<Func<SystemUser, bool>>>()).Throws(new Exception());

            AuthenticationManager authenticationManager = new AuthenticationManager(systemUserRepository, userService, encrypService);

            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                Username = "prueba",
                Password = "password"
            };

            IOperationResult<AuthenticationViewModel> actual = await authenticationManager.Login(authenticationRequest);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error en la autenticación del usuario", actual.Message);
        }

        [Test]
        public async Task LoginWhenUserServiceThrowShouldReturnFailOperationResult()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            IUserService userService = Substitute.For<IUserService>();
            IEncrypService encrypService = Substitute.For<IEncrypService>();

            SystemUser userResult = new SystemUser { Active = true };
            systemUserRepository.FindAsync(Arg.Any<Expression<Func<SystemUser, bool>>>()).Returns(userResult);

            userService.GetToken(Arg.Any<SystemUser>()).Throws(new Exception());

            AuthenticationManager authenticationManager = new AuthenticationManager(systemUserRepository, userService, encrypService);

            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                Username = "prueba",
                Password = "password"
            };

            IOperationResult<AuthenticationViewModel> actual = await authenticationManager.Login(authenticationRequest);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error en la autenticación del usuario", actual.Message);
        }

        [Test]
        public async Task LoginWhenEncrypServiceThrowShouldReturnFailOperationResult()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            IUserService userService = Substitute.For<IUserService>();
            IEncrypService encrypService = Substitute.For<IEncrypService>();

            SystemUser userResult = new SystemUser { Active = true };
            systemUserRepository.FindAsync(Arg.Any<Expression<Func<SystemUser, bool>>>()).Returns(userResult);

            encrypService.EncrypText(Arg.Any<string>()).Throws(new Exception());

            AuthenticationManager authenticationManager = new AuthenticationManager(systemUserRepository, userService, encrypService);

            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                Username = "prueba",
                Password = "password"
            };

            IOperationResult<AuthenticationViewModel> actual = await authenticationManager.Login(authenticationRequest);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error en la autenticación del usuario", actual.Message);
        }

        [Test]
        public async Task LoginWhenAllSuccessShouldReturnSuccessOperationResult()
        {
            ISystemUserRepository systemUserRepository = Substitute.For<ISystemUserRepository>();
            IUserService userService = Substitute.For<IUserService>();
            IEncrypService encrypService = Substitute.For<IEncrypService>();

            SystemUser userResult = new SystemUser { Active = true };
            systemUserRepository.FindAsync(Arg.Any<Expression<Func<SystemUser, bool>>>()).Returns(userResult);

            AuthenticationManager authenticationManager = new AuthenticationManager(systemUserRepository, userService, encrypService);

            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                Username = "prueba",
                Password = "password"
            };

            IOperationResult<AuthenticationViewModel> actual = await authenticationManager.Login(authenticationRequest);

            Assert.IsTrue(actual.Success);
        }
    }
}
