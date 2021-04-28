using System;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneRules.Rules;
using Funzone.Domain.Zones;
using NUnit.Framework;
using Shouldly;

namespace Funzone.UnitTests.Zones
{
    public class ZoneRuleTests : ZoneTestBase
    {
        private Zone _zone;

        [SetUp]
        public void SetUp()
        {
            _zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;
        }

        [Test]
        public void AddRule_WithMember_BreakZoneRuleCannotAddedByMemberRule()
        {
            var member = _zone.Join(new UserId(Guid.NewGuid()));

            ShouldBrokenRule<ZoneRuleCannotAddedByMemberRule>(() => _zone.AddRule(member, "title", "", 1));
        }

        [Test]
        public void AddRule_WithAdministrator_Successful()
        {
            var administrator = _zone.AddAdministrator();

            var ruleInfo = new
            {
                Title = "No job postings",
                Description ="Description",
                Sort = 1,
            };

            var rule = _zone.AddRule(administrator, ruleInfo.Title, ruleInfo.Description, ruleInfo.Sort);
            rule.Title.ShouldBe(ruleInfo.Title);
            rule.Description.ShouldBe(ruleInfo.Description);
            rule.Sort.ShouldBe(ruleInfo.Sort);
        }

      

        [Test]
        public void DeleteRule_WithAdministrator_Successful()
        {
            var administrator = _zone.AddAdministrator();

            var ruleInfo = new
            {
                Title = "No job postings",
                Description = "Description",
                Sort = 1,
            };

            var rule = _zone.AddRule(administrator, ruleInfo.Title, ruleInfo.Description, ruleInfo.Sort);
            rule.Delete(administrator);
            rule.IsDeleted.ShouldBeTrue();
        }
    }
}