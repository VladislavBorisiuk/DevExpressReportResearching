using System;
using System.Collections.Generic;
using System.Text;

namespace DevExpressReportResearching.Services.Interfaces
{
    internal interface IUserDialog
    {
        void Choose(object? item);
        void ShowInformation(string Message, string Caption);
        void ShowWarning(string Message, string Caption);
        void ShowError(string Message, string Caption);
        bool Confirm(string Message, string Caption, bool Exclamation = false);
    }
}
