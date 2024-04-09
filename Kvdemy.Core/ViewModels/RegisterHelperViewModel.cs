using Kvdemy.Core.ViewModels;


namespace Kvdemy.Core.ViewModels
{
    public class RegisterHelperViewModel
    {
        public List<NationalityViewModel> nationalities { get; set; }
        public List<LanguageViewModel> languages { get; set; }
        public List<LanguageLevelViewModel> levels { get; set; }
    }
}
