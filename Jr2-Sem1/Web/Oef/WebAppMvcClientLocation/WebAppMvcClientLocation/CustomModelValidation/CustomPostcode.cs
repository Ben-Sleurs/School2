using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebAppMvcClientLocation.CustomModelValidation
{
    public class CustomPostcode : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            string[] arr = new string[] { "3500", "3600", "3990" };
            var lst = new List<ModelValidationResult>();
            if (!arr.Contains(context.Model.ToString()))
            {
                lst.Add(new ModelValidationResult("", "Postcode moet 3500,3500 of 3990 zijn"));
            }
            return lst;
        }
    }
}
