using System;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
using NSubstitute;

namespace MarchNote.UnitTests.Spaces
{
    public static class SpaceTestUtil
    {
        public static Space CreateSpace()
        {
            var spaceChecker = Substitute.For<ISpaceChecker>();

            spaceChecker.CalculateSpaceCountAsync(Arg.Any<Guid>())
                .Returns(0);

            spaceChecker.IsUniqueNameAsync(Arg.Any<Guid>(), Arg.Any<string>())
                .Returns(true);

            return Space.Create(spaceChecker,
                    Guid.NewGuid(),
                    "space",
                    new Background(),
                    Visibility.Public,"")
                .GetAwaiter().GetResult();
        }
    }
}