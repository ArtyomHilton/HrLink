namespace HrLink.Domain.Entities;

public class Status
{
    public byte Id { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}