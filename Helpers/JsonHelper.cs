using System.Text.Json;
namespace e_commerce_website.Helpers
    {
    public sealed class JsonHelper
        {
        public static string AsJsonString(object obj)
            {
            try
                {
                return JsonSerializer.Serialize(obj);
                }
            catch (Exception ex)
                {
                return $"Exception Occurred: {ex}";
                }

            }
        }
    }
