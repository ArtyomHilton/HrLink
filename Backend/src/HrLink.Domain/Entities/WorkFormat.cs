namespace HrLink.Domain.Entities;

public class WorkFormat
{
    public short Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<VacancyWorkFormat> WorkFormats = new List<VacancyWorkFormat>();
}