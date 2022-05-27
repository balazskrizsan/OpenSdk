using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSdk.Constants;

namespace OpenSdk.UnitTest.Constants.ResponseEntityPropertyConstTest;

[TestClass]
public class AsListWithoutDataTest
{
    [TestMethod]
    public void GetAllValuesWithoutData_Perfect()
    {
        // Arrange
        var expectedValues = new[] { "success", "errorCode", "requestId" };

        // Act
        var actual = ResponseEntityPropertyConsts.AsListWithoutData();

        // Assert
        actual.Should().Equal(expectedValues);
    }
}