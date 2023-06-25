﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GPL_Application
{
    public class CommandParser
    {
        int size;
        int x, y, z;
        Color Color;
        Form1 f1;
        ShapeFactory factory;
        public Shape shape;
        protected internal Graphics g1;
        Bitmap bitmap;
        public Dictionary<string, string> VariableAndValue = new Dictionary<string, string>();
        public Dictionary<string, string> NumberOfErrors = new Dictionary<string, string>();
        internal protected List<string> multipleIfData = new List<string>();
        internal protected List<Shape> flashShape = new List<Shape>();
        internal protected List<string> whileDatas = new List<string>();
        protected internal string backError = "";
        protected internal int backLine = 0;
        internal protected bool multiLineIf = false;
        private string[] operators = { "<", ">", "==", ">=", "<=", "!=", "THEN" }; // array for valid operator
        bool if_status = false;
        protected internal bool while_status = false;
        protected internal bool isMethod = false;
        Method Mymethod;
        protected internal bool splitterStatus = false;
        protected internal int whileCount = 0;
        protected internal int ifCount = 0;
        protected internal bool lastWhile = false;
        protected internal bool lastIf = false;
        NameRepository nameRepo;


        public CommandParser(Form1 f1)
        {
            this.f1 = f1;
            factory = new ShapeFactory();
            bitmap = new Bitmap(f1.drawingArea.Width, f1.drawingArea.Height);
            g1 = Graphics.FromImage(bitmap);
            Mymethod = new Method(f1, this);
            nameRepo = new NameRepository();
        }

        /// <summary>
        /// THIS METHOD IS HELPS TO CHECK THE BOOLEAN STATUS OF DIFFERENT METHODS LIKE WHILELOOP AND IFVALIDATION AFTER GETTING THE TEXT FROM  TEXT BOX.
        /// </summary>
        /// <param name="Code">THIS CODE PARAMETERS HOLD THE TEXTS WHICH COME FROM THE USERS</param>
        public void runCondStatement(string Code)
        {

            String[] codeLines = f1.focusedRtb.Text.ToUpper().Trim().Split('\n');
            if (Regex.IsMatch(Code.Trim().ToUpper(), @"^\s?METHOD{1}") || isMethod || Regex.IsMatch(Code, @"^ENDMETHOD$"))
            {
                Mymethod.Operate_Method(Code.Trim().ToUpper(), false); //false = not called
                return;
            }
            else
            {
                string[] calledMethod = Code.Split(new char[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                //if (calledMethod.Length > 0)
                //{
                if (Mymethod.methodList.Contains(calledMethod[0].Trim().ToUpper()))
                {
                    if (Regex.IsMatch(Code, @"[A-Z]\w?\s?\({1}[A-Z]+((,.[A-Z]+)+)?\){1}$", RegexOptions.IgnoreCase))
                    {
                        Mymethod.Operate_Method(Code.Trim().ToUpper(), true); //true = called
                        return;
                    }
                    else if (Regex.IsMatch(Code, @"[A-Z]\w?\s?\({1}\){1}$", RegexOptions.IgnoreCase)) //works for no param meth
                    {
                        Mymethod.Operate_Method(Code.Trim().ToUpper(), true);
                        return;
                    }
                }
                //}
                if (WhileLoop(Code.Trim(), codeLines[codeLines.Length-1]) != true)
                {
                    if (ifValidation(Code.Trim() ,codeLines[codeLines.Length - 1]) != true)
                    {
                        bool variableStatus = variableValidation(Code.Trim().ToUpper());
                        if (variableStatus != true)
                        {
                            getProgramText(Code);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// THIS METHOD IS MADE TO READ CONDITIONAL STATEMTS WHICH IS ONLY WRITTEN IN WHILE LOOP AND MUTIPLE IF STATEMENTS.
        /// </summary>
        /// <param name="oneLineStatements">ONELINESTATEMENTS PARAMETERS HOLD THE STATEMENTS WHICH IS FETCHED FROM THE METHODS WRITTEN FOR WHILE LOOP AND IF STATEMENTS</param>
        public void execute(String oneLineStatements)
        {
            bool variableStatus = variableValidation(oneLineStatements.Trim().ToUpper());
            if (variableStatus != true)
            {
                getProgramText(oneLineStatements);
            }
        }
        /// <summary>
        /// This method is made to validate the variables assigned by the users and also to add the variable and its value in dictionary.
        /// </summary>
        /// <param name="Variables">Variable parameters is to store if the variable is passed in this parameters.</param>
        /// <returns></returns>
        public bool variableValidation(string Variables)
        {
            splitterStatus = false;
            char[] splitter = { '=' };

            foreach (char ch in Variables)
            {
                if (ch.Equals('='))
                {
                    string[] getSplittedVariables = Variables.Split(splitter, 2, StringSplitOptions.RemoveEmptyEntries);

                    if (int.TryParse(getSplittedVariables[0], out _))
                    {
                        showError($"Variable not valid ({getSplittedVariables[0]})");
                    }
                    else
                    {
                        if (getSplittedVariables.Length > 1)
                        {
                            if (String.IsNullOrEmpty(getSplittedVariables[1].Trim()) || Regex.IsMatch(getSplittedVariables[1].Trim(), "^\\s*$"))
                            {
                                showError($"{getSplittedVariables[0]} cannot assigned to null value");
                            }
                            else
                            {
                                
                                for (iterator iter = nameRepo.getIterator(); iter.HasNext();)
                                {
                                    string name = (string)iter.next();
                                    if (getSplittedVariables[1].Contains(name))
                                    {
                                        string[] splitOperations = getSplittedVariables[1].Trim().Split(new char[]{ '+', '-', '*', '/' }, 2, StringSplitOptions.RemoveEmptyEntries);
                                        string leftOpe = splitOperations[0];
                                        string rightOpe = splitOperations[1];
                                        string operation = name;

                                        if (rightOpe != "")
                                        {
                                            getSplittedVariables[1] = Convert.ToString(AirthmeticOperation(leftOpe, rightOpe, operation));
                                        }
                                        else
                                        {
                                            showError($"Invalid expression{getSplittedVariables[1]}");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            showError($"{getSplittedVariables[0]} cannot assigned to null value");
                        }
                        if (getSplittedVariables.Length > 1)
                        {
                            try
                            {
                                VariableAndValue.Add(getSplittedVariables[0].Trim(), getSplittedVariables[1].Trim());
                            }
                            catch
                            {
                                //showError($"{getSplittedVariables[0]} Same name for variables has already been assigned.");
                                VariableAndValue[getSplittedVariables[0].Trim()] = getSplittedVariables[1].Trim();
                            }
                        }
                    }
                    splitterStatus = true;
                    break;
                }
            }
            return splitterStatus;
        }

        /// <summary>
        /// THIS FUNCTION IS TO VERIFY WHETHER THE GIVEN PARAMETER IS VARIABLE OR INTEGER VALUE AND IF THE PARAMETER IS     VARIABLE THEN EXTRACT ITS VALUES FROM DICTIONARY.
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public int Conversion(String codes)
        {
            int assignedValue = 0;
            try
            {
                if (int.TryParse(codes.Trim(), out _))

                {
                    assignedValue = int.Parse(codes.Trim());
                }
                else
                {
                    assignedValue = int.Parse(VariableAndValue[codes.Trim()]);
                }
            }
            catch (Exception e)
            {
                if (assignedValue == 0)

                {
                    showError($"\n\t Variable {codes} has not been initilized");
                }
            }
            return assignedValue;
        }
        /// <summary>
        /// This method is to complete the airthmetic operation.
        /// </summary>
        /// <param name="Left_ope"></param>
        /// <param name="right_ope"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public int AirthmeticOperation(String Left_ope, String right_ope, String Operation)

        {
            float Value1, Value2;
            int Result = 0;

            Value1 = Conversion(Left_ope);
            Value2 = Conversion(right_ope);

            switch (Operation)
            {
                case "+":
                    Result = (int)Math.Round(Value1 + Value2);
                    break;
                case "-":
                    Result = (int)Math.Round(Value1 - Value2);
                    break;
                case "/":
                    Result = (int)Math.Round(Value1 / Value2);
                    break;
                case "*":
                    Result = (int)Math.Round(Value1 * Value2);
                    break;
                default:
                    showError("Error \t \nInvalid Operator for airthmetic operation");
                    break;
            }
            return Result;
        }
        /// <summary>
        /// This methhod is for while loop which will return the boolean value true if it is while loop 
        /// </summary>
        /// <param name="programCodes"></param>
        /// /// <param name="Terminator"></param>
        /// <returns></returns>
        public bool WhileLoop(string programCodes, string Terminator)
        {
            if (programCodes.Contains("WHILE"))
            {
                while_status = true;
            }
            if (whileDatas.FirstOrDefault() != null)
            {
                if (whileDatas.Last() == "ENDLOOP")
                {
                    while_status = false;
                }
            }
            if (while_status == true)
            {
                whileDatas.Add(programCodes);
                if (whileDatas.Count == 1)
                {
                    ValidateWhile(whileDatas);
                }
                if (whileDatas.Count > 1 && whileDatas.Last() != "ENDLOOP")
                {
                    execute(whileDatas[whileCount]);
                }
                whileCount++;

                if (whileDatas.FirstOrDefault() != null)
                {
                    if (whileDatas.Last() == "ENDLOOP" || whileDatas.Last() == Terminator)
                    {
                        lastWhile = true;
                        ValidateWhile(whileDatas);
                    }
                }
               
            }
            return while_status;
        }

        public void ValidateWhile(List<String> whileData)
        {
            if (whileCount == 0 || f1.validationCheck
                == false || lastWhile == true)
            {
                string leftOperand;
                string rightOperand;
                string terminate;
                string statement = "";
                string[] spaceDelimeter = { " " };
                string[] Terminated = { "ENDLOOP" };
                string[] splitSpace = whileData[0].Split(spaceDelimeter, 2, StringSplitOptions.RemoveEmptyEntries);
                string operatorAssigned = "";
                string[] splitVarValue = new String[] { };
                if (splitSpace.Length <= 1)
                {
                    showError("Invalid Syntax for While");
                    return;
                }
                else
                {
                    for (int i = 0; i < operators.Length - 1; i++)
                    {
                        if (splitSpace[1].Contains(operators[i]))
                        {
                            if (NumberOfErrors.Count != 0)
                            {
                                string errorLine = Convert.ToString(f1.Lines);
                                NumberOfErrors.Remove(errorLine);
                            }
                            f1.ErrorTextBox.Text = "";

                            splitVarValue = splitSpace[1].Split(operators, 2, StringSplitOptions.RemoveEmptyEntries);
                            operatorAssigned = operators[i];
                            leftOperand = splitVarValue[0].Trim();
                            rightOperand = splitVarValue[1].Trim();
                            
                            List<string> bulkStatements = new List<string>();
                                for (int j = 0; j < whileData.Count; j++)
                                {
                                    if (j != 0 && j != whileDatas.Count - 1)
                                    {
                                        bulkStatements.Add(whileData[j]);
                                    }
                                }
                                string[] whileBulk = bulkStatements.ToArray();
                                if (whileData.Last() != "ENDLOOP" && whileData.First() != "WHILE")
                                {
                                    if (whileCount > 0 || f1.validationCheck == false)
                                    {
                                        showError("Syntax Error. \n Syntax structure: WHILE VARIABLE == 0 \nSTATEMENT... \nENDLOOP");
                                    }
                                }
                                else if (whileData.Last() != "ENDLOOP")
                                {
                                    if (lastWhile == true)
                                    {
                                        showError("Syntax Error. \n While Loop Was Never Terminated");
                                    }
                                }
                                checkWhileOperator(operatorAssigned, leftOperand, rightOperand, whileBulk);
                                break;
                            //}
                        }
                        else
                        {
                            showError("Relational operator required for comparison");
                        }
                    }
                    f1.ErrorTextBox.Clear();
                }
            }
        }
        public void checkWhileOperator(string operators, string leftOperand, string rightOperand, params string[] statements)
        {
            int leftOpe = 0;
            int rightOpe = 0;
            if (whileCount == 0 || f1.validationCheck == false)
            {
                leftOpe = Conversion(leftOperand);
                rightOpe = Conversion(rightOperand);
            }

            string[] onlyWhileStatements = { "" };
            if (f1.validationCheck == false && whileCount > 0) {
                switch (operators)
                {
                    case "==":
                        while (leftOpe == rightOpe)
                        {
                            foreach (String oneStatements in statements)
                            {
                                execute(oneStatements.Trim());
                                leftOpe = Conversion(leftOperand);
                                rightOpe = Conversion(rightOperand);
                            }
                        }
                        break;
                    case ">=":
                        while (leftOpe >= rightOpe)
                        {
                            foreach (String oneStatements in statements)
                            {
                                execute(oneStatements.Trim());
                                leftOpe = Conversion(leftOperand);
                                rightOpe = Conversion(rightOperand);

                            }
                        }
                        break;
                    case "<=":
                        while (leftOpe <= rightOpe)
                        {
                            foreach (String oneStatements in statements)
                            {
                                execute(oneStatements.Trim());
                                leftOpe = Conversion(leftOperand);
                                rightOpe = Conversion(rightOperand);

                            }
                        }
                        break;
                    case ">":
                        while (leftOpe > rightOpe)
                        {
                            foreach (String oneStatements in statements)
                            {
                                execute(oneStatements.Trim());
                                leftOpe = Conversion(leftOperand);
                                rightOpe = Conversion(rightOperand);

                            }
                        }
                        break;
                    case "<":
                        while (leftOpe < rightOpe)
                        {
                            foreach (String oneStatements in statements)
                            {
                                execute(oneStatements.Trim());
                                leftOpe = Conversion(leftOperand);
                                rightOpe = Conversion(rightOperand);
                            }
                        }
                        break;
                    case "!=":
                        while (leftOpe != rightOpe)
                        {
                            foreach (String oneStatements in statements)
                            {
                                execute(oneStatements.Trim());
                                leftOpe = Conversion(leftOperand);
                                rightOpe = Conversion(rightOperand);
                            }
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// This method is for single and multiple if with validation parts.
        /// </summary>
        /// <param name="programCodes"></param>
        /// <param name="Terminator"></param>

        /// <returns></returns>
        public bool ifValidation(string programCodes, string Terminator)
        {
            bool singleLine = false;
            if (programCodes.Contains("IF") || multiLineIf == true)
            {
                if_status = true;
                if (programCodes.Contains("IF") && (programCodes.Contains("THEN")))
                {
                    singleLine = true;
                }
                else
                {
                    multiLineIf = true;
                    if_status = true;
                    if (multipleIfData.Contains("IF") && !multipleIfData.Contains("ENDIF")) showError("Syntax Error. \n Invalid syntax for if must try with proper syntax.");
                    if (multipleIfData.FirstOrDefault() != null)
                    {
                        if (multipleIfData.Last() == "ENDIF")
                        {
                            multiLineIf = false;
                            if_status = false;
                        }
                    }
                    if (multiLineIf == true)
                    {
                        //validate every if syntax every statements one by one.
                        multipleIfData.Add(programCodes);
                        if (multipleIfData.Count == 1)
                        {
                            multiIf(multipleIfData);
                        }
                        if(multipleIfData.Count > 1 && multipleIfData.Last() != "ENDIF")
                        {
                            execute(multipleIfData[ifCount]);
                        }
                        ifCount++;  
                    }
                    if (multipleIfData.FirstOrDefault() != null)
                    {
                        if (multipleIfData.Last() == "ENDIF" || multipleIfData.Last() == Terminator)
                        {
                            lastIf = true;
                            multiIf(multipleIfData);
                        }
                    }
                }
            }
            else
            {
                if_status = false;
            }
            if (singleLine)
            {
                string leftOperand;
                string rightOperand;
                string statement = "";
                string[] spaceDelimeter = { " " };
                string[] splitSpace = programCodes.Split(spaceDelimeter, 2, StringSplitOptions.RemoveEmptyEntries);
                string operatorAssigned = "";
                string[] splitVarValue = new String[] { };

                if (splitSpace.Length <= 1)
                {
                    showError("Invalid Syntax for if");
                    return true;
                }
                //if (!splitSpace[1].Contains("THEN")) showError("Missing Then Keyword");
                else
                {

                    for (int i = 0; i < operators.Length - 1; i++)
                    {
                        if (splitSpace[1].Contains(operators[i]))
                        {
                            splitVarValue = splitSpace[1].Split(operators, 3, StringSplitOptions.RemoveEmptyEntries);
                            operatorAssigned = operators[i];
                            break;
                        }
                    }
                }
                if (splitVarValue.Length < 3)
                {
                    showError("Syntax Error. \n Syntax structure: IF VARIABLE == 0 THEN STATEMENT");
                    return true;
                }
                else
                {
                    leftOperand = splitVarValue[0].Trim();
                    rightOperand = splitVarValue[1].Trim();
                    statement = splitVarValue[2].Trim();
                }

                try
                {
                    if (!VariableAndValue.ContainsKey(splitVarValue[0].Trim())) //Check variables is available in dictionary or not
                    {
                        if (splitVarValue[0] != "THEN")
                        {
                            showError($"{splitVarValue[0]} is not declared.");
                            return true;
                        }
                        else
                        {
                            showError("Syntax Error. \n Syntax structure: IF VARIABLE == 0 THEN STATEMENT");
                        }
                    }
                }
                catch (Exception e)
                {
                    showError("Error. \n Variable has not been initialized.");
                }

                try
                {
                    int.TryParse(leftOperand, out _);
                    string[] removeExtraAssignment = rightOperand.Split(splitSpace, StringSplitOptions.RemoveEmptyEntries);
                    if (removeExtraAssignment.Length != 1)  // validation for invalid assignment in right side operands.
                    {
                        showError($"{rightOperand} Error \n \t Invalid operand");
                    }
                }
                catch (Exception e)
                {
                    if (leftOperand == "")
                    {
                        showError($"'{leftOperand}' was not found.");
                    }
                    else
                    {
                        showError($"'{rightOperand}' was not found");
                    }
                    return true;
                }
                checkOperator(operatorAssigned, leftOperand, rightOperand, statement);
                if_status = false;
            }
            return if_status;
        }

        public void multiIf(List<String> multiIfData)
        {
            if (ifCount == 0 || f1.validationCheck
               == false || lastIf == true)
            {
                string leftOperand;
                string rightOperand;
                string terminate;
                string statement = "";
                string[] spaceDelimeter = { " " };
                string[] Terminated = { "ENDIF" };
                string[] splitSpace = multiIfData[0].Split(spaceDelimeter, 2, StringSplitOptions.RemoveEmptyEntries);
                string operatorAssigned = "";
                string[] splitVarValue = new String[] { };
                if (splitSpace.Length <= 1)
                {
                    showError("Missing Statement for IF");
                }
                else
                {
                    for (int i = 0; i < operators.Length - 1; i++)
                    {
                        if (splitSpace[1].Contains(operators[i]))
                        {
                            if (NumberOfErrors.Count != 0)
                            {
                                string errorLine = Convert.ToString(f1.Lines);
                                NumberOfErrors.Remove(errorLine);
                            }
                            f1.ErrorTextBox.Text = "";
                            splitVarValue = splitSpace[1].Split(operators, 2, StringSplitOptions.RemoveEmptyEntries);
                            operatorAssigned = operators[i];
                            leftOperand = splitVarValue[0].Trim();
                            rightOperand = splitVarValue[1].Trim();

                            List<string> bulkStatements = new List<string>();
                            for (int j = 0; j < multiIfData.Count; j++)
                            {
                                if (j != 0 && j != multipleIfData.Count - 1)
                                {
                                    bulkStatements.Add(multiIfData[j]);
                                }
                            }
                            string[] multiIfBulk = bulkStatements.ToArray();
                            try
                            {
                                if (!VariableAndValue.ContainsKey(splitVarValue[0].Trim())) //Check variables is available in dictionary or not
                                {
                                    showError($"{splitVarValue[0]} is not declared.");
                                }
                                if (multiIfData.Last() != "ENDIF" && multiIfData.First() != "IF")
                                {
                                    //To check syntactical error
                                    if (ifCount > 0 || f1.validationCheck == false)
                                    {
                                        showError("Syntax Error. \n Syntax structure: IF VARIABLE == 0 \nSTATEMENT... \nENDIF");
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                showError("Error. \n Variable has not been initialized.");
                            }
                            try
                            {
                                int.TryParse(leftOperand, out _);
                                string[] removeExtraAssignment = rightOperand.Split(splitSpace, StringSplitOptions.RemoveEmptyEntries);
                                if (removeExtraAssignment.Length != 1)  // validation for invalid assignment in right side operands.
                                {
                                    showError($"{rightOperand} Error \n \t Invalid operand");
                                }
                            }
                            catch (Exception e)
                            {
                                if (leftOperand == "")
                                {
                                    showError($"'{leftOperand}' was not found.");
                                }
                                else
                                {
                                    showError($"'{rightOperand}' was not found");
                                }
                            }
                            checkOperator(operatorAssigned, leftOperand, rightOperand, multiIfBulk);
                            break;
                        }
                        else
                        {
                            showError("Relational operator required for comparison");
                        }
                    }
                    f1.ErrorTextBox.Clear();
                }
            }
        }
        public void checkOperator(string operators, string leftOperand, string rightOperand, params string[] statements)
        {
            int leftOpe = 0;
            int rightOpe = 0;
            if (ifCount == 0 || f1.validationCheck == false)
            {
                leftOpe = Conversion(leftOperand);
                rightOpe = Conversion(rightOperand);
            }
            switch (operators)
            {
                case "==":
                    if (leftOpe == rightOpe)
                    {
                        for (int i = 0; i < statements.Length; i++)
                        {
                            execute(statements[i].Trim());
                        }
                    }

                    break;
                case "!=":
                    if (leftOpe != rightOpe)
                    {
                        for (int i = 0; i < statements.Length; i++)
                        {
                            execute(statements[i]);
                        }
                    }
                    break;
                case "<=":
                    if (leftOpe <= rightOpe)
                    {
                        for (int i = 0; i < statements.Length; i++)
                        {
                            execute(statements[i]);
                        }
                    }

                    break;
                case ">=":
                    if (leftOpe >= rightOpe)
                    {
                        for (int i = 0; i < statements.Length; i++)
                        {
                            execute(statements[i]);
                        }
                    }
                    break;
                case "<":
                    if (leftOpe < rightOpe)
                    {
                        for (int i = 0; i < statements.Length; i++)
                        {
                            execute(statements[i]);
                        }
                    }

                    break;
                case ">":
                    if (leftOpe > rightOpe)
                    {
                        for (int i = 0; i < statements.Length; i++)
                        {
                            execute(statements[i]);
                        }
                    }
                    break;
                case "THEN":
                    break;
            }
        }
        public void getProgramText(string programCodes)
        {
            try
            {
                string[] splitProgramCodes = programCodes.Split(' ');
                if (!f1.commandList.Contains(splitProgramCodes[0]))
                {
                    if (!isMethod)
                    {
                        showError("Error Occured \n\t Invalid Command");
                    }
                }
                else {
                    switch (splitProgramCodes[0])
                    {

                        case "CIRCLE":
                            if (splitProgramCodes.Length == 1)
                            {
                                showError("Error Occured \n\t Parameters Missing.");
                            }
                            else
                            {
                                commandValidation(splitProgramCodes[0], splitProgramCodes[1]);
                            }
                            break;
                        case "RECTANGLE":
                            if (splitProgramCodes.Length == 1)
                            {
                                showError("Error Occured \n\t Parameters Missing.");
                            }
                            else
                            {
                                commandValidation(splitProgramCodes[0], splitProgramCodes[1]);
                            }
                            break;
                        case "SQUARE":
                            if (splitProgramCodes.Length == 1)
                            {
                                showError("Error Occured \n\t Parameters Missing.");
                            }
                            else
                            {
                                commandValidation(splitProgramCodes[0], splitProgramCodes[1]);
                            }
                            break;
                        case "TRIANGLE":
                            if (splitProgramCodes.Length == 1)
                            {
                                showError("Error Occured \n\t Parameters Missing.");
                            }
                            else
                            {
                                commandValidation(splitProgramCodes[0], splitProgramCodes[1]);
                            }
                            break;
                        case "MOVETO":
                            if (splitProgramCodes.Length == 1)
                            {
                                showError("Error Occured \n\t Parameters Missing.");
                            }
                            else
                            {
                                commandValidation(splitProgramCodes[0], splitProgramCodes[1]);
                            }
                            break;
                        case "DRAWTO":
                            if (splitProgramCodes.Length == 1)
                            {
                                showError("Error Occured \n\t Parameters Missing.");
                            }
                            else
                            {
                                commandValidation(splitProgramCodes[0], splitProgramCodes[1]);
                            }
                            break;
                        case "FILL":
                            if (splitProgramCodes.Length == 1)
                            {
                                showError("Error Occured \n\t Parameters Missing.");
                            }
                            else
                            {
                                commandValidation(splitProgramCodes[0], splitProgramCodes[1]);
                            }
                            break;
                        case "PEN":
                            if (splitProgramCodes.Length == 1)
                            {
                                showError("Error Occured \n\t Parameters Missing.");
                            }
                            else
                            {
                                commandValidation(splitProgramCodes[0], splitProgramCodes[1]);
                            }
                            break;
                        case "RUN":
                            try
                            {
                                f1.clearAll();
                                f1.lineIncrement = 0;
                                f1.escapeNewLine();
                            }

                            catch (Exception e)
                            {
                                MessageBox.Show("No commands in textbox to run", "Empty!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }

                            break;
                        case "RESET":
                            f1.comParser1.commandValidation("MOVETO", "0,0");
                            f1.ErrorTextBox.Text = "";
                            MessageBox.Show("Coordinates are moved to 0,0", "RESET", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                            break;
                        case "CLEAR":
                            f1.clearAll();
                            break;
                        case "BLACKRED":
                            f1.ThreadToBeUse = new Thread(() =>
                            {
                                ColorToBeFlash(true, Color.Black, Color.Red);
                            });
                            f1.ThreadToBeUse.Start();
                            break;
                        case "GRAYPINK":
                            f1.ThreadToBeUse = new Thread(() =>
                            {
                                ColorToBeFlash(true, Color.Gray, Color.Pink);
                            });
                            f1.ThreadToBeUse.Start();
                            break;
                        case "PINKBLACK":
                            f1.ThreadToBeUse = new Thread(() =>
                            {
                                ColorToBeFlash(true, Color.Pink, Color.Black);
                            });
                            f1.ThreadToBeUse.Start();
                            break;
                        case "BROWNRED":
                            f1.ThreadToBeUse = new Thread(() =>
                            {
                                ColorToBeFlash(true, Color.Brown, Color.Red);
                            });
                            f1.ThreadToBeUse.Start();
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                showError("Error Occured: " + ex.Message);
            }
        }
        int flag = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flashStatus"></param>
        /// <param name="firstColor"></param>
        /// <param name="secondColor"></param>
        public void ColorToBeFlash(bool flashStatus, Color firstColor, Color secondColor)
        {
            while (flashStatus)
            {
                try
                {
                    try
                    {
                        foreach (Shape s in flashShape)
                        {
                            try
                            {
                                if (flag % 2 == 0)
                                {
                                    shape.draw(g1, f1.Fills, firstColor);

                                    f1.Invoke(new MethodInvoker(delegate ()
                                    {
                                        f1.drawingArea.Image = bitmap;
                                    }));
                                }
                                else
                                {
                                    s.draw(g1, f1.Fills, secondColor);
                                    f1.Invoke(new MethodInvoker(delegate ()
                                    {
                                        f1.drawingArea.Image = bitmap;
                                    }));
                                }
                            }
                            catch (ObjectDisposedException)
                            {
                                f1.ThreadToBeUse.Abort();
                            }
                            catch (InvalidOperationException)
                            {
                                f1.ThreadToBeUse.Abort();
                            }
                        }
                        flag++;
                        Thread.Sleep(1000);
                    }
                    catch (InvalidOperationException)
                    {
                        f1.ThreadToBeUse.Abort();
                    }
                }
                catch (ThreadInterruptedException)
                {
                    //code to handle thread interruption
                    break;
                }

            }



        }
        int JustCount = 0;
        public void commandValidation(string codeCommands, string parameters)
        {
            string[] stringParameters = parameters.Split(',');
            int[] intParameters = new int[stringParameters.Length];
            try
            {
                //To check if parameters are direct variable or value.
                if (codeCommands != "PEN" && codeCommands != "FILL")
                {
                    int outValue;
                    for (int i = 0; i < stringParameters.Length; i++)
                    {
                        //intParameters[i] = int.Parse(stringParameters[i]);
                        if (int.TryParse(stringParameters[i].Trim(), out outValue)) intParameters[i] = outValue;
                        //else intParameters[i] = int.Parse(VariableAndValue[stringParameters[i].Trim()]);
                        else
                        {
                            if (isMethod || Mymethod.called)
                            {
                                try
                                {
                                    intParameters[i] = int.Parse(Mymethod.methodLocalVar[stringParameters          [i]]); //for parameterized method

                                }
                                catch (KeyNotFoundException)
                                {
                                    intParameters[i] = int.Parse(VariableAndValue[stringParameters                 [i]]); //for non parameterized method
                                }
                            }
                            else
                            {
                                intParameters[i] = int.Parse(VariableAndValue[stringParameters[i]]);
                            }
                        }
                    }
                }

                //}
                if (codeCommands != "MOVETO")
                {
                    g1.DrawEllipse(new Pen(Color.White, 2), x, y, 2, 2);
                    f1.drawingArea.Image = bitmap;
                }

                    switch (codeCommands)
                    {
                        case "CIRCLE":
                            
                            if (stringParameters.Length == 1)
                            {
                            if (f1.validationCheck == false)
                            {
                                shape = factory.getShape("circle");
                                shape.set(x, y, intParameters[0]);
                                shape.draw(g1, f1.Fills, f1.color);
                                //Console.WriteLine(JustCount++);
                                f1.drawingArea.Image = bitmap;
                                flashShape.Add(shape);
                            }
                            }
                            else
                            {
                                showError("Error Occured \n\t Numbers of parameters didn't match");
                            }
                            break;
                        case "SQUARE":
                            if (stringParameters.Length == 1)
                            {
                            if (f1.validationCheck == false)
                            {
                                shape = factory.getShape("square");
                                shape.set(x, y, intParameters[0]);
                                shape.draw(g1, f1.Fills, f1.color);
                                f1.drawingArea.Image = bitmap;
                                flashShape.Add(shape);
                            }
                            }
                            else
                            {
                                showError("Error Occured \n\t Numbers of parameters didn't match");
                            }
                            break;
                        case "TRIANGLE":
                            if (stringParameters.Length == 3)
                            {
                            if (f1.validationCheck == false)
                            {
                                shape = factory.getShape("triangle");
                                shape.set(x, y, intParameters[0], intParameters[1], intParameters[2]);
                                shape.draw(g1, f1.Fills, f1.color);
                                f1.drawingArea.Image = bitmap;
                                flashShape.Add(shape);
                            }
                            }
                            else
                            {
                                showError("Error Occured \n\t Numbers of parameters didn't match");
                            }
                            break;
                        case "RECTANGLE":
                            if (stringParameters.Length == 2)
                            {
                            if (f1.validationCheck == false)
                            {
                                shape = factory.getShape("rectangle");
                                shape.set(x, y, intParameters[0], intParameters[1]);
                                shape.draw(g1, f1.Fills, f1.color);
                                f1.drawingArea.Image = bitmap;
                                flashShape.Add(shape);
                            }
                            }
                            else
                            {
                                showError("Error Occured \n\t Numbers of parameters didn't match");
                            }
                            break;
                        case "MOVETO":

                            if (stringParameters.Length == 2)
                            {
                            if (f1.validationCheck == false)
                            {
                                x = intParameters[0];
                                y = intParameters[1];
                                shape = factory.getShape("circle");
                                shape.set(x, y, 1);
                                shape.draw(g1, f1.Fills, f1.color);
                                f1.drawingArea.Image = bitmap;
                                flashShape.Add(shape);
                            }
                            }
                            else
                            {
                                showError("Error Occured \n\t Numbers of parameters didn't match. \n\t Moveto command required 2 parameters.");
                            }
                            break;
                        case "DRAWTO":
                            if (stringParameters.Length == 2)
                            {
                            if (f1.validationCheck == false)
                            {
                                shape = factory.getShape("drawto");
                                shape.set(x, y, intParameters[0], intParameters[1]);
                                shape.draw(g1, f1.Fills, f1.color);
                                f1.drawingArea.Image = bitmap;
                                flashShape.Add(shape);
                            }
                            }
                            else
                            {
                                showError("Error Occured \n\t Numbers of parameters didn't match. \n\t Drawto command required 2 parameters.");
                            }
                            break;
                        case "FILL":
                            if (stringParameters.Length == 1)
                            {
                                f1.RunCommand(codeCommands, stringParameters[0]);
                            }
                            else
                            {
                                showError("Error Occured \n\t Numbers of parameters didn't match. \n\t Fill command required 1 parameters.");
                            }
                            break;

                        case "PEN":
                            if (stringParameters.Length == 1)
                            {
                                f1.RunCommand(codeCommands, stringParameters[0]);
                            }
                            else
                            {
                                showError("Error Occured \n\t Numbers of parameters didn't match. \n\t PEN command required 1 parameters.");
                            }
                            break;

                }
            }
            catch (Exception e)
            {
                //showError($"Invalid Parameter For {codeCommands}");
            }
        }
        public void showError(string code)
        {
            string lineNum = Convert.ToString(f1.Lines);
            string lineNo =  $"Error in line no.{f1.Lines}";
            //f1.EkrrorTextBox.Text += $"{lineNo} {code}\n\n";
            string errorBoxs = $"{lineNo} {code}\n\n";
            try
            {
                NumberOfErrors.Add(lineNum, errorBoxs);
            }
            catch
            {
                NumberOfErrors[lineNum] = errorBoxs;
            }
        }
    }
}
