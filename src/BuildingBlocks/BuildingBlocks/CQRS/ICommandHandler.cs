using MediatR;

namespace BuildingBlocks.CQRS
{

	public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
		where TCommand : ICommand
	{
		void Handle(TCommand command);
	}

	public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
		where TResponse : notnull
	{
		void Handle(TCommand command); 
	}
}
