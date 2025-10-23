using HrLink.Application.Interfaces;
using HrLink.Application.UseCases.InterviewUseCases.GetInterviewByDay;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Quartz;

namespace HrLink.BackgroundService.Jobs;

public class SendInterviewNotification : IJob
{
    public static readonly JobKey JobKey = new JobKey(nameof(SendInterviewNotification), "NotificationGroup");
    public static readonly TriggerKey TriggerKey = new TriggerKey($"{nameof(SendInterviewNotification)}Trigger", "NotificationGroup");

    private readonly IGetInterviewsByDateUseCase _useCase;
    private readonly IEmailService _emailService;

    private readonly string _subject = "Напоминаем тебе о сегодняшнем собеседовании!";

    private readonly string _text =
        "Привет {0}! Напоминаем тебе о сегодняшнем собеседовании {1} в {2}, на позицию {3}." +
        "Удачи на собеседовании!";

    public SendInterviewNotification(IGetInterviewsByDateUseCase useCase, IEmailService emailService)
    {
        _useCase = useCase;
        _emailService = emailService;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        var result = await _useCase.Execute(new GetInterviewsByDateQuery(DateTime.UtcNow), context.CancellationToken);
        
        foreach (var interview in result.Value! )
        {
            await _emailService.SendMessage(interview.Candidate.Email, _subject,
                string.Format(_text, interview.Candidate.FirstName,
                    interview.Interview.InterviewDateTime.ToString("dd MMMM yyyy"),
                    interview.Interview.InterviewDateTime.ToString("HH:mm")), context.CancellationToken);
        }
    }
}