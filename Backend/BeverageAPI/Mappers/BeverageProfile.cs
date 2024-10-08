namespace BeverageAPI.Mappers;

public class BeverageProfile : Profile
{
    public BeverageProfile()
    {
        CreateMap<BeverageDTO, Beverage>();
        CreateMap<Beverage, BeverageDTO>();
    }
}
