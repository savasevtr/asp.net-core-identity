using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyIdentity.Context;

namespace UdemyIdentity.TagHelpers
{
    [HtmlTargetElement("UsersRoles")]
    public class UserRolesTagHelper : TagHelper
    {
        private readonly UserManager<AppUser> userManager;

        public UserRolesTagHelper(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public int UserId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == UserId);

            var roles = await userManager.GetRolesAsync(user);

            var builder = new StringBuilder();

            foreach (var item in roles)
            {
                builder.Append($"<span class='badge badge - success'>{item}</span>");
            }

            output.Content.SetHtmlContent(builder.ToString());
        }
    }
}
