using Kvdemy.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using System.Drawing;
using System.Reflection.Emit;

namespace Kvdemy.Web.Data
{
	public class KvdemyDbContext : IdentityDbContext<User>
	{
		public KvdemyDbContext(DbContextOptions<KvdemyDbContext> options)
			: base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Category>().HasQueryFilter(x => !x.IsDelete);

            builder.Entity<Slider>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<UserSpecialty>()
                .HasOne(s => s.User)
                .WithMany(u => u.UserSpecialties)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserSpecialty>()
                .HasOne(s => s.Category)
                .WithMany()
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserSpecialty>()
                .HasOne(s => s.Subcategory)
                .WithMany()
                .HasForeignKey(s => s.SubcategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Product>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<City>().HasQueryFilter(x => !x.IsDelete);


            //builder.Entity<Cart>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<CartItem>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<Coupon>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<FinanceAccount>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<Order>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<OrderItem>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<Point>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<PointTransaction>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<Transactions>().HasQueryFilter(x => !x.IsDelete);


            //builder.Entity<User>()
            //       .HasQueryFilter(x => !x.IsDelete);

            //builder.Entity<Favorite>().HasKey(x => new { x.ProductId, x.UserId });

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<ContactPhoneNumber> ContactPhoneNumbers { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageLevel> LanguageLevels { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<RegistrationInfo> RegistrationInfos { get; set; }
        public DbSet<StudentLanguage> StudentLanguages { get; set; }
        public DbSet<Video> Videos { get; set; }
		public DbSet<Category> Categories { get; set; }

		public DbSet<Settings> Settings { get; set; }
		public DbSet<Slider> Sliders { get; set; }
        public DbSet<UserSpecialty> UserSpecialties { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingMessage> BookingMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }

    }
}
