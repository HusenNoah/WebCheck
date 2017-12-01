using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModels
{
    public class Login
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public Permissions CurrentPermissions { get; set; }
        public bool Active { get; set; }
        public enum Permissions
        {
            None = 0,
            Update = 1 << 0,
            Delete = 1 << 1,
            Add = 1 << 2,
            View = 1 << 3 | Add,
            Super = (Update | View),
            Admin = (Update | Delete | Add | View)
        }

    }
}