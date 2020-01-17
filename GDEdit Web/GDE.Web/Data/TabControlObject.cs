namespace GDE.Web.Data
{
    public class TabControlObject
    {
        public string String { get; set; }

        public bool Selected { get; set; }

        public bool Disabled { get; set; }

        public TabControlObject(string String, bool selected = false, bool disabled = false)
        {
            this.String = String;
            Selected = selected;
            Disabled = disabled;
        }
    }
}