namespace BeverageAPI.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Map User entity to UserDTO
        CreateMap<User, UserDTO>();

        // Map UserDTO to User entity
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));  // Map nested Customer

        CreateMap<UserDTO, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}