using AutoMapper;
using CarService.AutoMapper;
using CarService.BL.Services;
using CarService.Controllers;
using CarService.DL.Interfaces;
using CarService.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CraService.Test
{
    public class TyreTest
    {
        private IList<Tyre> _tyres = new List<Tyre>()
        {
            new Tyre()
            {
                Id = 1,
                TyreName = "Debica",
                Quantity = 10,
                Price = 50
            },
            new Tyre()
            {
                Id = 2,
                TyreName = "Continental",
                Quantity = 20,
                Price = 250
            }
        };

        private readonly IMapper _mapper;
        private Mock<ILogger<TyreService>> _loggerMock;
        private Mock<ILogger<TyreController>> _loggerCarControllerMock;
        private readonly Mock<ITyreRepo> _tyreRepoMock;

        public TyreTest()
        {
            var mockMapperCongif = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapperCongif.CreateMapper();
            _loggerMock = new Mock<ILogger<TyreService>>();
            _loggerCarControllerMock = new Mock<ILogger<TyreController>>();
            _tyreRepoMock = new Mock<ITyreRepo>();
        }

        [Fact]
        public async Task Tyre_GetTyreById_Ok()
        {
            //setup
            var tyreId = 1;
            var expectedTyre = _tyres.First(x => x.Id == tyreId);

            _tyreRepoMock.Setup(x => x.GetTyreById(tyreId)).ReturnsAsync(expectedTyre);

            //inject
            var service = new TyreService(_tyreRepoMock.Object, _loggerMock.Object, _mapper);
            var controller = new TyreController(service, _loggerCarControllerMock.Object);

            //act
            var result = await controller.GetTyreById(tyreId);

            //Assert
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);

            var tyre = okObjectResult.Value as Tyre;
            Assert.NotNull(tyre);
            Assert.Equal(expectedTyre.Id, tyreId);
        }

        [Fact]
        public async Task Tyre_GetTyreById_NotFound()
        {
            //setup
            var tyreId = 3;
            var expectedTyre = _tyres.FirstOrDefault(x => x.Id == tyreId);

            _tyreRepoMock.Setup(x => x.GetTyreById(tyreId)).ReturnsAsync(expectedTyre);

            //inject
            var service = new TyreService(_tyreRepoMock.Object, _loggerMock.Object, _mapper);
            var controller = new TyreController(service, _loggerCarControllerMock.Object);

            //act
            var result = await controller.GetTyreById(tyreId);

            //Assert
            var notObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notObjectResult);

            var returnedTyreId = notObjectResult.Value;

            Assert.NotNull(returnedTyreId);
            Assert.Equal("Tyre not exist", returnedTyreId);
        }

        [Fact]
        public async Task Tyre_GetAllCount_Check()
        {
            //setup
            var expectedCount = 2;

            _tyreRepoMock.Setup(x => x.GetAllTyres()).ReturnsAsync(_tyres);

            //inject
            var service = new TyreService(_tyreRepoMock.Object, _loggerMock.Object, _mapper);
            var controller = new TyreController(service, _loggerCarControllerMock.Object);

            //act
            var result = await controller.GetAllTyres();

            //Assert
            var okObjectResult = result as OkObjectResult;
            var tyres = okObjectResult.Value as IEnumerable<Tyre>;

            Assert.NotNull(tyres);
            Assert.NotEmpty(tyres);
            Assert.Equal(expectedCount, tyres.Count());
            Assert.Equal(tyres, _tyres);
        }

        [Fact]
        public async Task Tyre_AddTyre_Ok()
        {
            //setup
            var tyre = new Tyre()
            {
                Id = 3,
                TyreName = "Gumi",
                Quantity = 100,
                Price = 100
            };

            var expectedId = 3;

            _tyreRepoMock.Setup(x => x.AddTyre(It.IsAny<Tyre>())).Callback(() =>
            {
                _tyres.Add(new Tyre()
                {
                    Id = expectedId,
                    TyreName = tyre.TyreName,
                    Quantity = tyre.Quantity,
                    Price = tyre.Price
                });
            }).ReturnsAsync(() => _tyres.FirstOrDefault(x => x.Id == expectedId));

            //inject
            var service = new TyreService(_tyreRepoMock.Object, _loggerMock.Object, _mapper);
            var controller = new TyreController(service, _loggerCarControllerMock.Object);

            //act
            var result = await controller.AddTyre(tyre);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as Tyre;
            Assert.NotNull(resultValue);
            Assert.Equal(expectedId, resultValue.Id);
        }

        [Fact]
        public async Task Tyre_DeleteTyre_Ok()
        {
            //setup
            var tyreId = 1;
            var tyreToDelete = _tyres.FirstOrDefault(x => x.Id == tyreId);
            var beforeDeleteCount = _tyres.Count();

            _tyreRepoMock.Setup(x => x.GetTyreById(tyreId)).ReturnsAsync(() => _tyres.FirstOrDefault(x => x.Id == tyreId));

            _tyreRepoMock.Setup(x => x.DeleteTyre(tyreId)).Callback(() =>
            {
                _tyres.Remove(tyreToDelete);

            })!.ReturnsAsync(() => tyreToDelete);

            //inject
            var service = new TyreService(_tyreRepoMock.Object, _loggerMock.Object, _mapper);
            var controller = new TyreController(service, _loggerCarControllerMock.Object);

            //act
            var result = await controller.DeleteTyre(tyreId);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(1, tyreId);

            var resultValue = okObjectResult.Value as Tyre;
            Assert.NotNull (resultValue);
            Assert.Equal(tyreToDelete, resultValue);
        }
    }
}