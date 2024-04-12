namespace BlazorAppForClient.ViewModels
{
    public class CompleteJobViewModel
    {

        public int id { get; set; }
        public string model { get; set; }
        public string status { get; set; }
        public string mechanicFullName { get; set; }
        public string managerName { get; set; }
        public string managerPhone { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime? finishDate { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }

        public bool ShowDetails = false;


    }
}
