using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace NKnife.UnitTests.Extensions
{
    public partial class StringTest
    {
        [Fact]
        public void MatchFilters_01()
        {
            "Ft.abc".MatchFilters(new[] {"Ft."}).Should().BeTrue();
            "Ft.abc".MatchFilters(new[] {"ft."}).Should().BeFalse();
            "Ft.abc.doc".MatchFilters(new[] {"Ft.*"}).Should().BeTrue();
            "Ft.abc.exe".MatchFilters(new[] {"Ft.*.exe"}).Should().BeTrue();
            "Ft.abc.dll".MatchFilters(new[] {"Ft.*.exe"}).Should().BeFalse();
            "Ft.abc.dll".MatchFilters(new[] {"Ft.*.exe", "Ft.*.dll"}).Should().BeFalse();

            "abc,xyz,mn,123".MatchFilters(new[] {"abc", "123"}).Should().BeTrue();
            "abc,xyz,mn,123".MatchFilters(new[] {"abc.*.123"}).Should().BeTrue();
        }
    }
}
