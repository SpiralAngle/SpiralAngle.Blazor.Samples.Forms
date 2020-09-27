using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorFormSample.Shared
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
