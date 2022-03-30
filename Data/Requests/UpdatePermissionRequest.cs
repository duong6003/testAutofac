using FluentValidation;
using testAutofac.Definitions;
using testAutofac.Repositories;

namespace testAutofac.Data.Requests
{
    public class UpdatePermissionRequest
    {
        public string? Name { get; set; }
    }
    public class UpdatePermissionValidation : AbstractValidator<UpdatePermissionRequest>
    {
        private readonly IRepositoryWrapper RepositoryWrapper;

        public UpdatePermissionValidation(IRepositoryWrapper repositoryWrapper)
        {
            RepositoryWrapper = repositoryWrapper;
            RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Permissions.NameEmpty);
        }
    }
}