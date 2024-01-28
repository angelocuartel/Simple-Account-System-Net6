using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SimpleAccountSystem.Mvc.Dto;

namespace SimpleAccountSystem.Mvc.Controllers.Base
{
    public class CustomControllerBase : Controller
    {
        protected async Task<ValidationResult> ValidateModelAsync<Tmodel, Tvalidator>(Tmodel model) where Tvalidator : IValidator<Tmodel>
        {
            if(model is null)
            {
                new ValidationResult() 
                {
                    Errors = new List<ValidationFailure>
                    {
                        new ValidationFailure(string.Empty,"model is required") 
                    } 
                };
            }
            
            var validator = Activator.CreateInstance<Tvalidator>();

            return await validator.ValidateAsync(model);
        }

        protected IActionResult FluentBadRequest(List<ValidationFailure> errors)
        {
            var firstError = errors.FirstOrDefault()?.ErrorMessage;
            return BadRequest(firstError);
        }

        protected JsonResult DataTableResult<T>(IEnumerable<T> records, int dataTableDraw)
        {
            var genericResultDto =  new GenericResultDto<T>
            {
                draw = dataTableDraw,
                data = records,
                recordsTotal = records.Count(),
                recordsFiltered = records.Count()
            };

            return new JsonResult(genericResultDto);
        }
        

        
    }
}
