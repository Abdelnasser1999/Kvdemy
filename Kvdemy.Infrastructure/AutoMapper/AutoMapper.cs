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
            CreateMap<StudentViewModel, User>();
            CreateMap<User, TeacherViewModel>();
            //CreateMap<User, AdminViewModel>().ForMember(x => x.UserType, x => x.MapFrom(x => x.UserType.ToString()));

            CreateMap<CreateStudentDto, User>();
            CreateMap<CreateTeacherDto, User>();

            //CreateMap<CreateAdminDto, User>();

            CreateMap<UpdateStudentDto, User>().ReverseMap();

            //CreateMap<UserViewModel, UpdateUserDto>();

            ///// Nationality
            CreateMap<Nationality, NationalityViewModel>();

            ///// Language
            CreateMap<Language, LanguageViewModel>();

            ///// LanguageLevel
            CreateMap<LanguageLevel, LanguageLevelViewModel>();



            ///// Category
            //CreateMap<Category, CategoryViewModel>();

            //CreateMap<CreateCategoryDto, Category>();

            //CreateMap<UpdateCategoryDto, Category>().ReverseMap().ForMember(x => x.Image, x => x.Ignore());

            ///// Product
            //CreateMap<Product, ProductViewModel>();

            //CreateMap<CreateProductDto, Product>();

            //CreateMap<UpdateProductDto, Product>().ReverseMap().ForMember(x => x.Image, x => x.Ignore());

            ///// Slider
            //CreateMap<Slider, SliderViewModel>();

            //CreateMap<CreateSliderDto, Slider>();

            //CreateMap<UpdateSliderDto, Slider>().ReverseMap().ForMember(x => x.Image, x => x.Ignore());

            ///// City
            //CreateMap<City, CityViewModel>();

            ////CreateMap<CreateCityDto, City>();

            ////CreateMap<UpdateCityDto, City>().ReverseMap();


            ///// Settings
            //CreateMap<Settings, SettingsViewModel>();
            //CreateMap<UpdateSettingsDto, Settings>().ReverseMap();


            ///// Cart
            //CreateMap<Cart, CartViewModel>();



            ///// CartItem
            //CreateMap<CartItem, CartItemViewModel>();
            //CreateMap<CreateCartItemDto, CartItem>();


            ///// Coupon
            //CreateMap<Coupon, CouponViewModel>();

            ///// Order
            //CreateMap<Order, OrderViewModel>();
            //CreateMap<UpdateOrderDto, Order>().ReverseMap();

            ///// OrderItem
            //CreateMap<OrderItem, OrderItemViewModel>();

            ///// FinanceAccount
            //CreateMap<FinanceAccount, FinanceAccountViewModel>();

            ///// Transactions
            //CreateMap<Transactions, TransactionsViewModel>();

            ///// Points
            //CreateMap<Point, PointViewModel>();

            ///// Transactions
            //CreateMap<PointTransaction, PointTransactionViewModel>();





            ///// Notifications
            //CreateMap<Notification, NotificationViewModel>();




        }
    }
}
