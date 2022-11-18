using FluentValidation;

namespace InternAppApi.Dtos.SelectionDtos;
    public class SelectionValidator : AbstractValidator<AddSelectionDto>
    {
        public SelectionValidator()
        {
            RuleFor(u => u.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.StartDate)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.EndDate)
                .NotNull()
                .NotEmpty()
                .GreaterThan(u=>u.StartDate);
            RuleFor(u => u.Description)
                .NotNull()
                .NotEmpty();
        }
    }
 
    public class UpdateSelectionValidator : AbstractValidator<UpdateSelectionDto>
    {
        public UpdateSelectionValidator()
        {
            RuleFor(u => u.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.StartDate)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.EndDate)
                .NotNull()
                .NotEmpty()
                .GreaterThan(u=>u.StartDate);
            RuleFor(u => u.Description)
                .NotNull()
                .NotEmpty();
        }
    }
    public class AddApplicantValidator : AbstractValidator<AddApplicantDto>
    {
        public AddApplicantValidator()
        {
            RuleFor(u => u.ApplicationId)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.SelectionId)
                .NotNull()
                .NotEmpty();
        }
    }
 