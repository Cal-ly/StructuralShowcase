namespace BeverageAPI.Profiles;

public class BeverageProfile : Profile
{
    public BeverageProfile()
    {
        CreateMap<BeverageDTO, Beverage>();
        CreateMap<Beverage, BeverageDTO>();
    }
}