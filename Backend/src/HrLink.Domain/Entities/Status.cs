namespace HrLink.Domain.Entities;

public class Status
{
    public byte Id { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public List<Interview> Interviews = new List<Interview>();
}