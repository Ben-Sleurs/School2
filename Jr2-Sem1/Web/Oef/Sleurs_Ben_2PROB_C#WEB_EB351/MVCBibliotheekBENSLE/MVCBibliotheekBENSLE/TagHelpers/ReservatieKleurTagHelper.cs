using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVCBibliotheekBENSLE.TagHelpers
{
    public class ReservatieKleurTagHelper:TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(DateTime.Now.DayOfWeek==DayOfWeek.Saturday|| DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                output.Attributes.SetAttribute("class", "text-danger");
            }
            else
            {
                output.Attributes.SetAttribute("class", "text-success");
            }
        }
    }
}
