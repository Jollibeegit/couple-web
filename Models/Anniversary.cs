namespace Blazor_Serial_Test.Models;

public class Anniversary
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Today;
    public bool IsRecurring { get; set; }
    public string? RecurrenceType { get; set; }
    
    public int DaysUntil()
    {
        var nextDate = Date;
        if (IsRecurring)
        {
            var thisYearDate = new DateTime(DateTime.Today.Year, Date.Month, Date.Day);
            nextDate = thisYearDate < DateTime.Today 
                ? thisYearDate.AddYears(1) 
                : thisYearDate;
        }
        
        return (nextDate - DateTime.Today).Days;
    }

    // 감성적인 안내 문구 제공
    public string DaysUntilMessage()
    {
        var days = DaysUntil();
        if (days == 0) return "오늘 함께하는 특별한 날 💖";
        if (days == 1) return "내일 다가올 설렘 가득한 날 ✨";
        if (days > 1) return $"D-{days} 설렘을 함께 세어봐요";
        return "지난 추억, 더 소중히 간직해요 🕯";
    }

    public string DisplayDateLabel()
    {
        var label = Date.ToString("yyyy년 MM월 dd일");
        return IsRecurring ? $"{label} · 매년 함께해요" : label;
    }
}
