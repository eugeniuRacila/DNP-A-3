using System.Text.Json;

namespace ResourcesAPI.Models
{
    public class Adult : Person
    {
        public enum JobTitles
        {
            Teacher,
            Engineer,
            Programmer,
            Captain,
            Driver,
            Superman,
            Pirate,
            Ninja,
            Fireman
        }

        public JobTitles JobTitle { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public void Update(Adult toUpdate)
        {
            base.Update(toUpdate);
            JobTitle = toUpdate.JobTitle;
        }
    }
}
