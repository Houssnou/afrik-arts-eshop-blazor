using System.Collections.Generic;

namespace API.Helpers
{
    public class BaseResponse<T> where T : class
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Messages { get; set; } = new List<string>();
        public T Data { get; set; }
    }
}
