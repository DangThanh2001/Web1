namespace DemoAutoMigration.Common
{
    public static class Validation
    {
        public static bool checkStringIsEmpty(params string[] inputs)
        {
            try
            {
                foreach (var input in inputs)
                {
                    var o = input.Trim();
                    if (string.IsNullOrEmpty(o) && string.IsNullOrWhiteSpace(o))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { return true; }
        }
    }
}
