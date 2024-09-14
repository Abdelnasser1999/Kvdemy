using AutoMapper;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;

namespace Kvdemy.infrastructure.Mapper
{

    public class AutoMapper : Profile
    {
        public AutoMapper()
        {

            /// User
            CreateMap<User, StudentViewModel>();
            CreateMap<User, TeacherViewModel>();
            CreateMap<User, TeacherSearchViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<User, SenderViewModel>();
            //CreateMap<User, AdminViewModel>().ForMember(x => x.UserType, x => x.MapFrom(x => x.UserType.ToString()));
            // User to TeacherViewModel mapping
            CreateMap<User, TeacherViewModel>()
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.Educations, opt => opt.MapFrom(src => src.Educations))
                .ForMember(dest => dest.Awards, opt => opt.MapFrom(src => src.Awards))
                .ForMember(dest => dest.Gallery, opt => opt.MapFrom(src => src.Gallery))
                .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Video))
                .ForMember(dest => dest.UserSpecialties, opt => opt.MapFrom(src => src.UserSpecialties))
                .ForMember(dest => dest.AvailableHours, opt => opt.MapFrom(src => src.AvailableHours))
                .ForMember(dest => dest.StartingPrice, opt => opt.MapFrom(src => src.StartingPrice))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.RatingSum, opt => opt.MapFrom(src => src.RatingSum))
                .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => src.RatingCount));

            CreateMap<CreateStudentDto, User>();
            CreateMap<CreateTeacherDto, User>();


            CreateMap<UpdateStudentDto, User>().ReverseMap();

            // UserSpecialty
            CreateMap<UserSpecialty, UserSpecialtyViewModel>();

            // Booking Mappings
            CreateMap<Booking, BookingViewModel>();
            CreateMap<CreateBookingDto, Booking>();

            // BookingMessage Mappings
            CreateMap<BookingMessage, BookingMessageViewModel>();
            CreateMap<CreateBookingMessageDto, BookingMessage>();

            // Notification Mappings
            CreateMap<Notification, NotificationDto>();


            ///// Nationality
            CreateMap<Nationality, NationalityViewModel>();

            ///// Language
            CreateMap<Language, LanguageViewModel>();

            ///// LanguageLevel
            CreateMap<LanguageLevel, LanguageLevelViewModel>();

            CreateMap<Education, EducationViewModel>();
            CreateMap<Award, AwardViewModel>();
            CreateMap<Gallery, GalleryViewModel>();
            CreateMap<Video, VideoViewModel>();


            /// Category
            CreateMap<Category, CategoryViewModel>();

            CreateMap<CreateCategoryDto, Category>();

            CreateMap<UpdateCategoryDto, Category>().ReverseMap().ForMember(x => x.Image, x => x.Ignore());

            ///// Slider
            CreateMap<Slider, SliderViewModel>();

            CreateMap<CreateSliderDto, Slider>();

            CreateMap<UpdateSliderDto, Slider>().ReverseMap().ForMember(x => x.Image, x => x.Ignore());

            ///// Gallery
            CreateMap<Gallery, GalleryViewModel>();

            CreateMap<GalleryDto, Gallery>();


            ///// Settings
            CreateMap<Settings, SettingsViewModel>();
            CreateMap<UpdateSettingsDto, Settings>().ReverseMap();


            ///// Notifications
            CreateMap<Notification, NotificationViewModel>();

            // Reports
            CreateMap<Report, ReportViewModel>();
			CreateMap<CreateReportDto, Report>();

            /// FinanceAccount
            CreateMap<FinanceAccount, FinanceAccountViewModel>();
            CreateMap<UpdateFinancialAccountDto, FinanceAccount>();

            /// Transactions
            CreateMap<AccountTransactions, TransactionsViewModel>();
            CreateMap<CreateTransactionDto, AccountTransactions>();


            /// WalletRequest
            CreateMap<WalletRequest, WalletRequestViewModel>();


        }
    }
}
