using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HogeschoolPXL.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement(Attributes ="source")]
    public class PxlImageTagHelper : TagHelper
    {
        public string Source { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.Attributes.SetAttribute("class", "img-thumbnail");
            output.Attributes.SetAttribute("src", Source);
            string styling = "width:50px;height:50px;";
            output.Attributes.SetAttribute("style", styling);
        }
    }
}
