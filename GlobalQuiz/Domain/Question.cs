namespace GlobalQuiz.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string Category { get; set; }
   
        public List<string> Options { get; set; } = new();
        public float Difficulty { get; set; }
    }
}
