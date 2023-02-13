using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using static System.Net.Mime.MediaTypeNames;

namespace MVCGroentenEnFruit.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("user")]
    public class UserTagHelper : TagHelper
    {
        private UserManager<IdentityUser> _userManager;
        public UserTagHelper(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [ViewContext]
        [HtmlAttributeName]
        public ViewContext ViewContext{ get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = ViewContext.HttpContext.User;
            //user = ClaimsPrincipal, dus niet de identityUser
            var identity = await _userManager.GetUserAsync(user);
            output.Content.SetHtmlContent(UserCard(identity));
            

            //string content = $"<div class='card'>";
            //content += $"<h5 class='card-title'>Identity Card</h5>";
            //content += $"<h6 class='card-subtitle mb-2 text-muted p-2'>EMAIL</h6>";
            //content += $"<p class='card-text m-1 p-3'>{identity.Email}</p>";
            //content += $"<h6 class='card-subtitle mb-2 text-muted p-2'>USERNAME</h6>";
            //content += $"<p class='card-text m-1 p-3'>{identity.UserName}</p>";
            //content += $"</div>";
            //output.Content.AppendHtml(content);


        }
        private TagBuilder UserCard(IdentityUser user)
        {
            TagBuilder divCard = new TagBuilder("div");
            divCard.Attributes["class"] = "car";
            divCard.Attributes["style"] = "width: 18rem";
            divCard.InnerHtml.AppendHtml(UserCardTitle("Identity Card"));
            if (user is not null)
            {
                //email
                divCard.InnerHtml.AppendHtml(UserCardItem("EMAIL",user.Email));
                //username
                divCard.InnerHtml.AppendHtml(UserCardItem("USERNAME", user.UserName));
            }
            return divCard;
        }
        private TagBuilder UserCardTitle(string title)
        {
            TagBuilder titleH5 = new TagBuilder("h5");
            titleH5.Attributes["class"] = "card-title";
            titleH5.InnerHtml.Append(title);
            return titleH5;
        }
        private TagBuilder UserCardItem(string header,string item)
        {
            TagBuilder cardItem = new TagBuilder("h6");
            cardItem.Attributes["class"] = "card-subtitle mb-2 text-muted p-2";
            cardItem.InnerHtml.Append(header);
            TagBuilder cardText = new TagBuilder("p");
            cardText.Attributes["class"] = "card-text m-1 p-3";
            cardText.InnerHtml.Append(item);
            cardItem.InnerHtml.AppendHtml(cardText);
            return cardItem;
            return cardItem;
        }
    }
}
