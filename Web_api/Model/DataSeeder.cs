using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_api.Model
{
    public class DataSeeder
    {
        private readonly UsersContext usersContext;

        public DataSeeder(UsersContext usersContext)
        {
            this.usersContext = usersContext;
        }
        public void Seed()
        {
            if (!usersContext.UserInfos.Any())
            {
                var users = new List<UserInfo>()
                {
                    new UserInfo()
                    {
                        Id=1 ,Name="Alex", Lastname="Ryan",Email="abc@string.com"
                    },
                     new UserInfo()
                     {
                        Id=2,Name="Mehmet", Lastname="Ryan",Email="abc@string.com"
                     }
                };
                usersContext.UserInfos.AddRange(users);
                usersContext.SaveChanges();
            }
        }
    }
}
