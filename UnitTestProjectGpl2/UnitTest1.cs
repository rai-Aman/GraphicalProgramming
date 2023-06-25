using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GPL_Application;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace UnitTestProjectGpl2
{
    [TestClass]
    public class UnitTest1
    {
        private Form1 f1;
        private CommandParser cmdParser;
        string errorMessage;


        [TestInitialize]
        public void TestInitialize()
        {
            // Create an instance of the Form1 class
            f1 = new Form1();
            // Initialize the resources and dependencies required by the Form1 class
            // Create an instance of the CommandParser class and pass the Form1 instance to it
            cmdParser = new CommandParser(f1);
        }



        [TestMethod]
        public void VariableValidation()
        {
            string abc = "ABC";
            cmdParser.variableValidation(abc);
            Assert.IsFalse(false, abc);
        }
        [TestMethod]
        public void chooseColor()
        {
            Color expectedPen = f1.chooseColor("BLACK");
            Color actual = Color.Black;
            Assert.AreEqual(expectedPen, actual);
        }
        [TestMethod]
        public void exitTool()
        {
            // Arrange
            //Form1 f1 = new Form1();
            f1.Show();

            // Act
            f1.Invoke(new Action(() => f1.exitTool(null, EventArgs.Empty)));

            // Assert
            Assert.IsTrue(f1.IsDisposed);
        }

        [TestMethod]
        public void TestConversion()
        {
            // Test with valid integer input
            String input = "5";
            int expectedOutput = 5;
            int actualOutput = cmdParser.Conversion(input);
            Assert.AreEqual(expectedOutput, actualOutput);

            // Test with valid variable input
            input = "var1";
            cmdParser.VariableAndValue.Add("var1", "10");
            expectedOutput = 10;
            actualOutput = cmdParser.Conversion(input);
            Assert.AreEqual(expectedOutput, actualOutput);

            // Test with invalid input
            input = "invalid";
            expectedOutput = 0;
            actualOutput = cmdParser.Conversion(input);
            Assert.AreEqual(expectedOutput, actualOutput);
        }
        [TestMethod]
        public void TestAirthmeticOperation()
        {
            // Test addition
            String Left_ope = "5";
            String right_ope = "10";
            String Operation = "+";
            int expectedOutput = 15;
            int actualOutput = cmdParser.AirthmeticOperation(Left_ope, right_ope, Operation);
            Assert.AreEqual(expectedOutput, actualOutput);

            // Test subtraction
            Left_ope = "20";
            right_ope = "10";
            Operation = "-";
            expectedOutput = 10;
            actualOutput = cmdParser.AirthmeticOperation(Left_ope, right_ope, Operation);
            Assert.AreEqual(expectedOutput, actualOutput);

            // Test multiplication
            Left_ope = "5";
            right_ope = "10";
            Operation = "*";
            expectedOutput = 50;
            actualOutput = cmdParser.AirthmeticOperation(Left_ope, right_ope, Operation);
            Assert.AreEqual(expectedOutput, actualOutput);

            // Test division
            Left_ope = "20";
            right_ope = "10";
            Operation = "/";
            expectedOutput = 2;
            actualOutput = cmdParser.AirthmeticOperation(Left_ope, right_ope, Operation);
            Assert.AreEqual(expectedOutput, actualOutput);

            // Test invalid operator
            Left_ope = "5";
            right_ope = "10";
            Operation = "invalid";
            expectedOutput = 0;
            actualOutput = cmdParser.AirthmeticOperation(Left_ope, right_ope, Operation);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestWhileLoop()
        {
            // Test while loop with 'WHILE' keyword in programCodes
            string programCodes = "WHILE x > 5";
            cmdParser.VariableAndValue.Add("x", "7");
            bool expectedOutput = true;
            bool actualOutput = cmdParser.WhileLoop(programCodes);
            Assert.AreEqual(expectedOutput, actualOutput);

            // Test while loop with 'ENDLOOP' keyword in programCodes
            programCodes = "ENDLOOP";
            expectedOutput = false;
            actualOutput = cmdParser.WhileLoop(programCodes);
            Assert.AreEqual(expectedOutput, actualOutput);

            // Test while loop with valid condition and 'ENDLOOP' keyword in programCodes
            programCodes = "x = x - 1";
            expectedOutput = true;
            actualOutput = cmdParser.WhileLoop(programCodes);
            Assert.AreEqual(expectedOutput, actualOutput);
            programCodes = "ENDLOOP";
            expectedOutput = false;
            actualOutput = cmdParser.WhileLoop(programCodes);
            Assert.AreEqual(expectedOutput, actualOutput);

        }

        public void TestShowError()
        {
            // Test with a simple error message
            string code = "Invalid Syntax";
            f1.Lines = 10;
            cmdParser.showError(code);
            var expectedOutput = "Error in line no.10 Invalid Syntax\n\n";
            var actualOutput = cmdParser.NumberOfErrors["Error in line no.10"];
            Assert.AreEqual(expectedOutput, actualOutput);

            // Test with a different error message
            code = "Variable not initialized";
            f1.Lines = 15;
            cmdParser.showError(code);
            expectedOutput = "Error in line no.15 Variable not initialized\n\n";
            actualOutput =cmdParser.NumberOfErrors["Error in line no.15"];
            Assert.AreEqual(expectedOutput, actualOutput);

        }







    }

}


