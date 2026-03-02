using Blazor_Serial_Test.Models;

namespace Blazor_Serial_Test.Services;

public class CoupleDataService
{
    private List<Schedule> _schedules = new();
    private List<Memo> _memos = new();
    private List<Anniversary> _anniversaries = new();
    
    public event Action? OnChange;
    
    // Schedule methods
    public List<Schedule> GetSchedules() => _schedules.OrderBy(s => s.Date).ToList();
    
    public void AddSchedule(Schedule schedule)
    {
        _schedules.Add(schedule);
        OnChange?.Invoke();
    }
    
    public void UpdateSchedule(Schedule schedule)
    {
        var index = _schedules.FindIndex(s => s.Id == schedule.Id);
        if (index != -1)
        {
            _schedules[index] = schedule;
            OnChange?.Invoke();
        }
    }
    
    public void DeleteSchedule(Guid id)
    {
        _schedules.RemoveAll(s => s.Id == id);
        OnChange?.Invoke();
    }
    
    // Memo methods
    public List<Memo> GetMemos() => _memos.OrderByDescending(m => m.CreatedAt).ToList();
    
    public void AddMemo(Memo memo)
    {
        _memos.Add(memo);
        OnChange?.Invoke();
    }
    
    public void UpdateMemo(Memo memo)
    {
        var index = _memos.FindIndex(m => m.Id == memo.Id);
        if (index != -1)
        {
            memo.UpdatedAt = DateTime.Now;
            _memos[index] = memo;
            OnChange?.Invoke();
        }
    }
    
    public void DeleteMemo(Guid id)
    {
        _memos.RemoveAll(m => m.Id == id);
        OnChange?.Invoke();
    }
    
    // Anniversary methods
    public List<Anniversary> GetAnniversaries() => _anniversaries.OrderBy(a => a.DaysUntil()).ToList();
    
    public void AddAnniversary(Anniversary anniversary)
    {
        _anniversaries.Add(anniversary);
        OnChange?.Invoke();
    }
    
    public void UpdateAnniversary(Anniversary anniversary)
    {
        var index = _anniversaries.FindIndex(a => a.Id == anniversary.Id);
        if (index != -1)
        {
            _anniversaries[index] = anniversary;
            OnChange?.Invoke();
        }
    }
    
    public void DeleteAnniversary(Guid id)
    {
        _anniversaries.RemoveAll(a => a.Id == id);
        OnChange?.Invoke();
    }
}
