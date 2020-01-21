namespace GDE.Web.Entities
{
    public class TabControlObject
    {
        public string String { get; }

        public bool Selected { get; }

        public bool Disabled { get; }

        public string Url { get; }

        public TabControlObject(string String, string url, bool selected = false, bool disabled = false)
        {
            this.String = String;
            Url = url;
            Selected = selected;
            Disabled = disabled;
        }
    }
}