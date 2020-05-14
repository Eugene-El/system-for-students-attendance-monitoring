using SAMS.BusinessLogic.Models.Common;

namespace SAMS.BusinessLogic.Factories
{
    public class MainFactory
    {
        protected static string SelectLocalization(Language language, string eng, string lv, string ru)
        {
            return language == Language.English ? eng : (language == Language.Latvian ? lv : ru);
        }

    }
}
