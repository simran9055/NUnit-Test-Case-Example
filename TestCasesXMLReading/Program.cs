

using System;
using System.Collections.Generic;
using System.Xml;
using NUnit.Framework;

namespace TestCasesXMLReading
{
    class Program
    {
        public static void Main()
        {
            
        }
    }

    [TestFixture]
    class NumberTest
    {
        [Test, TestCaseSource("GetNumbersToCheck")]
        public void EvenOddNumberTest(int value, bool result)
        {
            var expectedResult = (value % 2 == 0);
            var message = (expectedResult ? "Even Number" : "Odd Number");

            Assert.That(result, Is.EqualTo(expectedResult), message);

        }
        
        /*
         * This method is used to read from the XML test case
         * file and return an IEnumerable of each test case
         */
        private IEnumerable<object[]> GetNumbersToCheck()
        {
            var assembly = GetType().Module.Assembly;
            using (var stream = assembly.GetManifestResourceStream("TestCasesXMLReading.TestXMLExample.xml"))
            {
                if (stream == null)
                    throw new Exception("Could not obtain BusinessLayerTests.InternationalDialingTest.xml");

                using (var reader = new XmlTextReader(stream))
                {
                    int? value = null;
                    bool? result = null;

                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            if (reader.Name == "Value")
                            {
                                value = reader.ReadElementContentAsInt();
                            }

                            if (reader.Name == "Result")
                            {
                                result = reader.ReadElementContentAsBoolean();
                            }

                            if (value != null && result != null)
                            {
                                yield return
                                    new object[]
                                    {
                                        value, result
                                    };
                                value = null;
                                result = null;
                            }
                        }
                    }
                }
            }
        }
    }
}
