using AutoMapper;
using Core.Entities;
using Core.Entities.Email;
using Core.Interfaces;
using Core.Interfaces.Email;
using Core.Managers;
using Core.ViewModels;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Core.Test.Managers
{
    public sealed class SystemUserManagerTest
    {
        private ISystemUserRepository systemUserRepository;
        private IMapper mapper;
        private IEncrypService encrypService;
        private IEmailService emailService;
        SystemUserCreateViewModel systemUserCreateViewModel;

        private static readonly object[] CreateWhenUserNotCompleteTestCases =
        {
                new object[]{  new SystemUserCreateViewModel
                {
                    Name = null,
                    Email = null,
                    Lastname = null,
                    Username = null,
                    Password = null
                }
              },
                new object[]{  new SystemUserCreateViewModel
                {
                    Name = " ",
                    Email = " ",
                    Lastname = " ",
                    Username = " ",
                    Password = " "
                }
              },
                new object[]{  new SystemUserCreateViewModel
                {
                    Name = "",
                    Email = "",
                    Lastname = "",
                    Username = "",
                    Password = ""
                },
              },
                   new object[]{  new SystemUserCreateViewModel
                {
                    Name = "lolo",
                    Email = "",
                    Lastname = "",
                    Username = "",
                    Password = ""
                },
              },
        };

        [SetUp]
        public void SetUp()
        {
            systemUserRepository = Substitute.For<ISystemUserRepository>();
            mapper = Substitute.For<IMapper>();
            encrypService = Substitute.For<IEncrypService>();
            emailService = Substitute.For<IEmailService>();

            systemUserCreateViewModel = new SystemUserCreateViewModel
            {
                Name = "nombre prueba",
                Email = "correo prueba",
                Lastname = "apellido prueba",
                Username = "usuario de prueba",
                Password = "password de prueba"
            };
        }

        [TestCaseSource("CreateWhenUserNotCompleteTestCases")]
        public async Task CreateWhenUserNotCompleteShouldReturnOperationResultFailed(SystemUserCreateViewModel systemUserCreateViewModel)
        {
            SystemUserManager systemUserManager = new SystemUserManager(systemUserRepository, mapper, encrypService, emailService);

            IOperationResult<bool> actual = await systemUserManager.Create(systemUserCreateViewModel);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Todos los campos son requeridos", actual.Message);
        }

        [Test]
        public async Task CreateWhenEncrypTextThrowShouldReturnOperationResultFailed()
        {
            encrypService.EncrypText(Arg.Any<string>()).Throws(new Exception());

            SystemUserManager systemUserManager = new SystemUserManager(systemUserRepository, mapper, encrypService, emailService);

            IOperationResult<bool> actual = await systemUserManager.Create(systemUserCreateViewModel);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error creando el usuario", actual.Message);
        }

        [Test]
        public async Task CreateWhenMapperThrowShouldReturnOperationResultFailed()
        {
            encrypService.EncrypText(Arg.Any<string>()).Returns("texto encriptado");
            mapper.Map<SystemUser>(systemUserCreateViewModel).Throws(new Exception());

            SystemUserManager systemUserManager = new SystemUserManager(systemUserRepository, mapper, encrypService, emailService);


            IOperationResult<bool> actual = await systemUserManager.Create(systemUserCreateViewModel);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error creando el usuario", actual.Message);
        }

        [Test]
        public async Task CreateWhenSystemUserRepositoryCreateThrowShouldReturnOperationResultFailed()
        {
            encrypService.EncrypText(Arg.Any<string>()).Returns("texto encriptado");

            systemUserRepository.When(systemUserRepository => systemUserRepository.Create(Arg.Any<SystemUser>())).Throw(new Exception());

            SystemUserManager systemUserManager = new SystemUserManager(systemUserRepository, mapper, encrypService, emailService);

            IOperationResult<bool> actual = await systemUserManager.Create(systemUserCreateViewModel);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error creando el usuario", actual.Message);
        }

        [Test]
        public async Task CreateWhenSystemUserRepositorySaveThrowShouldReturnOperationResultFailed()
        {
            encrypService.EncrypText(Arg.Any<string>()).Returns("texto encriptado");

            systemUserRepository.When(systemUserRepository => systemUserRepository.SaveAsync()).Throw(new Exception());

            SystemUserManager systemUserManager = new SystemUserManager(systemUserRepository, mapper, encrypService, emailService);

            IOperationResult<bool> actual = await systemUserManager.Create(systemUserCreateViewModel);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error creando el usuario", actual.Message);
        }

        [Test]
        public async Task CreateWhenEmailServiceSendEmailThrowShouldReturnOperationResultFailed()
        {
            SystemUser systemUser = new SystemUser
            {
                Email = "uncorreovacano"
            };

            encrypService.EncrypText(Arg.Any<string>()).Returns("texto encriptado");
            mapper.Map<SystemUser>(Arg.Any<SystemUserCreateViewModel>()).Returns(systemUser);
            emailService.SendActivationEmail(Arg.Any<SystemUserActivationRequest>()).Throws(new Exception());

            SystemUserManager systemUserManager = new SystemUserManager(systemUserRepository, mapper, encrypService, emailService);

            IOperationResult<bool> actual = await systemUserManager.Create(systemUserCreateViewModel);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Ha ocurrido un error creando el usuario", actual.Message);
        }

        [Test]
        public async Task CreateWhenAllSuccessShouldReturnOperationResultSuccess()
        {
            SystemUser systemUser = new SystemUser
            {
                Email = "uncorreovacano"
            };

            encrypService.EncrypText(Arg.Any<string>()).Returns("texto encriptado");
            mapper.Map<SystemUser>(Arg.Any<SystemUserCreateViewModel>()).Returns(systemUser);

            SystemUserManager systemUserManager = new SystemUserManager(systemUserRepository, mapper, encrypService, emailService);

            IOperationResult<bool> actual = await systemUserManager.Create(systemUserCreateViewModel);

            Assert.IsTrue(actual.Success);
        }
    }
}
