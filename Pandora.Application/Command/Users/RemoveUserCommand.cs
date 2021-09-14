using System;
using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class RemoveUserCommand
    {
        [Required]
        public Guid Id { get; set; }

    }
}
