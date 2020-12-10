using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public class OrganizationQuery : IOrganizationQuery
    {
        private readonly MeasurePlanContext _context;
        private readonly IViewModelMapper<Organization, OrganizationViewModel> _organizationViewModelMapper;

        public OrganizationQuery(
            MeasurePlanContext context,
            IViewModelMapper<Organization, OrganizationViewModel> organizationViewModelMapper)
        {
            _context = context;
            _organizationViewModelMapper = organizationViewModelMapper;
        }

        public async Task<IList<OrganizationViewModel>> GetAllAsync()
        {
            var organizations = await _context.Organizations
                .AsNoTracking()
                .OrderBy(organization => organization.Name)
                .ToListAsync();

            return organizations
                .ConvertAll(organization => _organizationViewModelMapper.MapToViewModel(organization));
        }

        public async Task<OrganizationViewModel> GetByIdAsync(int id)
        {
            var organization = await _context.Organizations
                .AsNoTracking()
                .SingleOrDefaultAsync(organization => organization.Id == id);

            return _organizationViewModelMapper.MapToViewModel(organization);
        }
    }
}
