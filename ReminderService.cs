using notlcd.Models;

namespace notlcd
{
    public class ReminderService : IReminderService
    {
        public void Add(Reminder reminder)
        {
            DB.Reminders.Add(reminder);
        }
    }
}