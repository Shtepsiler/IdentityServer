namespace BlazorAppForClient.ViewModels
{
    public class JobViewModel
    {

        public int id { get; set; }
        public int managerId { get; set; }
        public int modelId { get; set; }
        public string status { get; set; }
        public int clientId { get; set; }
        public int? mechanicId { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime? finishDate { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
    }
}
