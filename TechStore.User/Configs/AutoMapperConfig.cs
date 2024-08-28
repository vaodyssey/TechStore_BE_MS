using AutoMapper;
using TechStore.User.DTOs;
using TechStore.User.Payload;

namespace TechStore.User.Configs
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            RegisterRequestAndUser();
            UserAndUserDTO();
            UpdateUserRequestAndUser();
            NewProductDTOAndOrderDetail();
        }

        private void RegisterRequestAndUser()
        {
            CreateMap<RegisterRequest, Models.User>();
        }
        private void UserAndUserDTO()
        {
            CreateMap<Models.User, UserDTO>();
        }
        private void UpdateUserRequestAndUser()
        {
            CreateMap<UpdateUserRequest, Models.User>();
        }
        private void NewProductDTOAndOrderDetail()
        {
            CreateMap<NewProductDTO, Models.OrderDetail>().ForMember(orderDetail => orderDetail.Id,options => options.Ignore());
        }
    }

}
