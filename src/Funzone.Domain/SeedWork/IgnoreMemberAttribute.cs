using System;

namespace Funzone.Domain.SeedWork
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}