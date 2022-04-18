using System.ComponentModel;

namespace FormsCreator.Domain.Core.Forms
{
    /// <summary>
    /// Status formularza
    /// </summary>
    public enum FormStatus
    {
        [Description("Kopia robocza")]
        WorkCopy,
        [Description("Potwierdzony")]
        Confirmed,
        [Description("Wysłany")]
        Sent,
        [Description("Anulowany")]
        Canceled
    }
}