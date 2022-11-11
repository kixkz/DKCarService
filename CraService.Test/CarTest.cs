using CarService.Controllers;
using CarService.DL.Interfaces;
using CarService.Models.MediatR;
using CarService.Models.Models;
using CarService.Models.Requests;
using CarService.Models.Responses;
using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoMapper.Internal;

namespace CraService.Test
{
    public class CarTest
    {
        private IList<Car> _cars = new List<Car>()
        {
            new Car()
            {
                Id = 1,
                CarName = "Audi",
                CarModel = "A6"
            },
            new Car()
            {
                Id = 2,
                CarName = "Mercedes",
                CarModel = "E500"
            },
        };

        private Mock<IMediator> _mediator;
        private Mock<ICarRepo> _carRepo;

        public CarTest()
        {
            _mediator = new Mock<IMediator>();
            _carRepo = new Mock<ICarRepo>();
        }

        [Fact]
        public async Task Car_GetAllCars_Count()
        {
            //setup
            var expectedCount = 2;

            _mediator.Setup(x => x.Send(It.IsAny<GetAllCarsCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(_cars);

            //inject
            var controller = new CarController(_carRepo.Object, _mediator.Object);

            //act
            var result = await controller.GetAllCars();

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var cars = okObjectResult.Value as IEnumerable<Car>;
            Assert.NotNull(cars);
            Assert.NotEmpty(cars);
            Assert.Equal(expectedCount, cars.Count());
            Assert.Equal(cars, _cars);
        }

        [Fact]
        public async Task Car_GetAllCars_Count_Empty()
        {
            //setup
            _mediator.Setup(x => x.Send(It.IsAny<GetAllCarsCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Enumerable.Empty<Car>());

            //inject
            var controller = new CarController(_carRepo.Object, _mediator.Object);

            //act
            var result = await controller.GetAllCars();

            //assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var messageResult = notFoundObjectResult.Value as string;
            Assert.NotNull(messageResult);
            Assert.Equal("There aren't any cars in the collection", messageResult);
        }

        [Fact]
        public async Task Tyre_GetCarById_Ok()
        {
            //setup
            var carId = 1;
            var expectedCar = _cars.First(x => x.Id == carId);

            _mediator.Setup(x => x.Send(It.IsAny<GetCarByIdCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedCar);

            //inject
            var controller = new CarController(_carRepo.Object, _mediator.Object);

            //act
            var result = await controller.GetCarById(carId);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var car = okObjectResult.Value as Car;
            Assert.NotNull(car);
            Assert.Equal(car.Id, carId);
        }

        [Fact]
        public async Task Tyre_GetCarById_NotFound()
        {
            //setup
            var carId = 3;
            var expectedCar = _cars.FirstOrDefault(x => x.Id == carId);

            _mediator.Setup(x => x.Send(It.IsAny<GetCarByIdCommand>(), It.IsAny<CancellationToken>()));

            //inject
            var controller = new CarController(_carRepo.Object, _mediator.Object);

            //act
            var result = await controller.GetCarById(carId);

            //assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var returnedCarId = notFoundObjectResult.Value;
            Assert.NotNull(returnedCarId);
            Assert.Equal($"Car with id:{carId} does not exist", returnedCarId);
        }

        [Fact]
        public async Task Car_Add_Ok()
        {
            //setup
            var carRequest = new AddCarRequest()
            {
                CarName = "Skoda",
                CarModel = "Kodiaq"
            };

            var expectedCarId = 3;

            var carToAdd = new Car()
            {
                Id = expectedCarId,
                CarName = carRequest.CarName,
                CarModel = carRequest.CarModel
            };

            var response = new AddCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Successfully added car",
                Car = carToAdd
            };

            _mediator.Setup(x => x.Send(It.IsAny<AddCarCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

            //inject
            var controller = new CarController(_carRepo.Object, _mediator.Object);

            //act
            var result = await controller.AddCar(carRequest);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as AddCarResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(response, resultValue);
        }

        [Fact]
        public async Task Car_Update_Ok()
        {
            //setup
            var carRequest = new UpdateCarRequest()
            {
                Id = 1,
                CarName = "Skoda",
                CarModel = "Kodiaq"
            };

            var carToUpdate = _cars.FirstOrDefault(x => x.Id == carRequest.Id);

            var response = new AddCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Successfully added car",
                Car = carToUpdate
            };

            _mediator.Setup(x => x.Send(It.IsAny<UpdateCarCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

            //inject
            var controller = new CarController(_carRepo.Object, _mediator.Object);

            //act
            var result = await controller.UpdateCar(carRequest);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as AddCarResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(response.Car.Id, resultValue.Car.Id);
        }

        [Fact]
        public async Task Car_Delete_Ok()
        {
            //setup
            var carToDelete = _cars.FirstOrDefault(x => x.Id == 1);

            var response = new DeleteCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Successfullt deleted car",
                Car = carToDelete
            };

            _mediator.Setup(x => x.Send(It.IsAny<DeleteCarCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(carToDelete);

            //inject
            var controller = new CarController(_carRepo.Object, _mediator.Object);

            //act
            var result = await controller.DeleteCar(carToDelete.Id);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as Car;
            Assert.NotNull(resultValue);
            Assert.Equal(response.Message, "Successfullt deleted car");
        }

        [Fact]
        public async Task Car_Delete_BadRequest()
        {
            //setup
            _mediator.Setup(x => x.Send(It.IsAny<DeleteCarCommand>(), It.IsAny<CancellationToken>()));

            //inject
            var controller = new CarController(_carRepo.Object, _mediator.Object);

            //act
            var result = await controller.DeleteCar(-1);

            //assert
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult);

            var resultValue = badRequestObjectResult.Value as string;
            Assert.NotNull(resultValue);
            Assert.Equal("Parameter id must be greater than zero", resultValue);
        }
    }
}
