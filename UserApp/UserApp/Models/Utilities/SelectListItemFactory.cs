using System.Web.Mvc;

namespace UserApp.Models
{
    public static class SelectListItemFactory
    {

        public static SelectListItem CrateSecretQuestion(string text)
        {
            SelectListItem item = new SelectListItem()
            {
                Value = text,
                Text = text
            };
            return item;
        }
    }

}