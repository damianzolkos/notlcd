namespace notlcd.Models
{
    public class Reminder
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string FirstLineText { get; set; }
        public string? SecondLineText { get; set; }

        public bool Sent { get; set; }

        public Reminder(int hour, int minute, string firstLineText, string? secondLineText = null)
        {
            Hour = hour;
            Minute = minute;
            FirstLineText = firstLineText;
            SecondLineText = secondLineText;
        }
    }
}