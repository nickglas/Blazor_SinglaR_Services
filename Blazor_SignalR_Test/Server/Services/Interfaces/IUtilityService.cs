using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Services.Interfaces
{
    public interface IUtilityService
    {
        void PrintText(string text);
        void PrintText(string text, ConsoleColor color);

        void PrintText(string text, bool ExtraWhiteLine);
        void PrintText(string text, bool ExtraWhiteLine, ConsoleColor color);
        bool AskYesNoQuestions(string Question);


    }
}
