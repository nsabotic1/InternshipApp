using FluentValidation;
using InternAppApi.Dtos.SelectionComment;

namespace InternAppApi.Dtos.ApplicationCommentDtos;
    public class SelectionCommentValidator : AbstractValidator<AddSelectionCommentDto>
    {
        public SelectionCommentValidator()
        {
            RuleFor(u => u.Body)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.SelectionId)
                .NotNull()
                .NotEmpty();

        }
    }
 