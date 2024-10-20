using notlcd.Models;

namespace notlcd
{
    public static class DB
    {
        public static List<Notification> Notifications { get; set; } = [];
        public static List<Reminder> Reminders{ get; set; } = [];
    }    
}