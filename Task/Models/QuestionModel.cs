namespace Task.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public required string Area { get; set; }
        public required string Section { get; set; }
        public required string Subsection { get; set; }
        public required string Question { get; set; }
        public int QuestionNumber { get; set; }
    }
}
