using datingapp.api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace datingapp.api.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            users.ForEach(user =>
            {
                using var hmc = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmc.ComputeHash(Encoding.UTF8.GetBytes("Pass@Msewe3"));
                user.PasswordSalt = hmc.Key;
            });

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
