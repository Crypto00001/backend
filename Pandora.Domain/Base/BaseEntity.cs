using System;
using System.ComponentModel.DataAnnotations;

namespace Pandora.Domain.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
