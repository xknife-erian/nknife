using AutoMapper;
using Example.Common;
using Example.View;
using System.Collections.Generic;

namespace Example.Logic.MapProfiles
{
    public class GeneralProfiles : Profile
    {
        public GeneralProfiles()
        {
            CreateMap<Book, BookVo>().ForMember(
                d => d.Publisher,
                m => m.MapFrom((src, dest) => new Publisher() {Id = dest.Id}));
            CreateMap<BookVo, Book>().ForMember(
                d => d.Publisher, memberOptions:
                m => m.MapFrom((src, dest) => src.Publisher.Id));
            CreateMap<Publisher, PublisherVo>().ReverseMap();
            CreateMap<Person, PersonVo>().ReverseMap();
            CreateMap<BuyingRecord, BuyingRecordVo>().ReverseMap();
        }
    }
}
// CreateMap<User, OrganizationDto>()
            //     .Ignore(s => s.Id, s => s.Name, s => s.Address)
            //     .ForMember(d => d.User, src => src.MapFrom(s => s.Name))
            //     .ForMember(d => d.UserMobile, src => src.MapFrom(s => s.Mobile));
            // CreateMap<Organization, OrganizationDto>().ReverseMap()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
            //     {
            //         if (string.IsNullOrEmpty(src.Id)) //viewmodel向领域实体转换时，viewmodel如果没有ID，domain的ID将不被覆盖
            //             return dest.Id;
            //         return src.Id;
            //     }));
            // CreateMap<Role, RoleDto>().ReverseMap()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
            //     {
            //         if (string.IsNullOrEmpty(src.Id))
            //             return dest.Id;
            //         return src.Id;
            //     }));
            // CreateMap<User, UserDto>().ReverseMap()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
            //     {
            //         if (string.IsNullOrEmpty(src.Id))
            //             return dest.Id;
            //         return src.Id;
            //     }));
            // CreateMap<Community, CommunityDto>().ReverseMap()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
            //     {
            //         if (string.IsNullOrEmpty(src.Id))
            //             return dest.Id;
            //         return src.Id;
            //     }));
            // CreateMap<Household, HouseholdVm>().ReverseMap()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
            //     {
            //         if (string.IsNullOrEmpty(src.Id))
            //             return dest.Id;
            //         return src.Id;
            //     }));
            // CreateMap<HouseholdDto, HouseholdVm>().ReverseMap()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
            //     {
            //         if (string.IsNullOrEmpty(src.Id))
            //             return dest.Id;
            //         return src.Id;
            //     }));
            // CreateMap<BuildingAreaChain, BuildingAreaChainDto>().ReverseMap()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
            //     {
            //         if (string.IsNullOrEmpty(src.Id))
            //             return dest.Id;
            //         return src.Id;
            //     }));
            // CreateMap<WaterMeter, WaterMeterVm>().ReverseMap()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
            //     {
            //         if (string.IsNullOrEmpty(src.Id)) //viewmodel向领域实体转换时，viewmodel如果没有ID，domain的ID将不被覆盖
            //             return dest.Id;
            //         return src.Id;
            //     }));
            // CreateMap<WaterMeterDto, WaterMeterVm>().ReverseMap()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
            //     {
            //         if (string.IsNullOrEmpty(src.Id)) //viewmodel向领域实体转换时，viewmodel如果没有ID，domain的ID将不被覆盖
            //             return dest.Id;
            //         return src.Id;
            //     }));
            // CreateMap<MeterReading, MeterReadingDto>().ReverseMap();