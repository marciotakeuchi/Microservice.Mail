

using AutoFixture;
using AutoMapper;
using MicroserviceMail.Domain;
using MicroserviceMail.ExternalServices;
using MicroserviceMail.Repository;
using MicroserviceMail.Services;
using MicroserviceMail.Tests.ExternalServices;
using MicroserviceMail.Tests.Repository;
using MicroserviceMail.ViewModel;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using MimeKit;
using Moq;
using Shouldly;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace MicroserviceMail.Tests.Services
{

    public class MailServicesTest
    {
        /// <summary>
        /// Nomenclatura no padrão Given-When-Then, com AAA (Arrange, Act, Assert)
        /// </summary>
        [Fact]
        public void ValidMail_SendMessageIsCalled_ReturnStatusSENT()
        {
            //Arrange
            var mimeKitClientMock = new Mock<IClient>();
            var sendMailRepositoryMock = new Mock<ISendMailRepository>();
            var mapperMock = new Mock<IMapper>();
            var mailService = new MailServices(mimeKitClientMock.Object, sendMailRepositoryMock.Object, mapperMock.Object);
            
            var mailViewModel = new Fixture().Create<MailViewModel>();
            var configurationSettingsMail = new Fixture().Create<ConfigurationSettingsMail>();

            //Act
            var result = mailService.SendMessage(mailViewModel, configurationSettingsMail);

            //Assert
            Assert.Equal(mailViewModel.To, result.To);
            Assert.Equal(mailViewModel.Body, result.Body);
            result.From.ShouldBe(mailViewModel.From); //usando biblioteca Shouldly
            Assert.Contains("SENT", result.Status);

            //Verifica se o metodo interno foi chamado 1 vez.
            mimeKitClientMock.Verify(mk => mk.ConfigureMessage(It.IsAny<MailViewModel>()), Times.Once);
            mimeKitClientMock.Verify(mk => mk.SendMessage(It.IsAny<MimeMessage>(),It.IsAny<ConfigurationSettingsMail>()), Times.Once);
        }

        [Fact]
        public void InvalidMail_IsvalidEmailIsCalled_ReturnFalse()
        {
            //Arrange
            var mimeKitClientMock = new Mock<IClient>();
            var sendMailRepositoryMock = new Mock<ISendMailRepository>();
            var mapperMock = new Mock<IMapper>();
            var mailService = new MailServices(mimeKitClientMock.Object, sendMailRepositoryMock.Object, mapperMock.Object);
            string emails = "joaoteste#teste.com";

            //Act
            var result = mailService.IsValidEmails(emails, Enum.EFieldMail.To);

            //Assert
            result.ShouldBe(false);

        }

        [Fact]
        public void InvalidRangeMail_IsvalidEmailIsCalled_ReturnFalse()
        {
            //Arrange
            var mimeKitClientMock = new Mock<IClient>();
            var sendMailRepositoryMock = new Mock<ISendMailRepository>();
            var mapperMock = new Mock<IMapper>();
            var mailService = new MailServices(mimeKitClientMock.Object, sendMailRepositoryMock.Object, mapperMock.Object);
            string emails = "joaoteste#teste.com;maria@teste.com";

            //Act
            var result = mailService.IsValidEmails(emails, Enum.EFieldMail.To);

            //Assert
            result.ShouldBe(false);

        }

        [Fact]
        public void ValidRangeMail_IsvalidEmailIsCalled_ReturnTrue()
        {
            //Arrange
            var mimeKitClientMock = new Mock<IClient>();
            var sendMailRepositoryMock = new Mock<ISendMailRepository>();
            var mapperMock = new Mock<IMapper>();
            var mailService = new MailServices(mimeKitClientMock.Object, sendMailRepositoryMock.Object, mapperMock.Object);
            string emails = "joaoteste@gmail.com;maria@teste.com;marcio.abc_d@teste.com.br";

            //Act
            var result = mailService.IsValidEmails(emails, Enum.EFieldMail.To);

            //Assert
            result.ShouldBe(true);

        }

        [Fact]
        public void ValidMailViewModel_SaveMailInfoIsCalled_ReturnMailSaved()
        {
            //Arrange
            var mimeKitClientMock = new Mock<IClient>();
            var sendMailRepositoryMock = new SendMailRepositoryFake();
            //Configuração do AutoMapper
            var viewModelToDomainMappingProfile = new ViewModelToDomainMappingProfile();
            var domainToViewModelMappingProfile = new DomainToViewModelMappingProfile();
            List<Profile> profiles = new List<Profile> { viewModelToDomainMappingProfile, domainToViewModelMappingProfile };
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            var mapper = new Mapper(configuration);
            var mailService = new MailServices(mimeKitClientMock.Object, sendMailRepositoryMock, mapper);
            MailViewModel mailViewModel = new Fixture().Create<MailViewModel>();

            //Act
            var result = mailService.SaveMailInfo(mailViewModel).Result;

            //Assert
            result.Status.ShouldContain("Salvo em");

        }
    }
}
