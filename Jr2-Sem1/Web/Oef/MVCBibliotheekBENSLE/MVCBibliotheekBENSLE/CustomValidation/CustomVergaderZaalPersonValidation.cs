using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVCBibliotheekBENSLE.CustomValidation
{
    public class CustomVergaderZaalPersonValidation : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var result = context.Model.ToString();
            var lst = new List<ModelValidationResult>();
            int amountPersons;
            if (!int.TryParse(result,out amountPersons))
            {
                lst.Add(new ModelValidationResult("", "Aantal personen in vergaderzaal moet een integer zijn"));
                return lst;
            }
            if (amountPersons<1)
            {
                lst.Add(new ModelValidationResult("", "Aantal personen in vergaderzaal moet minstenst 1 zijn"));
                return lst;
            }
            if (amountPersons > 15)
            {
                lst.Add(new ModelValidationResult("", "Aantal personen in vergaderzaal mag maximum 15 zijn"));
                return lst;
            }
            return lst;
        }
    }
}
