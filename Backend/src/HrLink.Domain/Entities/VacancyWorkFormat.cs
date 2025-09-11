namespace HrLink.Domain.Entities;

public class VacancyWorkFormat
{
    public Guid VacancyId { get; set; }
    public Vacancy Vacancy { get; set; } = null!;
    public short WorkFormatId { get; set; }
    public WorkFormat WorkFormat { get; set; } = null!;
}