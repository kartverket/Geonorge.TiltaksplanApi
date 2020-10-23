using System.Collections.Generic;
using System.Linq;
using Geonorge.TiltaksplanApi.Application.Models;
using Microsoft.Extensions.Localization;

namespace Geonorge.TiltaksplanApi.Web.Resources
{
    public static class ViewModelWithValidationExtensions
    {
        public static ViewModelWithValidation SetErrorMessages(this ViewModelWithValidation viewModel, IStringLocalizer localizer)
        {
            if (viewModel == null)
                return null;

            if (!viewModel.HasErrors)
                return viewModel;

            var ownAndNestedValidationErrors = viewModel.AllValidationErrors();

            foreach (var validationError in ownAndNestedValidationErrors)
            {
                if (string.IsNullOrWhiteSpace(validationError.ErrorCode))
                    continue;

                validationError.ErrorMessage = localizer[validationError.ErrorCode];
            }

            return viewModel;
        }

        public static IEnumerable<ViewModelWithValidation> SetErrorMessages(this IEnumerable<ViewModelWithValidation> viewModels, IStringLocalizer localizer)
        {
            var viewModelArray = viewModels?.ToArray();

            if (viewModelArray == null)
                return null;

            if (!viewModelArray.Any(viewModel => viewModel.HasErrors))
                return viewModelArray;

            foreach (var viewModel in viewModelArray)
            {
                var ownAndNestedValidationErrors = viewModel.AllValidationErrors();

                foreach (var validationError in ownAndNestedValidationErrors)
                {
                    if (string.IsNullOrWhiteSpace(validationError.ErrorCode))
                        continue;

                    validationError.ErrorMessage = localizer[validationError.ErrorCode];
                }
            }
            return viewModelArray;
        }

        public static IEnumerable<string> AllErrorCodes(this ViewModelWithValidation viewModel)
        {
            var list = new List<string>();
            list.AddRange(viewModel.AllValidationErrors().Select(error => error.ErrorCode));

            return list;
        }
    }
}

