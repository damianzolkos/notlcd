namespace notlcd.Models
{
    public class Notification
    {
        public string FirstLineText { get; set; }
        public string? SecondLineText { get; set;}

        public bool Blink {get; set;}

        public Notification(string firstLineText, bool blink = false)
        {
            FirstLineText = firstLineText;
            Blink = blink;
        }

        public Notification(string firstLineText, string? secondLineText, bool blink = false)
        {
            FirstLineText = firstLineText;
            SecondLineText = secondLineText;
            Blink = blink;
        }
    }
}