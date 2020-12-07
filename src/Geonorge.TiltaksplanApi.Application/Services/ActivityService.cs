using FluentValidation;
using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWorkManager _uowManager;
        private readonly IActivityRepository _activityRepository;
        private readonly IActivityViewModelMapper _activityViewModelMapper;
        private readonly IValidator<Activity> _activityValidator;
        private readonly IOrganizationQuery _organizationQuery;
        private readonly IMeasureQuery _measureQuery;

        public ActivityService(
            IUnitOfWorkManager uowManager,
            IActivityRepository activityRepository,
            IActivityViewModelMapper activityViewModelMapper,
            IValidator<Activity> activityValidator,
            IOrganizationQuery organizationQuery,
            IMeasureQuery measureQuery)
        {
            _uowManager = uowManager;
            _activityRepository = activityRepository;
            _activityViewModelMapper = activityViewModelMapper;
            _activityValidator = activityValidator;
            _organizationQuery = organizationQuery;
            _measureQuery = measureQuery;
        }

        public async Task<ActivityViewModel> CreateAsync(ActivityViewModel viewModel)
        {
            var activity = _activityViewModelMapper.MapToDomainModel(viewModel);

            if (IsValid(activity))
            {
                using var uow = _uowManager.GetUnitOfWork();
                _activityRepository.Create(activity);
                await uow.SaveChangesAsync();
            }

            var resultViewModel = _activityViewModelMapper.MapToViewModel(activity, viewModel.Culture);
            await CompleteDataForViewModel(resultViewModel);

            return resultViewModel;
        }

        public async Task<ActivityViewModel> UpdateAsync(int id, ActivityViewModel viewModel)
        {
            var update = _activityViewModelMapper.MapToDomainModel(viewModel);

            using var uow = _uowManager.GetUnitOfWork();
            
            var activity = await _activityRepository
                .GetByIdAsync(id);

            activity.Update(update);

            if (IsValid(activity))
                await uow.SaveChangesAsync();

            var resultViewModel = _activityViewModelMapper.MapToViewModel(activity, viewModel.Culture);
            await CompleteDataForViewModel(resultViewModel);

            return resultViewModel;
        }

        public async Task DeleteAsync(int id)
        {
            using var uow = _uowManager.GetUnitOfWork();

            var activity = await _activityRepository.GetByIdAsync(id);
            _activityRepository.Delete(activity);

            await uow.SaveChangesAsync();
        }

        private async Task CompleteDataForViewModel(ActivityViewModel viewModel)
        {
            var organizations = await _organizationQuery.GetAllAsync();

            if (viewModel.MeasureId > 0)
            {
                var measure = await _measureQuery.GetByIdAsync(viewModel.MeasureId, null);
                viewModel.ResponsibleAgency = measure?.Owner;
            }

            viewModel.Participants.ForEach(participant =>
            {
                if (participant.OrganizationId.HasValue)
                {
                    var organization = organizations
                        .SingleOrDefault(organization => organization.Id == participant.OrganizationId);

                    participant.Name = organization.Name;
                    participant.OrgNumber = organization.OrgNumber;
                }
            });
        }

        private bool IsValid(Activity activity)
        {
            activity.ValidationResult = _activityValidator
                .Validate(activity);

            return activity.ValidationResult.IsValid;
        }
    }
}
