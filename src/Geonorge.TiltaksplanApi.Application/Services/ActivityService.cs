using FluentValidation;
using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Application.Services.Authorization;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork;
using System;
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
        private readonly IAuthorizationService _authorizationService;

        public ActivityService(
            IUnitOfWorkManager uowManager,
            IActivityRepository activityRepository,
            IActivityViewModelMapper activityViewModelMapper,
            IValidator<Activity> activityValidator,
            IOrganizationQuery organizationQuery,
            IMeasureQuery measureQuery,
            IAuthorizationService authorizationService)
        {
            _uowManager = uowManager;
            _activityRepository = activityRepository;
            _activityViewModelMapper = activityViewModelMapper;
            _activityValidator = activityValidator;
            _organizationQuery = organizationQuery;
            _measureQuery = measureQuery;
            _authorizationService = authorizationService;
        }

        public async Task<ActivityViewModel> CreateAsync(ActivityViewModel viewModel)
        {
            await _authorizationService.AuthorizeActivity(UserActivity.CreateActivity, viewModel.MeasureId);

            var activity = _activityViewModelMapper.MapToDomainModel(viewModel);

            if (await IsValid(activity))
            {
                using var uow = _uowManager.GetUnitOfWork();
                activity.LastUpdated = DateTime.Now;
                _activityRepository.Create(activity);
                await uow.SaveChangesAsync();
            }

            var resultViewModel = _activityViewModelMapper.MapToViewModel(activity, viewModel.Culture);
            await CompleteDataForViewModel(resultViewModel);

            return resultViewModel;
        }

        public async Task<ActivityViewModel> UpdateAsync(int id, ActivityViewModel viewModel)
        {
            await _authorizationService.AuthorizeActivity(UserActivity.UpdateActivity, viewModel.MeasureId);

            var update = _activityViewModelMapper.MapToDomainModel(viewModel);

            using var uow = _uowManager.GetUnitOfWork();
            
            var activity = await _activityRepository
                .GetByIdAsync(id);

            activity.Update(update);

            if (await IsValid(activity))
            {
                activity.LastUpdated = DateTime.Now;
                await uow.SaveChangesAsync();
            }

            var resultViewModel = _activityViewModelMapper.MapToViewModel(activity, viewModel.Culture);
            await CompleteDataForViewModel(resultViewModel);

            return resultViewModel;
        }

        public async Task DeleteAsync(int id)
        {          
            using var uow = _uowManager.GetUnitOfWork();

            var activity = await _activityRepository.GetByIdAsync(id);

            await _authorizationService.AuthorizeActivity(UserActivity.DeleteActivity, activity.MeasureId);

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

        private async Task<bool> IsValid(Activity activity)
        {
            activity.ValidationResult = await _activityValidator
                .ValidateAsync(activity);

            return activity.ValidationResult.IsValid;
        }
    }
}
