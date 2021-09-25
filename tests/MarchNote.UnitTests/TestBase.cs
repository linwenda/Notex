using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using MarchNote.Domain;
using MarchNote.Domain.SeedWork;

namespace MarchNote.UnitTests
{
    public abstract class TestBase
    {
        protected static void ShouldThrowBusinessException(Action action,
            ExceptionCode expectExceptionCode,
            string expectExceptionMessage)
        {
            var ex = Should.Throw<BusinessException>(action);
            ex.Code.ShouldBe(expectExceptionCode);
            ex.Message.ShouldBe(expectExceptionMessage);
        }

        protected static async Task ShouldThrowBusinessExceptionAsync(Func<Task> func,
            ExceptionCode expectExceptionCode,
            string expectExceptionMessage)
        {
            var ex = await Should.ThrowAsync<BusinessException>(func);
            ex.Code.ShouldBe(expectExceptionCode);
            ex.Message.ShouldBe(expectExceptionMessage);
        }
    }
}