using Philbor.Application.Abstractions;
using Philbor.Infrastructure.DataContext;

namespace Philbor.Infrastructure.Repository
{
    public class ApplicationRepository : Repository<ApplicationDbContext>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
