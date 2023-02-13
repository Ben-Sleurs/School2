using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVCGroentenEnFruit.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("td", Attributes="role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<IdentityUser>? _userManager;
        private RoleManager<IdentityRole>? _roleManager;
        [HtmlAttributeName("role")]
        public string Role { get; set; }

        public RoleUsersTagHelper(UserManager<IdentityUser>? userManager, RoleManager<IdentityRole>? roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
           List<string> Names = new List<string>();
            IdentityRole role = await _roleManager.FindByIdAsync(Role);
            if (role != null)
            {
                foreach (var user in _userManager.Users)
                {
                    if (user !=null && await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        Names.Add(user.UserName);
                    }
                }
            }
            output.Content.SetContent(Names.Count == 0 ? "No Users": string.Join(", ", Names));
        }
    }
}
