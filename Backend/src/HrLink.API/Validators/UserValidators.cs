using System.Net.Mail;
using System.Text.RegularExpressions;
using HrLink.API.DTOs.Users;
using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;

namespace HrLink.API.Validators;

public static class UserValidators
{
    private static readonly string ErrorValidateNotEmptyMessage = "{0} cannot be empty";
    private static readonly string ErrorNotAcceptableCharacters = "{0} contains not acceptable symbols";
    private static readonly string ErrorUserAgeMust = "User age must be at least 18 years old";
    private static readonly string ErrorInvalidFormat = "{0} invalid format";

    private static readonly Regex PasswordRegex = new Regex(@"[^А-Яа-яЁё\s]{6,}");

    private static readonly char[] AcceptableSymbols = new []{' ', '-', '.', '\'', ',', '(', ')'};

    public static Result Validate(this AddUserRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName))
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateNotEmptyMessage, nameof(dto.FirstName)), nameof(dto.FirstName)));
        }

        var firstNameSymbolsDigits = dto.FirstName.Where(x => !char.IsLetter(x))
            .ToArray();

        if (firstNameSymbolsDigits.Any(x => !AcceptableSymbols.Contains(x)))
        {
            return Result.Failure(new ValidateError(string.Format(ErrorNotAcceptableCharacters, nameof(dto.FirstName)), nameof(dto.FirstName)));
        }

        if (string.IsNullOrWhiteSpace(dto.SecondName))
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateNotEmptyMessage, nameof(dto.SecondName)), nameof(dto.SecondName)));
        }
        
        var secondNameSymbolsDigits = dto.SecondName.Where(x=> !char.IsLetter(x))
            .ToArray();

        if (secondNameSymbolsDigits.Any(x => !AcceptableSymbols.Contains(x)))
        {
            return Result.Failure(new ValidateError(string.Format(ErrorNotAcceptableCharacters, nameof(dto.SecondName)), nameof(dto.SecondName)));
        }

        if (!string.IsNullOrWhiteSpace(dto.Patronymic))
        {
            var patronymicSymbolsDigits = dto.Patronymic.Where(x=> !char.IsLetter(x))
                .ToArray();
            
            if (patronymicSymbolsDigits.Any(x => !AcceptableSymbols.Contains(x)))
            {
                return Result.Failure(new ValidateError(string.Format(ErrorNotAcceptableCharacters, nameof(dto.Patronymic)), nameof(dto.Patronymic)));
            }
        }

        if (dto.DateOfBirthday is null)
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateNotEmptyMessage, nameof(dto.DateOfBirthday)), nameof(dto.DateOfBirthday)));
        }
        
        var minUserBirthdayDate = DateTime.UtcNow.AddYears(-18);
        
        if (dto.DateOfBirthday.Value >= minUserBirthdayDate)
        {
            return Result.Failure(new ValidateError(string.Format(ErrorUserAgeMust, nameof(dto.DateOfBirthday)), nameof(dto.DateOfBirthday)));
        }

        if (string.IsNullOrWhiteSpace(dto.Email))
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateNotEmptyMessage, nameof(dto.Email)), nameof(dto.Email)));
        }

        if (!MailAddress.TryCreate(dto.Email, out _))
        {
            return Result.Failure(new ValidateError(string.Format(ErrorInvalidFormat, nameof(dto.Email)), nameof(dto.Email)));
        }

        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateNotEmptyMessage, nameof(dto.Password)), nameof(dto.Password)));
        }

        if (!PasswordRegex.IsMatch(dto.Password))
        {
            return Result.Failure(new ValidateError(string.Format(ErrorInvalidFormat, nameof(dto.Password)), nameof(dto.Password)));
        }
        
        return Result.Success();
    }
}