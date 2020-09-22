using System;
using System.Text.Json.Serialization;

namespace UserService.Models.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdentityNumber { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string WrongPasswordCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        [JsonIgnore]
        public string Role { get; set; }
    }
}