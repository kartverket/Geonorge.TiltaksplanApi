﻿using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public class ActionPlanQuery : IAsyncQuery<IEnumerable<ActionPlanViewModel>>
    {
        private readonly ActionPlanContext _context;
        private readonly IViewModelMapper<ActionPlan, ActionPlanViewModel> _actionPlanViewModelMapper;

        public ActionPlanQuery(
            ActionPlanContext context,
            IViewModelMapper<ActionPlan, ActionPlanViewModel> actionPlanViewModelMapper)
        {
            _context = context;
            _actionPlanViewModelMapper = actionPlanViewModelMapper;
        }

        public async Task<IEnumerable<ActionPlanViewModel>> ExecuteAsync()
        {
            var actionPlans = await _context
                .ActionPlans
                .AsQueryable()
                .ToListAsync();

            return actionPlans.ConvertAll(actionPlan => _actionPlanViewModelMapper.MapToViewModel(actionPlan));
        }
    }
}