using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace SmartNote.UnitTests;

public class Test2
{
    public Test2()
    {
        TestForeach();
    }
    
    [Test]
    public Task Test()
    {
        return TestForeach();
    }

    private Task TestForeach()
    {
        var result = true;

        var list = new List<string>() {"1", "2"};

        list.ForEach(async x =>
        {
            await Task.Delay(1000);
            result = false;
        });

        result.ShouldBe(false);

        return Task.CompletedTask;
    }
}