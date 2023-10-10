using SchoolManagmentSystem.Contract.Enums;

namespace SchoolManagmentSystem.Contract.IServices;

public interface ICurrentLanguageService
{
    public LanguageEnum CurrentLanguage { get; }
}
