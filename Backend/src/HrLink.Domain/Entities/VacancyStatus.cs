namespace HrLink.Domain.Entities;

public class VacancyStatus
{
    public short Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
}