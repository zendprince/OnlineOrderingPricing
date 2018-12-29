using System;

namespace OnlineOrdering.Common.Utils
{
    /// <summary>
    /// Using AssertHelper to verify and do not want to END the test after facing the error!
    /// </summary>
    public static class AssertHelper
    {

        public static void AreEqual<T>(T expected, T actual, ref string log)
        {
            var result = Compare(expected, actual);
            if (!result)
                log += @"The Actual and Expected value does not match. "
                        + "\nActual Result  : " + actual
                        + "\nExpected Result: " + expected + ".\n";
        }

        public static void AreEqual<T>(T expected, T actual, string message, ref string log)
        {
            var result = Compare(expected, actual);
            if (!result)
                log += message;
        }

        public static bool AreNotEqual<T>(T expected, T actual)
        {
            var result = Compare(expected, actual);
            if (result) return false;
            return true;
        }

        public static bool AreNotEqual<T>(T expected, T actual, string message)
        {
            var result = Compare(expected, actual);
            if (result)
                return false;
            message += message;
            return true;
        }

        public static void IsFalse(bool condition, ref string log)
        {
            if (condition) log += "The Actual Result is not return a \"False\" value" + ".\n";
        }

        public static void IsFalse(bool condition, string message, ref string log)
        {
            if (condition) log += message;
        }

        public static void IsTrue(bool condition, ref string log)
        {
            if (!condition) log += "The Actual Result is not return a \"True\" value" + ".\n";
        }

        public static void IsTrue(bool condition, string message, ref string log)
        {
            if (!condition) log += message;
        }

        public static void IsNull(object value)
        {
            throw new NotImplementedException();
        }

        public static void IsNull(object value, string message)
        {
            throw new NotImplementedException();
        }

        public static void IsNotNull(object value)
        {
            throw new NotImplementedException();
        }

        public static void IsNotNull(object value, string message)
        {
            throw new NotImplementedException();
        }

        public static void Fail(string message)
        {
        }

        public static void Fail(string message, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public static void IsInstanceOfType(object value, Type expectedType)
        {
            throw new NotImplementedException();
        }

        public static void IsInstanceOfType(object value, Type expectedType, string message)
        {
            throw new NotImplementedException();
        }

        public static void AreDateTimesEqual(DateTime? expectedDate, DateTime? actualDate, int deltaSeconds)
        {
        }

        public static void AreDateTimesEqual(DateTime? expectedDate, DateTime? actualDate, int deltaSeconds, string message)
        {
        }

        private static bool Compare<T>(T obj, T another)
        {
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            if (obj.GetType() == typeof(string) || !obj.GetType().IsClass)
            {
                return obj.Equals(another);
            }
            else // do not support if obj is a class
            {
                throw new NotSupportedException("AssertHelper - Class type is not supported.");
            }
        }

    }
}
