﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ch16_P2_LateBindingWithDynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddWithReflection();
            AddWithDynamic();
            Console.ReadLine();
        }

        private static void AddWithDynamic()
        {
            Assembly asm = Assembly.Load("MathLibrary");
            try
            {
                // Get metadata for the SimpleMath type.
                Type math = asm.GetType("MathLibrary.SimpleMath");
                // Create a SimpleMath on the fly.
                dynamic obj = Activator.CreateInstance(math);
                // Note how easily we can now call Add().
                Console.WriteLine("Result is: {0}", obj.Add(10, 70));
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddWithReflection()
        {
            Assembly asm = Assembly.Load("MathLibrary");
            try
            {
                // Get metadata for the SimpleMath type.
                Type math = asm.GetType("MathLibrary.SimpleMath");
                // Create a SimpleMath on the fly.
                object obj = Activator.CreateInstance(math);
                // Get info for Add.
                MethodInfo mi = math.GetMethod("Add");
                // Invoke method (with parameters).
                object[] args = { 10, 70 };
                Console.WriteLine("Result is: {0}", mi.Invoke(obj, args));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
