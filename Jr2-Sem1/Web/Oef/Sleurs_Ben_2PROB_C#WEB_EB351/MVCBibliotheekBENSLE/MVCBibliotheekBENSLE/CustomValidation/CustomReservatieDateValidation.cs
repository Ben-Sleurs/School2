using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVCBibliotheekBENSLE.CustomValidation
{
    public class CustomReservatieDateValidation : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var lst = new List<ModelValidationResult>();
            var today = DateTime.Now.Date;
            var result = context.Model.ToString();
            DateTime date;
            if (!DateTime.TryParse(result,out date))
            {
                lst.Add(new ModelValidationResult("", "Geen geldige datum"));
                return lst;
            }
            if (date.Date<today)
            {
                lst.Add(new ModelValidationResult("", "Datum mag nie voor vandaag zijn"));
                return lst;
            }
            return lst;

        }
    }
}
