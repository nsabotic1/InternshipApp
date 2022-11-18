using FluentValidation;

namespace InternAppApi.Dtos.ApplicationCommentDtos;
    public class CommentsValidator : AbstractValidator<AddCommentDto>
    {
        public CommentsValidator()
        {
            RuleFor(u => u.Body)
                .NotNull()
                .NotEmpty();

        }
    }
 
    public class AddCommentReqValidator : AbstractValidator<AddCommentReq>
    {
        public AddCommentReqValidator()
        {
            RuleFor(u => u.body)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.applicationId)
                .NotNull()
                .NotEmpty();

        }
    }
 