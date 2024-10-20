using notlcd;
using notlcd.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ILcdClient, LcdWebClient>();

builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IReminderService, ReminderService>();

builder.Services.AddHostedService<TimedHostedNotificationsService>();
builder.Services.AddHostedService<TimedHostedRemindersService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/notification/add", async (string firstLine, string? secondLine, bool blink, CancellationToken cancellationToken) =>
{
    DB.Notifications.Add(new Notification(firstLine, secondLine, blink));
})
.WithOpenApi();

app.MapGet("/reminder/add", async (int hour, int minute, string firstLine, string? secondLine, CancellationToken cancellationToken) =>
{
    DB.Reminders.Add(new Reminder(hour, minute, firstLine, secondLine));
})
.WithOpenApi();

app.Run();