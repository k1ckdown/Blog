namespace Blog.Application.Common;

public static class Constants
{
    public static class AdditionalPartHouseType
    {
        public const string BuildingWing = "к.";
        public const string Structure = "стр.";
        public const string Construction = "соор.";
    }
    
    public static class HouseType
    {
        public const string Estate = "Владение";
        public const string House = "Дом";
        public const string Townhouse = "Домовладение";
        public const string Garage = "Гараж";
        public const string Building = "Здание";
        public const string Mine = "Шахта";
        public const string Structure = "Строение";
        public const string Construction = "Сооружение";
        public const string Block = "Литера";
        public const string BuildingWing = "Корпус";
        public const string Basement = "Подвал";
        public const string BoilerHouse = "Котельная";
        public const string Cellar = "Погреб";
        public const string UnfinishedConstructionObject = "Объект незавершенного строительства (ОНС)";
    }
    
    public static class AddressLevel
    {
        public const string Region = "Субъект РФ";
        public const string AdministrativeArea = "Административный район";
        public const string MunicipalArea = "Муниципальный район";
        public const string RuralUrbanSettlement = "Сельское/городское поселение";
        public const string City = "Город";
        public const string Locality = "Населенный пункт";
        public const string ElementOfPlanningStructure = "Элемент планировочной структуры";
        public const string ElementOfRoadNetwork = "Элемент улично-дорожной сети";
        public const string Land = "Земельный участок";
        public const string Building = "Здание (сооружение)";
        public const string Room = "Помещение";
        public const string RoomInRooms = "Помещения в пределах помещения";
        public const string AutonomousRegionLevel = "Уровень автономного округа (устаревшее)";
        public const string IntracityLevel = "Уровень внутригородской территории (устаревшее)";
        public const string AdditionalTerritoriesLevel = "Уровень дополнительных территорий (устаревшее)";
        public const string LevelOfObjectsInAdditionalTerritories = "Уровень объектов на дополнительных территориях (устаревшее)";
        public const string CarPlace = "Машиноместо";
    }
}