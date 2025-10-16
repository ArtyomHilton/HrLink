using System.Text.RegularExpressions;
using FluentValidation;
using HrLink.Application.UseCases.UserUseCases.AddUser;

namespace HrLink.Application.Validators;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithErrorCode("FirstNameEmpty")
            .Matches(@"^[А-ЯЁ][А-Яа-яёЁIV\s\-.,'()]*[А-Яа-яёЁIV]$")
            .WithErrorCode("NotCorrectFirstNameFormat");
        
        RuleFor(x=> x.SecondName)
            .NotEmpty()
            .WithErrorCode("SecondNameEmpty")
            .Matches(@"^[А-ЯЁ][А-Яа-яёЁIV\s\-.,'()]*[А-Яа-яёЁIV]$")
            .WithErrorCode("NotCorrectSecondNameFormat");
        
        RuleFor(x => x.Patronymic)
            .Must(BeValidPatronymic)
            .WithErrorCode("NotCorrectPatronymicFormat");
        
        RuleFor(x => x.DateOfBirthday)
            .LessThanOrEqualTo(DateTime.UtcNow.AddYears(-18))
            .WithErrorCode("AgeMustBeOver18Years");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithErrorCode("EmailEmpty")
            .EmailAddress()
            .WithErrorCode("NotCorrectEmailFormat");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithErrorCode("PasswordEmpty")
            .MinimumLength(6)
            .WithErrorCode("NotCorrectPasswordLength")
            .Matches(@"^[A-Za-z!@#$%&?*()]{6,}$")
            .WithErrorCode("NotCorrectPasswordFormat");
    }

    private bool BeValidPatronymic(string? patronymic) =>
        string.IsNullOrEmpty(patronymic) || Regex.IsMatch(patronymic, @"^[А-ЯЁ][А-Яа-яёЁIV\s\-.,'()]*[А-Яа-яёЁIV]$");
}