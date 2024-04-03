using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kvdemy.Web.Data
{
	public class KvdemyDbContext : IdentityDbContext
	{
		public KvdemyDbContext(DbContextOptions<KvdemyDbContext> options)
			: base(options)
		{
		}
	}
}
