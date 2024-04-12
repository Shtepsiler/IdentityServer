using System.ComponentModel.DataAnnotations;

namespace BlazorAppForClient.ViewModels
{
    public class NewJoobViewModel
    {
  
            [Required]
            public string ModelName { get; set; }
            [Required]

            public int ClientId { get; set; } = 0;
            [Required]

            public DateTime IssueDate { get; set; }
            [Required]
            [MaxLength(200)]
            public string Description { get; set; }
        
    }
}
