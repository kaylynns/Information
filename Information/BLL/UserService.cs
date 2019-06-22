﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using IDAO;
using IocContainer;
using DAO;
using Entity;

namespace BLL
{
   public class UserService:IUserBll
    {
      
       IUserDao iud = IocCreate.CreateAll<UserDao>("UserOne", "UserDao");

        public List<info_User> DengLu(string username, string pass) {
            return iud.DengLu(username, pass);
        }
        public List<info_User> SelectAll() {
            return iud.SelectAll();
        }
    }
}
