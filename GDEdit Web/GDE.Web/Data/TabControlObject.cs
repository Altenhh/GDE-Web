namespace GDE.Web.Data
{
    public class TabControlObject
    {
        public string String { get; }

        public bool Selected { get; }

        public bool Disabled { get; }

        public TabControlObject(string String, bool selected = false, bool disabled = false)
        {
            this.String = String;
            Selected = selected;
            Disabled = disabled;
        }
    }
}