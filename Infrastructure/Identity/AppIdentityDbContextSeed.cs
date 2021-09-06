using System.Threading.Tasks;
using Core.Entities.Identity;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if(!userManager.Users.Any()){
                var user = new AppUser{
                    DisplayName= "Lejla",
                    Email = "lejla@test.com",
                    UserName= "lejla@test.com",
                    Address= new Address{
                        FirstName="Lejla",
                        LastName="Hodzic",
                        Street ="Raveien 227A",
                        City="Horten",
                        State="Vestfold",
                        ZipCode="3184"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}