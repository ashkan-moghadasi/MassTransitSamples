using System;

namespace GettingStarted.Contracts
{
    public record HelloMessage
    {
        public Guid Id { get; set; }

        public string Name { get; init; }
    }
}