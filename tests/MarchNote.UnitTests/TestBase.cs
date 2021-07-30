using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using MarchNote.Domain;
using MarchNote.Domain.SeedWork;

namespace MarchNote.UnitTests
{
    public abstract class TestBase
    {
        public static void ShouldThrowBusinessException(Action action, ExceptionCode expectExceptionCode)
        {
            var ex = Should.Throw<BusinessException>(action);
            ex.Code.ShouldBe(expectExceptionCode);
        }
    }
}