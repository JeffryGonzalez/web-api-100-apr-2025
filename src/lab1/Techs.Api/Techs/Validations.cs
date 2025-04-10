using System.Text.RegularExpressions;

namespace SoftwareCenter.Api.Shared;

public partial class ValidationGenerators
{
    [GeneratedRegex(@".+\@.+\..+")]
    public static partial Regex ValidEmailRegularExpression();
}