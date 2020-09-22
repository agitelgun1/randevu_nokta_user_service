using System;

namespace UserService.Models.Entities
{
    public class ErrorLog
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStacktrace { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int StatusCode { get; set; }
        public string Device { get; set; }
        public string DeviceVersion { get; set; }
        public string Ip { get; set; }
        public string OperatingSystem { get; set; }
        public string Path { get; set; }
    }
}