using SchoolManagmentSystem.Contract.Enums;
using SchoolManagmentSystem.Contract.IServices;


namespace SchoolManagmentSystem.API.Services;

public class CurrentLanguageService : ICurrentLanguageService
{
    public CurrentLanguageService(IHttpContextAccessor httpContextAccessor)
    {
        var language = httpContextAccessor?.HttpContext?.Request?.Headers["Accept-Language"].ToString();
        if (!string.IsNullOrWhiteSpace(language) && language == "ar-EG")
            CurrentLanguage = LanguageEnum.Arabic;
        else
            CurrentLanguage = LanguageEnum.English;

    }
    public LanguageEnum CurrentLanguage { get; }
}
