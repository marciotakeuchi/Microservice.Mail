using AutoMapper;
using MicroserviceMail.Domain;
using MicroserviceMail.ViewModel;

namespace MicroserviceMail.Services
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Mail, MailViewModel>();
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MailViewModel, Mail>();
        }
    }
}
