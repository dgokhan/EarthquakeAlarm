using System;
namespace DepremAlarmi.Controls.Interfaces
{
    public interface IMessage
    {
        void LongMessage(string message);
        void ShortMessage(string message);
    }
}
