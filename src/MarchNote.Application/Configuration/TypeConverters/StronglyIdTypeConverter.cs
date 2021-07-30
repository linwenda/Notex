using System;
using AutoMapper;
using MarchNote.Domain;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Application.Configuration.TypeConverters
{
    public class StronglyIdTypeConverter : ITypeConverter<TypedIdValueBase, Guid>
    {
        public Guid Convert(TypedIdValueBase source, Guid destination, ResolutionContext context)
        {
            return source == null ? Guid.Empty : source.Value;
        }
    }
}