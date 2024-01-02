
namespace LeHotelMax.Tests
{
    /// <summary>
    /// Example xUnit test with repository mocking
    /// </summary>
    public class CreateHotelCommandHandlerTests
    {
        private readonly CreateHotelCommandHandler _createHotelCommandHandler;
        private readonly IMapper _mapper;

        private readonly Hotel _expectedHotel = new Hotel() { Name = "HotelTest", Price = 100, GeoLocation = new GeoLocation(0, 0) };

        public CreateHotelCommandHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<HotelProfiles>();
            });

            _mapper = config.CreateMapper();
            var mockedHotelRepository = new Mock<IHotelRepository>();
            mockedHotelRepository.Setup(x => x.AddHotelAsync(It.IsAny<Hotel>(), It.IsAny<CancellationToken>())).
            ReturnsAsync(_expectedHotel);
            _createHotelCommandHandler = new CreateHotelCommandHandler(mockedHotelRepository.Object, _mapper);
        }

        [Fact]
        public async Task CreateHotelCommandHandlerAsync_WithNullParameter_ThrowsException()
        {
            //Arrange
            CreateHotelCommand command = null;
            //Act
            OperationResult<HotelDto> actualhandledResponse = await _createHotelCommandHandler.Handle(command, new CancellationToken());
            //Assert
            Assert.True(actualhandledResponse.IsError);
            Xunit.Assert.Collection<Error>(actualhandledResponse.Errors, m => Xunit.Assert.Contains(HotelErrorMessages.HotelCommandQueryNull, m.Message));
        }


        [Fact]
        public async Task CreateHotelCommandHandlerAsync_Success()
        {
            //Arrange
            CreateHotelCommand command = new CreateHotelCommand()
            {
                Name= "HotelTest",
                Price = 100,
                GeoLocation = new GeoLocationDto()
                {
                    Longitude = 0,
                    Latitude = 0
                }
            };
            //Act
            OperationResult<HotelDto> result = await _createHotelCommandHandler.Handle(command, new CancellationToken());
            //Assert
            Assert.False(result.IsError);
            Assert.NotNull(result.Data);
            Assert.IsType<HotelDto>(result.Data);
            Assert.Equal(_expectedHotel.Name, command.Name);
        }



    }
}
