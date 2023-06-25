using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GPL_Application
{
    internal class Method
    {
        protected internal ArrayList methodList = new ArrayList();
        public ArrayList methodCodeBlockLines = new ArrayList();
        public Dictionary<string, string> methodLocalVar = new Dictionary<string, string>();
        protected internal bool called;
        CommandParser cp;
        Form1 f1;
        public Method(Form1 f1, CommandParser cp)
        {
            this.cp = cp;
            this.f1 = f1;
        }

        /// <summary>
        /// Performs action for method to operate
        /// </summary>
        /// <param name="text">contains parameters, statements from within method</param>
        /// <param name="called">contains parameters, statements from within method</param>
        /// <returns></returns>
        public void Operate_Method(string text, bool called)
        {
            if (called)
            {
                this.called = called;

                ArrayList methodParam = new ArrayList();

                string[] methodState = text.Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 1; i < methodState.Length; i++)
                {
                    methodState[i] = methodState[i].Trim().ToUpper();
                    methodParam.Add(methodState[i]);
                }

                if (methodLocalVar.Count == methodParam.Count)
                {
                    for (int i = 0; i < methodParam.Count; i++)
                    {
                        string value = convertToVarInt((string)methodParam[i]).ToString();
                        if (value == "-1") return; //when no variable found
                        string key = methodLocalVar.ElementAt(i).Key;
                        methodLocalVar[key] = value;
                    }

                    if (cp.NumberOfErrors.Count != 0) cp.NumberOfErrors.Remove($"Line No. {f1.Lines}");
                }
                else
                {
                    cp.showError($"Invalid number of parameters for Method {methodState[0]}");
                    return;
                }

                foreach (string line in methodCodeBlockLines)
                {
                    cp.execute(line);
                }
                this.called = false;
                return;
            }

            //Eneter inside if methodStat[0] index is uppercase Method
            string[] methodStat = text.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (methodStat.Length > 1 && Regex.IsMatch(methodStat[0].Trim().ToUpper(), @"^METHOD{1}"))
            {
                if (Regex.IsMatch(methodStat[1], @"[A-Z]\w?\s?", RegexOptions.IgnoreCase))//method name and ignoring different cases
                {

                    if (Regex.IsMatch(methodStat[1], @"[A-Z]\w\({1}\){1}$")) //works for no param meth
                    {
                        methodStat = methodStat[1].Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        methodList.Add(methodStat[0].Trim().ToUpper());
                        cp.isMethod = true; //activating the flag
                        if (cp.NumberOfErrors.Count != 0) cp.NumberOfErrors.Remove($"Line No. {f1.Lines}");
                    }
                    else if (Regex.IsMatch(methodStat[1], @"[A-Z]\w?\s?\({1}[A-Z]+((,.[A-Z]+)+)?\){1}$")) //works for multiple params
                    {
                        methodStat = methodStat[1].Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        methodList.Add(methodStat[0].Trim().ToUpper());
                        if (methodStat.Length > 1) //for handling and operating single and multiple parameters
                            for (int i = 1; i < methodStat.Length; i++)
                            {
                                methodStat[i] = methodStat[i].Trim().ToUpper();
                                try
                                {
                                    methodLocalVar.Add(methodStat[i], "");
                                }
                                catch (ArgumentException)
                                {
                                    methodLocalVar[methodStat[i]] = "";
                                }
                            }

                        cp.isMethod = true;
                        if (cp.NumberOfErrors.Count != 0) cp.NumberOfErrors.Remove($"Line No. {f1.Lines}");
                    }
                    else
                        cp.showError($"Invalid Parameter for * Method {methodStat[1]} *\nYou are missing () or (param name...)");
                }
                else
                    cp.showError($"Invalid Method Name {methodStat[1]}\nMust Start with an alpha character.");
            }
            else if (Regex.IsMatch(text, @"(\s+|^)ENDMETHOD{1}$", RegexOptions.IgnoreCase)) // works for endloop termination
            {
                cp.isMethod = false;
                if (cp.NumberOfErrors.Count != 0) cp.NumberOfErrors.Remove($"Line No. {f1.Lines}");
                return;
            }
            else if (cp.isMethod)
            {
                if (f1.Refresh)
                {
                    methodCodeBlockLines = new ArrayList();
                    f1.Refresh = false;
                }
                methodCodeBlockLines.Add(text);
                return;
            }
            else
                cp.showError($"Invalid Syntax for {methodStat[0]}\nThe Correct Syntax is * Method yourMethodName(<parameter>) *");
        }

        private int convertToVarInt(string numStr)
        {
            int numInt = -1;
            try
            {   //the try block works out for conversion of user given variable to integer
                if (int.TryParse(numStr, out _)) numInt = Convert.ToInt32(numStr);
                else numInt = Convert.ToInt32(cp.VariableAndValue[numStr]);
            }
            catch (KeyNotFoundException) //the catch block is invoked when user given variable not defined before
            {
                cp.showError($"'{numStr}' has not been initialized yet.");
            }
            return numInt;
        }


    }
}
