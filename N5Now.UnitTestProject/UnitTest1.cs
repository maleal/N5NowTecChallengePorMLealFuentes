using Microsoft.Extensions.Logging;
using Moq;
using N5Now.Core.DTOs;
using N5Now.Core.Entities;
using N5Now.Core.Interfaces;
using N5Now.Core.Interfaces.Indexer;
using N5Now.Core.Interfaces.Repository;
using N5Now.Core.Interfaces.Services;
using N5Now.Infrastructure.Interfaces.Services;

namespace N5Now.UnitTestProject
{
    /// <summary>
    /// MSTest para los Unit Tests
    /// </summary>
    [TestClass]
    public class PermissionsServiceTests
    {
        // Arrange
        private PermissionService _permissionsService;

        private Mock<IUnitOfWork> _mockUow;
        private Mock<IPermissionsIndexer> _mocElasticSearch;
        private Mock<ILogger<PermissionService>> _mockLogger;
        private Mock<IKafkaProducer> _mocKafka;
        private Mock<IPermissionRepository> _mockRepo;


        [TestInitialize]
        public void Initialize()
        {
            _mockRepo = new Mock<IPermissionRepository>();
            _mockUow = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<PermissionService>>();
            _permissionsService = new PermissionService(_mockUow.Object, _mocElasticSearch.Object,
                _mockLogger.Object, _mocKafka.Object);
        }

        [TestMethod]
        public async Task Test_Service_RequestPermissionAsync()
        {
            //Creo un
            var dto = new PermissionRequestDto
            {
                EmployeeForeName = "Pepe",
                EmployeeSurName = "Jose",
                PermissionsDate = DateTime.Today,
                PermissionTypeId = 1
            };

            //Servicio
            await _permissionsService.RequestPermissionAsync(dto);

            // Assert
            /*
             _mockRepo.Verify(...)
             Verify() método de Moq que se usa para comprobar que una determinada llamada a un método ocurrió durante la ejecución del código bajo prueba.
            */
            _mockRepo.Verify(r => r.AddAsync(It.Is<Permissions>(
                p => p.EmployeeForeName == "Juan" && p.EmployeeSurName == "Pérez")), Times.Once);

            _mockUow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}