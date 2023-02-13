using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebAppMvcClientLocation.CustomModelValidation
{
    public class CustomNoNumbers : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var lst = new List<ModelValidationResult>();
            if (context.Model.ToString().All(char.IsDigit))
            {
                lst.Add(new ModelValidationResult("", "Name cannot contain numbers"));
            }
            return lst;
        }
    }
}
