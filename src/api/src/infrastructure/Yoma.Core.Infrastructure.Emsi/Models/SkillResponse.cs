namespace Yoma.Core.Infrastructure.Emsi.Models
{
    public class SkillResponse
    {
        public Attribution[] attributions { get; set; }
        public Skill[] data { get; set; }
    }

    public class Attribution
    {
        public string name { get; set; }
        public string text { get; set; }
    }

    public class Skill
    {
        public string id { get; set; }
        public string name { get; set; }
        public SkillType type { get; set; }
        public string infoUrl { get; set; }
    }

    public class SkillType
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}
