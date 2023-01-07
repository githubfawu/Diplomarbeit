using Prism.Mvvm;

namespace VZEintrittsApp.Domain
{
    public class DirectReport : BindableBase
    {
        private string? directReportSamAccountName;
        public string? DirectReportSamAccountName
        {
            get => directReportSamAccountName;
            set
            {
                if (value != directReportSamAccountName) SetProperty(ref directReportSamAccountName, value);
            }
        }

        private string? directReportDisplayName;
        public string? DirectReportDisplayName
        {
            get => directReportDisplayName;
            set
            {
                if (value != directReportDisplayName) SetProperty(ref directReportDisplayName, value);
            }
        }

        private string? directReportPosition;
        public string? DirectReportPosition
        {
            get => directReportPosition;
            set
            {
                if (value != directReportPosition) SetProperty(ref directReportPosition, value);
            }
        }

        private string? directReportStartDate;
        public string? DirectReportStartDate
        {
            get => directReportStartDate;
            set
            {
                if (value != directReportStartDate) SetProperty(ref directReportStartDate, value);
            }
        }
    }
}
