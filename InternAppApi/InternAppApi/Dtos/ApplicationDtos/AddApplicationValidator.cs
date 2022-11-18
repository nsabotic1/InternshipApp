using FluentValidation;

namespace InternAppApi.Dtos.ApplicationDtos;
    public class ApplicationsValidator : AbstractValidator<AddApplicationDto>
    {
        public ApplicationsValidator()
        {
            RuleFor(u => u.FirstName)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.LastName)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.CoverLetter)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.CV)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.EducationLevel)
                .NotNull()
                .NotEmpty();
        }
    }
    public class UpdateApplicationsValidator : AbstractValidator<UpdateApplicationDto>
    {
        public UpdateApplicationsValidator()
        {
            RuleFor(u => u.FirstName)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.LastName)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.CoverLetter)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.CV)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.EducationLevel)
                .NotNull()
                .NotEmpty();
        }
    }
 