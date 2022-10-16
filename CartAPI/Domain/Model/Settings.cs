namespace CartAPI.Domain.Model
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public int JwtTimeSpan { get; set; } = 0;
    }
}