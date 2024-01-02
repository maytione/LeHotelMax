using FluentValidation.TestHelper;
using LeHotelMax.Application.Hotels.Validators;


namespace LeHotelMax.Tests
{
    public class CreateHotelCommandValidatorTests
    {
        [Fact]
        public void Validate_InvalidName_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new CreateHotelCommandValidator();
            var command = new CreateHotelCommand { Name = null }; // Invalid name

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Name)
                .WithErrorMessage("Hotel name can't be null");
        }

        [Fact]
        public void Validate_InvalidPrice_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new CreateHotelCommandValidator();
            var command = new CreateHotelCommand { Price = 0 }; // Invalid price

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Price)
                .WithErrorMessage("Hotel price must be greater than 0");
        }

        [Fact]
        public void Validate_InvalidGeoLocation_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new CreateHotelCommandValidator();
            var command = new CreateHotelCommand { GeoLocation = null }; // Invalid GeoLocation

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.GeoLocation)
                .WithErrorMessage("Hotel Geo Location must be provided");
        }

        [Fact]
        public void Validate_InvalidGeoLocation_OutOfRange_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new CreateHotelCommandValidator();
            var command = new CreateHotelCommand { 
                Name = "Hotel", 
                Price=100, 
                GeoLocation = new GeoLocationDto()  // Invalid GeoLocation
                { 
                    Latitude = -356, 
                    Longitude = 987 
                } 
            }; 

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        [Fact]
        public void Validate_InvalidGeoLocationLatitude_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new CreateHotelCommandValidator();
            var command = new CreateHotelCommand
            {
                GeoLocation = new GeoLocationDto () { Latitude = -100, Longitude = 50 } // Invalid latitude
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.GeoLocation.Latitude)
                  .WithErrorMessage("Latitude must be between -90 and 90 degrees");
        }

        [Fact]
        public void Validate_InvalidGeoLocationLongitude_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new CreateHotelCommandValidator();
            var command = new CreateHotelCommand
            {
                GeoLocation = new GeoLocationDto() { Latitude = 50, Longitude = 200 } // Invalid longitude
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.GeoLocation.Longitude)
                  .WithErrorMessage("Longitude must be between -180 and 180 degrees");
        }

    }
}
