namespace HrLink.Domain.Entities;

public class Vacancy
{
    public Guid Id { get; set; }
    public string Position { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public short StatusId { get; set; }
    public VacancyStatus Status { get; set; } = null!;
    public ICollection<VacancyWorkFormat> WorkFormats { get; set; } = new List<VacancyWorkFormat>();
    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}