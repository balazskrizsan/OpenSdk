using NUnit.Framework;
using OpenSdk.Constants;

namespace OpenSdk.UnitTest.Constants.ResponseEntityPropertyConstTest;

public class AsListWithoutDataTest
{
    [Test]
    public void GetAllValuesWithoutData_Perfect()
    {
        // Arrange
        var expectedValues = new[] { "success", "errorCode", "requestId" };

        // Act
        var actual = ResponseEntityPropertyConst.AsListWithoutData();

        // Assert
        Assert.AreEqual(expectedValues, actual);
    }
}