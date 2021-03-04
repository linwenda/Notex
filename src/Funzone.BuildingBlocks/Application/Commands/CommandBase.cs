using System;

namespace Funzone.BuildingBlocks.Application.Commands
{
    public abstract class CommandBase : ICommand
    {
        public Guid Id { get; }

        protected CommandBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            this.Id = id;
        }
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        protected CommandBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}