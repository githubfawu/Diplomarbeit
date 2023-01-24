using System.ComponentModel.DataAnnotations;
using Prism.Mvvm;

namespace VZEintrittsApp.Model.Domain
{
    public class ManagementLevel : BindableBase
    {
        [Key]
        private int mgmtLevelId;
        public int MgmtLevelId
        {
            get => mgmtLevelId;
            set
            {
                if (value != mgmtLevelId) SetProperty(ref mgmtLevelId, value);
            }
        }

        private string mgmtLevel;
        public string MgmtLevel
        {
            get => mgmtLevel;
            set
            {
                if (value != mgmtLevel) SetProperty(ref mgmtLevel, value);
            }
        }

        private string mgmtLevelGroupName;
        public string MgmtLevelGroupName
        {
            get => mgmtLevelGroupName;
            set
            {
                if (value != mgmtLevelGroupName) SetProperty(ref mgmtLevelGroupName, value);
            }
        }
    }
}
