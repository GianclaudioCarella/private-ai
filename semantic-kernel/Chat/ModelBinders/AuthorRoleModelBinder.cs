﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Chat.ModelBinders
{
    public class AuthorRoleModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            ArgumentNullException.ThrowIfNull(bindingContext);

            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(new AuthorRole(value));

            //if (!AuthorRole.TryParse(value, out var id))
            //{
            //    // Non-integer arguments result in model state errors
            //    bindingContext.ModelState.TryAddModelError(
            //        modelName, "Author Id must be an integer.");

            //    return Task.CompletedTask;
            //}

            //// Model will be null if not found, including for
            //// out of range id values (0, -3, etc.)
            //var model = _context.Authors.Find(id);
            //bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;

        }
    }
}
