using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVCBibliotheekBENSLE.CustomValidation
{
    public class CustomVergaderZaalNameValidation : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var result = context.Model.ToString();
            string numbers = "0123456789";
            var lst = new List<ModelValidationResult>();
            foreach (char character in result)
            {
                if (numbers.Contains(character))
                {
                    lst.Add(new ModelValidationResult("", "Naam van vergaderzaal mag geen nummers bevatten"));
                    return lst;
                }
            }
            return lst;
        }
    }
}
