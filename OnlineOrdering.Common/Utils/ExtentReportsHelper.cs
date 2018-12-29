using AventStack.ExtentReports;
using OnlineOrdering.Common.BaseClass;
using System;
using System.Runtime.CompilerServices;

namespace OnlineOrdering.Common.Utils
{
    public class ExtentReportsHelper
    {
        [ThreadStatic]
        private static ExtentTest _parentTest;

        [ThreadStatic]
        private static ExtentTest _childTest;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string testName, string description = null)
        {
            _parentTest = BaseValues.ExtentReports.CreateTest(testName, description);
            return _parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string testName, string description = null)
        {
            _childTest = _parentTest.CreateNode(testName, description);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTestStep(string testName, string description = null)
        {
            _childTest = _childTest.CreateNode(testName, description);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest AddScreenCapture(string path, string title = null)
        {
            if (BaseValues.SaveImageAsBase64)
                // log with snapshot
                _childTest = _childTest.Info(title, MediaEntityBuilder.CreateScreenCaptureFromBase64String(path).Build());
            else
                _childTest = _childTest.Info(title, MediaEntityBuilder.CreateScreenCaptureFromPath(path).Build());

            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogFail(string pathImg = null, string details = "Details: ")
        {
            if (pathImg != null)
            {
                if (BaseValues.SaveImageAsBase64)
                    // log with snapshot
                    _childTest = _childTest.Fail(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
                else
                    _childTest = _childTest.Fail(details, MediaEntityBuilder.CreateScreenCaptureFromPath(pathImg).Build());
            }
            else
                _childTest = _childTest.Fail(details);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogInformation(string pathImg = null, string details = "Details: ")
        {
            if (pathImg is null)
                _childTest = _childTest.Info(details);
            else
            {
                if (BaseValues.SaveImageAsBase64)
                    // log with snapshot
                    _childTest = _childTest.Pass(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
                else
                    _childTest = _childTest.Pass(details, MediaEntityBuilder.CreateScreenCaptureFromPath(pathImg).Build());
            }
            System.Threading.Thread.Sleep(300);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogPass(string pathImg = null, string details = "Details: ")
        {
            if (pathImg is null)
                _childTest = _childTest.Pass(details);
            else
            {
                if (BaseValues.SaveImageAsBase64)
                    // log with snapshot
                    _childTest = _childTest.Pass(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
                else
                    _childTest = _childTest.Pass(details, MediaEntityBuilder.CreateScreenCaptureFromPath(pathImg).Build());
            }
            System.Threading.Thread.Sleep(300);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _childTest;
        }
    }
}
