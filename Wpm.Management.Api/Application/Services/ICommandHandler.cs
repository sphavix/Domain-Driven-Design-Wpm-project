namespace Wpm.Management.Api.Application.Services
{
    public interface ICommandHandler<T>
    {
        Task Handle(T command);
    }
}
