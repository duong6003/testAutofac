
using FluentValidation;
using testAutofac.Definitions;
using testAutofac.Repositories;

namespace testAutofac.Data.Requests
{
    public class CreatePermissionRequest
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
    public class CreatePermissionValidation : AbstractValidator<CreatePermissionRequest>
    {
        private readonly IRepositoryWrapper RepositoryWrapper;

        public CreatePermissionValidation(IRepositoryWrapper repositoryWrapper)
        {
            RepositoryWrapper = repositoryWrapper;
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(Messages.Permissions.CodeEmpty)
                .MustAsync(async (code, cancellationToken) => !await RepositoryWrapper.Permissions.IsAnyValue(x => x.Code == code))
                .WithMessage(Messages.Permissions.CodeExisted);
            RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Permissions.NameEmpty);
        }
    }
}