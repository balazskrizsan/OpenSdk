using System;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using FluentAssertions;

namespace OpenSdk.IntegrationTest.Extensions;

public static class AssertionExtensions
{
    public static FileAssertion Should(this TestFile testFile) => new FileAssertion { TestFile = testFile };

    public class FileAssertion
    {
        public TestFile TestFile;

        public void NotExist(string because = "", params object[] reasonArgs)
            => System.IO.File.Exists(TestFile.Path).Should().BeFalse(because, reasonArgs);

        public void Exist(string because = "", params object[] reasonArgs)
            => System.IO.File.Exists(TestFile.Path).Should().BeTrue(because, reasonArgs);

        public void SameAs(string filePath, string because = "", params object[] reasonArgs)
        {
            var diff = InlineDiffBuilder.Diff(
                string.Join(Environment.NewLine, System.IO.File.ReadAllLines(TestFile.Path)),
                string.Join(Environment.NewLine, System.IO.File.ReadAllLines(filePath))
            );

            if (diff.Lines.Count > 0)
            {
                Console.WriteLine("-- File diff error:");
                Console.WriteLine("File A: " + TestFile.Path);
                Console.WriteLine("File B: " + filePath);

                var savedColor = Console.ForegroundColor;
                foreach (var line in diff.Lines)
                {
                    switch (line.Type)
                    {
                        case ChangeType.Inserted:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("+ ");
                            break;
                        case ChangeType.Deleted:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("- ");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray; // compromise for dark or light background
                            Console.Write("  ");
                            break;
                    }

                    Console.WriteLine(line.Text);
                }

                Console.ForegroundColor = savedColor;
            }

            diff.Lines.Count.Should().Be(0);
        }
    }

    public static TestFile File(string fName) => new TestFile { Path = fName };

    public class TestFile
    {
        public string Path;
    }
}