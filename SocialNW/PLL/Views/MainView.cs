using System;
using System.Collections.Generic;
using System.Text;

using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNW.PLL.Views
{
    public class AddFriendView
    {
        UserService userService;

        public AddFriendView(UserService userService)
        {
            this.userService = userService;
        }

        public void Show(User user)
        {
            try
            {
                var userAddFriendData = new UserFriendAddingData();
                Console.WriteLine("Введите email пользователя для добавления в друзья: ");

                userAddFriendData.FriendEmail = Console.ReadLine();
                userAddFriendData.UserId = user.Id;

                this.userService.AddFriend(userAddFriendData);
                SuccessMessage.Show("Пользователь успешно добавлен в друзья.");
            }

            catch(UserNotFoundException)
            {
                AlertMessage.Show("Пользователь с указанным email не существует.");
            }
            
            catch(Exception)
            {
                AlertMessage.Show("Ошибка при добавлении пользователя в друзья.");
            }
 
        }
    }
}
