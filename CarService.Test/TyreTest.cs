using AutoMapper;
using CarService.AutoMapper;
using CarService.BL.Services;
using CarService.Controllers;
using CarService.DL.Interfaces;
using CarService.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarServiceTest.Test
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
    }
}