using AutoMapper;
using Notebook.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Notebook.Models
{
    public class AccountDM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public static MD5 md5hash;

        public AccountDM() {}

        static AccountDM()
        {
            md5hash = MD5.Create();
            if (!RoleExists("Admin"))
            {
                CreateRole("Admin");
            }
            if (!Global.business.UserExists("Admin"))
            {
                Global.business.CreateUser(Mapper.Map<User>(new AccountDM() { Name = "Admin", Password = CalculateMD5Hash("123456") } ));
            }
            if (!IsUserInRole(GetUserId("Admin"), "Admin"))
            {
                AddUserToRole(GetUserId("Admin"), "Admin");
            }
        }

        public static void Register(string name, string password)
        {
            if (Global.business.UserExists(name))
            {
                throw new Exception("User with that name already exists in a database");
            }
            AccountDM account = new AccountDM() { Name = name, Password = CalculateMD5Hash(password)};
            Global.business.CreateUser(Mapper.Map<User>(account));
        }

        public static List<AccountDM> GetAll()
        {
            return Mapper.Map<List<AccountDM>>(Global.business.GetAllUsers());
        }

        public static string GetUserName(int id)
        {
            return GetAll().First(u => u.Id == id).Name;
        }

        public static int GetUserId(string name)
        {
            return GetAll().First(u => u.Name == name).Id;
        }

        public static bool Login(string name, string password, bool rememberMe)
        {
            
            if (GetAll().Any(u => u.Name == name && u.Password == CalculateMD5Hash(password)))
            {
                FormsAuthentication.RedirectFromLoginPage(name, rememberMe);
                return true;
            }
            return false;
        }

        public static int CurrentUserId()
        {
            return GetUserId(HttpContext.Current.User.Identity.Name);
        }

        public static string CalculateMD5Hash(string input)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5hash.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static bool RoleExists(string role)
        {
            return Global.business.RoleExists(role);
        }

        public static void CreateRole(string role)
        {
            Global.business.CreateRole(role);
        }

        public static bool IsUserInRole(int userId, string role)
        {
            return Global.business.IsUserInRole(userId, role);
        }

        public static bool IsUserInRole(string role)
        {
            return IsUserInRole(CurrentUserId(), role);
        }

        public static void AddUserToRole(int userId, string role)
        {
            Global.business.AddUserToRole(userId, role);
        }
    }
}