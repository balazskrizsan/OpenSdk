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
            ResponseEntityPropertyConst.SUCCESS,
            ResponseEntityPropertyConst.ERROR_CODE,
            ResponseEntityPropertyConst.REQUEST_ID,
            ResponseEntityPropertyConst.DATA
        };

        // Assert
        actual.Should().Equal(expectedValues);
    }
}