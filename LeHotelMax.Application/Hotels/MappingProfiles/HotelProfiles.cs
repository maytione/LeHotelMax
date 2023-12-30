using AutoMapper;
using LeHotelMax.Application.Hotels.Command;
using LeHotelMax.Application.Hotels.Dtos;
using LeHotelMax.Domain.Aggregates;
using LeHotelMax.Domain.Aggregates.ValueObjects;

namespace LeHotelMax.Application.Hotels.MappingProfiles
{
    public class HotelProfiles: Profile
    {
        public HotelProfiles()
        {
            CreateMap<Hotel, HotelDto>();

            CreateMap<HotelDistanceInfo, HotelDistanceDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src =>
                src.Hotel!.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                src.Hotel!.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src =>
                src.Hotel!.Price))
             .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(src =>
                 new GeoLocation(src.Hotel!.GeoLocation.Latitude, src.Hotel.GeoLocation.Longitude)));

            CreateMap<HotelDto, UpdateHotelCommand>();
            CreateMap<UpdateHotelCommand, Hotel>();
            CreateMap<UpdateHotelCommand, Hotel>()
            .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(src =>
                new GeoLocation(src.GeoLocation!.Latitude,src.GeoLocation.Longitude)));

            CreateMap<HotelCreateDto, CreateHotelCommand>();
            CreateMap<CreateHotelCommand, Hotel>();
            CreateMap<CreateHotelCommand, Hotel>()
            .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(src =>
                new GeoLocation(src.GeoLocation!.Latitude, src.GeoLocation.Longitude)));


            CreateMap<GeoLocation, GeoLocationDto>();

        }
    }
}
