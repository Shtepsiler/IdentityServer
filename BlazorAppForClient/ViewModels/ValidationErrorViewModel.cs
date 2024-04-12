using System.Collections.Generic;

namespace BlazorAppForClient.ViewModels
{
    public class ValidationErrorViewModel
    {
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
