using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<string> operatorStack = new Stack<string>();
            Queue<string> outputQueue = new Queue<string>();
            Stack<int> resultStack = new Stack<int>();
            
            //Operator and presedence
            var operators = new Dictionary<string, int>()
            {
                {"+", 2},
                {"-", 2},
                {"/", 3},
                {"*", 3}

            };
            string inputData;
            string originalInput;

            Console.WriteLine("Input some math");
            inputData = Console.ReadLine();
            originalInput = inputData;


            //https://en.wikipedia.org/wiki/Shunting-yard_algorithm
            while (inputData.Length > 0)
            {

                if (int.TryParse(inputData.Substring(0, 1), out int number))
                {
                    outputQueue.Enqueue(inputData.Substring(0, 1));
                }
                else if (operators.TryGetValue(inputData.Substring(0, 1), out int presedence))
                {
                    
                  
                    while (TryPeek(operatorStack,operators) >= presedence)
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(inputData.Substring(0, 1));
                }


                inputData = inputData.Remove(0, 1);
            }
            while(operatorStack.Count != 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }
            while (outputQueue.Count != 0)
            {
                string nextToken = outputQueue.Dequeue();
                if (int.TryParse(nextToken, out int number))
                {
                    resultStack.Push(number);
                }
                else
                {
                    int a, b = 0;
                    switch (nextToken)

                    {
                        case "+":
                            b = resultStack.Pop();
                            a = resultStack.Pop();
                            resultStack.Push(a + b);
                            break;
                        case "-":
                            b = resultStack.Pop();
                            a = resultStack.Pop();
                            resultStack.Push(a - b);
                            break;

                        case "/":
                            b = resultStack.Pop();
                            a = resultStack.Pop();
                            resultStack.Push(a / b);
                            break;

                        case "*":
                            b = resultStack.Pop();
                            a = resultStack.Pop();
                            resultStack.Push(a * b);
                            break;

                        default:
                            throw new ArgumentException("op");
                    }

                }
            }
            int result = resultStack.Pop();
            Console.WriteLine("=" + result);
            Console.ReadKey();
            

        }

        static int TryPeek(Stack<string> stack, Dictionary<string, int> ops)
        {
            try
            {
                return ops[stack.Peek()];
            }
            catch (InvalidOperationException)
            {

                return 0;
            }
        }
    }
}
