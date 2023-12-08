using Application.Common;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Address;
using Domain.Entities.Address.Enums;

namespace Application.DTOs.Addresses;

public sealed class SearchAddressModel : IMapFrom<AddressElement>
{
    public required long ObjectId { get; set; }
    public required Guid ObjectGuid { get; set; }
    public string? Text { get; set; }
    public required GarAddressLevel ObjectLevel { get; set; }
    public string? ObjectLevelText { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<House, SearchAddressModel>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => GetAddressOfHouse(src)))
            .ForMember(dest => dest.ObjectLevel, opt => opt.MapFrom(src => GarAddressLevel.Building))
            .ForMember(dest => dest.ObjectLevelText, opt => opt.MapFrom(src => GetObjectLevelText(GarAddressLevel.Building)));

        profile.CreateMap<AddressElement, SearchAddressModel>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => $"{src.TypeName} {src.Name}"))
            .ForMember(dest => dest.ObjectLevel, opt => opt.MapFrom(src => GetAddressLevel(src.Level)))
            .ForMember(dest => dest.ObjectLevelText, opt => opt.MapFrom(src => GetObjectLevelText(GetAddressLevel(src.Level))));
    }

    private static GarAddressLevel GetAddressLevel(string objectLevelString) =>
        (GarAddressLevel)Convert.ToInt32(objectLevelString);

    private static string? GetObjectLevelText(GarAddressLevel level) =>
        level switch
        {
            GarAddressLevel.Region => Constants.AddressLevel.Region,
            GarAddressLevel.AdministrativeArea => Constants.AddressLevel.AdministrativeArea,
            GarAddressLevel.MunicipalArea => Constants.AddressLevel.MunicipalArea,
            GarAddressLevel.RuralUrbanSettlement => Constants.AddressLevel.RuralUrbanSettlement,
            GarAddressLevel.City => Constants.AddressLevel.City,
            GarAddressLevel.Locality => Constants.AddressLevel.Locality,
            GarAddressLevel.ElementOfPlanningStructure => Constants.AddressLevel.ElementOfPlanningStructure,
            GarAddressLevel.ElementOfRoadNetwork => Constants.AddressLevel.ElementOfRoadNetwork,
            GarAddressLevel.Land => Constants.AddressLevel.Land,
            GarAddressLevel.Building => Constants.AddressLevel.Building,
            GarAddressLevel.Room => Constants.AddressLevel.Room,
            GarAddressLevel.RoomInRooms => Constants.AddressLevel.RoomInRooms,
            GarAddressLevel.AutonomousRegionLevel => Constants.AddressLevel.AutonomousRegionLevel,
            GarAddressLevel.IntracityLevel => Constants.AddressLevel.IntracityLevel,
            GarAddressLevel.AdditionalTerritoriesLevel => Constants.AddressLevel.AdditionalTerritoriesLevel,
            GarAddressLevel.LevelOfObjectsInAdditionalTerritories => Constants.AddressLevel
                .LevelOfObjectsInAdditionalTerritories,
            GarAddressLevel.CarPlace => Constants.AddressLevel.CarPlace,
            _ => null
        };

    private static string GetAddressOfHouse(House house)
    {
        var houseTypeText = house.Type switch
        {
            HouseType.Estate => Constants.HouseType.Estate,
            HouseType.House => Constants.HouseType.House,
            HouseType.Townhouse => Constants.HouseType.Townhouse,
            HouseType.Garage => Constants.HouseType.Garage,
            HouseType.Building => Constants.HouseType.Building,
            HouseType.Mine => Constants.HouseType.Mine,
            HouseType.Structure => Constants.HouseType.Structure,
            HouseType.Construction => Constants.HouseType.Construction,
            HouseType.Block => Constants.HouseType.Block,
            HouseType.BuildingWing => Constants.HouseType.BuildingWing,
            HouseType.Basement => Constants.HouseType.Basement,
            HouseType.BoilerHouse => Constants.HouseType.BoilerHouse,
            HouseType.Cellar => Constants.HouseType.Cellar,
            HouseType.UnfinishedConstructionObject => Constants.HouseType.UnfinishedConstructionObject,
            _ => ""
        };
        
        var address = $"{houseTypeText} {house.Number}";
        
        if (house.FirstAdditionalNumber != null) 
            address += $" {AdditionalTypeText(house.FirstAdditionalType)} {house.FirstAdditionalNumber}";
        if (house.SecondAdditionalNumber != null)
            address += $" {AdditionalTypeText(house.SecondAdditionalType)} {house.SecondAdditionalNumber}";

        return address;

        string AdditionalTypeText(AdditionalPartHouseType? addPartHouseType) =>
            addPartHouseType switch
            {
                AdditionalPartHouseType.BuildingWing => Constants.AdditionalPartHouseType.BuildingWing,
                AdditionalPartHouseType.Structure => Constants.AdditionalPartHouseType.Structure,
                AdditionalPartHouseType.Construction => Constants.AdditionalPartHouseType.Construction,
                _ => ""
            };
    }
}