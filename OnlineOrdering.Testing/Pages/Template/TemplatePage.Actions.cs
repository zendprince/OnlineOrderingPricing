

namespace OnlineOrdering.Testing.Template.Pages
{
    public partial class TemplatePage
    {

        public string DoSomething_1()
        {
            return Template_lbl.Text;
        }

        public TemplatePage DoSomething_2(string pwd)
        {
            Template_txt.SetText(pwd);
            return this;
        }

        public TemplatePage DoSomething_3()
        {
            Template_btn.Click();
            return this;
        }
    }
}
