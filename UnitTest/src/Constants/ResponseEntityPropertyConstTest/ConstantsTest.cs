using NUnit.Framework;
using OpenSdk.Constants;

namespace OpenSdk.UnitTest.Constants.ResponseEntityPropertyConstTest;

public class ConstantsTest
{
    [Test]
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
        Assert.AreEqual(expectedValues, actual);
    }
}