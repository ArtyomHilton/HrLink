namespace HrLink.Application.Interfaces;

public interface ILocalizationService
{
     Task<string?> GetLocalizationMessage(string messageCode, string culture, CancellationToken cancellationToken);
}