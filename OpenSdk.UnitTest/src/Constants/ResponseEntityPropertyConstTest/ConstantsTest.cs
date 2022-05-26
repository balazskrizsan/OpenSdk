using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSdk.Constants;

namespace OpenSdk.UnitTest.Constants.ResponseEntityPropertyConstTest;

[TestClass]
public class ConstantsTest
{
    [TestMethod]
    public void CheckNecessaryConstants_returnsConstantValues()
    {
        // Arrange
        var expectedValues = new[] { "success", "errorCode", "requestId", "data" };

        // Act
        var actual = new[]
        {
            ResponseEntityPropertyConsts.SUCCESS,
            ResponseEntityPropertyConsts.ERROR_CODE,
            ResponseEntityPropertyConsts.REQUEST_ID,
            ResponseEntityPropertyConsts.DATA
        };

        // Assert
        actual.Should().Equal(expectedValues);
    }
}